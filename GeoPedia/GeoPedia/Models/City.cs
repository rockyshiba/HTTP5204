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
        public DateTime Established { get; set; }
    }
}