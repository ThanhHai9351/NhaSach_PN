using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NhaSachPN.Models;

namespace NhaSachPN.ApiControllers
{
    public class AuthorController : ApiController
    {
        public List<Author> Get()
        {
            CompanyDBContext db = new CompanyDBContext();
            List<Author> authors = db.Authors.ToList();
            return authors;
        }
    }
}
