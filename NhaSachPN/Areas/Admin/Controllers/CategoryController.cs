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
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        public ActionResult Index(string search = "", int page = 1)
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Category> categories = db.Categories.Where(row => row.CategoryName.Contains(search)).ToList();
            ViewBag.Search = search;
            int soSP = 10;
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(categories.Count) / Convert.ToDouble(soSP)));
            int trangSkip = (page - 1) * soSP;
            ViewBag.Page = page;
            ViewBag.SoTrang = soTrang;
            categories = categories.Skip(trangSkip).Take(soSP).ToList();

            return View(categories);
        }

        public ActionResult Edit(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Category au = db.Categories.Where(row => row.ID == id).FirstOrDefault();
            return View(au);
        }

        [HttpPost]
        public ActionResult Edit(Category au)
        {
            CompanyDBContext db = new CompanyDBContext();
            Category a = db.Categories.Where(row => row.ID == au.ID).FirstOrDefault();
            a.CategoryName = au.CategoryName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category au)
        {
            CompanyDBContext db = new CompanyDBContext();
            db.Categories.Add(au);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Category au = db.Categories.Where(row => row.ID == id).FirstOrDefault();
            db.Categories.Remove(au);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}