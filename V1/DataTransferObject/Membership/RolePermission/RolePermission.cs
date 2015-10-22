using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.RolePermission
{
    [DataContract()]
    public class RolePermission
    {
        [DataMember(Name = "role_permission_id")]
        public long RolePermissionId { get; set; }

        [DataMember(Name = "role_id")]
        public int RoleId { get; set; }

        [DataMember(Name = "permission_id")]
        public int PermissionId { get; set; }
 
        [DataMember(Name = "create_date")]
        public DateTime CreateDate{ get; set; }
 
        [DataMember(Name = "created_by")]
        public Guid CreatedBy { get; set; }
    }
}
