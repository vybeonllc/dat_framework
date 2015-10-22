using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract()] 
    public class Paging
    {
        [DataMember(Name = "start_index")]
        public int StartIndex { get; set; }

        [DataMember(Name = "page_size")]
        public int PageSize { get; set; }
    }
}
