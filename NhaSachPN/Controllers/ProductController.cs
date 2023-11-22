using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NhaSachPN.Models;

namespace NhaSachPN.Controllers
{
    public class ProductController : Controller
    {
        public string Search = "";
        // GET: Product
        public ActionResult Index(string search = "", int? rangePrice = 0, int page = 1, int? cateID = 0, int? authorID = 0, int? publisherID = 0,
            int? sortPrice = 0, int? sortName = 0, int? sortNew = 0)
        {
            Search = search;
            CompanyDBContext db = new CompanyDBContext();
            List<Product> pro = db.Products.ToList();
            long maxPrice = 0;
            List<Product> products = db.Products.Where(row => row.ProductName.Contains(search)).ToList();
            if (rangePrice > 0)
            {
                products = products.Where(row => row.Price < rangePrice).ToList();
            }
            if (cateID > 0)
            {
                products = products.Where(row => row.CategoryID == cateID).ToList();
            }
            if (authorID > 0)
            {
                products = products.Where(row => row.AuthorID == authorID).ToList();
            }
            if (publisherID > 0)
            {
                products = products.Where(row => row.PublisherID == publisherID).ToList();
            }
            foreach (var item in pro)
            {
                if (item.Price > maxPrice)
                {
                    maxPrice = (long)item.Price;
                }
            }
            ViewBag.MAX = maxPrice;
            if (rangePrice > 0)
            {
                ViewBag.PRICE = rangePrice;
            }
            else
            {
                ViewBag.PRICE = maxPrice;
            }
            if (sortPrice == 1)
            {
                products = products.OrderBy(row => row.Price).ToList();
            }
            if (sortPrice == 2)
            {
                products = products.OrderByDescending(row => row.Price).ToList();
            }
            if (sortName == 1)
            {
                products = products.OrderBy(row => row.ProductName).ToList();
            }
            if (sortName == 2)
            {
                products = products.OrderByDescending(row => row.ProductName).ToList();
            }
            if (sortNew != 0)
            {
                products = products.OrderByDescending(row => row.Date).ToList();
            }
            ViewBag.Search = Search;
            ViewBag.CateID = cateID;
            ViewBag.AuthorID = authorID;
            ViewBag.PublisherID = publisherID;
            ViewBag.SortName = sortName;
            ViewBag.SortPrice = sortPrice;
            ViewBag.SortNew = sortNew;
            int soSP = 9;
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
    }
}