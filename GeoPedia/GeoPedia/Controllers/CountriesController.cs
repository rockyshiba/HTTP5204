using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoPedia.Models;

namespace GeoPedia.Controllers
{
    public class CountriesController : Controller
    {
        GeoContext db = new GeoContext();
        // GET: Countries
        public ActionResult Index()
        {
            List<Country> countries = db.Countries.ToList();
            return View(countries);
        }

        public ActionResult Details()
        {
            string country_code = "CAN";
            Country_Cities country_cities = new Country_Cities();

            //This will retrieve a single row based on a boolean condition
            country_cities.Ctry = db.Countries.Single(m => m.Code == country_code);
            country_cities.Cities = db.Cities.Where(m => m.Country_Code == country_code).ToList();

            return View(country_cities);
        }
    }
}