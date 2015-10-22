using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.Role
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "role")]
        public Role Role { get; set; }

        public Manifest()
        {
        }

    }
}
