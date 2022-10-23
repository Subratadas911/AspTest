using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MidPractice.Models;
namespace MidPractice.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public ActionResult AddOrEdit(User user)
        {
            using (EcomEntities1 ecm = new EcomEntities1())
            {
                if (ecm.Users.Any(x => x.Username == user.Username))
                {

                    ViewBag.duplicateMessage = "Username already exists.";
                    return View("AddOrEdit", user);
                }

                ecm.Users.Add(user);
                ecm.SaveChanges();

            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful.";
            return View("AddOrEdit", new User());
        }
    }
}