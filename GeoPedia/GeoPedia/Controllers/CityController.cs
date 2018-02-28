using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoPedia.Models;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace GeoPedia.Controllers
{
    public class CityController : Controller
    {
        GeoContext db = new GeoContext();

        // GET: City
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if(id == 0)
            {
                //Go back to the index action if no id has been provided (ints cannot be null in C# so the default is 0)
                return RedirectToAction("Index");
            }

            try
            {
                Countries_Cities countries_Cities = new Countries_Cities();
                countries_Cities.City = db.Cities.SingleOrDefault(c => c.Id == id);

                if (countries_Cities.City == null)
                {
                    return RedirectToAction("Index");
                }

                countries_Cities.Country = db.Countries.SingleOrDefault(c => c.Code == countries_Cities.City.Country_Code);

                return View(countries_Cities);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionDetails = genericException.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }

        [HttpGet] //You can annotate your actions to specify if this action is a GET or a POST request
        public ActionResult Add()
        {
            try
            {
                ViewBag.Countries = db.Countries.ToList(); //this viewbag will populate a dropdownlist from the database
                return View();
            }
            catch(Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(City city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Cities.Add(city);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                //Although not the direct answer, the inspiration for this solution is from https://forums.asp.net/t/2135677.aspx?System+ArgumentNullException+Value+cannot+be+null+ 
                ViewBag.Countries = db.Countries.ToList(); //CAREFUL. This took hours to figure out. If this line is reached, it only makes sense that the dropdownlist needs to be repopulated so ViewBag.Countries needs to be set again here to then be passed into the view. 
                                                           //This return statement will prepopulate the form with the model in the case that the above code was unsuccessful.
                return View(city);
            }
            catch(DbUpdateException DbException) //DbUpdateException catches exceptions during database manipulation errors
            {
                ViewBag.DbExceptionMessage = DbException.Message;
            }
            catch(Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException;
            }

            return View("~/Views/Errors/Details.cshtml");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            try
            {
                if(id == 0)
                {
                    return RedirectToAction("Index");
                }

                City city = db.Cities.SingleOrDefault(c => c.Id == id);

                if(city == null)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.Countries = db.Countries.ToList();
                return View(city);
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(City city)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(city).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Countries = db.Countries.ToList();
                return View(city);
            }
            catch (DbUpdateException dbException) //DbUpdateException catches exceptions during database manipulation errors. You use this in actions that will perform DML on your database. 
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(dbException);
            }
            catch(SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if(id == 0)
            {
                return RedirectToAction("Index");
            }

            try
            {
                City city = db.Cities.SingleOrDefault(c => c.Id == id);

                if(city == null)
                {
                    return RedirectToAction("Index");
                }

                return View(city);
            }
            catch(SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch(Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormCollection form) //FormCollection allows access to form values by Name attributes
        {
            try
            {
                int id = Convert.ToInt32(form["Id"]); //This is getting from an input inside <form> with Name="Id"
                City city = db.Cities.Find(id);
                db.Cities.Remove(city);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbException)
            {
                ViewBag.DbExceptionMessage = ErrorHandler.DbUpdateHandler(dbException);
            }
            catch (SqlException sqlException)
            {
                ViewBag.SqlExceptionMessage = sqlException.Message;
            }
            catch (Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return View("~/Views/Errors/Details.cshtml");
        }

        public PartialViewResult Cities()
        {
            try
            {
                List<City> cities = db.Cities.ToList();
                return PartialView("~/Views/City/_Cities.cshtml", cities);
            }
            catch(Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            //A try/catch on a PartialView action behaves differently than a normal action. Because we are not returning a view, you'd have to return a partial view while the rest of the parent view renders. 
            return PartialView("~/Views/Errors/_Partial_Error.cshtml");
        }
    }
}