using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NhaSachPN.Models;

namespace NhaSachPN.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Product> products = db.Products.Take(5).ToList();
            return View(products);
        }
    }
}