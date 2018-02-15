using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoPedia.Models; //Include the models directory

namespace GeoPedia.Controllers
{
    public class HomeController : Controller
    {
        GeoContext db = new GeoContext(); //Set up an instance of the context class to be used in actions

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Countries()
        {
            //pass a list of countries from the countries table using the GeoContext instance, db. 
            return View(db.Countries.ToList()); 
        }
    }
}