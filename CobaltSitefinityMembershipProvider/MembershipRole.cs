using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobaltSitefinityMembershipProvider
{
    public class MembershipRole
    {
        /// <summary>
        /// The Display Name of the membership role
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A description of the membership role
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The unique identifier of the membership role
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The type of the membership role (e.g. Website User, Webiste Role, Website Group)
        /// </summary>
        public string Type { get; set; }
    }
}
