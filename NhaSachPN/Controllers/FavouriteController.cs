using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NhaSachPN.Models;

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
    }
}