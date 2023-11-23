using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NhaSachPN.Models;
using NhaSachPN.Filters;

namespace NhaSachPN.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Product> products = db.Products.ToList();
            return View(products);
        }
    }
}