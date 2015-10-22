using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Administrative.Asset
{
    [DataContract()]
    public class Asset
    {
        [DataMember(Name = "asset_guid")]
        public Guid AssetGuid { get; set; }

        [DataMember(Name = "asset_name")]
        public string AssetName { get; set; }

        [DataMember(Name = "asset_code")]
        public string AssetCode { get; set; }
 
        [DataMember(Name = "create_date")]
        public DateTime CreateDate{ get; set; }
 
        [DataMember(Name = "created_by")]
        public Guid CreatedBy { get; set; }
    }
}
