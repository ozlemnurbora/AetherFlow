using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AetherFlow.Models;

namespace AetherFlow.Controllers
{
    public class DestinationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // ADMIN ONLY

        // GET: Destinations (Admin panel)
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Destinations.ToList());
        }

        // GET: Destinations/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Destinations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,TourName,CityName,Description,WhatsIncluded,MeetingPoint,StartDate,EndDate,Duration,Transportation,Price,Capacity,ImageUrl,IsPopular")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Destinations.Add(destination);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(destination);
        }

        // GET: Destinations/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Destination destination = db.Destinations.Find(id);
            if (destination == null)
                return HttpNotFound();

            return View(destination);
        }

        // POST: Destinations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,TourName,CityName,Description,WhatsIncluded,MeetingPoint,StartDate,EndDate,Duration,Transportation,Price,Capacity,ImageUrl,IsPopular")] Destination destination)
        {
            if (ModelState.IsValid)
            {
                db.Entry(destination).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(destination);
        }

        // GET: Destinations/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Destination destination = db.Destinations.Find(id);
            if (destination == null)
                return HttpNotFound();

            return View(destination);
        }

        // POST: Destinations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
                return HttpNotFound();

            db.Destinations.Remove(destination);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: Destinations/Details/5  (Tour detail page)
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Destination destination = db.Destinations.Find(id);
            if (destination == null)
                return HttpNotFound();

            return View(destination);
        }

        // GET: Destinations/Browse?city=Istanbul&from=2026-01-01&to=2026-01-02
        [AllowAnonymous]
        public ActionResult Browse(string city, DateTime? from, DateTime? to)
        {
            if (string.IsNullOrEmpty(city))
                return RedirectToAction("Index", "Home");

            var query = db.Destinations.Where(d => d.CityName == city);

            if (from.HasValue) query = query.Where(d => d.StartDate >= from.Value);
            if (to.HasValue) query = query.Where(d => d.EndDate <= to.Value);

            ViewBag.City = city;
            return View(query.OrderBy(d => d.StartDate).ToList());
        }

        // GET: Destinations/AllCities  
        [AllowAnonymous]
        public ActionResult AllCities()
        {
            var cities = db.Destinations
                .GroupBy(d => d.CityName)
                .Select(g => g.OrderBy(x => x.Id).FirstOrDefault())
                .OrderBy(x => x.CityName)
                .ToList();

            return View(cities);
        }

        
        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
