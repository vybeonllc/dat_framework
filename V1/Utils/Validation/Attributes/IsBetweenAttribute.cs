using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class IsBetweenAttribute : ValidationAttribute
    {
        public long MinimumValue { get; set; }
        public long MaximumValue { get; set; }

        public IsBetweenAttribute(string friendlyName, long minimumValue, long maximumValue, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, action)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
        }
    }
}
