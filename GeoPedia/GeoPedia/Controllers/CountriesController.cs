using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoPedia.Models;
using System.Data.SqlClient;

namespace GeoPedia.Controllers
{
    public class CountriesController : Controller
    {
        GeoContext db = new GeoContext();
        // GET: Countries
        public ActionResult Index()
        {
            try
            {
                List<Country> countries = db.Countries.ToList();
                return View(countries);
            }
            catch(Exception exception)
            {
                ViewBag.ExceptionMessage = exception.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
            
        }

        public ActionResult Details(string code)
        {
            //.NET MVC makes it easy to capture querystrings. Simply provide the name of the querystring variable as a parameter for the action. Note that "id" is a reserved parameter by default that expects int. 
            try
            {
                if (code == null)
                {
                    return RedirectToAction("Index");
                }

                Countries_Cities country_cities = new Countries_Cities();

                //This will retrieve a single row based on a boolean condition
                //SingleOrDefault allows null results. We can use this to detect for empty result sets.
                country_cities.Country = db.Countries.SingleOrDefault(m => m.Code == code);

                //This condition checks for an empty result set from the database. Here, it is checking if there are no countries found based on the above condition.
                if (country_cities.Country == null)
                {
                    return RedirectToAction("Index");
                }

                country_cities.Cities = db.Cities.Where(m => m.Country_Code == code).ToList();

                return View(country_cities);
            }
            catch(Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
                
            }

            return View("~/Views/Errors/Details.cshtml");
        }
    }
}