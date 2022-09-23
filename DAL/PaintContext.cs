using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaintManagement.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PaintManagement.DAL
{
    public class PaintContext : DbContext
    {

        public PaintContext() : base("PaintContext")
        {
          
        }

        //defining intial database tables
        public DbSet<Paint> Paints { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaintOrder> PaintOrders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
  
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<Manager> Managers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

           
        }




    }
}