using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PaintManagement.Models
{
    public class Person
    {

        //attributes
        [Required(ErrorMessage ="Please enter a name")]
        [StringLength(60, ErrorMessage ="Name must be between 3 and 60 characters", MinimumLength =3)]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}