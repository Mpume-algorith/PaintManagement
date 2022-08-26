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
using System.Linq.Expressions;

namespace PaintManagement.Controllers
{
   [Authorize]
    public class BookingController : Controller
    {
        private PaintContext db = new PaintContext();

        // GET: Booking
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
             var Bookings = db.Bookings.Include(b => b.Customer).Include(b => b.Workshop);
            foreach(Booking b in Bookings)
            {
                Bookings.Include(c => c.Customer);

            }
            #region Searching and ordering method
            ViewBag.CurrentSort = sortOrder;
         
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var bookings = from b in db.Bookings
                           select b;
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b => b.Date.ToString().Contains(searchString) || b.Customer.Name.Contains(searchString)
                || b.Workshop.Description.Contains(searchString) || b.Time.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                //case "cost_desc":
                //    bookings = bookings.OrderByDescending(b => b.Cost);
                //    break;
                case "Date":
                    bookings = bookings.OrderBy(b => b.Date);
                    break;
                case "date_desc":
                    bookings = bookings.OrderByDescending(b => b.Date);
                    break;
                default:
                    bookings = bookings.OrderBy(b => b.BookingID);
                    break;
            }
            
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            #endregion
            return View(bookings.ToPagedList(pageNumber, pageSize));

            
        }

        // GET: Booking/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            var bookings = db.Bookings.Include(i => i.Customer);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            ViewBag.WorkshopID = new SelectList(db.Workshops, "WorkshopID", "Place");
            return View();
        }

        // POST: Booking/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,WorkshopID,Date,Time,Cost")] Booking booking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Bookings.Add(booking);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException){
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", booking.CustomerID);
            ViewBag.WorkshopID = new SelectList(db.Workshops, "WorkshopID", "Place", booking.WorkshopID);
            return View(booking);
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", booking.CustomerID);
            ViewBag.WorkshopID = new SelectList(db.Workshops, "WorkshopID", "Place", booking.WorkshopID);
            return View(booking);
        }

        // POST: Booking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingID,CustomerID,WorkshopID,Date,Time,Cost")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", booking.CustomerID);
            ViewBag.WorkshopID = new SelectList(db.Workshops, "WorkshopID", "Place", booking.WorkshopID);
            return View(booking);
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
