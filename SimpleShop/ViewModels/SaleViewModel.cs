using SimpleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleShop.ViewModels
{
    public class SaleInfoViewModel
    {
        public  Sale customerin{ get; set; }
        public List<SaleProduct> productin{ get; set; }

    }
    public class SaleViewModel
    {
        public List<Customer> AllCustomers { get; set; }
        public List<Product> AllProducts { get; set; }

    }
    public class TotalInfosave
    {
        public int saleId { get; set; }
        public int saleProductId { get; set; }
        public List<Customer> AllCustomers { get; set; }
        public List<Product> AllProducts { get; set; }
        public string productName { get; set; }
        public string Unit { get; set; }
       
        public decimal Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public string DeliveryAddress { get; set; }

        public DateTime SaleDateTime { get; set; }

        public decimal TotalAmount { get; set; }
    }
}