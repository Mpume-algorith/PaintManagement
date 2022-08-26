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
using System.Web.Services.Description;
using PaintManagement.Migrations;
using Rotativa.MVC;
using PaintManagement.ViewModels;
using PaintQuantity = PaintManagement.ViewModels.PaintQuantity;
using System.Collections;

namespace PaintManagement.Controllers
{
    [Authorize]
    public class PaintController : Controller
    {
        private PaintContext db = new PaintContext();

        // GET: Paint
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var Paints = db.Paints.Include(p => p.ProductOrders).Include(p => p.Orders);
            #region Searching and sorting method   
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.IDSortParm = sortOrder == "PaintID" ? "PaintID_desc" : "PaintID";

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var paints = from p in db.Paints select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                paints = paints.Where(p => p.Name.Contains(searchString) || p.Size.Contains(searchString)
                || p.Quantity.ToString().Contains(searchString) || p.CostPrice.ToString().Contains(searchString)|| p.SalePrice.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case"name_desc":
                    paints = paints.OrderByDescending(p => p.Name);
                    break;
                case "PaintID":
                    paints = paints.OrderBy(p => p.PaintID);
                    break;
                case "PaintID_desc":
                    paints = paints.OrderByDescending(p => p.PaintID);
                    break;
                default:// ascending
                    paints = paints.OrderBy(p => p.Name);
                    break;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            #endregion
            return View(paints.ToPagedList(pageNumber, pageSize));
        }


        #region Stock check method
        [AllowAnonymous]
        public ActionResult PaintAmountByName()
        {
           
            IQueryable<PaintQuantity> data = from paint in db.Paints
                                             select new PaintQuantity()
                                             {
                                                
                                                 PaintName = paint.Name,
                                                 Quantity = paint.Quantity
                                             };

            return View(data.ToList());
        }
        #endregion

        [AllowAnonymous]
        //public ActionResult GeneratePDF()
        //{
        //    return new Rotativa.ActionAsPdf("PaintAmountByName");
        //}
        // GET: Paint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paint paint = db.Paints.Find(id);
            var paints = db.Paints.Include(p => p.ProductOrders).Include(p => p.Orders);
            foreach(Paint t in paints)
            {
                foreach(ProductOrder a in t.ProductOrders)
                {

                }
                foreach(Order r in t.Orders)
                {

                }
            }
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
        public ActionResult Create([Bind(Include = "Name,CostPrice,SalePrice,Size,Quantity")] Paint paint)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Paints.Add(paint);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
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
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var paintToUpdate = db.Paints.Find(id);
            if(TryUpdateModel(paintToUpdate, "", new string[] { "Name", "Cost Price", "Sale Price", "Size", "Quantity" }))
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
            return View(paintToUpdate);
            }
        // GET: Paint/Delete/5
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
            Paint paint = db.Paints.Find(id);
            if (paint == null)
            {
                return HttpNotFound();
            }
            return View(paint);
        }

        // POST: Paint/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteC(int id)
        {
            try
            {
                Paint paint = db.Paints.Find(id);
                db.Paints.Remove(paint);
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
