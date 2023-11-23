﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NhaSachPN.Models;
using NhaSachPN.Filters;

namespace NhaSachPN.Controllers
{
    public class PurcharseVoucherController : Controller
    {
        // GET: PurcharseVoucher
        public ActionResult Index()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Checks> chks = db.Checks.ToList();
            return View(chks);
        }

        [HttpGet]
        public ActionResult UpdateQuantity(int ProductID, int Quantity)
        {
            CompanyDBContext db = new CompanyDBContext();
            Checks ch = db.Checks.Where(row => row.ProductID == ProductID).FirstOrDefault();
            ch.Quantity = Quantity;
            ch.Total = (int)(ch.Quantity * ch.Product.Price);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Product pro = db.Products.Where(row => row.ProductID == id).FirstOrDefault();
            Checks chks = new Checks();
            chks.ProductID = pro.ProductID;
            chks.Quantity = 1;
            chks.Total = (int)(chks.Quantity * pro.Price);
            db.Checks.Add(chks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Remove(int id)
        {
            CompanyDBContext db = new CompanyDBContext();
            Checks chks = db.Checks.Where(row => row.ProductID == id).FirstOrDefault();
            db.Checks.Remove(chks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [MyAuthenFilter]
        public ActionResult Pay()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Checks> lvChks = db.Checks.ToList();
            foreach (var item in lvChks)
            {
                PurchaseVoucher purcharse = new PurchaseVoucher();
                purcharse.ProductID = item.ProductID;
                purcharse.Quantity = item.Quantity;
                purcharse.Money = item.Total;

                db.PurchaseVouchers.Add(purcharse);
            }
            foreach (var item in lvChks)
            {
                Product pro = db.Products.Where(row => row.ProductID == item.ProductID).FirstOrDefault();
                pro.Quantity = pro.Quantity - item.Quantity;
                db.Checks.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}