using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Project.Models.AccountModel;

namespace Project.Controllers
{
    public class CategoriesController : Controller
    {
        ApplicationDBContext context = new ApplicationDBContext();

        // GET: Categories
        public ActionResult Category()
        {
            return View(context.Categories.ToList());
        }
    }
}