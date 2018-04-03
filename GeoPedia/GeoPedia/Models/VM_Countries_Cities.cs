using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeoPedia.Models
{
    /// <summary>
    /// View model to represent countries and cities. Contains listable and singular properties of both countries and cities
    /// </summary>
    public class VM_Countries_Cities
    {
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }

        public Country country { get; set; }
        public City city { get; set; }
    }
}