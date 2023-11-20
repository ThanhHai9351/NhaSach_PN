using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NhaSachPN.Models
{
    public class Category
    {
        [Key]
        public long ID { get; set; }

        public string CategoryName { get; set; }
    }
}