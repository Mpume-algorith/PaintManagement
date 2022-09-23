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
    public class PaintController : Controller
    {
        private PaintContext db = new PaintContext();

        // GET: Paint
        public ActionResult Index()
        {
            return View(db.Paints.ToList());
        }

        // GET: Paint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paint paint = db.Paints.Find(id);
            if (paint == null)
            {
                return HttpNotFound();
            }
            return View(paint);
        }

        // GET: Paint/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paint/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaintID,Name,CostPrice,SalePrice,Size,ImagePath,Contents,Image")] Paint paint)
        {
            if (ModelState.IsValid)
            {
                db.Paints.Add(paint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paint);
        }

        // GET: Paint/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paint paint = db.Paints.Find(id);
            if (paint == null)
            {
                return HttpNotFound();
            }
            return View(paint);
        }

        // POST: Paint/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaintID,Name,CostPrice,SalePrice,Size,ImagePath,Contents,Image")] Paint paint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paint);
        }

        // GET: Paint/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paint paint = db.Paints.Find(id);
            if (paint == null)
            {
                return HttpNotFound();
            }
            return View(paint);
        }

        // POST: Paint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paint paint = db.Paints.Find(id);
            db.Paints.Remove(paint);
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
