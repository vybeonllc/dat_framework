using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.Authentication
{
    [DataContract()]
    public class AuthenticationInfo
    {
        [DataMember(Name = "user_guid")]
        public Guid UserGuid { get; set; }

        [DataMember(Name = "email_address")]
        public string EmailAddress{ get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}
