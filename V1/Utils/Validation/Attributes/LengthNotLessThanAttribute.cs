using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class LengthNotLessThanAttribute : ValidationAttribute
    {
        public int MinimumLength { get; set; }

        public LengthNotLessThanAttribute(string friendlyName, int minimumLength, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, action)
        {
            MinimumLength = minimumLength;
        }
    }
}
