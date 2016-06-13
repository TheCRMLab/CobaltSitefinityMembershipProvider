using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltSitefinityMembershipProvider
{
    /// <summary>
    /// Manages storage of role membership information in a Cobalt CRM server instance.
    /// </summary>    
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        #region Private Fields
        private string applicationName;
        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of the RoleProvider class.
        /// </summary>
        public RoleProvider()
        {            
        }

        #endregion
        #region Public Properties

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"></see> on a provider after the provider has already been initialized.</exception>
        /// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (name == null || name == string.Empty)
            {
                name = "Cobalt";
            }

            base.Initialize(name, config);
        }
        /// <summary>
        /// Gets or sets the name of the application to store and retrieve role information for.
        /// </summary>
        /// <value></value>
        /// <returns>The name of the application to store and retrieve role information for.</returns>
        public override string ApplicationName
        {
            get
            {
                if (applicationName == null || applicationName == String.Empty)
                {
                    applicationName = "Cobalt";
                }
                return applicationName;
            }
            set
            {
                applicationName = value;
            }
        }

        #endregion
        #region Public Methods

        /// <summary>
        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
        /// </summary>
        /// <param name="userName">The user name to search for.</param>
        /// <param name="roleName">The role to search in.</param>
        /// <returns>
        /// true if the specified user is in the specified role for the configured applicationName; otherwise, false.
        /// </returns>
        public override bool IsUserInRole(string userName, string roleName)
        {
            MembershipUser user = CobaltWebApi.GetUserByUserName(userName);
            if (user != null)
            {
                return user.UserRoles.Any(r => r.Name == roleName);
            }
            return false;
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured applicationName.
        /// </summary>
        /// <param name="userName">The user to return a list of roles for.</param>
        /// <returns>
        /// A string array containing the names of all the roles that the specified user is in for the configured applicationName.
        /// </returns>
        public override string[] GetRolesForUser(string userName)
        {
            MembershipUser user = CobaltWebApi.GetUserByUserName(userName);
            if (user != null)
            {
                return user.UserRoles.Select(r => r.Name).ToArray();
            }
            return new string[] { };
        }

        /// <summary>
        /// When implemented in a derived class, adds a new role to the data source for the configured applicationName.  Invoking the method will result in an exception as 
        /// all roles are defined in the CRM system.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        public override void CreateRole(string roleName)
        {            
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// When implemented in a derived class, removes a role from the data source for the configured applicationName.  Invoking the method will result in an exception as
        /// all roles are defined in the CRM system.
        /// </summary>
        /// <param name="roleName">The name of the role to delete.</param>
        /// <param name="throwOnPopulatedRole">If true, throw an exception if roleName has one or more members and do not delete roleName.</param>
        /// <returns>
        /// true if the role was successfully deleted; otherwise, false.
        /// </returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to search for in the data source.</param>
        /// <returns>
        /// true if the role name already exists in the data source for the configured applicationName; otherwise, false.
        /// </returns>
        public override bool RoleExists(string roleName)
        {
            return CobaltWebApi.GetRoles().Any(r => r.Name == roleName);
        }

        /// <summary>
        /// When implemented in a derived class, adds the specified user names to the specified roles for the configured applicationName.  Invoking the method will result in an 
        /// exception as all roles are defined in the CRM sytem.
        /// </summary>
        /// <param name="userNames">A string array of user names to be added to the specified roles.</param>
        /// <param name="roleNames">A string array of the role names to add the specified user names to.</param>
        public override void AddUsersToRoles(string[] userNames, string[] roleNames)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// When implmented in a derived class, removes the specified user names from the specified roles for the configured applicationName.  Invoking the method will result in 
        /// an exception as all roles are defined in the CRM system.
        /// </summary>
        /// <param name="userNames">A string array of user names to be removed from the specified roles.</param>
        /// <param name="roleNames">A string array of role names to remove the specified user names from.</param>
        public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// Gets a list of users in the specified role for the configured applicationName.  
        /// </summary>
        /// <param name="roleName">The name of the role to get the list of users for.</param>
        /// <returns>
        /// A string array containing the names of all the users who are members of the specified role for the configured applicationName.
        /// </returns>
        public override string[] GetUsersInRole(string roleName)
        {
            MembershipRole role = CobaltWebApi.GetRoles().FirstOrDefault(r => r.Name == roleName);
            if (role != null)
            {
                return CobaltWebApi.GetUsersInRole(role.Id, CobaltWebApi.MAX_NUMBER_OF_RECORDS, 0).Select(u => u.UserName).ToArray();
            }
            return new string[] { };
        }

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles stored in the data source for the configured applicationName.
        /// </returns>
        public override string[] GetAllRoles()
        {
            return CobaltWebApi.GetRoles().Select(r => r.Name).ToArray();
        }

        /// <summary>
        /// Gets an array of user names in a role where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="roleName">The role to search in.</param>
        /// <param name="userNameToMatch">The user name to search for.</param>
        /// <returns>
        /// A string array containing the names of all the users where the user name matches userNameToMatch and the user is a member of the specified role.
        /// </returns>
        public override string[] FindUsersInRole(string roleName, string userNameToMatch)
        {
            MembershipUser user = CobaltWebApi.GetUserByUserName(userNameToMatch);
            if (user != null)
            {
                return new string[] { user.UserName };
            }
            return new string[] { };
        }
        #endregion
    }
}
