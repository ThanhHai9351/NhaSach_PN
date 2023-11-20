using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NhaSachPN.Models
{
    public class CompanyDBContext: DbContext
    {
        public CompanyDBContext() : base("MyConnectionString") { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Evaluate> Evalutes { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Favourite> Favourites { get; set; }

        public DbSet<PurchaseVoucher> PurchaseVouchers { get; set; }
    }
}