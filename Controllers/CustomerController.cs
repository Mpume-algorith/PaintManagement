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
using PagedList.Mvc;
using Rotativa.MVC;

namespace PaintManagement.Controllers
{
   [Authorize]
    public class CustomerController : Controller
    {
        private PaintContext db = new PaintContext();

        // GET: Customer
        //
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
           var Customers = db.Customers.Include(c => c.Bookings).Include(c => c.Orders);
            foreach (Customer c in Customers)
            {
                Customers.Include(b => b.Orders).Include(b => b.Bookings);

            }
            #region Searching and Sorting Method
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

           // ViewBag.AmountDueSortParm = sortOrder == "Amount Due" ? "Amount_desc" : "Amount Due";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var customers = from c in db.Customers
                            select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.Name.Contains(searchString) || c.PhoneNumber.ToString().Contains(searchString)
                || c.Email.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    customers = customers.OrderByDescending(c => c.Name);
                    break;
              
                default:
                    customers = customers.OrderBy(c => c.Name);
                    break;
              
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            #endregion
            return View(customers.ToPagedList(pageNumber, pageSize));
            
        }
        // report method
        [AllowAnonymous]
        public ActionResult Customers()
        {
            var allCustomers = db.Customers.ToList();
            return View(allCustomers);
        }
        [AllowAnonymous]
        public ActionResult GeneratePDF()
        {
            var c = new ActionAsPdf("Customers");
            return c;
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Customer customer = db.Customers.Find(id);
            var customers = db.Customers.Include(v => v.Bookings).Include(v => v.Orders);
            foreach(Customer c in customers)
            {
                foreach(Booking b in c.Bookings)
                {
                   
                }
                foreach(Order O in c.Orders)
                {

                }
            }
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(/*customer*/);
        }
        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, PhoneNumber, Email, AmountDue ")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                  return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View();
        }

        // GET: Customer/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customerToUpdate = db.Customers.Find(id);
            if (TryUpdateModel(customerToUpdate, "",
               new string[] { "Name, Phone Number, Email" })) 
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(customerToUpdate);
        }

        // GET: Customer/Delete/5
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
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
