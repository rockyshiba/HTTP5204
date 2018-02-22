using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoPedia.Models
{
    public class Country_Cities
    {
        public Country Ctry { get; set; }
        public List<City> Cities { get; set; }
    }
}