using SpiceWithoutAuthentication.Global;
using SpiceWithoutAuthentication.Identity;
using SpiceWithoutAuthentication.Models;
using SpiceWithoutAuthentication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SpiceWithoutAuthentication.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        SpiceDbContext sdb = new SpiceDbContext();



        public ViewResult ShowCart()
        {
            var cartItems = sdb.ShoppingCart.Where(user => user.ApplicationUserId == User.Identity.Name).ToList();
            foreach (var item in cartItems)
            {
                item.MenuItem = sdb.MenuItem.Where(r => r.Id == item.MenuItemId).FirstOrDefault();
            }

            return View(cartItems);
        }

        public ActionResult AddToCart(int id)//,int uid)
        {
            // var _menuitem= sdb.MenuItem.Where(r => r.Id==id).ToList();
            //if(sdb.ShoppingCart.(id)==null)

            var item = sdb.ShoppingCart.Where(user => user.ApplicationUserId == User.Identity.Name && user.MenuItemId == id).FirstOrDefault();

            if (item != null)
            {
                item.Count = item.Count + 1;
            }
            else
            {
                var addcartitem = new ShoppingCart()
                {
                    ApplicationUserId = User.Identity.Name,
                    MenuItemId = id

                };
                sdb.ShoppingCart.Add(addcartitem);
            }
            sdb.SaveChanges();
            GlobalVariables.cartItemCount = GlobalVariables.cartItemCount + 1;
            return RedirectToAction("ShowCart");
            // return View();
        }

        //[HttpPost]
        public ActionResult CartItemCount(int id, string type)
        {

            var cartItems = sdb.ShoppingCart.Where(user => user.ApplicationUserId == User.Identity.Name && user.Id == id).FirstOrDefault();
            if (type == "add")
                cartItems.Count = cartItems.Count + 1;
            else
            {
                if (cartItems.Count == 1) return RedirectToAction("ShowCart") ;
                cartItems.Count = cartItems.Count - 1;
            }

            sdb.SaveChanges();
            return RedirectToAction("ShowCart");
        }

        //[HttpPost]
        public ActionResult DeleteItem(int id)
        {
            var cartItems = sdb.ShoppingCart.Where(user => user.ApplicationUserId == User.Identity.Name && user.Id == id).FirstOrDefault();
            if (cartItems != null)
            {
                var data = sdb.ShoppingCart.Remove(cartItems);
                sdb.SaveChanges();
                GlobalVariables.cartItemCount = GlobalVariables.cartItemCount - 1;
            }
            return RedirectToAction("ShowCart");
        }
        public ActionResult Summary()
        {
            string userId = User.Identity.Name;
            var currentUser = _db.Users.FirstOrDefault(x => x.Id == userId);
            ViewBag.UserPhoneNumber = currentUser.PhoneNumber;
            ViewBag.Address = currentUser.StreetAddress + ", " + currentUser.City + ", " + currentUser.State + ", " + currentUser.PostalCode;
            var allCartitem = sdb.ShoppingCart.Where(user => user.ApplicationUserId == User.Identity.Name).ToList();
            foreach (var item in allCartitem)
            {
                item.MenuItem = sdb.MenuItem.Where(r => r.Id == item.MenuItemId).FirstOrDefault();
            }
            return View(allCartitem);
        }
        public ActionResult Payment()
        {
            return View();
        }
    }
}