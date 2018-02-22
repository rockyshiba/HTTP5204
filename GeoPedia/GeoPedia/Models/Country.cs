using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema; //Additional using statement
using System.ComponentModel.DataAnnotations; //Additional using statement

namespace GeoPedia.Models
{
    [Table("Country")] //Table annotation for a table in the database
    public class Country
    {
        [Key] //Key annotation to denote a key, unique value
        [Display(Name="Country Code")] //Will display Code as Country Code in the view
        public string Code { get; set; }

        public int Population { get; set; }
        public string Continent { get; set; }
        public string Name { get; set; }
        public DateTime Established { get; set; }
    }
}