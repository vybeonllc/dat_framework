using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.Permission
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "permission")]
        public Permission Permission { get; set; }

        public Manifest()
        {
        }

    }
}
