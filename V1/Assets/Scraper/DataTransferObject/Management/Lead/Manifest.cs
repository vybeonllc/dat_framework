using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.Assets.Scraper.V1.Dto.Management.Lead
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "leads")]
        public List<LeadInfo> Leads { get; set; }

        public Manifest()
        {
        }

    }
}
