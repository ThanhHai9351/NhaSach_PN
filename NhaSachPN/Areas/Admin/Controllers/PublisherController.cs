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
    public class PublisherController : Controller
    {
        // GET: Admin/Publisher
        public ActionResult Index(string search = "", int page = 1)
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Publisher> publishers = db.Publishers.Where(row => row.PublisherName.Contains(search)).ToList();
            ViewBag.Search = search;
            int soSP = 10;
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(publishers.Count) / Convert.ToDouble(soSP)));
            int trangSkip = (page - 1) * soSP;
            ViewBag.Page = page;
            ViewBag.SoTrang = soTrang;
            publishers = publishers.Skip(trangSkip).Take(soSP).ToList();

            return View(publishers);
        }

        public ActionResult Edit(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Publisher pb = db.Publishers.Where(row => row.ID == id).FirstOrDefault();
            return View(pb);
        }

        [HttpPost]
        public ActionResult Edit(Publisher pb)
        {
            CompanyDBContext db = new CompanyDBContext();
            Publisher a = db.Publishers.Where(row => row.ID == pb.ID).FirstOrDefault();
            a.PublisherName = pb.PublisherName;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Publisher pb)
        {
            CompanyDBContext db = new CompanyDBContext();
            db.Publishers.Add(pb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Publisher pb = db.Publishers.Where(row => row.ID == id).FirstOrDefault();
            db.Publishers.Remove(pb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}