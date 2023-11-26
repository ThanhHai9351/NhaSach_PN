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
    public class AuthorController : Controller
    {
        // GET: Admin/Author
        public ActionResult Index(string search = "", int page = 1)
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Author> authors = db.Authors.Where(row => row.AuthorName.Contains(search)).ToList();
            ViewBag.Search = search;
            int soSP = 10;
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(authors.Count) / Convert.ToDouble(soSP)));
            int trangSkip = (page - 1) * soSP;
            ViewBag.Page = page;
            ViewBag.SoTrang = soTrang;
            authors = authors.Skip(trangSkip).Take(soSP).ToList();

            return View(authors);
        }

        public ActionResult Edit(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Author au = db.Authors.Where(row => row.ID == id).FirstOrDefault();
            return View(au);
        }

        [HttpPost]
        public ActionResult Edit(Author au)
        {
            CompanyDBContext db = new CompanyDBContext();
            Author a = db.Authors.Where(row => row.ID == au.ID).FirstOrDefault();
            a.AuthorName = au.AuthorName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author au)
        {
            CompanyDBContext db = new CompanyDBContext();
            db.Authors.Add(au);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete (int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Author au = db.Authors.Where(row => row.ID == id).FirstOrDefault();
            db.Authors.Remove(au);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    } 
}