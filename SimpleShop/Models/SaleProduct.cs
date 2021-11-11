using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
    public class SaleProduct
    {
        public int Id { get; set; }
        public int SaleId{ get; set; } 
        public Sale Sale { get; set; }
        public string ProductId { get; set; }
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}