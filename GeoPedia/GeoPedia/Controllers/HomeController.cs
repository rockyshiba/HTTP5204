using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoPedia.Models; //Include the models directory
using System.Data.SqlClient;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;

namespace GeoPedia.Controllers
{
    public class HomeController : Controller
    {
        GeoContext db = new GeoContext(); //Set up an instance of the context class to be used in actions

        // GET: Home
        public ActionResult Index()
        {
            try
            {
                //A view model being passed into a view
                Countries_Cities countries_cities = new Countries_Cities();
                countries_cities.Countries = db.Countries.ToList();
                countries_cities.Cities = db.Cities.ToList();

                //Everything is running as expected so the Action goes to this return line.
                return View(countries_cities);
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = dbException.Message;
            }
            catch (EntityException entityException)
            {
                //InnerException.Message gives a more detailed message, a message that I wouldn't show on a webpage
                ViewBag.EntityExceptionMessage = entityException.InnerException.Message;

                //This one also gives a message but very vague. 
                //ViewBag.EntityExceptionMessage = entityException.Message;
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionNumber = sqlException.Number;
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch(Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            //If everything goes well, then the return line below won't get reached. 
            return View("~/Views/Errors/Details.cshtml");
        }

        public ActionResult Countries()
        {
            try
            {
                //pass a list of countries from the countries table using the GeoContext instance, db. 
                return View(db.Countries.ToList());
            }
            catch(Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }

        /// <summary>
        /// This action handles the search bar functionality from the view.
        /// </summary>
        /// <param name="form">The form from the view</param>
        /// <returns>A partial view populated by a list of countries and/or cities</returns>
        public PartialViewResult Countries_Cities_Search(FormCollection form)
        {
            string search_term = form["term"];
            Countries_Cities countries_Cities = new Countries_Cities();

            if (!String.IsNullOrWhiteSpace(search_term))
            {
                try
                {
                    //The method looks long, but this is the equivalent of using the WHERE and LIKE in an SQL query
                    //Where() expects a lamba expression and Contains() accepts a string that acts like LIKE in SQL
                    countries_Cities.Countries = db.Countries.Where(c => c.Name.ToUpper().Contains(search_term.ToUpper())).ToList();
                    countries_Cities.Cities = db.Cities.Where(c => c.Name.ToUpper().Contains(search_term.ToUpper())).ToList();
                }
                catch (Exception genericException)
                {
                    ViewBag.ExceptionMessage = genericException.Message;
                    return PartialView("~/Views/Errors/_Partial_Error.cshtml");
                }
            }

            return PartialView("~/Views/Home/_Search_Results.cshtml", countries_Cities); //careful, the string argument for the path of the view is a big source of error. 
        }

        /// <summary>
        /// Will attempt to retrieve a row from the City table and pass that result to the view. 
        /// </summary>
        /// <param name="form">A value from Name = 'country_name' in the view</param>
        /// <returns>A partial view typed to a single city result</returns>
        [HttpPost]
        public PartialViewResult Countries_DDL(FormCollection form)
        {
            //Set up country instance
            Country country = new Country();

            //store the result from the form with Name="Country"
            string country_code = form["Country"];

            //If country_code is not null
            if (!string.IsNullOrWhiteSpace(country_code))
            {
                try
                {
                    //country = db.Countries.Find(country_code);
                    //Try assigning country to the result where the Code == country_code. If there is no result, then country will remain empty. 
                    country = db.Countries.SingleOrDefault(c => c.Code == country_code);
                }
                catch (Exception genericException)
                {
                    //Catch possible errors from interacting with the database
                    ViewBag.ExceptionMessage = genericException.Message;
                    return PartialView("~/Views/Errors/_Partial_Error.cshtml");
                }
            }
            //Render the PartialView with the country object. 
            return PartialView("~/Views/Countries/_Country.cshtml", country);
        }

        public PartialViewResult City_DDL(FormCollection form)
        {
            //Set up city instance
            City city = new City();

            //Store the POST result from the form with Name = Id
            string city_id = form["City"];
            int id;

            //If city_id is a number
            if(int.TryParse(city_id, out id))
            {
                try
                {
                    //Try assigning city to the result where Id == id. If there is no result, then city remains emtpy. 
                    city = db.Cities.SingleOrDefault(c => c.Id == id);
                }
                catch(Exception genericException)
                {
                    ViewBag.ExceptionMessage = genericException.Message;
                    return PartialView("~/Views/Errors/_Partial_Error.cshtml");
                }
            }

            return PartialView("~/Views/City/_City.cshtml", city);
        }
    }
}