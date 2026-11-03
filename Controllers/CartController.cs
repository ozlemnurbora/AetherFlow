using AetherFlow.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace AetherFlow.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        // Session cart (guest)       
        private string SessionCartKey()
        {
            // guest key
            return "CART_GUEST";
        }

        private List<CartItem> GetSessionCart()
        {
            var key = SessionCartKey();
            var cart = Session[key] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session[key] = cart;
            }
            return cart;
        }

        
        // DB cart (logged-in)
        private DbCart GetOrCreateDbCart(string userId)
        {
            var cart = db.DbCarts
                .Include(c => c.Items)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new DbCart { UserId = userId, UpdatedAt = DateTime.Now, Items = new List<DbCartItem>() };
                db.DbCarts.Add(cart);
                db.SaveChanges();

                cart = db.DbCarts
                    .Include(c => c.Items)
                    .FirstOrDefault(c => c.UserId == userId);
            }

            if (cart.Items == null) cart.Items = new List<DbCartItem>();
            return cart;
        }

        private List<CartItem> GetViewCart()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var dbCart = GetOrCreateDbCart(userId);

                return dbCart.Items.Select(i => new CartItem
                {
                    DestinationId = i.DestinationId,
                    TourName = i.TourName,
                    CityName = i.CityName,
                    ImageUrl = i.ImageUrl,
                    Price = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList();
            }

            return GetSessionCart();
        }


        // Pages
        public ActionResult Index()
        {
            var viewCart = GetViewCart();
            ViewBag.Total = viewCart.Sum(x => x.Price * x.Quantity);
            return View(viewCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int id, int qty = 1)
        {
            var tour = db.Destinations.Find(id);
            if (tour == null) return HttpNotFound();

            if (qty < 1) qty = 1;
            if (qty > tour.Capacity) qty = tour.Capacity;

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var dbCart = GetOrCreateDbCart(userId);

                var item = dbCart.Items.FirstOrDefault(x => x.DestinationId == id);
                if (item == null)
                {
                    item = new DbCartItem
                    {
                        DestinationId = tour.Id,
                        TourName = tour.TourName,
                        CityName = tour.CityName,
                        ImageUrl = tour.ImageUrl,
                        UnitPrice = (decimal)tour.Price,
                        Quantity = qty
                    };
                    dbCart.Items.Add(item);
                }
                else
                {
                    item.Quantity = Math.Min(tour.Capacity, item.Quantity + qty);
                }

                dbCart.UpdatedAt = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                var cart = GetSessionCart();
                var item = cart.FirstOrDefault(x => x.DestinationId == id);

                if (item == null)
                {
                    cart.Add(new CartItem
                    {
                        DestinationId = tour.Id,
                        TourName = tour.TourName,
                        CityName = tour.CityName,
                        ImageUrl = tour.ImageUrl,
                        Price = (decimal)tour.Price,
                        Quantity = qty
                    });
                }
                else
                {
                    item.Quantity = Math.Min(tour.Capacity, item.Quantity + qty);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var dbCart = GetOrCreateDbCart(userId);

                var item = dbCart.Items.FirstOrDefault(x => x.DestinationId == id);
                if (item != null)
                {
                    dbCart.Items.Remove(item);
                    dbCart.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
            else
            {
                var cart = GetSessionCart();
                var item = cart.FirstOrDefault(x => x.DestinationId == id);
                if (item != null) cart.Remove(item);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Increase(int id)
        {
            var tour = db.Destinations.Find(id);

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var dbCart = GetOrCreateDbCart(userId);

                var item = dbCart.Items.FirstOrDefault(x => x.DestinationId == id);
                if (item != null && tour != null && item.Quantity < tour.Capacity)
                {
                    item.Quantity++;
                    dbCart.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
            else
            {
                var cart = GetSessionCart();
                var item = cart.FirstOrDefault(x => x.DestinationId == id);
                if (item != null && tour != null && item.Quantity < tour.Capacity)
                {
                    item.Quantity++;
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Decrease(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var dbCart = GetOrCreateDbCart(userId);

                var item = dbCart.Items.FirstOrDefault(x => x.DestinationId == id);
                if (item != null)
                {
                    item.Quantity--;
                    if (item.Quantity <= 0)
                        dbCart.Items.Remove(item);

                    dbCart.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
            else
            {
                var cart = GetSessionCart();
                var item = cart.FirstOrDefault(x => x.DestinationId == id);
                if (item != null)
                {
                    item.Quantity--;
                    if (item.Quantity <= 0) cart.Remove(item);
                }
            }

            return RedirectToAction("Index");
        }


        // Checkout
        [Authorize]
        [HttpGet]
        public ActionResult Checkout()
        {
            // login DB cart checkout
            var userId = User.Identity.GetUserId();
            var dbCart = GetOrCreateDbCart(userId);

            var cart = dbCart.Items.Select(i => new CartItem
            {
                DestinationId = i.DestinationId,
                TourName = i.TourName,
                CityName = i.CityName,
                ImageUrl = i.ImageUrl,
                Price = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList();

            if (!cart.Any()) return RedirectToAction("Index");

            ViewBag.Total = cart.Sum(x => x.Price * x.Quantity);
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(
    string fullName,
    string email,
    string cardNumber,
    string expMonth,
    string expYear,
    string cvv,
    bool? secure3d,
    decimal? grandTotal)
        {
            var userId = User.Identity.GetUserId();
            var dbCart = GetOrCreateDbCart(userId);

            var cart = dbCart.Items.Select(i => new CartItem
            {
                DestinationId = i.DestinationId,
                TourName = i.TourName,
                CityName = i.CityName,
                ImageUrl = i.ImageUrl,
                Price = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList();

            if (!cart.Any())
                return RedirectToAction("Index");

            
            var subtotal = cart.Sum(x => x.Price * x.Quantity);
            var serviceFee = 9.99m;
            var discount = 0.00m;
            var totalPaid = subtotal + serviceFee - discount;

            if (grandTotal.HasValue && grandTotal.Value > 0)
                totalPaid = grandTotal.Value;


            //  CAPACITY -

            foreach (var x in cart)
            {
                var destination = db.Destinations.Find(x.DestinationId);
                if (destination == null)
                    continue;

                if (destination.Capacity < x.Quantity)
                {
                    ModelState.AddModelError(
                        "",
                        $"Not enough capacity for {destination.TourName}. Remaining: {destination.Capacity}"
                    );

                    ViewBag.Total = subtotal;
                    return View(cart);
                }

                
                destination.Capacity -= x.Quantity;
            }


            // 3️⃣ BOOKING CREATE

            var bookingCode = "AF-" + Guid.NewGuid().ToString("N")
                .Substring(0, 8)
                .ToUpper();

            var booking = new Booking
            {
                BookingCode = bookingCode,
                UserId = userId,
                CreatedAt = DateTime.Now,

                Subtotal = subtotal,
                ServiceFee = serviceFee,
                Discount = discount,
                Total = totalPaid,

                FullName = fullName,
                Email = email,

                Items = cart.Select(x => new BookingItem
                {
                    DestinationId = x.DestinationId,
                    TourName = x.TourName,
                    CityName = x.CityName,
                    ImageUrl = x.ImageUrl,
                    UnitPrice = x.Price,
                    People = x.Quantity,
                    LineTotal = x.Price * x.Quantity
                }).ToList()
            };

            db.Bookings.Add(booking);

           
            // DB CART CLEAN 

            var itemsToDelete = db.DbCartItems
                .Where(i => i.DbCartId == dbCart.Id)
                .ToList();

            db.DbCartItems.RemoveRange(itemsToDelete);
            dbCart.UpdatedAt = DateTime.Now;

           
            db.SaveChanges();


            //SUCCESS SESSION

            Session["LAST_ORDER_TOTAL"] = totalPaid;
            Session["LAST_ORDER_NAME"] = fullName;
            Session["LAST_ORDER_EMAIL"] = email;
            Session["LAST_ORDER_ID"] = bookingCode;

            Session["LAST_ORDER_CARD"] =
                (!string.IsNullOrEmpty(cardNumber) && cardNumber.Length >= 4)
                    ? cardNumber.Substring(cardNumber.Length - 4)
                    : "****";

            return RedirectToAction("Success");
        }

        [Authorize]
        public ActionResult Success()
        {
            ViewBag.OrderId = Session["LAST_ORDER_ID"];
            ViewBag.Total = Session["LAST_ORDER_TOTAL"];
            ViewBag.Name = Session["LAST_ORDER_NAME"];
            return View();
        }
    }
}
