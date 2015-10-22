using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.RolePermission
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "role_permission")]
        public RolePermission RolePermission { get; set; }

        public Manifest()
        {
        }

    }
}
