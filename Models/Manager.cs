using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PaintManagement.Models
{
    public class Manager : Person
    {

        [Key]
        public int ManagerID { get; set; }

        //navaigation properties
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}