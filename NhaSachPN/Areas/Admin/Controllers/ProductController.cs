using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NhaSachPN.Models;
using NhaSachPN.Filters;
using System.IO;

namespace NhaSachPN.Areas.Admin.Controllers
{
    [AdminAuthorization]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult Index(string search = "",int page = 1)
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Product> products = db.Products.Where(row => row.ProductName.Contains(search)).ToList();
            ViewBag.Search = search;
            int soSP = 10;
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(products.Count) / Convert.ToDouble(soSP)));
            int trangSkip = (page - 1) * soSP;
            ViewBag.Page = page;
            ViewBag.SoTrang = soTrang;
            products = products.Skip(trangSkip).Take(soSP).ToList();
            return View(products);
        }

        public ActionResult Detail(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return View(pro);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase Image, Product pro)
        {
            CompanyDBContext db = new CompanyDBContext();
            pro.Date = DateTime.Now;
            if (Image != null && Image.ContentLength > 0)
            {
                string imageName = Path.GetFileName(Image.FileName);
                string imagePath = Path.Combine(Server.MapPath("~/Images"), imageName);
                Image.SaveAs(imagePath);
                pro.Image = "/Images/" + imageName;
            }
            db.Products.Add(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            return View(pro);
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase Image, Product pro)
        {
            CompanyDBContext db = new CompanyDBContext();
            Product pro1 = db.Products.Where(row => row.ProductID == pro.ProductID).FirstOrDefault();
            if (Image != null && Image.ContentLength > 0)
            {
                string imageName = Path.GetFileName(Image.FileName);
                string imagePath = Path.Combine(Server.MapPath("~/Images"), imageName);
                Image.SaveAs(imagePath);
                pro1.Image = "/Images/" + imageName;
            }
            pro1.Description = pro.Description;
            pro1.ProductName = pro.ProductName;
            pro1.Quantity = pro.Quantity;
            pro1.Price = pro.Price;
            pro1.CategoryID = pro.CategoryID;
            pro1.AuthorID = pro.AuthorID;
            pro1.PublisherID = pro.PublisherID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}