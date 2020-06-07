using Microsoft.AspNet.Identity;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Project.Models.AccountModel;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDBContext context = new ApplicationDBContext();
        public ActionResult Search(string ProductName)
        {
            ViewBag.ProductName = ProductName;

            List<Product> pro =
                context.Products.Where(d => d.Name.Contains(ProductName)).ToList();
            return View("index", pro);
        }
        

        public ActionResult Index()
        {
            return View(context.Products.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


       
    }
}