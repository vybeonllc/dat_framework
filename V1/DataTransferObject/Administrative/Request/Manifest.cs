using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Administrative.RequestInfo
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "request_info")]
        public RequestInfo Request { get; set; }

        public Manifest()
        {
        }

    }
}
