using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
    }
}