using SpiceWithoutAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpiceWithoutAuthentication.Migrations;
using SpiceWithoutAuthentication.Models.ViewModels;
using System.IO;
using SpiceWithoutAuthentication.Global;

namespace SpiceWithoutAuthentication.Controllers
{
    public class MenuController : Controller
    {
        //public MenuItemViewModel MenuItemVM { get; set; }
        SpiceDbContext db;
        public MenuController()
        {
            db = new SpiceDbContext();
        }
        public ActionResult Index()
        {
            GlobalVariables.cartItemCount = 0;
            if (User.Identity.IsAuthenticated)
                GlobalVariables.cartItemCount = db.ShoppingCart.Where(user => user.ApplicationUserId == User.Identity.Name).ToList().Count;
            //Category = db.Category;
            var menuItems = db.MenuItem.Include("Category").Include("SubCategory").ToList();
            return View(menuItems);
            //return View();
        }
        public ActionResult CategoryShow(int catid)
        {

            //Category = db.Category;
            var menuItems = db.MenuItem.Where(r => r.CategoryId == catid).ToList();
            return View(menuItems);
            //return View();
        }
        public ActionResult Details(int id)
        {
            var menuItems = db.MenuItem.Where(r => r.Id == id).ToList();

            return View(menuItems);
        }
        public ActionResult Spicy(string sname)
        {
            var menuItems = db.MenuItem.Where(r => r.Spicyness == sname).ToList();
            ViewBag.sname = sname;
            return View(menuItems);
        }
        public ActionResult Create()
        {
            ViewBag.Category = db.Category.ToList();
            ViewBag.SubCategory = db.SubCategory.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(MenuItem menuItem)
        {

            if (Request.Files.Count >= 1)
            {
                var photo = Request.Files[0];
                var imgBytes = new Byte[photo.ContentLength - 1];
                photo.InputStream.Read(imgBytes, 0, photo.ContentLength - 1);
                var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                menuItem.Image = base64String;
            }
            db.MenuItem.Add(menuItem);
            db.SaveChanges();

            //



            //



            return RedirectToAction("Index");
        }
    }
}