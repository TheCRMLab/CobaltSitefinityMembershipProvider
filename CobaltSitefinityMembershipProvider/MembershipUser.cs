using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltSitefinityMembershipProvider
{
    public class MembershipUser
    {
        /// <summary>
        /// The primary email address of the membership user
        /// </summary>
        public string PrimaryEmailAddress { get; set; }
        /// <summary>
        /// The display name of the membership user (e.g. Jon Doe)
        /// </summary>
        public string FriendlyName { get; set; }
        /// <summary>
        /// The user name of the membership user
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The unique identifier of the membership user
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// A collection of membership roles of type Website Group that the user belongs to based on the CMS User Roles configured in Cobalt xRM.
        /// </summary>
        public List<MembershipRole> UserGroups { get; set; }
        /// <summary>
        /// A collection of membership roles of type Website User or Website Role that the user belongs to based on the CMS User Roles configured in Cobalt xRM.
        /// </summary>
        public List<MembershipRole> UserRoles { get; set; }

    }
}