using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fypProjectWebApp.Models;

namespace fypProjectWebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult AddOrEdit(int i = 0)
        {
            User mod = new User();
            return View();
        }

        [HttpPost]
        public ActionResult AddOrEdit(User mod)
        {
            using (TestDbModel db = new TestDbModel())
            {
                if (db.Users.Any(x => x.idNumber == mod.idNumber))
                {
                    ViewBag.DuplicateMessage = "Student ID already exists. Please try again";
                    return View("AddOrEdit", mod);
                }
                db.Users.Add(mod);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("AddOrEdit", new User());
        }
    }
}