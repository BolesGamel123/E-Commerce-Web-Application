using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static Project.Models.AccountModel;

namespace Project.Controllers
{
    public class AccountController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
                return View(userLogin);
            try
            {
                
                ApplicationDBContext context = new ApplicationDBContext();
                UserStore<IdentityUser> store = new UserStore<IdentityUser>(context);
                UserManager<IdentityUser> manager =
                    new UserManager<IdentityUser>(store);
                IdentityUser user = new IdentityUser();
                user.UserName = userLogin.Username;
                user.PasswordHash = userLogin.Password;
      
                var appUser = manager.Find(user.UserName, userLogin.Password);
                if (appUser != null)
                {
                    IAuthenticationManager authenticationManager =
                        HttpContext.GetOwinContext().Authentication;
                    SignInManager<IdentityUser, string> signinmanager =
                        new SignInManager<IdentityUser, string>
                        (manager, authenticationManager);
                    signinmanager.SignIn(appUser, true, true);
                    return RedirectToAction("Index", "Home");

                }
                else
                { 
                    return View(userLogin);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userLogin);
            }

        }
        [Authorize]
        public ActionResult SignOut()
        {
            IAuthenticationManager manager = HttpContext.GetOwinContext().Authentication;
            manager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Registration()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Registration(RegistrationViewModel userRegister)
        {
            if (!ModelState.IsValid)
                return View(userRegister);
            try
            {
                ApplicationDBContext context = new ApplicationDBContext();
                UserStore<IdentityUser> store = new UserStore<IdentityUser>(context);
                UserManager<IdentityUser> manager =
                   new UserManager<IdentityUser>(store);
                IdentityUser user = new IdentityUser();
                user.UserName = userRegister.Username;
                user.PasswordHash = userRegister.Password;
                user.Email = userRegister.Email;

                IdentityResult resulte = await manager.CreateAsync(user, userRegister.Password);
                if (resulte.Succeeded)
                {
                    manager.AddToRole(user.Id, "Customer");
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    SignInManager<IdentityUser, string> signinmanager =
                        new SignInManager<IdentityUser, string>
                        (manager, authenticationManager);
                    signinmanager.SignIn(user, true, true);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", (resulte.Errors.ToList())[0]);
                    return View(userRegister);

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userRegister);
            }




        }




       [Authorize(Roles ="Admin")]
        public ActionResult RegistrationAdmin()
        {

            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> RegistrationAdmin(RegistrationViewModel userRegister)
        {
            if (!ModelState.IsValid)
                return View(userRegister);
            try
            {
                ApplicationDBContext context = new ApplicationDBContext();
                UserStore<IdentityUser> store = new UserStore<IdentityUser>(context);
                UserManager<IdentityUser> manager =
                   new UserManager<IdentityUser>(store);
                IdentityUser user = new IdentityUser();
                user.UserName = userRegister.Username;
                user.PasswordHash = userRegister.Password;
                user.Email = userRegister.Email;

                IdentityResult resulte = await manager.CreateAsync(user, userRegister.Password);
                if (resulte.Succeeded)
                {
                    manager.AddToRole(user.Id, "Admin");
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    SignInManager<IdentityUser, string> signinmanager =
                        new SignInManager<IdentityUser, string>
                        (manager, authenticationManager);
                    signinmanager.SignIn(user, true, true);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", (resulte.Errors.ToList())[0]);
                    return View(userRegister);

                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(userRegister);
            }




        }


    }
}