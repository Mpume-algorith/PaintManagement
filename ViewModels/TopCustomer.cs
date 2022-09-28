using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaintManagement.Models;

namespace PaintManagement.ViewModels
{
    public class TopCustomer
    {
        public string Name { get; set; }
        public int SumOfOrders { get; set; }//number of orders made
    }
}