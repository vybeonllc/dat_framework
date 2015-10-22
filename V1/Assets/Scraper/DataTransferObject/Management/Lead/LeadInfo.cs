using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dat.Assets.Scraper.V1.Dto.Management.Lead
{
    [DataContract()]
    public class LeadInfo
    {
        [DataMember(Name =  "lead_id")]
        public long LeadId { get; set; }

        [DataMember(Name = "queue_id")]
        public long QueueId { get; set; }

        [DataMember(Name =  "create_date")]
        public DateTime CreateaDate { get; set; }

        [DataMember(Name =  "slogan")]
        public string Slogan { get; set; }

        [DataMember(Name =  "website")]
        public string Website { get; set; }

        [DataMember(Name =  "creative")]
        public string Creative { get; set; }

        [DataMember(Name =  "phone_number")]
        public long PhoneNumber { get; set; }

        [DataMember(Name =  "address")]
        public string Address { get; set; }

        [DataMember(Name =  "link")]
        public string Link { get; set; }

        [DataMember(Name =  "latitude")]
        public float Latitude { get; set; }

        [DataMember(Name =  "longitude")]
        public float Longitude { get; set; }

    }
}
