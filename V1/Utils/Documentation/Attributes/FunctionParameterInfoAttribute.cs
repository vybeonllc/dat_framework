using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Documentation.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = true)]
    public class FunctionParameterInfoAttribute : DocumentationAttribute
    {
        public string Name { get; set; }
        public string FriendlyName { get; set; }
        public Type Type { get; set; }
        public string Description { get; set; }
    }
}
