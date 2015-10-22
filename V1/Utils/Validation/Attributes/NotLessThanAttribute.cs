using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class NotLessThanAttribute : ValidationAttribute
    {
        public long MinimumValue { get; set; }

        public NotLessThanAttribute(string friendlyName, long minimumValue, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, action)
        {
            MinimumValue = minimumValue;
        }
    }
}
