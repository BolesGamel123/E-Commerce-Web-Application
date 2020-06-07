using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Project.Models.AccountModel;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace Project.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDBContext context = new ApplicationDBContext();

        public ActionResult Search(string SearchName)
        {
            ViewBag.ProductName = SearchName;
            List<Product> pro =
                context.Products.Where(p => p.Name.Contains(SearchName)).ToList();
            return View(pro);
        }


        public ActionResult Index()
        {
            var prds = context.Products.ToList();
            return View(prds);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult New()
        {
            
            ViewBag.Categ = context.Categories.ToList();
            return View();
        }
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult New(Product pro,HttpPostedFileBase upload)
        {
            ViewBag.Categ = context.Categories.ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);
                    upload.SaveAs(path);
                    pro.image = upload.FileName;
                     context.Products.Add(pro);
                    context.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    return View(pro);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(pro);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Categ = context.Categories.ToList();
            var pr = context.Products.Where(p => p.Id == id).FirstOrDefault();

            return View(pr);
        }
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit( Product product, HttpPostedFileBase upload)
        {
            var catt = context.Categories.FirstOrDefault(c => c.Id == product.CategoriesID);
            product.Categories = catt;
            ViewBag.Categ = context.Categories.ToList();
            try
            {
                if (ModelState.IsValid)
                {

                    string path = Path.Combine(Server.MapPath("~/Content/images"), upload.FileName);
                    upload.SaveAs(path);
                    product.image = upload.FileName;
                    var pro = context.Products.FirstOrDefault(p => p.Id == product.Id);
                    pro.Name = product.Name;
                    pro.Categories = product.Categories;
                    pro.price = product.price;
                    pro.image = product.image;
                    pro.Categories.Name = product.Categories.Name;
                    context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(product);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(product);
            }
        }

        [Authorize(Roles = "Admin")]
      
        public ActionResult Delete(int id)
        {

            var pro = context.Products.Where(c => c.Id == id).FirstOrDefault();

            return View(pro);
        }

        
        public ActionResult SaveDelete(int id)
        {
            var pro = context.Products.Where(c => c.Id == id).FirstOrDefault();
            context.Products.Remove(pro);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult GetProductsForCategory(int id)
        {
            ViewBag.img = context.Categories.Where(c => c.Id == id).Select(c => c.Imagecat).FirstOrDefault();
            var Products = context.Products.Where(p => p.CategoriesID == id).ToList();
            return View(Products);
        }
        public ActionResult Details(int id)
        {
            Product p = context.Products.FirstOrDefault(c => c.Id == id);
            Session["id"] = id;
            return View(p);
        }

    }
}