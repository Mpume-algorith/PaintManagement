using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PaintManagement.DAL;
using PaintManagement.Models;
using PagedList;
using Microsoft.CodeAnalysis.CSharp;

namespace PaintManagement.Controllers
{
    [Authorize]
    public class WorkshopController : Controller
    {
        private PaintContext db = new PaintContext();
        
          // GET: Workshop
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
        
            var Workshops = db.Workshops.Include(w => w.Bookings);

            #region Searching and sorting method
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PlaceSortParm = sortOrder == "Place" ? "Place_desc" : "Place";
           
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var workshops = from w in db.Workshops
                            select w;
            if (!String.IsNullOrEmpty(searchString))
            {
                workshops = workshops.Where(w => w.Place.Contains(searchString) || w.Time.ToString().Contains(searchString) || w.Description.Contains(searchString) || w.Date.ToString().Contains(searchString));
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    workshops = workshops.OrderByDescending(s => s.Date);
                    break;
                case "Place":
                    workshops = workshops.OrderBy(w => w.Place);
                    break;
                case "Place_desc":
                    workshops = workshops.OrderByDescending(w => w.Place);
                    break;
                default:
                    workshops = workshops.OrderBy(s => s.Date);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            #endregion
            return View(workshops.ToPagedList(pageNumber, pageSize));
        } 

        // GET: Workshop/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workshop workshop = db.Workshops.Find(id);

            var workshops = db.Workshops.Include(t => t.Bookings);
            foreach( Workshop w in workshops)
            {
                
                foreach(Booking b in w.Bookings)
                {

                  

                }
                
            }

            if (workshop == null)
            {
                return HttpNotFound();
            }
            return View(workshop);
          
        }

        // GET: Workshop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workshop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkshopID,Description,Date,Time,Place")] Workshop workshop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Workshops.Add(workshop);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(workshop);
        }

        // GET: Workshop/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workshop workshop = db.Workshops.Find(id);
            if (workshop == null)
            {
                return HttpNotFound();
            }
            return View(workshop);
        }

        // POST: Workshop/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var workshopToUpdate = db.Workshops.Find(id);
            if (TryUpdateModel(workshopToUpdate, "", new string[] { "WorkshopID" ,"Description", "Date" ,"Time" , "Place" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(workshopToUpdate);

        }

        // GET: Workshop/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Workshop workshop = db.Workshops.Find(id);
            if (workshop == null)
            {
                return HttpNotFound();
            }
            return View(workshop);
        }

        // POST: Workshop/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Workshop workshop = db.Workshops.Find(id);
                db.Workshops.Remove(workshop);
                db.SaveChanges();
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }
        //To close the database connections
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
