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
    public class OrderController : Controller
    {
        private PaintContext db = new PaintContext();

        // GET: Order
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            var Orders = db.Orders.Include(o => o.Customer).Include(o => o.Paints);
            foreach (Order b in Orders)
            {
                Orders.Include(c => c.Customer);

            }
            #region Searching and sorting method
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = sortOrder == "Date"? "date_desc" : "Date";
            ViewBag.AmountSortParm = sortOrder == "Amount" ? "Amount_desc" : "Amount";
            ViewBag.QunatitySortParm = sortOrder == "Quantity" ? "Quantity_desc" : "Quantity";

            if (searchString != null)
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
                Orders = Orders.Where(o => o.Date.ToString().Contains(searchString) || o.Customer.Name.Contains(searchString)
                || o.Amount.ToString().Contains(searchString) || o.Quantity.ToString().Contains(searchString));
            }
            var orders = from o in db.Orders
                            select o;
           
            switch (sortOrder)
            {
                case "date_desc":
                    orders = orders.OrderByDescending(o => o.Date);
                    break;
                case "Quantity":
                    orders = orders.OrderBy(o => o.Quantity);
                    break;
                case "Quantity_desc":
                    orders = orders.OrderByDescending(o => o.Quantity);
                    break;
                case "Amount":
                    orders = orders.OrderBy(o => o.Amount);
                    break;
                case "Amount_desc":
                    orders = orders.OrderByDescending(o => o.Amount);
                    break;
                default:
                    orders = orders.OrderBy(s => s.Date);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            #endregion
            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            var orders = db.Orders.Include(o => o.Paints);
            foreach (Order d in orders)
            {
                foreach (Paint b in d.Paints)
                {

                }
                
            }
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,Amount,Date,CustomerID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
               return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", order.CustomerID);
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            var orders = db.Orders.Include(o => o.Paints);
            foreach(Order o in orders)
            {
                foreach(Paint p in o.Paints)
                {

                }
            }
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", order.CustomerID);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,Amount,Date,CustomerID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", order.CustomerID);
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
