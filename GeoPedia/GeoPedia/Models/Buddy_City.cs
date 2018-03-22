using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GeoPedia.Models
{
    [MetadataType(typeof(CityMetadata))]
    public partial class City
    {
        class CityMetadata
        {
            [Display(Name = "City Id")]
            public int Id { get; set; }

            [Display(Name = "City name")]
            [Required]
            public string Name { get; set; }

            [Display(Name = "Country code")]
            public string Country_Code { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> Established { get; set; }
        }
    }
}