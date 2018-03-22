using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using GeoPedia.Models;

namespace GeoPedia.Models
{
    public class MyRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            //This exception means that this method is not used. 
            //You have to customize these methods
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is used to compare the user's role with the application.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns an string array of userrole(s) from the database.</returns>
        public override string[] GetRolesForUser(string username)
        {
            GeoContext db = new GeoContext();

            //The role is stored in another table. 
            //Site_Users is tied to the Site_Roles and the Role_Name column contains the name of the role.
            string role = db.Site_Users.Where(u => u.Username == username).FirstOrDefault().Site_Roles.Role_Name;
            
            //results array containing a single item
            string[] results = { role };
            return results;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}