using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime SaleDateTime { get; set; }

        public decimal TotalAmount { get; set; }
        
    }
}