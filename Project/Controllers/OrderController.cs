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
    public class OrderController : Controller
    {
        ApplicationDBContext context = new ApplicationDBContext();
        [Authorize]
        [HttpGet]
        public ActionResult addtocart()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addtocart(string Description,int Amount,string Address)
        {
           

            var userid = User.Identity.GetUserId();
            var ProID = (int)Session["id"];
            var pro = context.Products.Where(p => p.Id == ProID).Select(p => p.price).FirstOrDefault();
            var order = new Order();
            order.UserID = userid;
            order.Description = Description;
            order.Amount = Amount;
            order.Address = Address;
            order.Date = DateTime.Now;
            order.TotalPrice = pro * (double)Amount;
            context.Orders.Add(order);
            context.SaveChanges();

            return RedirectToAction("Category", "Categories");
        }
        [Authorize]
        public ActionResult OrderByUserID()
        {
            ViewBag.pro = context.Products.FirstOrDefault();
            var userid = User.Identity.GetUserId();
            var ord = context.Orders.Where(o => o.UserID == userid).ToList();

            return View(ord);
        }
        [Authorize]
        public ActionResult DetailsOrder()
        {
            ViewBag.Pro = context.Products.FirstOrDefault();
            var userid = User.Identity.GetUserId();
    
            var order = context.Orders.Where(o => o.UserID== userid).ToList();
            return View(order);
        }
        [Authorize]
        [HttpGet]
        public ActionResult EditOrder(int id)
        {
            var order = context.Orders.Where(o => o.Id == id).FirstOrDefault();

            return View(order);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditOrder(int id,Order order)
        {
           
            try
            {
                if (ModelState.IsValid)
                {

                    var orderr = context.Orders.Where(o => o.Id == id).FirstOrDefault();
                    orderr.Description = order.Description;
                    orderr.Address = order.Address;
                    orderr.Amount = order.Amount;
                    orderr.Date = DateTime.Now;
                    context.SaveChanges();
                    return RedirectToAction("OrderByUserID");
                }
                else
                {
                    return View(order);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(order);
            }
        }







        [Authorize]
        public ActionResult DeleteOrder(int id)
        {

            var order = context.Orders.Where(o => o.Id == id).FirstOrDefault();

            return View(order);
        }


        public ActionResult SaveDeleteOrder(int id)
        {
            var order = context.Orders.Where(o => o.Id == id).FirstOrDefault();
            context.Orders.Remove(order);
            context.SaveChanges();
            return RedirectToAction("OrderByUserID");
        }
    }
}