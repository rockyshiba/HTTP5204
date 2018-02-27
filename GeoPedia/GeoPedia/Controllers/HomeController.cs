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
    }
}