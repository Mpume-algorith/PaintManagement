using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaintManagement.Models
{
    public class Customer : Person
    {

        // Attribute access

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CustomerID { get; set; }
       // public int CountOrder { get; set; }
    

        // Navigation properties
        public virtual ICollection<Booking> Bookings { get; set; }
         public virtual ICollection<Order> Orders { get; set; }
    }
}