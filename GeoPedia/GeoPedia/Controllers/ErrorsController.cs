using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoPedia.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Code(int? id)
        {
            //By default, if you want to use a number parameter you have to use "id" as the variable name
            //If you want to use an optional paramter, you include the "?" beside the datatype. 

            switch (id)
            {
                //[domain]/errors/code/404
                case 404:
                    return View("~/Views/Errors/Missing.cshtml");
                default:
                    return View("~/Views/Errors/Details.cshtml");
            }
            
        }
    }
}