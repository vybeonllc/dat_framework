using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.Assets.Scraper.V1.Dto.Management.Queue
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "queue")]
        public QueueInfo Queue { get; set; }

        public Manifest()
        {
        }

    }
}
