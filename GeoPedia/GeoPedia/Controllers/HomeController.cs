using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoPedia.Models;
using System.Web.Security;

namespace GeoPedia.Controllers
{
    public class HomeController : Controller
    {
        GeoContext db = new GeoContext();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Site_Users user)
        {
            //Return the number of rows returned from the database (should be 1)
            int count = db.Site_Users.Where(
                u => u.Username == user.Username
                &&
                u.Password == user.Password).Count(); 

            if(count == 1)
            {
                //set the authcookie with your username or any other value. This username is also being used to determine your user role.
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Invalid username and/or password";
            return View(user);
        }

        public ActionResult Logout()
        {
            //unset the authcookie
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize(Roles = "Admin")]
        public string Restricted()
        {
            //User is a predefined object containing the string you provided at login to SetAuthCookie. 
            return "Hello " + User.Identity.Name;
        }
    }
}