using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CobaltSitefinityMembershipProvider;
using Telerik.Sitefinity.Security.Model;
using System.Linq;
using System.Collections.Generic;

namespace ProviderUnitTests
{
    [TestClass]
    public class RoleProviderTests
    {
        private User testUser = null;
        private Role testRole = null;
        private UserLink testUserLink = null;
        SitefinityRoleDataProvider roleProvider = new SitefinityRoleDataProvider();
        SitefinityMembershipDataProvider membershipProvider = new SitefinityMembershipDataProvider();


        public void TestGetUsers()
        {
            IQueryable<User> users = membershipProvider.GetUsers();
            Assert.IsNotNull(users, "User list is null");
            Assert.IsTrue(users.Count() > 0, "Need at least one valid user in the system in order to test");
            if (users.Count() > 0)
            {
                testUser = users.FirstOrDefault();
                Assert.AreNotEqual(testUser.UserName, string.Empty);
            }
        }

        [TestMethod]
        public void TestGetTest123User()
        {
            User user = membershipProvider.GetUser("test123");
            Assert.IsNotNull(user, "User could not be retrieved by User Name");
            Assert.IsTrue(user.UserName != string.Empty, "User Name is empty");
            Assert.IsTrue(user.UserName == "test123", "User Name is not the same as the request");

        }
        [TestMethod]
        public void TestGetRoles()
        {
            IQueryable<Role> roles = roleProvider.GetRoles();
            Assert.IsNotNull(roles, "Role list is null");
            Assert.IsTrue(roles.Count() > 0, "Need at least one valid user role in the system in order to test");
            if (roles.Count() > 0)
            {
                testRole = roles.FirstOrDefault();
                Assert.AreNotEqual(testRole.Id, Guid.Empty);
                Assert.AreNotEqual(testRole.Name, string.Empty);
            }
        }

        [TestMethod]
        public void TestGetRoleById()
        {
            TestGetRoles();
            Role role = roleProvider.GetRole(testRole.Id);
            Assert.IsNotNull(role, "Known role could not be retrieved by Id");
            Assert.AreEqual(role.Id, testRole.Id, "Retrieved role id does not match requested role id");
        }

        [TestMethod]
        public void TestGetRoleByName()
        {
            TestGetRoles();
            Role role = roleProvider.GetRole(testRole.Name);
            Assert.IsNotNull(role, "Known role could not be retrieved by Name");
            Assert.AreEqual(role.Name, testRole.Name, "Retrieved role does not match requested role");
        }

        [TestMethod]
        public void TestGetRoleNames()
        {
            string[] roles = roleProvider.GetRoleNames();
            Assert.IsNotNull(roles, "Role list is null");
            Assert.IsTrue(roles.Count() > 0, "Need at least one valid user role in the system in order to test");
            if (roles.Count() > 0)
            {
                Assert.AreNotEqual(roles[0], string.Empty);
            }
        }

        [TestMethod]
        public void TestGetUsersInRole()
        {
            TestGetRoles();
            IList<User> users = roleProvider.GetUsersInRole(testRole.Id);
            Assert.IsNotNull(users, "User list is null");
            Assert.IsTrue(users.Count() > 0, "Need at least one valid user in the system in order to test");
            Assert.AreNotEqual(users[0].UserName, string.Empty);
        }
        [TestMethod]
        public void TestGetUserById()
        {
            this.TestGetUsers();
            User user = membershipProvider.GetUser(testUser.Id);
            Assert.IsNotNull(user, "User could not be retrieved by Id");
            Assert.IsTrue(user.UserName != string.Empty, "User Name is empty");
            Assert.IsTrue(user.Id == testUser.Id, "User Id is not the same as the request");

            user = membershipProvider.GetUser(Guid.NewGuid());
            Assert.IsNull(user, "Found user that should not exist");

        }
        [TestMethod]
        public void TestGetUserByUserName()
        {
            this.TestGetUsers();
            User user = membershipProvider.GetUser(testUser.UserName);
            Assert.IsNotNull(user, "User could not be retrieved by User Name");
            Assert.IsTrue(user.UserName != string.Empty, "User Name is empty");
            Assert.IsTrue(user.UserName == testUser.UserName, "User Name is not the same as the request");
        }
        [TestMethod]
        public void TestValidateUserFail()
        {
            Assert.IsFalse(membershipProvider.ValidateUser(Guid.Empty, Guid.NewGuid().ToString()), "A user was incorrectly validated");
            Assert.IsFalse(membershipProvider.ValidateUser("Test", Guid.NewGuid().ToString()), "A user was incorrectly validated");
            Assert.IsFalse(membershipProvider.ValidateUser(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), "A user was incorrectly validated");
        }
        [TestMethod]
        public void TestGetUsersRoles()
        {
            this.TestGetUsers();
            IQueryable<Role> usersRoles = roleProvider.GetRolesForUser(testUser.Id);
            Assert.IsNotNull(usersRoles, "User's roles could not be retrieved");
            Assert.IsTrue(usersRoles.Count() > 0, "A valid user should have at least one role");
        }
        [TestMethod]
        public void TestGetUserLinks()
        {
            this.TestGetUsers();
            IQueryable<UserLink> userLinks = roleProvider.GetUserLinks();
            Assert.IsNotNull(userLinks, "User Links could not be retrieved");
            Assert.IsTrue(userLinks.Count() > 0, "There must be at least one user with a role in the system");
            if (userLinks.Count() > 0)
            {
                testUserLink = userLinks.FirstOrDefault();
                Assert.AreNotEqual(testUserLink.UserId, Guid.Empty, "User Link User Id is Empty");
                Assert.IsNotNull(testUserLink.Role, "User Link Role is Null");
            }
        }

        [TestMethod]
        public void TestUserIsInRole()
        {
            this.TestGetUserLinks();
            Assert.IsTrue(roleProvider.IsUserInRole(testUserLink.UserId, testUserLink.Role.Id), "Known user in role did not return true");
        }
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException), "Create user did not throw not implemented")]
        public void TestCreateUserUserName()
        {
            membershipProvider.CreateUser(Guid.NewGuid().ToString());
        }
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException), "Create user did not throw not implemented")]
        public void TestCreateUserUserId()
        {
            membershipProvider.CreateUser(Guid.NewGuid(), Guid.NewGuid().ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException), "Delete user did not throw not implemented")]
        public void TestDeleteUserUser()
        {
            TestGetUsers();
            membershipProvider.Delete(testUser);
        }

    }
}
