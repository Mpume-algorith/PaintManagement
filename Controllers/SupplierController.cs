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
using System.Data.SqlClient;

namespace PaintManagement.Controllers
{
    [Authorize]
    public class SupplierController : Controller
    {
        private PaintContext db = new PaintContext();

        // GET: Supplier
        public ViewResult Index(string sortOrder, string currentFilter ,string searchString,  int? page )
        {
            var Suppliers = db.Suppliers.Include(s => s.ProductOrders);
            #region Searching and sorting method
            //Sorting by the SupplierID and Name
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.SupplierIDSortParm = sortOrder == "SupplierID" ? "supplierID_desc" : "SupplierID";
            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

           if (searchString != null)
            {
                page = 1;
            }
            else
            {
               searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString; 

            var suppliers = from s in db.Suppliers
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                suppliers = suppliers.Where(s => s.Name.ToUpper()
                .Contains(searchString.ToUpper()) || s.Address.Contains(searchString) || s.Email.ToString().Contains(searchString) || s.PhoneNumber.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    suppliers = suppliers.OrderByDescending(s => s.Name);
                    break;
                case "SupplierID":
                    suppliers = suppliers.OrderBy(s => s.SupplierID);
                    break;
                case "supplierID_desc":
                    suppliers = suppliers.OrderByDescending(s => s.SupplierID);
                    break;
              
                default:
                    suppliers = suppliers.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            #endregion
            return View(suppliers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            var suppliers = db.Suppliers.Include(f => f.ProductOrders);
            foreach (Supplier s in suppliers)
            {
                foreach(ProductOrder p in s.ProductOrders)
                {

                }
            }
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SupplierID,Name,Address,Email,PhoneNumber")] Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Suppliers.Add(supplier);
                    db.SaveChanges();
                  return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName ("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var supplierToUpdate = db.Suppliers.Find(id);
            if (TryUpdateModel(supplierToUpdate, "", new string[] { "SupplierID", "Name", "Address", "Email", "PhoneNmber" }))
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
            return View(supplierToUpdate);

        }


        // GET: Supplier/Delete/5
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
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Supplier supplier = db.Suppliers.Find(id);
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
        }

        //Closes database connections
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
