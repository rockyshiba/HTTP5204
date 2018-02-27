using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeoPedia.Models
{
    [Table("City")]
    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public string Mayor { get; set; }
        public string Country_Code { get; set; }
        public Nullable<DateTime> Established { get; set; } //Dates are nullable in SQL. In order for them to be nullable in C#, you have to declare them as Nullable. 
    }
}