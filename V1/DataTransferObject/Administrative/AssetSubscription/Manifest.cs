using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Administrative.AssetSubscription
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "asset_subscription")]
        public AssetSubscription AssetSubscription { get; set; }

        public Manifest()
        {
        }

    }
}
