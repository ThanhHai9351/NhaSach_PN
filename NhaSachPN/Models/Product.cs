using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NhaSachPN.Models
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        
        public string ProductName { get; set; }

        public Nullable<long> Price { get; set; }

        public Nullable<long> Quantity { get; set; }

        public string Image { get; set; }

        public DateTime Date { get; set; }
        
        public string Description { get; set; }

        public Nullable<long> PublisherID { get; set; }

        public Nullable<long> CategoryID { get; set; }

        public Nullable<long> AuthorID { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual Category Category { get; set; }

        public virtual Author Author { get; set; }

    }
}