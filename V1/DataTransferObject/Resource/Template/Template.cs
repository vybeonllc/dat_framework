using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Resource.Template
{
    [DataContract()]
    public class Template
    {
        [DataMember(Name = "template_name")]
        public string TemplateName { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

    }
}
