using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Security.Model;

namespace CobaltSitefinityMembershipProvider
{
    public class SitefinityRoleDataProvider : Telerik.Sitefinity.Security.Data.RoleDataProvider
    {
        private Dictionary<string, UserLink> linkLookup = new Dictionary<string, UserLink>();

        public SitefinityRoleDataProvider()
        {

        }

        public override bool IsUserInRole(Guid userId, Guid roleId)
        {
            MembershipUser user = CobaltWebApi.GetUserById(userId);
            return user.UserRoles.Any(r => r.Id == roleId) || user.UserGroups.Any(r => r.Id == roleId);
        }

        public override bool IsUserInRole(Guid userId, string roleName)
        {
            MembershipUser user = CobaltWebApi.GetUserById(userId);
            return user.UserRoles.Any(r => r.Name == roleName) || user.UserGroups.Any(r => r.Name == roleName);
        }
        public override Role GetRole(Guid id)
        {
            MembershipRole userRole = CobaltWebApi.GetRoleById(id);
            if (userRole != null)
            {
                return new Role() { Id = userRole.Id, ApplicationName = base.ApplicationName, Name = userRole.Name };
            }
            return null;
        }

        public override IQueryable<Role> GetRoles()
        {
            return CobaltWebApi.GetRoles().Select(r => new Role() { Id = r.Id, ApplicationName = base.ApplicationName, Name = r.Name }).AsQueryable();
        }

        public override Role GetRole(string roleName)
        {
            MembershipRole role = CobaltWebApi.GetRoles().FirstOrDefault(r => r.Name == roleName);
            return new Role() { Id = role.Id, ApplicationName = base.ApplicationName, Name = role.Name };
        }

        public override string[] GetRoleNames()
        {
            return CobaltWebApi.GetRoles().Select(r => r.Name).ToArray();
        }

        public override IList<User> GetUsersInRole(Guid roleId)
        {
            //You could loop through to get every users but it's not recommended from a performance standpoint. This will return only the first 5000 records.
            List<MembershipUser> users = CobaltWebApi.GetUsersInRole(roleId, CobaltWebApi.MAX_NUMBER_OF_RECORDS, 0);
            List<User> returnList = new List<User>();
            foreach (MembershipUser user in users)
            {
                returnList.Add(new User() { Id = user.Id, ApplicationName = base.ApplicationName, IsApproved = true });
            }
            return returnList;
        }
        public override IQueryable<Role> GetRolesForUser(Guid userId)
        {
            MembershipUser user = CobaltWebApi.GetUserById(userId);
            List<Role> roles = new List<Role>();
            roles.AddRange(user.UserRoles.Select(r => new Role() { Id = r.Id, ApplicationName = base.ApplicationName, Name = r.Name }));
            roles.AddRange(user.UserGroups.Select(r => new Role() { Id = r.Id, ApplicationName = base.ApplicationName, Name = r.Name }));
            return roles.AsQueryable<Role>();
        }
        public override UserLink GetUserLink(Guid id)
        {
            KeyValuePair<string, UserLink> linkRecord = linkLookup.Where(l => l.Value.Id == id).FirstOrDefault();
            if (!linkRecord.Equals(default(KeyValuePair<string, UserLink>)))
            {
                return linkRecord.Value;
            }
            return null;
        }

        public override IQueryable<UserLink> GetUserLinks()
        {
            List<UserLink> links = new List<UserLink>();
            //This could be slow if there are a lot of potential users.
            int pageIndex = 0;
            List<MembershipUser> users = CobaltWebApi.GetUsers(CobaltWebApi.MAX_NUMBER_OF_RECORDS, pageIndex);
            while (users.Count > 0)
            {
                foreach (MembershipUser user in users)
                {
                    foreach (string role in user.UserGroups.Select(r => r.Name).Union(user.UserRoles.Select(r => r.Name)))
                    {
                        Role userRole = this.GetRoles().Where(r => r.Name == role).FirstOrDefault();
                        UserLink link = new UserLink() { ApplicationName = base.ApplicationName, Id = Guid.NewGuid(), Role = userRole, UserId = user.Id };
                        links.Add(link);
                        linkLookup[user.Id.ToString() + userRole.Id.ToString()] = link;
                    }
                }
                pageIndex++;
                users = CobaltWebApi.GetUsers(CobaltWebApi.MAX_NUMBER_OF_RECORDS, pageIndex);

            }
            return links.AsQueryable();
        }

        public override Role CreateRole(string roleName)
        {
            throw new NotImplementedException("Creating roles is not currently supported through the Cobalt API");
        }

        public override Role CreateRole(Guid id, string roleName)
        {
            throw new NotImplementedException("Creating roles is not currently supported through the Cobalt API");
        }

        public override void Delete(Role item)
        {
            throw new NotImplementedException("Deleting roles is not currently supported through the Cobalt API");
        }
        public override void Delete(UserLink item)
        {
            throw new NotImplementedException("Deleting roles is not currently supported through the Cobalt API");
        }

        public override UserLink CreateUserLink()
        {
            throw new NotImplementedException("Adding users to roles is not currently supported through the Cobalt API");
        }

        public override UserLink CreateUserLink(Guid id)
        {
            throw new NotImplementedException("Adding users to roles is not currently supported through the Cobalt API");
        }
    }
}