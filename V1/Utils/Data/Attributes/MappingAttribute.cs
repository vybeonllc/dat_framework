using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Data.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MappingAttribute : DataAttribute
    {
        public string TargetName { get; set; }
        public object DefaultValue { get; set; }
        public bool IsPrimaryKey { get; set; }
    }
}
