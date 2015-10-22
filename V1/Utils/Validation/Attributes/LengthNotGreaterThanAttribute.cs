using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class LengthNotGreaterThanAttribute : ValidationAttribute
    {
        public int MaximumLength { get; set; }

        public LengthNotGreaterThanAttribute(string friendlyName, int maximumLength, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, action)
        {
            MaximumLength = maximumLength;
        }
    }
}
