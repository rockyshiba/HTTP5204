using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GeoPedia.Models;
using System.IO;

namespace GeoPedia.Controllers
{
    public class CountriesController : Controller
    {
        private GeoContext db = new GeoContext();

        // GET: Countries
        public ActionResult Index()
        {
            try
            {
                return View(db.Countries.ToList());
            }
            catch(Exception genericException)
            {
                ViewBag.ExceptionMessage = genericException.Message;
            }

            return View("~/Views/Error/Index.cshtml");
        }

        // GET: Countries/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // GET: Countries/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Code,Population,Continent,Name,Established")] Country country, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";

                //If a file has been sent to this action
                if (file.ContentLength > 0)
                {
                    //Gets the filename of the file
                    fileName = Path.GetFileName(file.FileName);

                    //Assigning filename to the model property
                    country.Flag_Img = fileName;
                }

                db.Countries.Add(country);
                db.SaveChanges();

                if (fileName != "")
                {
                    //Find the path in the server to store the images then add in a directory with the custom directory
                    //Locally, this path is different than the eventual path on another server so this follows a path
                    string path = Path.Combine(Server.MapPath("~/Images/Countries/" + country.Code.ToUpper() + "/"));

                    //C# requires you to create the directory. The server responds by creating directories that don't exist on top of the directories that do exist
                    Directory.CreateDirectory(path);

                    path = Path.Combine(Server.MapPath("~/Images/Countries/" + country.Code.ToUpper() + "/"), fileName);
                    file.SaveAs(path);
                }

                return RedirectToAction("Index");
            }

            return View(country);
        }

        // GET: Countries/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }

            if (!string.IsNullOrEmpty(country.Flag_Img))
            {
                //store the previous value of the country's flag image filename in case the user replaces this file with another.
                TempData["CountryFlagImage"] = country.Flag_Img;
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Code,Population,Continent,Name,Established")] Country country, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //replacing an image
                if(file.ContentLength > 0)
                {
                    country.Flag_Img = Path.GetFileName(file.FileName);
                    //Create the path if it didn't exist previously
                    string path = Path.Combine(Server.MapPath("~/Images/Countries/" + country.Code.ToUpper() + "/"));
                    Directory.CreateDirectory(path);

                    //Check if old file exists
                    //Or check if files exist already; count the number of files in a directory using C#

                    //Delete the old file
                    /*
                    string pathToRemoveFile = Request.MapPath("~/Images/Countries/" + country.Code.ToUpper() + "/" + TempData["CountryFlagImage"]);
                    System.IO.File.Delete(pathToRemoveFile); 
                    */

                    //Alternatively delete all files in the directory
                    DirectoryInfo dirInfo = new DirectoryInfo(Request.MapPath("~/Images/Countries/" + country.Code.ToUpper()));
                    foreach(FileInfo fi in dirInfo.GetFiles())
                    {
                        fi.Delete();
                    }                     

                    //Upload the new file
                    string pathToUpload = Path.Combine(Server.MapPath("~/Images/Countries/" + country.Code.ToUpper() + "/" + country.Flag_Img));
                    file.SaveAs(pathToUpload);
                }

                db.Entry(country).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Countries/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = db.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            Country country = db.Countries.Find(id);
            db.Countries.Remove(country);

            //Delete all files in the directory
            string pathToRemoveFiles = Request.MapPath("~/Images/Countries/" + country.Code.ToUpper());
            System.IO.DirectoryInfo DirInfo = new DirectoryInfo(pathToRemoveFiles);

            foreach(FileInfo file in DirInfo.GetFiles())
            {
                file.Delete();
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// This action will remove the flag image file from the server directory of a country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteFlag(string id)
        {
            //Remove the file column value from the database

            //Get the country object with all its values
            Country country = db.Countries.Find(id);

            //Remove the flag_img value
            country.Flag_Img = "";

            //Update the country as you normally would from the Edit action
            db.Entry(country).State = EntityState.Modified;
            db.SaveChanges();

            //Remove the flag image from the directory
            string pathToRemoveFiles = Request.MapPath("~/Images/Countries/" + country.Code.ToUpper());
            System.IO.DirectoryInfo DirInfo = new DirectoryInfo(pathToRemoveFiles);

            foreach(FileInfo file in DirInfo.GetFiles())
            {
                file.Delete();
            }

            return RedirectToAction("Details", new { id = country.Code});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Custom code
        public JsonResult IsCodeAvailable(string code)
        {
            return Json(!db.Countries.Any(c => c.Code.ToUpper() == code.ToUpper()), JsonRequestBehavior.AllowGet);
        }
    }
}
