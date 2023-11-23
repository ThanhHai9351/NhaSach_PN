using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NhaSachPN.Models;
using NhaSachPN.Filters;

namespace NhaSachPN.Controllers
{
    [MyAuthenFilter]
    public class EvaluateController : Controller
    {
        // GET: Evaluate
        public ActionResult Index()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Evaluate> evaluates = db.Evalutes.Where(row => row.ProductEvaluate == "...").ToList();
            return View(evaluates);
        }

        public ActionResult Delete(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Evaluate evaluate = db.Evalutes.Where(row => row.ID == id).FirstOrDefault();
            db.Evalutes.Remove(evaluate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Send(int id,string ProductEvaluate)
        {
            CompanyDBContext db = new CompanyDBContext();
            Evaluate evaluate = db.Evalutes.Where(row => row.ID == id).FirstOrDefault();
            evaluate.ProductEvaluate = ProductEvaluate;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}