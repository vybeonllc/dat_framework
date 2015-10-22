using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Administrative.AssetRole
{
    [DataContract()]
    public class AssetRole
    {
        [DataMember(Name = "asset_role_id")]
        public int AssetRoleId { get; set; }

        [DataMember(Name = "asset_guid")]
        public Guid AssetGuid { get; set; }

        [DataMember(Name = "role_id")]
        public int RoleId { get; set; }
 
        [DataMember(Name = "create_date")]
        public DateTime CreateDate{ get; set; }
 
        [DataMember(Name = "created_by")]
        public Guid CreatedBy { get; set; }
    }
}
