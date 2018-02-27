using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeoPedia.Models;

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
                return RedirectToAction("Index");
            }

            try
            {
                Countries_Cities countries_Cities = new Countries_Cities();
                countries_Cities.City = db.Cities.SingleOrDefault(c => c.Id == id);

                if(countries_Cities.City == null)
                {
                    return RedirectToAction("Index");
                }

                countries_Cities.Country = db.Countries.SingleOrDefault(c => c.Code == countries_Cities.City.Country_Code);

                return View(countries_Cities);
            }
            catch(Exception genericException)
            {
                ViewBag.ExceptionDetails = genericException.Message;
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