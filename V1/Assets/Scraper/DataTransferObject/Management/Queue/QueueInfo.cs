using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.Assets.Scraper.V1.Dto.Management.Queue
{
    [DataContract()]
    public class QueueInfo
    {
        [DataMember(Name = "queue_id")]
        public long QueueId { get; set; }

        [DataMember(Name = "server_id")]
        public int ServerId { get; set; }

        [DataMember(Name = "error")]
        public int Error { get; set; }

        [DataMember(Name = "priority")]
        public int Priority { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "enqueued_on")]
        public DateTime EnqueuedOn { get; set; }

        [DataMember(Name = "dequeued_on")]
        public DateTime DequeuedOn { get; set; }

        [DataMember(Name = "started_on")]
        public DateTime StartedOn { get; set; }

        [DataMember(Name = "downloaded_on")]
        public DateTime DownloadedOn { get; set; }
 
        [DataMember(Name = "parsed_on")]
        public DateTime ParsedOn { get; set; }

        [DataMember(Name = "finished_on")]
        public DateTime FinishedOn { get; set; }

        [DataMember(Name = "attempt")]
        public int Attempt { get; set; }

        [DataMember(Name = "results")]
        public int Results { get; set; } 

        [DataMember(Name = "keyword")]
        public string Keyword { get; set; }

        [DataMember(Name = "keyword_id")]
        public int KeywordId { get; set; }

        [DataMember(Name = "location_id")]
        public int LocationId { get; set; }

        [DataMember(Name = "location_type")]
        public int LocationType { get; set; }

        [DataMember(Name = "location")]
        public string Location { get; set; }
    }
}
