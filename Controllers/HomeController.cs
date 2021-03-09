using SpiceWithoutAuthentication.Models;
using SpiceWithoutAuthentication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
//using Spice.Data;
//using Spice.Models;
//using Spice.Models.ViewModels;
//using Spice.Utility;

namespace Spice.Controllers
{
   
    public class HomeController : Controller
    {
        SpiceDbContext _db;

        public HomeController()
        {
             _db=new SpiceDbContext();
        }


        public ActionResult Index()
        {
            IndexViewModel IndexVM = new IndexViewModel()
            {
                MenuItem = _db.MenuItem.Include("Category").Include("SubCategory").ToList(),
                Category =_db.Category.ToList(),
                Coupon = _db.Coupon.Where(c => c.IsActive == true).ToList()

            };

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim!=null)
            {
                var cnt = _db.ShoppingCart.Where(u => u.ApplicationUserId == claim.Value).ToList().Count;
                
            }


            return View(IndexVM);
        }

        
        public ActionResult Details(int id)
        {
            var menuItemFromDb =_db.MenuItem.Include("Category").Include("SubCategory").Where(m => m.Id == id).FirstOrDefault();

            ShoppingCart cartObj = new ShoppingCart()
            {
                MenuItem = menuItemFromDb,
                MenuItemId = menuItemFromDb.Id
            };

            return View(cartObj);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ShoppingCart CartObject)
        {
            CartObject.Id = 0;
            if(ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb = _db.ShoppingCart.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId
                                                && c.MenuItemId == CartObject.MenuItemId).FirstOrDefault();

                if(cartFromDb==null)
                {
                    _db.ShoppingCart.Add(CartObject);
                }
                else
                {
                    cartFromDb.Count = cartFromDb.Count + CartObject.Count;
                }
                _db.SaveChangesAsync();

                var count = _db.ShoppingCart.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId).ToList().Count();
                //HttpContext.Session.SetInt32(SD.ssShoppingCartCount, count);

                return RedirectToAction("Index");
            }
            else
            {

                var menuItemFromDb =  _db.MenuItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == CartObject.MenuItemId).FirstOrDefault();

                ShoppingCart cartObj = new ShoppingCart()
                {
                    MenuItem = menuItemFromDb,
                    MenuItemId = menuItemFromDb.Id
                };

                return View(cartObj);
            }
        }





        
    }
}
