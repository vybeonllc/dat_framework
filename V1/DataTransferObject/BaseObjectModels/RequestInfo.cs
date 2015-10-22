using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Dto.Bom
{
    [DataContract()]
    public class RequestInfo
    {
        [DataMember(Name = "time_taken")]
        public string TimeTaken { get; set; }

        [DataMember(Name = "trace_id")]
        public string TraceId { get; set; }

        [DataMember(Name = "end_point")]
        public string EndPoint { get; set; }

        [DataMember(Name = "Service")]
        public string Service { get; set; }

        [DataMember(Name = "asset")]
        public string Asset { get; set; }

        [DataMember(Name = "asset_description")]
        public string AssetDescription { get; set; }

        [DataMember(Name = "asset_version")]
        public string AssetVersion { get; set; }

        [DataMember(Name = "request_format")]
        public string RequestFormat { get; set; }

        [DataMember(Name = "response_format")]
        public string ResponseFormat { get; set; }

        [DataMember(Name = "asset_logo")]
        public string AssetLogo { get; set; }
    }
}
