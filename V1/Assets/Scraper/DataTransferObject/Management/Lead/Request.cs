using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dat.Assets.Scraper.V1.Dto.Management.Lead
{
    [DataContract()]
    public class Request : Dat.V1.Dto.Bom.Request, Dat.V1.Utils.Serialization.ISerializable
    {
        [DataMember(Name = "manifest")]
        public Manifest Manifest { get; set; }

        public Request()
        {
        }

      
    }
}
