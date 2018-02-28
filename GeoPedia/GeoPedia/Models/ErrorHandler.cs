using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GeoPedia.Models
{
    /// <summary>
    /// This is a custom class set up by Lee. It contains methods meant to handle a variety of errors. 
    /// </summary>
    public static class ErrorHandler
    {
        public static string DbUpdateHandler(DbUpdateException dbEx)
        {
            //This solution was inpsired by a previous student of mind, Ivan M. The exact source of the solution is unknown. 
            string message = "";
            SqlException ex = dbEx.GetBaseException() as SqlException;
            if(ex != null)
            {
                switch (ex.Number)
                {
                    case 547:
                        message += "There is a parent/foreign relation violated. You are either trying to remove and entry that other entries are dependent on or adding entries that with dependencies that don't exist.";
                        break;
                    case 2627:
                        message += "An entry with this identifier already exists";
                        break;
                    default:
                        message = "An unknown database error has occured. Please try again later.";
                        break;
                }
            }

            return message;
        }
    }
}