using SimpleShop.Models;
using SimpleShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public  ActionResult SaleProductInfo(int saleId)
        {
            SaleInfoViewModel model = new SaleInfoViewModel();
            model.productin =  context.saleProducts.Where(s=>s.SaleId==saleId).ToList();

            return PartialView(model);
        }
        public ActionResult SaleInfoCustomer(int customerId)
        {
            SaleInfoViewModel model = new SaleInfoViewModel();

            model.customerin = context.sales.OrderByDescending(p=>p.Id).Where(p => p.CustomerId==customerId).First();
            
            return PartialView(model);
        }
        public ActionResult SaleSave(int customerId)
        {
            var customerinfo = context.customers.Find(customerId);
            

            var newSale = new Sale();
           
            newSale.CustomerId = customerinfo.Id;
            newSale.DeliveryAddress = customerinfo.CustomerAddress;
            newSale.SaleDateTime = DateTime.Now;
            newSale.TotalAmount = 0;

            context.sales.Add(newSale);
            context.SaveChanges();

            return RedirectToAction("SaleInfoCustomer", new { customerId =newSale.CustomerId});

        }
        public ActionResult SaleProductSave(int productId, int customerId)
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

            return RedirectToAction("SaleProductInfo", new { saleId = newProductSale.SaleId });
        }
        [HttpPost]
        public JsonResult TotalInfoUpdate(int customerId,int saleId, decimal totalamount,decimal qty,decimal amount)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                var saleinfo = context.sales.OrderByDescending(p => p.Id).Where(p => p.CustomerId == customerId).First();

                saleinfo.TotalAmount = totalamount;

                var saleproductinfo = context.saleProducts.Find(saleId);

                saleproductinfo.Qty = qty;

                saleproductinfo.Amount = amount;

                context.Entry(saleinfo).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                context.Entry(saleproductinfo).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                

                result.Data = new { success = true };
            }
            else
            {
                result.Data = new { success = false, message = "Invalid update" };
            }
            return result;

        }
    }
}