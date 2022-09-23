using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PaintManagement.Models
{
    public class PaintOrder
    {
        /*
         * Join Table between many-to-many cardinality of Order and Paint
         */
        public int PaintOrderID { get; set; }
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        [ForeignKey("Paint")]
        public int PaintID { get; set; }
        public int Quantity { get; set; }

        //Reference Entities
        public virtual Order Order { get; set; }
        public virtual Paint Paint { get; set; }
    }
}