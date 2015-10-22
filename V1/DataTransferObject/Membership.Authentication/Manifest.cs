using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.Authentication
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "authentication_info")]
        public AuthenticationInfo AuthenticationInfo { get; set; }

        public Manifest()
        {
        }
    }
}
