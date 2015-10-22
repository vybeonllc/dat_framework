using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract()]
    public class FilteredColumn
    {
        [DataMember(Name = "name")] 
        public string Name { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "filters")]
        public List<Filter> Filters { get; set; }


    }
}
