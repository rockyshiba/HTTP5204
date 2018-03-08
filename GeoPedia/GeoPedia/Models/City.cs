using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GeoPedia.Models
{
    [Table("City")]
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name of city required")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be a number")]
        public int Population { get; set; } = 0; //setting default to 0

        [Required(ErrorMessage = "Mayor name required")]
        public string Mayor { get; set; }

        public string Country_Code { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] //This date format seems to be the right one to display an existing date from the database
        public Nullable<DateTime> Established { get; set; } //Dates are nullable in SQL. In order for them to be nullable in C#, you have to declare them as Nullable. 
    }
}