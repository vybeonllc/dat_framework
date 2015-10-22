using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract()]
    public class HelloMonkey
    {
        [DataMember(Name = "version")] 
        public string Version { get { return "Version 1.0"; } set { } }

        [DataMember(Name = "author")] 
        public string Author { get { return "29Prime"; } set { } }

        [DataMember(Name = "published_date")] 
        public string PublishedDate { get { return "December 9, 2014"; } set { } }
    }
}
