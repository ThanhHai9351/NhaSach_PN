using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NhaSachPN.Models
{
    public class PurchaseVoucher
    {
        [Key]
        public long ID { get; set; }

        public Nullable<long> ProductID { get; set; }

        public int Quantity { get; set; }

        public int Money { get; set; }

        public virtual Product Product { get; set; } 
    }
}