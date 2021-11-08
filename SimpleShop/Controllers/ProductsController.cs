using SimpleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductList()
        {
            var AllProducts = context.products.ToList();

            return PartialView(AllProducts);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            var newProduct = new Product();

            newProduct.ProductName = product.ProductName;
            newProduct.Unit = product.Unit;
            newProduct.Rate = product.Rate;

            context.products.Add(newProduct);
            context.SaveChanges();

            return View();
        }
        public ActionResult Update(int Id)
        {
            var prodcut = context.products.Find(Id);
            if (prodcut != null)
            {
                return View(prodcut);
            }

            return View("Error");
        }
        public JsonResult Update(Product product)
        {
            JsonResult result = new JsonResult();

            if (ModelState.IsValid)
            {
                var oldProduct = context.products.Find(product.Id);

                oldProduct.ProductName = product.ProductName;
                oldProduct.Unit = product.Unit;
                oldProduct.Rate = product.Rate;

                context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                result.Data = new { success = true };
            }
            else
            {
                result.Data = new { success = false, message = "Invalid Update Inputs." };
            }

            return result;
        }

    }
}