using SpiceWithoutAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpiceWithoutAuthentication.Controllers
{
    public class SubCategoryController : Controller
    {
        SpiceDbContext _db = new SpiceDbContext();
        // GET: SubCategory
        public ActionResult Index()
        {
            var allCat = _db.SubCategory.ToList();
            return View(allCat);
        }
        public ActionResult Create()
        {
            ViewBag.Category = _db.Category.ToList();
            return View();
        }


        //POST - CREATE
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {
                //if valid
                _db.SubCategory.Add(subcategory);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(subcategory);
        }

        ////
    }
}