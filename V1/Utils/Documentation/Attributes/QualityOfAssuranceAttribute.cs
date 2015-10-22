using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Documentation.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = true)]
    public class QualityOfAssuranceAttribute : DocumentationAttribute
    {
        public string[] ReviewingBy { get; set; }
        public string[] AssigningTo { get; set; }
        public string Date { get; set; }
        public Enumerations.QualityOfAssuranceStatus Status { get; set; }
        public string Note { get; set; }
    }
}
