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

namespace PaintManagement.Controllers
{
    public class PaintOrderController : Controller
    {
        private PaintContext db = new PaintContext();

        // GET: PaintOrder
        public ActionResult Index()
        {
            var paintOrders = db.PaintOrders.Include(p => p.Order).Include(p => p.Paint);
            return View(paintOrders.ToList());
        }

        // GET: PaintOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaintOrder paintOrder = db.PaintOrders.Find(id);
            if (paintOrder == null)
            {
                return HttpNotFound();
            }
            return View(paintOrder);
        }

        // GET: PaintOrder/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID");
            ViewBag.PaintID = new SelectList(db.Paints, "PaintID", "Name");
            return View();
        }

        // POST: PaintOrder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaintOrderID,OrderID,PaintID,Quantity")] PaintOrder paintOrder)
        {
            if (ModelState.IsValid)
            {
                db.PaintOrders.Add(paintOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", paintOrder.OrderID);
            ViewBag.PaintID = new SelectList(db.Paints, "PaintID", "Name", paintOrder.PaintID);
            return View(paintOrder);
        }

        // GET: PaintOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaintOrder paintOrder = db.PaintOrders.Find(id);
            if (paintOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", paintOrder.OrderID);
            ViewBag.PaintID = new SelectList(db.Paints, "PaintID", "Name", paintOrder.PaintID);
            return View(paintOrder);
        }

        // POST: PaintOrder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaintOrderID,OrderID,PaintID,Quantity")] PaintOrder paintOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paintOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", paintOrder.OrderID);
            ViewBag.PaintID = new SelectList(db.Paints, "PaintID", "Name", paintOrder.PaintID);
            return View(paintOrder);
        }

        // GET: PaintOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaintOrder paintOrder = db.PaintOrders.Find(id);
            if (paintOrder == null)
            {
                return HttpNotFound();
            }
            return View(paintOrder);
        }

        // POST: PaintOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaintOrder paintOrder = db.PaintOrders.Find(id);
            db.PaintOrders.Remove(paintOrder);
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
