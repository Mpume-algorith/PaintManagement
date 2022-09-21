using PaintManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaintManagement.Models;
using PaintManagement.ViewModels;

namespace PaintManagement.Controllers
{
    public class DashboardController : Controller
    {
        private PaintContext db = new PaintContext();
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dashboard/Details/5

        public ActionResult Dashboard()
        {
            
            {
                ViewBag.CountCustomers = db.Customers.Count();
                ViewBag.CountPaints = db.Paints.Count();
                ViewBag.CountOrders = db.Orders.Count();
            }

            return View();
        }

        // GET: Dashboard/Create
        /*public ActionResult OrderByMonth()
        {
            List<Order> quantityOfOrders = null;
                
            var orderByMonth = (from m in db.Orders
                                group m by m.Date.Month
                                into ordersPerMonth
                                select new
                                {
                                    month = ordersPerMonth.Key,
                                    numberOfOrders = ordersPerMonth.Sum(f => f.Quantity)
                                }).ToList();
            //ViewBag.MONTH = orderByMonth.  ;
            //ViewBag.NumOfOrders = "";
            var months = (from m in db.Orders select m.Date.Month).ToList();
            quantityOfOrders = (from m in db.Orders
                                    join o in orderByMonth
                                    on m.Date.Month equals o.month
                                    select new Order
                                    {
                                        Quantity = o.numberOfOrders
                                    }).ToList();

            ViewBag.Months = months;
            ViewBag.OrdersPerMonth = quantityOfOrders;

            return View();
        }*/
        public ActionResult Revenue()
        {
            List<Revenue> quantitySum = new List<Revenue>();
            List<Revenue> month = new List<Revenue>();

            var revenueByMonth = (from data in db.Orders
                                  group data by data.Date.Month
                                 into g
                                  select new
                                  {
                                      monthValue = g.Key,
                                      summedValue = g.Sum(x => x.Amount)
                                  }).ToList();
            foreach (var item in revenueByMonth)
            {
                Revenue monthList = new Revenue();
                monthList.Month = item.monthValue.ToString("MMM");
                month.Add(monthList);
            }
            foreach (var item in revenueByMonth)
            {
                Revenue revSum = new Revenue();
                revSum.Amount = item.summedValue;
                quantitySum.Add(revSum);
            }
            ViewBag.MONTH = month;
            ViewBag.orderVolume = quantitySum;
            return View();
        }
        
        
        public ActionResult DashboardDisplay()
        {
            var list = db.Orders;
            List<int> counts = new List<int>();
            var customerNames = list.Select(x => x.Customer.Name).Distinct();
            foreach(var item in customerNames)
            {
                counts.Add(list.Count(x => x.Customer.Name == item));
            }

            var customerCount = counts;
            ViewBag.NAME = customerNames;
            ViewBag.CUSTOMERCOUNT = customerCount.ToList();
            
            return View();
        }
        
        // POST: Dashboard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
