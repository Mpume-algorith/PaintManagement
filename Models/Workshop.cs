using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaintManagement.Models
{
    public class Workshop
    {

        //attributes
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int WorkshopID { get; set; }
        [StringLength(60,ErrorMessage ="Description must be between 3 and 60 characters", MinimumLength = 3)]
        public string Description { get; set; }
        [DataType(DataType.Date)]// date of the workshop
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]//time of the workshop
        public DateTime Time { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "Place name must have characters between 3 and 60", MinimumLength = 3)]
        public string Place { get; set; }// where the workshop takes place

        //navigation properties
        public ICollection<Booking> Bookings { get; set; }// a workshop has a collection of bookings
    }
}