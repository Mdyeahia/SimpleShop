using SimpleShop.Models;
using SimpleShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Controllers
{
    public class SaleController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Sale
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SalePage()
        {
            SaleViewModel model = new SaleViewModel();

            model.AllCustomers = context.customers.ToList();
            model.AllProducts = context.products.ToList();

            return View(model);
        }
        public  ActionResult SaleProductList(int saleId)
        {
            SaleInfoViewModel model = new SaleInfoViewModel();
            model.productin =  context.saleProducts.Where(s=>s.Id==saleId).ToList();

            return PartialView(model);
        }
        public ActionResult CustomerInfo(int customerId)
        {
            SaleInfoViewModel model = new SaleInfoViewModel();

            model.customerin = context.sales.OrderByDescending(p=>p.Id).Where(p => p.CustomerId==customerId).First();
            
            return PartialView(model);
        }
        public   void CustomerSelectSave(int customerId)
        {
            var customerinfo = context.customers.Find(customerId);
            

            var newSale = new Sale();
           
            newSale.CustomerId = customerinfo.Id;
            newSale.DeliveryAddress = customerinfo.CustomerAddress;
            newSale.SaleDateTime = DateTime.Now;
            newSale.TotalAmount = 0;

            context.sales.Add(newSale);
            context.SaveChanges();

        }
        public void ProductSelectSave(int productId, int customerId)
        {
            var productinfo = context.products.Find(productId);
            var saleid = context.sales.OrderByDescending(p => p.Id).Where(p => p.CustomerId == customerId).First();

            var newProductSale = new SaleProduct
            {
                SaleId = saleid.Id,
                ProductId = productinfo.ProductName,
                Qty = 0,
                Rate = productinfo.Rate,
                Amount = 0
            };

            context.saleProducts.Add(newProductSale);
            context.SaveChanges();

        }
        public ActionResult TotalInfoUpdate(TotalInfosave model)
        {
            var saleinfo = context.sales.Find(model.saleId);

            saleinfo.CustomerId = model.CustomerId;
            saleinfo.DeliveryAddress = model.DeliveryAddress;
            saleinfo.SaleDateTime = DateTime.Now;
            saleinfo.TotalAmount = model.TotalAmount;
           
            var saleproductinfo = context.saleProducts.Find(model.saleProductId);
            saleproductinfo.SaleId = model.CustomerId;
            saleproductinfo.ProductId = model.productName;
            saleproductinfo.Qty = model.Qty;
            saleproductinfo.Rate = model.Rate;
            saleproductinfo.Amount = model.Amount;

            context.Entry(saleinfo).State = System.Data.Entity.EntityState.Modified;
            context.Entry(saleproductinfo).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("SalePage");
        }
    }
}