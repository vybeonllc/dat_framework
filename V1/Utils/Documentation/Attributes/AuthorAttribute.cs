using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Documentation.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = true)]
    public class AuthorAttribute : DocumentationAttribute
    {
        public string[] Name { get; set; }
        public double Version { get; set; }
        public string Date { get; set; }
    }
}
