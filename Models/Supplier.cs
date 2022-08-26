using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintManagement.Models
{
    public class Supplier
    {
        //attributes
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SupplierID { get; set; }
        [Required]
        [StringLength(60,ErrorMessage ="Name must be between 3 and 60 characters", MinimumLength = 3)]
        public string Name { get; set; }
        [StringLength(60, ErrorMessage = "Address must be between 3 and 60 characters", MinimumLength =3)]
        public string Address { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage ="Enter valid email format")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Enter valid Phone Numnber format")]
        public int PhoneNumber { get; set; }

        //navigation properties
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }


    }
}