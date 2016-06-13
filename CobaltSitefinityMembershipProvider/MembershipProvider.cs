using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CobaltSitefinityMembershipProvider
{
    /// <summary>
    /// Manages storage of membership information in a Cobalt CRM server instance.
    /// </summary>
    public class MembershipProvider : System.Web.Security.MembershipProvider
    {
        #region Private Attributes
        private string applicationName;
        #endregion
        #region Public Methods

        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <param name="username">The user to update the password for.</param>
        /// <param name="oldPassword">The current password for the specified user.</param>
        /// <param name="newPassword">The new password for the specified user.</param>
        /// <returns>
        /// true if the password was updated successfully; otherwise, false.
        /// </returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// When implemented in a derived class, processes a request to update the password question and answer for a membership user.  Invoking the method 
        /// will result in an exception as members are defined in the CRM system.
        /// </summary>
        /// <param name="username">The user to change the password question and answer for.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
        /// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
        /// <returns>
        /// true if the password question and answer are updated successfully; otherwise, false.
        /// </returns>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <param name="username">The name of the user to validate.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        public override bool ValidateUser(string username, string password)
        {
            return CobaltWebApi.ValidateUser(username, password);
        }
        /// <summary>
        /// Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <param name="userName">The membership user to clear the lock status for.</param>
        /// <returns>
        /// Always returns false since a membership can only be locked through the CRM system.
        /// </returns>
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Removes a user from the membership data source.
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
        /// <returns>
        /// Always returns false since a membership can only be deleted through the CRM system.
        /// </returns>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// When implemented in a derived class, gets the number of users currently accessing the application.
        /// </summary>
        /// <returns>
        /// The number of users currently accessing the application.
        /// </returns>
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public virtual string GetPassword(string username)
        {
            return GetPassword(username, String.Empty);
        }

        /// <summary>
        /// Gets the password for the specified user name from the data source.
        /// </summary>
        /// <param name="username">The user to retrieve the password for.</param>
        /// <param name="answer">The password answer for the user.</param>
        /// <returns>
        /// The password for the specified user name.
        /// </returns>
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public virtual string ResetPassword(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <param name="username">The user to reset the password for.</param>
        /// <param name="answer">The password answer for the specified user.</param>
        /// <returns>The new password for the specified user.</returns>
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the user name associated with the specified e-mail address.
        /// </summary>
        /// <param name="email">The e-mail address to search for.</param>
        /// <returns>
        /// The user name associated with the specified e-mail address. If no match is found, return null.
        /// </returns>
        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser"></see> object that represents the user to update and the updated information for the user.</param>
        public override void UpdateUser(System.Web.Security.MembershipUser membershipUser)
        {
        }

        /// <summary>
        /// Adds a new membership user to the data source.
        /// </summary>
        /// <param name="username">The user name for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="passwordQuestion">The password question for the new user.</param>
        /// <param name="passwordAnswer">The password answer for the new user</param>
        /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
        /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
        /// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus"></see> enumeration value indicating whether the user was created successfully.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"></see> object populated with the information for the newly created user.
        /// </returns>
        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public virtual System.Web.Security.MembershipUser GetUser(string username)
        {
            return GetUser(username, true);
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="username">The name of the user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"></see> object populated with the specified user's information from the data source.
        /// </returns>
        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser user = CobaltWebApi.GetUserByUserName(username);
            if (user != null)
            {
                return this.CreateMembershipUser(user);
            }
            return null;
        }

        /// <summary>
        /// Gets information from the data source for a user based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"></see> object populated with the specified user's information from the data source.
        /// </returns>
        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {            
            Guid id = (Guid)providerUserKey;
            MembershipUser user = CobaltWebApi.GetUserById(id);
            if (user != null)
            {
                return this.CreateMembershipUser(user);
            }
            return null;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public virtual MembershipUserCollection GetAllUsers()
        {
            int recordCount = 0;
            return GetAllUsers(0, int.MaxValue, out recordCount);
        }

        /// <summary>
        /// Gets a collection of all the users in the data source in pages of data.
        /// </summary>
        /// <param name="pageIndex">The index of the page of results to return. pageIndex is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"></see> collection that contains a page of pageSize<see cref="T:System.Web.Security.MembershipUser"></see> objects beginning at the page specified by pageIndex.
        /// </returns>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = CobaltWebApi.MAX_NUMBER_OF_RECORDS;
            return this.ConvertToMembershipUserCollection(CobaltWebApi.GetUsers(pageSize, pageIndex).ToArray());
        }
        /// <summary>
        /// Finds the users by email.
        /// </summary>
        /// <param name="emailToMatch">The email to match.</param>
        /// <returns></returns>
        public virtual MembershipUserCollection FindUsersByEmail(string emailToMatch)
        {
            int totalRecords;
            MembershipUserCollection members = this.FindUsersByEmail(emailToMatch, 0, int.MaxValue, out totalRecords);
            return members;
        }
        /// <summary>
        /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
        /// </summary>
        /// <param name="emailToMatch">The e-mail address to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. pageIndex is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"></see> collection that contains a page of pageSize<see cref="T:System.Web.Security.MembershipUser"></see> objects beginning at the page specified by pageIndex.
        /// </returns>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Finds the name of the users by.
        /// </summary>
        /// <param name="usernameToMatch">The username to match.</param>
        /// <returns></returns>
        public virtual MembershipUserCollection FindUsersByName(string usernameToMatch)
        {
            int totalRecords;
            MembershipUserCollection members = this.FindUsersByName(usernameToMatch, 0, int.MaxValue, out totalRecords);
            return members;
        }
        /// <summary>
        /// Gets a collection of membership users where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. pageIndex is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched users.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"></see> collection that contains a page of pageSize<see cref="T:System.Web.Security.MembershipUser"></see> objects beginning at the page specified by pageIndex.
        /// </returns>
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            totalRecords = 1;
            return this.RetrieveMembershipUsers(usernameToMatch);
        }
        #endregion
        #region Public Properties

        /// <summary>
        /// Gets the friendly name used to refer to the provider during configuration.
        /// </summary>
        /// <value></value>
        /// <returns>The friendly name used to refer to the provider during configuration.</returns>
        public override string Name
        {
            get
            {
                return "Cobalt";
            }
        }
        /// <summary>
        /// The name of the application using the custom membership provider.
        /// </summary>
        /// <value></value>
        /// <returns>The name of the application using the custom membership provider.</returns>
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
        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        /// <value></value>
        /// <returns>The number of invalid password or password-answer attempts allowed before the membership user is locked out.</returns>
        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return int.MaxValue;
            }
        }
        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </summary>
        /// <value></value>
        /// <returns>The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.</returns>
        public override int PasswordAttemptWindow
        {
            get
            {
                return 1;
            }
        }
        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        /// <value></value>
        /// <returns>The minimum number of special characters that must be present in a valid password.</returns>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <value></value>
        /// <returns>The minimum length required for a password. </returns>
        public override int MinRequiredPasswordLength
        {
            get
            {
                return 6;
            }
        }
        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <value></value>
        /// <returns>A regular expression used to evaluate a password.</returns>
        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return "^.+";
            }
        }
        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <value></value>
        /// <returns>One of the <see cref="T:System.Web.Security.MembershipPasswordFormat"></see> values indicating the format for storing passwords in the data store.</returns>
        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return MembershipPasswordFormat.Clear;
            }
        }
        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
        /// </summary>
        /// <value></value>
        /// <returns>true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.</returns>
        public override bool EnablePasswordRetrieval
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        /// <value></value>
        /// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.</returns>
        public override bool EnablePasswordReset
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <value></value>
        /// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.</returns>
        public override bool RequiresUniqueEmail
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        /// <value></value>
        /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.</returns>
        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return false;
            }
        }

        #endregion
        #region Protected Methods
        protected virtual System.Web.Security.MembershipUser CreateMembershipUser(MembershipUser user)
        {
            return new System.Web.Security.MembershipUser(this.Name, user.UserName, user.Id, user.PrimaryEmailAddress, string.Empty, string.Empty, true, false, DateTime.MinValue, DateTime.Now, DateTime.Now, DateTime.MinValue, DateTime.MinValue);
        }
        /// <summary>
        /// Retrieves the membership users.
        /// </summary>
        /// <returns></returns>
        protected virtual MembershipUserCollection RetrieveMembershipUsers()
        {
            return this.ConvertToMembershipUserCollection(CobaltWebApi.GetUsers(CobaltWebApi.MAX_NUMBER_OF_RECORDS, 0).ToArray());
        }
        /// <summary>
        /// Retrieves the membership users.
        /// </summary>
        /// <param name="emailAddress">The email address.</param>
        /// <returns></returns>
        protected virtual MembershipUserCollection RetrieveMembershipUsers(string userName)
        {
            return this.ConvertToMembershipUserCollection(new MembershipUser[] { CobaltWebApi.GetUserByUserName(userName) });
        }


        /// <summary>
        /// Converts the users to membership user collection.
        /// </summary>
        /// <param name="dynamicCmsUsers">The dynamic users.</param>
        /// <returns></returns>
        protected virtual MembershipUserCollection ConvertToMembershipUserCollection(MembershipUser[] users)
        {
            MembershipUserCollection collection = new MembershipUserCollection();
            foreach (MembershipUser user in users)
            {
                if (collection[user.UserName] == null)
                {
                    collection.Add(this.CreateMembershipUser(user));
                }
            }
            return collection;
        }

        #endregion
        #region Protected Properties


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

        #endregion
    }
}
