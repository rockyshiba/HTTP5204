using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoPedia.Models
{
    /// <summary>
    /// This view model will hold a list of countries and cities
    /// </summary>
    public class Countries_Cities
    {
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }
    }
}