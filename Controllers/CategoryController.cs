using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
//using SpiceWithoutAuthentication.Migrations;
using SpiceWithoutAuthentication.Models;
using System.Web.Mvc;

namespace Spice.Areas.Admin.Controllers
{

    public class CategoryController : Controller
    {

        SpiceDbContext _db = new SpiceDbContext();
        //public CategoryController()
        //{
            
        //}


        //GET 
        public ActionResult Index()
        {
            return View(_db.Category.ToList());
        }

        //GET - CREATE
        public ActionResult Create()
        {
            return View();
        }


        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                //if valid
                _db.Category.Add(category);
                _db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(category);
        }

        //

    }
}





      