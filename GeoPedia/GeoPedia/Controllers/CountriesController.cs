using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoPedia.Models;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

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

        /// <summary>
        /// A GET action that displays a single country based on the code parameter.
        /// </summary>
        /// <param name="code">A querystring variable named "code"</param>
        /// <returns>A View typed with a country object or an error page</returns>
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

        /// <summary>
        /// GET action that displays an empty form to add a country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Country country)
        {

            if (ModelState.IsValid) //This validation is required for ValidateFor message to appear
            {
                db.Countries.Add(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country);
        }

        [HttpGet]
        public ActionResult Edit(string code) //looking for a querystring variable ?code={value}
        {
            if (string.IsNullOrWhiteSpace(code)) //if the code variable isn't in the querystring or there is no value assigned, go back to the index action
            {
                return RedirectToAction("Index");
            }

            try
            {
                Country country = db.Countries.SingleOrDefault(c => c.Code == code); //Find a row in the Db using a country code, assign it to a country object
                
                if(country != null) //if the country object is not null (it was found in the database) then populate the Edit view with the details of the country object
                {
                    return View(country);
                }

                return RedirectToAction("Index"); //This line is reached because the country object was null (it was not found in the database)
            }
            catch(Exception genericException) //Exception handling in case something goes wrong with our database request
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return View("~/Views/Errors/Details.cshtml"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country country)
        {
            //REMINDER: you can't do here what you can't do in your database. That means you cannot edit the primary key of an entity that has dependent entities.
            //.NET MVC disables the editing of primary keys by default.

            try
            {
                if (ModelState.IsValid) //The input from the user is valid according to the model definition, in this case Country
                {
                    db.Entry(country).State = System.Data.Entity.EntityState.Modified; //Update the database based on the model provided.
                    db.SaveChanges(); //Save changes to the database
                    return RedirectToAction("Details", new { code = country.Code }); //Return to the details action with using the countrie's code. 
                }

                return View(country);
            }
            catch(DbUpdateException DbException)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(DbException);
            }
            catch(SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch(Exception exception)
            {
                ViewBag.genericException = exception.Message;
            }

            return View("~/Errors/Details.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return RedirectToAction("Index");
            }

            try
            {
                Country country = db.Countries.Find(code);
                return View(country);
            }
            catch(DbUpdateException DbException)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(DbException);
            }
            catch(Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return RedirectToAction("Index", "Errors");
        }


        /// <summary>
        /// DeleteThis will handle the actual deletion of a row. Recall that method overloading allows methods of the same name as along as the parameters are different. 
        /// 
        /// Because the deletion is based on the primary key, the POST action of Delete cannot take the same parameters so another action must be named differently. 
        /// </summary>
        /// <param name="code">A 3 character code for a country</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteThis(string code)
        {
            try
            {
                Country country = db.Countries.Find(code);
                db.Countries.Remove(country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(DbUpdateException DbException)
            {
                //ViewBag only works when returning a view, not redirecting to actions. TempData can persist across Actions
                TempData["DbExceptionMessage"] = "Cannot delete: " + ErrorHandler.DbUpdateHandler(DbException);
            }
            catch(SqlException sqlException)
            {
                TempData["SqlExceptionMessage"] = sqlException.Message;
            }
            catch(Exception genericException)
            {
                TempData["ExceptionMessage"] = genericException.Message;
            }

            return RedirectToAction("Delete", new { code = code});
        }

        /// <summary>
        /// Takes in a code string and checks the database if that code is available.
        /// </summary>
        /// <param name="code">3 Character code for country</param>
        /// <returns>Json result of database results</returns>
        public JsonResult IsCodeAvailable(string code)
        {
            //If a country is found, then false. Notice the ! in front of db
            //If a country isn't found, then true
            return Json(!db.Countries.Any(c => c.Code.ToUpper() == code.ToUpper()), JsonRequestBehavior.AllowGet);
        }
    }
}