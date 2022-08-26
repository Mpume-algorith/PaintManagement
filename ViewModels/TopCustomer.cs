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
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Orders { get; set; }//number of orders made
    }
}