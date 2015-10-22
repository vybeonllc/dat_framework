using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom.SharedObjects
{
    [DataContract()]
    public class HourMinute
    {
        [DataMember(Name = "hour")]
        public int Hour { get; set; }

        [DataMember(Name = "minute")]
        public int Minute { get; set; }
    }
}
