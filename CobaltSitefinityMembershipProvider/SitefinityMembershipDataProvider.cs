using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Security.Model;

namespace CobaltSitefinityMembershipProvider
{
    //For more information on custom membership data providers for Sitefinity. See http://docs.sitefinity.com/tutorial-create-a-custom-membership-provider
    public class SitefinityMembershipDataProvider : Telerik.Sitefinity.Security.Data.MembershipDataProvider
    {
        public SitefinityMembershipDataProvider()
        {
        }
        public override bool ValidateUser(Guid userId, string password)
        {
            MembershipUser user = CobaltWebApi.GetUserById(userId);
            if (user != null && !string.IsNullOrEmpty(user.UserName))
            {
                return this.ValidateUser(user.UserName, password);
            }
            return false;
        }
        public override bool ValidateUser(string userName, string password)
        {
            return CobaltWebApi.ValidateUser(userName, password);
        }
        public override bool ValidateUser(User user, string password)
        {
            return CobaltWebApi.ValidateUser(user.UserName, password);
        }
        public override User CreateUser(Guid id, string userName)
        {
            throw new NotImplementedException("Creating users is not currently supported through the Cobalt API");
        }
        public override User CreateUser(string userName)
        {
            throw new NotImplementedException("Creating users is not currently supported through the Cobalt API");
        }
        public override void Delete(User item)
        {
            throw new NotImplementedException("Deleting users is not currently supported through the Cobalt API");
        }
        public override User GetUser(Guid id)
        {
            MembershipUser user = CobaltWebApi.GetUserById(id);
            if (user != null)
            {
                User newUser = new User() { Id = user.Id, ApplicationName = base.ApplicationName, IsApproved = true };
                newUser.SetUserName(user.UserName);
                return newUser;
            }
            return null;
        }

        public override User GetUser(string userName)
        {
            MembershipUser user = CobaltWebApi.GetUserByUserName(userName);
            if (user != null)
            {
                User newUser = new User() { Id = user.Id, ApplicationName = base.ApplicationName, IsApproved = true };
                newUser.SetUserName(userName);
                return newUser;
            }
            return null;
        }
        public override IQueryable<User> GetUsers()
        {
            //You could loop through to get every users but it's not recommended from a performance standpoint
            List<MembershipUser> top5000Users = CobaltWebApi.GetUsers(CobaltWebApi.MAX_NUMBER_OF_RECORDS, 0);
            List<User> newUsers = new List<User>();

            foreach (MembershipUser user in top5000Users)
            {
                User newUser = new User() { Id = user.Id, ApplicationName = base.ApplicationName, IsApproved = true };
                newUser.SetUserName(user.UserName);
                newUsers.Add(newUser);
            }
            return newUsers.AsQueryable();
        }
    }
}
