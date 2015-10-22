using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ValidationAttribute : Dat.V1.Utils.Attributes.UtilsAttribute
    {
        public string FriendlyName { get; set; }
        public Enumerations.Action Action { get; set; }
        public ValidationAttribute(string friendlyName, Enumerations.Action action)
        {
            FriendlyName = friendlyName;
            Action = action;
        }
    }
}
