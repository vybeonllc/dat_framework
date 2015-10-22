using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Administrative.RequestInfo
{
    [DataContract()]
    public class RequestInfo
    {

        [DataMember(Name = "request_id")]
        public long RequestId { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; }

        [DataMember(Name = "asset_guid")]
        public Guid AssetGuid{ get; set; }

        [DataMember(Name = "service")]
        public string Service{ get; set; }

        [DataMember(Name = "end_point")]
        public string EndPoint { get; set; }

        [DataMember(Name = "parameters")]
        public string Parameters { get; set; }

        [DataMember(Name = "method")]
        public string Method { get; set; }

        [DataMember(Name = "user_guid")]
        public Guid UserGuid { get; set; }

        [DataMember(Name = "ip_address")]
        public string IpAddress { get; set; }

        [DataMember(Name = "url")]
        public string Url{ get; set; }

        [DataMember(Name = "headers")]
        public string Headers { get; set; }

        [DataMember(Name = "http_auth")]
        public string HttpAuth{ get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "referrer")]
        public string Referrer { get; set; }

        [DataMember(Name = "user_agent")]
        public string UserAgent { get; set; }

        [DataMember(Name = "cookies")]
        public string Cookies{ get; set; }

        [DataMember(Name = "input_stream")]
        public string InputStream { get; set; }

        [DataMember(Name = "content_type")]
        public string ContentType{ get; set; }

        [DataMember(Name = "accept_type")]
        public string AcceptType { get; set; }

        [DataMember(Name = "result")]
        public string Result { get; set; }

        [DataMember(Name = "create_date")]
        public DateTime CreateDate{ get; set; }
 
    }
}
