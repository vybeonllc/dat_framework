using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Administrative.Asset
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "asset")]
        public Asset Asset { get; set; }

        public Manifest()
        {
        }

    }
}
