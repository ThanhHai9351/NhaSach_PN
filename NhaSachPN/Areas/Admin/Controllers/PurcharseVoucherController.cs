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
    public class PurcharseVoucherController : Controller
    {
        // GET: Admin/PurcharseVoucher
        public ActionResult Index(string search = "", int page = 1)
        {
            CompanyDBContext db = new CompanyDBContext();
            List<PurchaseVoucher> purchaseVouchers = db.PurchaseVouchers.Where(row => row.Product.ProductName.Contains(search)).ToList();

            ViewBag.Search = search;
            int soSP = 10;
            int soTrang = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(purchaseVouchers.Count) / Convert.ToDouble(soSP)));
            int trangSkip = (page - 1) * soSP;
            ViewBag.Page = page;
            ViewBag.SoTrang = soTrang;
            purchaseVouchers = purchaseVouchers.Skip(trangSkip).Take(soSP).ToList();
            return View(purchaseVouchers);
        }

        public ActionResult Edit(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            PurchaseVoucher pu = db.PurchaseVouchers.Where(row => row.ID == id).FirstOrDefault();
            return View(pu);
        }

        [HttpPost]
       public ActionResult Edit(PurchaseVoucher pu)
        {
            CompanyDBContext db = new CompanyDBContext();
            PurchaseVoucher pv = db.PurchaseVouchers.Where(row => row.ID == pu.ID).FirstOrDefault();
            pv.ProductID = pu.ProductID;
            pv.Quantity = pu.Quantity;
            pv.Money = (int)(pv.Quantity * pv.Product.Price);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PurchaseVoucher pu)
        {
            CompanyDBContext db = new CompanyDBContext();
            db.PurchaseVouchers.Add(pu);
            db.SaveChanges();
            UpdateMoneyOnDB();
            return RedirectToAction("Index");
        }

        internal void UpdateMoneyOnDB()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<PurchaseVoucher> pu = db.PurchaseVouchers.ToList();
            foreach(var item in pu)
            {
                item.Money = (int)(item.Quantity * item.Product.Price);
            }
            db.SaveChanges();
        }

        public ActionResult Delete(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            PurchaseVoucher pu = db.PurchaseVouchers.Where(row => row.ID == id).FirstOrDefault();
            db.PurchaseVouchers.Remove(pu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}