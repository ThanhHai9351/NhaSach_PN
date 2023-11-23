﻿using System;
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
        public ActionResult Index()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Author> authors = db.Authors.ToList();
            return View(authors);
        }
    } 
}