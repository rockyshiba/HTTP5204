using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity; //Additional using statement

namespace GeoPedia.Models
{
    public class GeoContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
    }
}