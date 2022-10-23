using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MidPractice.Models;
namespace MidPractice.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginAuthorization(MidPractice.Models.User user)
        {
            using (EcomEntities1 ecm = new EcomEntities1())

            {

                var ud = ecm.Users.Where(x => x.Username==user.Username && x.Password==user.Password).FirstOrDefault();

                if (ud != null)
                {
                   
                    return RedirectToAction("Create", "Products");
                   

                }
                else
                {
                    ViewBag.loginError = "Login Success.";
                    Session["UserId"] = ud.UserId;
                    return View("Login", user);
                }
            }
               
        }



    }

}