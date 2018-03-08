using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema; //Additional using statement
using System.ComponentModel.DataAnnotations; //Additional using statement
using System.Web.Mvc;

namespace GeoPedia.Models
{
    [Table("Country")] //Table annotation for a table in the database
    public class Country
    {
        [Key] //Key annotation to denote a key, unique value
        [Display(Name = "Country Code")] //Will display Code as Country Code in the view
        [Required]
        [Remote("IsCodeAvailable", "Countries", ErrorMessage = "Country code already in use")] //This annotation checks for existing country codes using the IsCodeAvailable action in the Countries Controller
        public string Code { get; set; }

        [Range(0, int.MaxValue)]
        [Required]
        public int Population { get; set; }

        [Required]
        public string Continent { get; set; }

        [Display(Name = "Name of Country")]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> Established { get; set; }
    }
}