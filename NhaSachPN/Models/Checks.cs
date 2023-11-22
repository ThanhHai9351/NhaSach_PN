using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NhaSachPN.Models
{
    public class Checks
    {
        [Key]
        public long ID { get; set; }

        public long ProductID { get; set; }

        public int Quantity { get; set; }

        public int Total { get; set; }

        public virtual Product Product { get; set; }
    }
}