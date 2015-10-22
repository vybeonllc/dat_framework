using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.User
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "user")]
        public User User { get; set; }

        public Manifest()
        {
        }

    }
}
