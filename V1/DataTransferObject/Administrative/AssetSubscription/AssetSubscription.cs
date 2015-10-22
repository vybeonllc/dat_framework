using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Administrative.AssetSubscription
{
    [DataContract()]
    public class AssetSubscription
    {
        [DataMember(Name = "asset_subscription_id")]
        public long AssetSubscriptionId { get; set; }

        [DataMember(Name = "subscriber_asset_guid")]
        public Guid SubscriberAssetGuid { get; set; }

        [DataMember(Name = "publisher_asset_guid")]
        public Guid PublisherAssetGuid { get; set; }

        [DataMember(Name = "create_date")]
        public DateTime CreateDate{ get; set; }
 
        [DataMember(Name = "created_by")]
        public Guid CreatedBy { get; set; }
    }
}
