using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DAT.v1.Assets.Developement.DTO.Database.Table
{

    [DataContract()]
    public class Table 
    {
        [DataMember(Name = "database_name")]
        public string DatabaseName { get; set; }

        [DataMember(Name = "table_name")]
        public string TableName { get; set; }
 
    }
}
