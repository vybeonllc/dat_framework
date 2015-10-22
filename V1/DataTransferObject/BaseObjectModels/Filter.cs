using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract()] 
    public class Filter
    {
        [DataMember(Name="type")] 
        public FilterTypes Type { get; set; }

        [DataMember(Name = "value")] 
        public string Value { get; set; }
    }
}
