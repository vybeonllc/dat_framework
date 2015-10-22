using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom.SharedObjects
{
    [DataContract()]
    public class LatLong
    {
        [DataMember(Name = "lat")] 
        public float Latitude { get; set; }

        [DataMember(Name = "lng")] 
        public float Longitude { get; set; }
    }
}
