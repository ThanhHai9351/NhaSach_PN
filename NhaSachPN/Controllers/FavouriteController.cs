using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NhaSachPN.Models;
using NhaSachPN.Filters;

namespace NhaSachPN.Controllers
{
    public class FavouriteController : Controller
    {
        // GET: Favourite
        public ActionResult Index()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Favourite> favourites = db.Favourites.ToList();
            return View(favourites);
        }

        public ActionResult Delete(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Favourite fv = db.Favourites.Where(row => row.ID == id).FirstOrDefault();
            db.Favourites.Remove(fv);
            db.SaveChanges();
            return RedirectToAction("Index", "Favourite");
        }

        public bool checkFavourite(Product pro)
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Favourite> favous = db.Favourites.ToList();
            foreach (var item in favous)
            {
                if (item.ProductID == pro.ProductID)
                    return false;
            }
            return true;
        }

        public ActionResult Create(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            if (checkFavourite(pro))
            {
                Favourite fv = new Favourite();
                fv.ProductID = id;
                db.Favourites.Add(fv);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Favourite");
        }
    }
}