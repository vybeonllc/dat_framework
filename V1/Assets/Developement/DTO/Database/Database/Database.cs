﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DAT.v1.Assets.Developement.DTO.Database.Database
{

    [DataContract()]
    public class Database 
    {
        [DataMember(Name = "database_name")]
        public string DatabaseName { get; set; }
 
    }
}
