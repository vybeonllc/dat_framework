using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.Subscription
{
    [DataContract()]
    public class Subscription
    {

        [DataMember(Name = "subscription_id")]
        public long SubscriptionId { get; set; }

        [DataMember(Name = "user_guid")]
        public Guid UserGuid { get; set; }


        [DataMember(Name = "asset_guid")]
        public Guid AssetGuid { get; set; }

        [DataMember(Name = "create_date")]
        public DateTime CreateDate { get; set; }

        [DataMember(Name = "created_by")]
        public Guid CreatedBy { get; set; }

     
    }
}
