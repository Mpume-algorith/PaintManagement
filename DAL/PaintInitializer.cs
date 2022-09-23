using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using PaintManagement.Models;

namespace PaintManagement.DAL
{
    public class PaintInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<PaintContext>
    {
        protected override void Seed(PaintContext context)
        {
            var managers = new List<Manager>
            {
                new Manager {Name= "Michelle Cretikos", PhoneNumber = 0774936282, Email= "greengeckoframes@gmail.com", ProductOrders = new List<ProductOrder>() }
            };
            managers.ForEach(m => context.Managers.Add(m));
            context.SaveChanges();

            var suppliers = new List<Supplier>
            {
                new Supplier { Name = "Tjhoko Paint", Address = "350 Reed Rd,Sea Point", Email = "tjhokopaint@gmail.com", PhoneNumber = 0853826392, /*ProductOrders= new List<ProductOrder>()*/ }
            };

            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();


            var productOrders = new List<ProductOrder>
            {
                 new ProductOrder { Quantity = 50,Date = DateTime.Parse("2019-05-01"), AmountDue = 750, SupplierID = suppliers.Single(r => r.Name == "Tjhoko Paint").SupplierID , ManagerID = managers.Single(q => q.Name == "Michelle Cretikos").ManagerID, Paints = new List<Paint>()},
                new ProductOrder { Quantity = 25,Date = DateTime.Parse("2019-07-01"), AmountDue = 400, SupplierID = suppliers.Single(r => r.Name == "Tjhoko Paint").SupplierID , ManagerID = managers.Single(q => q.Name == "Michelle Cretikos").ManagerID, Paints = new List<Paint>() },
                new ProductOrder{Quantity = 15, Date = DateTime.Parse("2018-05-24"), AmountDue = 300, SupplierID = suppliers.Single(r => r.Name == "Tjhoko Paint").SupplierID , ManagerID = managers.Single(q => q.Name == "Michelle Cretikos").ManagerID, Paints = new List<Paint>() },
                new ProductOrder { Quantity = 30 , Date = DateTime.Parse("2019-01-01"),  AmountDue = 250, SupplierID = suppliers.Single(r => r.Name == "Tjhoko Paint").SupplierID , ManagerID = managers.Single(q => q.Name == "Michelle Cretikos").ManagerID, Paints = new List<Paint>()},
                new ProductOrder { Quantity = 20, Date = DateTime.Parse("2019-08-01"),  AmountDue = 225, SupplierID = suppliers.Single(r => r.Name == "Tjhoko Paint").SupplierID , ManagerID = managers.Single(q => q.Name == "Michelle Cretikos").ManagerID, Paints = new List<Paint>()}
            };
            productOrders.ForEach(o => context.ProductOrders.Add(o));
            context.SaveChanges();
            var paints = new List<Paint>
             {
              new Paint{Name = "Lourain's Cream", CostPrice = 100, SalePrice = 110, Size = "250l", ProductOrders = new List<ProductOrder>(), PaintOrders = new List<PaintOrder>()},
              new Paint { Name = "Corey Blue", CostPrice = 80, SalePrice = 90, Size = "28ml", ProductOrders = new List<ProductOrder>(), PaintOrders = new List<PaintOrder>() },
              new Paint { Name = "Clear Glaze", CostPrice = 80, SalePrice = 90, Size="28ml", ProductOrders = new List<ProductOrder>(), PaintOrders = new List<PaintOrder>()},
              new Paint {Name ="Karema", CostPrice = 170, SalePrice = 187, Size = "500ml", ProductOrders = new List<ProductOrder>(), PaintOrders = new List<PaintOrder>()},
              new Paint {Name= "Asgat", CostPrice = 270, SalePrice = 288, Size = "1l", ProductOrders = new List<ProductOrder>(), PaintOrders = new List<PaintOrder>()},
              new Paint {Name = "Godfrey's Glimpse", CostPrice = 110, SalePrice= 117, Size = "250ml",  ProductOrders = new List<ProductOrder>(), PaintOrders = new List<PaintOrder>()}
             };
            paints.ForEach(p => context.Paints.Add(p));
            context.SaveChanges();


            var customers = new List<Customer>
            {
               new Customer {Name = "Davie Jones",Email = "davieJones78@gmail.com", PhoneNumber = 0746725746, Orders = new List<Order>(), Bookings = new List<Booking>()},
                new Customer{Name = "Busi Mudimo", Email ="BM@yahoo.com", PhoneNumber = 0472927592, Orders = new List<Order>(), Bookings = new List<Booking>()},
                new Customer { Name =" John Doe", Email = "John17@hotmail.com", PhoneNumber = 0371972012,Orders = new List<Order>(), Bookings = new List<Booking>()},
                new Customer {Name = "Mary Jane", Email = "maryJane28@gmail.com", PhoneNumber = 0572575131,Orders = new List<Order>(), Bookings = new List<Booking>()},
                new Customer {Name = "Susan Scott", Email = "Susan.Scott@yahoo.com", PhoneNumber = 0753725142,Orders = new List<Order>(), Bookings = new List<Booking>()}
            };
            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();
            var orders = new List<Order>
            {
               new Order{Date = DateTime.Parse("2019-04-03"),
                             CustomerID = customers.Single(o =>o.Name =="Davie Jones").CustomerID, PaintOrders = new List<PaintOrder>()},
               new Order {Date = DateTime.Parse("2019-07-27"),
                             CustomerID = customers.Single(o => o.Name == "Busi Mudimo").CustomerID, PaintOrders = new List<PaintOrder>()},
               new Order {Date = DateTime.Parse("2019-02-19"),
                             CustomerID = customers.Single(o => o.Name == "John Doe").CustomerID, PaintOrders = new List<PaintOrder>()},
               new Order {Date = DateTime.Parse("2019-07-05"),
                             CustomerID = customers.Single(o =>o.Name =="Mary Jane").CustomerID, PaintOrders = new List<PaintOrder>()},
               new Order {Date = DateTime.Parse("2019-01-18"),
                            CustomerID = customers.Single(o => o.Name == "Susan Scott").CustomerID, PaintOrders = new List<PaintOrder>()},


            };
            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();

            var workshops = new List<Workshop>
            {
                new Workshop{Bookings = new List<Booking>(),Description = "Furn0901", Date = DateTime.Parse("2020-09-14"), Time = DateTime.Parse("15:10"), Place = "15 Reed Rd, Green Point"},
                new Workshop{Bookings = new List<Booking>(),Description ="Bag0902", Date = DateTime.Parse("2020-09-19"), Time = DateTime.Parse("15:10"), Place = "15 Reed Rd, Green Point"},
                new Workshop {Bookings = new List<Booking>(),Description = "Ch0718" ,Date = DateTime.Parse("2020-09-21"), Time = DateTime.Parse("14:00"), Place = "250 Fig Street, Green Point"},
                new Workshop{Bookings = new List<Booking>(),Description = "Clo0625", Date = DateTime.Parse("2020-09-26"), Time = DateTime.Parse("14:00"), Place = "250 Fig Street, Green Point"},
                new Workshop {Bookings = new List<Booking>(),Description = "Sho0705", Date = DateTime.Parse("2020-09-28"),Time = DateTime.Parse("12:00"), Place = "25 Green Street, Green Point"},
            };
            workshops.ForEach(w => context.Workshops.Add(w));
            context.SaveChanges();

            var bookings = new List<Booking>
            {

                 new Booking {CustomerID = customers.Single(i => i.Name =="Davie Jones").CustomerID, WorkshopID = workshops.First(o => o.Description =="Furn0901").WorkshopID,
                                 Date =DateTime.Parse("2020-09-14"), Time = DateTime.Parse("15:10"), Cost = 350},
                new Booking {CustomerID = customers.Single(i => i.Name == "Busi Mudimo").CustomerID,WorkshopID = workshops.First(o => o.Description =="Bag0902").WorkshopID,
                                Date =DateTime.Parse("2020-09-19"), Time = DateTime.Parse("15:10"), Cost = 350},
                new Booking {CustomerID = customers.Single(i => i.Name== "John Doe").CustomerID, WorkshopID = workshops.First(o => o.Description =="Ch0718").WorkshopID,
                                Date = DateTime.Parse("2020-09-21"), Time = DateTime.Parse("14:00"), Cost = 350},
                new Booking {CustomerID = customers.Single(i => i.Name =="Mary Jane").CustomerID, WorkshopID = workshops.First(o => o.Description== "Clo0625").WorkshopID,
                                 Date = DateTime.Parse("2020-09-26"), Time = DateTime.Parse("14:00"), Cost = 350},
                new Booking {CustomerID = customers.Single(i => i.Name =="Susan Scott").CustomerID,  WorkshopID = workshops.First(o => o.Description =="Sho0705").WorkshopID,
                                Date = DateTime.Parse("2020-09-28"), Time = DateTime.Parse("12:00"), Cost = 350},

            };
            bookings.ForEach(b => context.Bookings.Add(b));
            context.SaveChanges();

           

        }


    }
}