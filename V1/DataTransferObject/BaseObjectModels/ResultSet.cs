using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract()] 
    public class ResultSet<T>
    {
        [DataMember(Name = "results")]
        public List<T> Results { get; set; }

        [DataMember(Name = "paging")]
        public Paging Paging { get; set; }

        [DataMember(Name = "filtering")]
        public Filtering Filtering { get; set; }

        [DataMember(Name = "returned_results")]
        public int ReturnedResults { get; set; }

        [DataMember(Name = "total_results")]
        public int TotalResults { get; set; }
    }
   
}