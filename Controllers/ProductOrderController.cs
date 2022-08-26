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

namespace PaintManagement.Controllers
{
   [Authorize]
    public class ProductOrderController : Controller
    {
        private PaintContext db = new PaintContext();

        // GET: ProductOrder
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var productOrders = db.ProductOrders.Include(p => p.Manager).Include(p => p.Supplier).Include(p => p.Paints);
            //foreach(ProductOrder p in productOrders)
            //{
            //    productOrders.Include(g => g.Supplier).Include(g => g.Manager);
            //}
            #region Searching and sorting method
            ViewBag.CurrentSort = sortOrder;
            ViewBag.AmountDueSortParm = sortOrder == "Amount Due" ? "Amount Due_desc" : "Amount Due";
            ViewBag.DateSortParm = sortOrder == "Date Ordered"? "date_desc" : "Date Ordered";
         
           var productorders = from o in db.ProductOrders select o;
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
                productorders = productorders.Where(p => p.Supplier.Name.Contains(searchString) || p.Date.ToString().Contains(searchString) 
                || p.Quantity.ToString().Contains(searchString) || p.Date.ToString().Contains(searchString) || p.Manager.Name.Contains(searchString) || p.AmountDue.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Amount Due_desc":
                    productorders = productorders.OrderByDescending(o => o.AmountDue);
                    break;
                case "Date Ordered":
                    productorders = productorders.OrderBy(o => o.Date);
                    break;
                case "date_desc":
                    productorders = productorders.OrderByDescending(o => o.Date);
                    break;
                default:// Amount due ascending
                    productorders = productorders.OrderBy(o => o.AmountDue);
                    break;
          
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            #endregion
            return View(productorders.ToPagedList(pageNumber, pageSize));
           
        }

        // GET: ProductOrder/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrders.Find(id);
            var productOrders = db.ProductOrders.Include(p => p.Paints);
            foreach(ProductOrder p in productOrders)
            {
                foreach(Paint a in p.Paints)
                {

                }
            }
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            return View(productOrder);
        }

        // GET: ProductOrder/Create
        public ActionResult Create()
        {
            ViewBag.ManagerID = new SelectList(db.Managers, "ManagerID", "Name");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name");
            return View();
        }

        // POST: ProductOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Quantity,Date,AmountDue,SupplierID,ManagerID")] ProductOrder productOrder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ProductOrders.Add(productOrder);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.ManagerID = new SelectList(db.Managers, "ManagerID", "Name", productOrder.ManagerID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name", productOrder.SupplierID);
            return View(productOrder);
        }
           

        // GET: ProductOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrders.Find(id);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagerID = new SelectList(db.Managers, "ManagerID", "Name", productOrder.ManagerID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name", productOrder.SupplierID);
            return View(productOrder);
        }

        // POST: ProductOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productOrderToUpdate = db.ProductOrders.Find(id);
            if(TryUpdateModel(productOrderToUpdate, "", new string[] { "Quantity", "Date", "Product", "AmountDue", }))
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
           
            return View(productOrderToUpdate);

            
        }

        // GET: ProductOrder/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            ProductOrder productOrder = db.ProductOrders.Find(id);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            return View(productOrder);
        }

        // POST: ProductOrder/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                ProductOrder productOrder = db.ProductOrders.Find(id);
                db.ProductOrders.Remove(productOrder);
                db.SaveChanges();
            }
            catch (DataException)
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
