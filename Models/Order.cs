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
        [ForeignKey("Customer")]
        public int CustomerID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        

        //navigation properties
        public virtual ICollection<PaintOrder> PaintOrders { get; set; }
        public virtual Customer Customer { get; set; }


    }
}