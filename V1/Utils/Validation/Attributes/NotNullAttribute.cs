using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class NotNullAttribute : ValidationAttribute
    {
        public NotNullAttribute(string friendlyName, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, action)
        {
            
        }
    }
}
