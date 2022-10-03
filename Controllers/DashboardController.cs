using PaintManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PaintManagement.Models;
using PaintManagement.ViewModels;
using System.Windows.Input;

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
            
            return View();
        }
        
        
        public ActionResult DashboardDisplay()
        {
            #region TopCustomer Bar Chart
            var list = db.Orders;
            List<int> counts = new List<int>();
            List<int> orders = new List<int>();
            var customerNames = list.Select(x => x.Customer.Name).Distinct();
            
            /// I want to attach the sum of orders to the name of the customer
            var quantByOrderID = from item1 in db.PaintOrders
                                 join item2 in db.Orders
                                 on item1.OrderID equals item2.OrderID
                                 select new
                                 {
                                     OrderID = item2.OrderID,
                                     CustomerID = item2.CustomerID,
                                     Quantity = item1.Quantity
                                 };

            var quantByName = from item1 in quantByOrderID
                              join item2 in db.Customers
                              on item1.CustomerID equals item2.CustomerID
                              select new
                              {
                                  CustomerName = item2.Name,
                                  Quantity = item1.Quantity,
                              };
            var orderByName = from item in quantByName
                              group item by item.CustomerName into g
                              let sumOfOrders = g.Sum(x => x.Quantity)
                              select new
                              {
                                  Key = g.Key,
                                  SumOfOrders = sumOfOrders,
                              };

            foreach (var item in customerNames)
            {
                counts.Add(list.Count(x => x.Customer.Name == item));
                foreach (var item1 in orderByName)
                {
                    if (item == item1.Key)
                    {
                        orders.Add(item1.SumOfOrders);
                    }
                }
            }
            var orderSum = orders;
            var customerCount = counts;
            ViewBag.NAME = customerNames;
            ViewBag.CUSTOMERCOUNT = customerCount.ToList();
            ViewBag.ORDERSUM = orderSum.ToList();
            #endregion
            #region Sales By Year Char

            #endregion
            #region Profit By Month
            List<decimal> profits = new List<decimal>();

            var paintJoinPaintOrder = from item1 in db.Paints
                                      join item2 in db.PaintOrders
                                      on item1.PaintID equals item2.PaintID
                                      select new
                                      {
                                          PaintID = item1.PaintID,
                                          OrderID = item2.OrderID,
                                          CostPrice = item1.CostPrice,
                                          SalePrice = item1.SalePrice,
                                          Quantity = item2.Quantity,
                                      };
            var paintJoinOrder = from item1 in paintJoinPaintOrder
                                 join item2 in db.Orders
                                 on item1.OrderID equals item2.OrderID
                                 orderby item2.Date.Month ascending
                                 let computation = (item1.SalePrice - item1.CostPrice) * item1.Quantity
                                 select new
                                 {
                                     Date = item2.Date.Month,
                                     PaintID = item1.PaintID,
                                     Profit = computation,                                                                      
                                 };

            var profitByMonth = (from item in paintJoinOrder
                                  group item by item.Date into g
                                  select new
                                  {
                                      Date = g.Key,
                                      SumProfit = g.Sum(x => x.Profit)
                                  }).ToList();


                             
            foreach(var item in profitByMonth)
            {
                profits.Add(item.SumProfit);
                
            }
            ViewBag.MonthlyProfit = profits;
            #endregion

            //ViewBag.SumOfOrders = orderQuantity.ToList();



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
