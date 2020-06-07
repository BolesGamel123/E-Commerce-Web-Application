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

    public class CartController : Controller
    {
        // GET: Cart
        ApplicationDBContext context = new ApplicationDBContext();

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public RedirectToRouteResult AddToCart(int Id, string returnUrl)
        {
            Product pro = context.Products.FirstOrDefault(p => p.Id == Id);
            if (pro != null)
            {
                GetCart().AddItem(pro);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int id, string returnUrl)
        {
            Product pro = context.Products.FirstOrDefault(p => p.Id == id);
            if (pro != null)
            {
                GetCart().RemoveLine(pro);
            }
            return RedirectToAction("Index", new { returnUrl });
        }



        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });

        }


        public ActionResult Summary()
        {
            return PartialView("_Summary", GetCart());
        }


        [Authorize]
        public ActionResult CreateOrder()
        {
            ViewBag.Amount = GetCart().Lines.Sum(p => p.Quantity);
            ViewBag.TotalPrice = GetCart().Lines.Sum(p => p.product.price * p.Quantity);
            ViewBag.UserID = User.Identity.GetUserId();

            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(order);
            }
            return View("OrderSuccess");
        }
    }
}