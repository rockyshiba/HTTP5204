using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GeoPedia.Models
{
    [MetadataType(typeof(Site_UsersMetaData))]
    public partial class Buddy_Site_Users
    {
        class Site_UsersMetaData
        {
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}