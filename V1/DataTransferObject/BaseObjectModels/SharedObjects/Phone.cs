using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Dat.V1.Dto.Bom.SharedObjects
{
    [DataContract()]
    public class Phone
    {
        /// <summary>
        /// ID of the Phone.
        /// </summary>
        [DataMember(Name = "id")] 
        public long ID { get; set; }

        /// <summary>
        /// Number of the Phone.
        /// </summary>
        [DataMember(Name = "number")] 
        public long Number { get; set; }

        /// <summary>
        /// Extension of the Phone.
        /// </summary>
        [DataMember(Name = "ext")] 
        public string Ext { get; set; }

        /// <summary>
        /// Type of the Phone.
        /// </summary>
        [DataMember(Name = "type")] 
        public string Type { get; set; }

    }
}
