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
            //A view model being passed into a view
            Countries_Cities countries_cities = new Countries_Cities();
            countries_cities.Countries = db.Countries.ToList();
            countries_cities.Cities = db.Cities.ToList();
            return View(countries_cities);
        }

        public ActionResult Countries()
        {
            //pass a list of countries from the countries table using the GeoContext instance, db. 
            return View(db.Countries.ToList()); 
        }
    }
}