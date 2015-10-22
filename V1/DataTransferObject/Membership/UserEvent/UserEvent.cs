using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.UserEvent
{
    [DataContract()]
    public class UserEvent
    { 
        [DataMember(Name = "user_event_id")]
        public long UserEventId { get; set; }

        [DataMember(Name = "event_id")]
        public long EventId { get; set; }

        [DataMember(Name = "user_guid")]
        public Guid UserGuid { get; set; }

        [DataMember(Name = "user_event_description")]
        public string UserEventDescription { get; set; }

        [DataMember(Name = "user_event_reference_key")]
        public string UserEventReferenceKey { get; set; }

        [DataMember(Name = "create_date")]
        public DateTime CreateDate { get; set; }
         
    }
}
