using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract()] 
    public class Result 
    {
        [DataMember(Name = "item")]
        public Dat.V1.Utils.Serialization.ISerializable Item { get; set; }

        [DataMember(Name = "index")]
        public int index { get; set; }

    }
}