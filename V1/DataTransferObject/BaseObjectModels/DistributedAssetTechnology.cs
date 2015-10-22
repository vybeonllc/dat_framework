using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Dto.Bom
{
    [DataContract()]
    public class DistributedAssetTechnology
    {
        [DataMember(Name = "copyright")]
        public string Copyright { get; set; }

        [DataMember(Name = "version")] 
        public string Version { get; set; }

        [DataMember(Name = "logo")] 
        public string Logo { get; set; }

        [DataMember(Name = "request_info")]
        public RequestInfo RequestInfo { get; set; }

    }
}
