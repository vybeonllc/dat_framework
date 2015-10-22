using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DAT.v1.Assets.Developement.DTO.Database.Column
{

    [DataContract()]
    public class Column 
    {
        [DataMember(Name = "database_name")]
        public string DatabaseName { get; set; }

        [DataMember(Name = "table_name")]
        public string TableName { get; set; }

        [DataMember(Name = "column_name")]
        public string ColumnName { get; set; }

        [DataMember(Name = "primary_key")]
        public bool PrimaryKey { get; set; }

        [DataMember(Name = "auto_increment")]
        public bool AutoIncrement { get; set; }

        [DataMember(Name = "is_nullable")]
        public bool IsNullable { get; set; }

        [DataMember(Name = "column_type")]
        public string ColumnType { get; set; }

        [DataMember(Name = "ordinal_position")]
        public int OrdinalPosition { get; set; }

    }
}
