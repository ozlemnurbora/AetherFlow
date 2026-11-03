using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AetherFlow.Models;

namespace AetherFlow.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var popular = db.Destinations
        .Where(d => d.IsPopular)
        .GroupBy(d => d.CityName)
        .Select(g => g.OrderBy(x => x.Id).FirstOrDefault())
        .ToList();

            var other = db.Destinations
                .Where(d => !d.IsPopular)
                .GroupBy(d => d.CityName)
                .Select(g => g.OrderBy(x => x.Id).FirstOrDefault())
                .ToList();

            ViewBag.OtherCities = other;
            return View(popular);
        }

        public ActionResult About() { return View(); }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactMessage model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.CreatedAt = DateTime.Now;

            db.ContactMessages.Add(model);
            db.SaveChanges();

            TempData["Success"] = "Your message has been sent successfully!";
            return RedirectToAction("Contact");
        }
    }
}
