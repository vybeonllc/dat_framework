using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT.v1.Assets.Developement.ValueObject
{
    public class Column
    {
        public string DatabaseName { get; set; }
        public string TableName { get; set; }
        public bool PrimaryKey { get; set; }
        public bool AutoIncrement { get; set; }
        public bool IsNullable { get; set; }
        public string ColumnType { get; set; }
        public int OrdinalPosition{ get; set; }
        public string ColumnName { get; set; }
    }
}
