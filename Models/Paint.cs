using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PaintManagement.Models
{
    public class Paint
    {
        //attributes
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PaintID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Are you sure the name is this long?")]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal CostPrice { get; set; }
        [DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }
        [Required]
        public string Size { get; set; }

        public int Quantity { get; set; }
        public string ImagePath { get; set; }

        [AllowHtml]
        public string Contents { get; set; }

        public byte[] Image { get; set; }

        //public List<string> ImageNames { get; set; }




        //navigation properties
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<Order>Orders { get; set; }


    }
}