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
            //Hash the incoming password with SHA256. The hash of what is stored in the database will be compared to the hash of what a login attempt will use as a password
            user.Password = Encryptor.Sha256String(user.Password);

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

        public PartialViewResult Countries_Cities_Search(FormCollection form)
        {
            string search_term = form["term"];
            List<Country> Countries = db.Countries.Where(ctry => ctry.Name.ToUpper().Contains(search_term.ToUpper())).ToList();
            List<City> Cities = db.Cities.Where(cts => cts.Name.ToUpper().Contains(search_term.ToUpper())).ToList();

            VM_Countries_Cities Countries_Cities = new VM_Countries_Cities()
            {
                Countries = Countries,
                Cities = Cities
            };

            return PartialView("~/Views/Home/_Countries_Cities_Results.cshtml", Countries_Cities);
        }
    }
}