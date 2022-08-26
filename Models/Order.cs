using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintManagement.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int OrderID { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }// amount owed to the sponsoer
        public int Quantity { get; set; }//  number of paint items an order has
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      
        public DateTime Date { get; set; }
        public int CustomerID { get; set; }

        //navigation properties
        public virtual ICollection<Paint> Paints { get; set; }
        public virtual Customer Customer { get; set; }


    }
}