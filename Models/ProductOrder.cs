using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaintManagement.Models
{
    public class ProductOrder
    {
        //attributes
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        
        public int Quantity { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
       // public string Product { get; set; }
        [DataType(DataType.Currency)]
        public decimal AmountDue { get; set; }

        public int SupplierID { get; set; }
        public int ManagerID { get; set; }

        //navigation properties
        public virtual ICollection<Paint> Paints { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Manager Manager { get; set; }


    }
}