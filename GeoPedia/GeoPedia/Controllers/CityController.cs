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

        public PartialViewResult Cities()
        {
            List<City> cities = db.Cities.ToList();
            return PartialView("~/Views/City/_Cities.cshtml", cities);
        }
    }
}