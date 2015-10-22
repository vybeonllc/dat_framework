using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Resource.Template
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "template")]
        public Template Template { get; set; }

        public Manifest()
        {
        }

    }
}
