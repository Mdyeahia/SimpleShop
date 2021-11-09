using SimpleShop.Models;
using SimpleShop.ViewModels;
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
            ProductViewModel model = new ProductViewModel();

            model.AllProducts = context.products.ToList();

            return PartialView(model);
        }
        public ActionResult Create()
        {
            return PartialView();
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

            return RedirectToAction("ProductList");
        }
        public ActionResult Update(int Id)
        {
            var prodcut = context.products.Find(Id);
            if (prodcut != null)
            {
                return PartialView(prodcut);
            }

            return View("Error");
        }
        [HttpPost]
        public ActionResult Update(Product product)
        {



            var oldProduct = context.products.Find(product.Id);

            oldProduct.ProductName = product.ProductName;
            oldProduct.Unit = product.Unit;
            oldProduct.Rate = product.Rate;

            context.Entry(oldProduct).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();



            return RedirectToAction("ProductList");
        }

    }
}