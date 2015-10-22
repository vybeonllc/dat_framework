using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Documentation.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = true)]
    public class MemberInfoAttribute : DocumentationAttribute
    {
        public string FullName { get; set; }
        public string Summary { get; set; }
        public string Remark { get; set; }
    }
}
