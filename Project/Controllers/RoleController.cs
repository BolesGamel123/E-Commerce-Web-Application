using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Project.Models.AccountModel;

namespace Project.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> New(string RoleName)
        {
            if (RoleName != null)
            {
                ApplicationDBContext context = new ApplicationDBContext();
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);

                IdentityRole role = new IdentityRole();
                role.Name = RoleName;
                IdentityResult result = await manager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ViewBag.Error = result.Errors;
                    ViewBag.RoleName = RoleName;
                    return View();
                }
            }
            return View();
        }






        
    }
}