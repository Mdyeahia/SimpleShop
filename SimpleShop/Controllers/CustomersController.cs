using SimpleShop.Models;
using SimpleShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleShop.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult CustomerList()
        {
            CustomerViewModel model = new CustomerViewModel();

            model.AllCustomers = context.customers.ToList();

            return PartialView(model);
        }
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            var newCustomer = new Customer();

            newCustomer.CustomerName = customer.CustomerName;
            newCustomer.CustomerAddress = customer.CustomerAddress;

            context.customers.Add(newCustomer);
            context.SaveChanges();

            return RedirectToAction("CustomerList");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {

            var customer=context.customers.Where(c => c.Id == ID).FirstOrDefault();

            context.customers.Remove(customer);
            context.SaveChanges();

            return RedirectToAction("CustomerList");
        }












        //public ActionResult Update(int Id)
        //{
        //    var customer = context.customers.Find(Id);
        //    if (customer != null)
        //    {
        //        return PartialView(customer);
        //    }

        //    return View("Error");
        //}
        //[HttpPost]
        //public ActionResult Update(Customer customer)
        //{



        //    var oldCustomer = context.customers.Find(customer.Id);

        //    oldCustomer.CustomerName = customer.CustomerName;
        //    oldCustomer.CustomerAddress = customer.CustomerAddress;

        //    context.Entry(oldCustomer).State = System.Data.Entity.EntityState.Modified;
        //    context.SaveChanges();



        //    return RedirectToAction("CustomertList");
        //}
    }
}