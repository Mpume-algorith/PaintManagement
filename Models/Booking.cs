using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintManagement.Models
{
    public class Booking
    {
        //attributes
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int WorkshopID { get; set; }
        

        [DataType(DataType.Date)]// date of the booking made
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]// time of the workshop
        public DateTime Time { get; set; }
     //   public string Place { get; set; }
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }// cost of the booking

        //navigation properties
        public virtual Customer Customer { get; set; }
        public virtual Workshop Workshop { get; set; }



    }
}