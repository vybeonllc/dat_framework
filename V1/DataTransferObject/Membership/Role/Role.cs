using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.Role
{
    [DataContract()]
    public class Role
    {

        [DataMember(Name = "role_id")]
        public int RoleId { get; set; }

        [DataMember(Name = "role_name")]
        public string RoleName { get; set; }


        [DataMember(Name = "role_description")]
        public string RoleDescription { get; set; }

        [DataMember(Name = "create_date")]
        public DateTime CreateDate{ get; set; }
 
        [DataMember(Name = "created_by")]
        public Guid CreatedBy { get; set; }
    }
}
