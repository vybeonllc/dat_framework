using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.Permission
{
    [DataContract()]
    public class Permission
    {

        [DataMember(Name = "permission_id")]
        public int PermissionId { get; set; }

        [DataMember(Name = "permission_name")]
        public string PermissionName { get; set; }


        [DataMember(Name = "permission_description")]
        public string PermissionDescription { get; set; }

        [DataMember(Name = "create_date")]
        public DateTime CreateDate{ get; set; }
 
        [DataMember(Name = "created_by")]
        public Guid CreatedBy { get; set; }
    }
}
