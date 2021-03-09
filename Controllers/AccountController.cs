using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SpiceWithoutAuthentication.Identity;
using SpiceWithoutAuthentication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SpiceWithoutAuthentication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        ApplicationDbContext appDbContext;

        public AccountController()
        {
            appDbContext = new ApplicationDbContext();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                //Register a user
                var userStore = new ApplicationUserStore(appDbContext);
                var userManager = new ApplicationUserManager(userStore);
                var passwordHash = Crypto.HashPassword(rvm.Password);
                var user = new AppUser()
                {
                    Email = rvm.Email,
                    Name = rvm.Name,
                    UserName=rvm.Name,
                    PasswordHash = passwordHash,
                    City = rvm.City,
                    StreetAddress = rvm.StreetAddress,
                    State = rvm.State,
                    PostalCode = rvm.PostalCode,
                    PhoneNumber = rvm.Mobile,
                    Id=rvm.Name
                    
                };
                IdentityResult result = userManager.Create(user);
                if (result.Succeeded)
                {
                    //assigning role
                    userManager.AddToRole(user.Name, "Customer");

                    //login
                    var authenticationManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                }
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("My Error", "Invalid Data");
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel lvm)
        {
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            var user = userManager.Find(lvm.Name, lvm.Password);
            if (user != null)
            {
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                return RedirectToAction("Index", "Menu");
            }
            else
            {
                ModelState.AddModelError("MyError", "Invalid user name and password");
                return View();
            }

        }
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Menu");
        }
        public ActionResult MyProfile()
        {
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            var appUser = userManager.FindById(User.Identity.GetUserId());
            return View(appUser);
        }
    }
}