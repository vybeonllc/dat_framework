using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.UserEvent
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "user_event")]
        public UserEvent UserEvent { get; set; }

        public Manifest()
        {
        }

    }
}
