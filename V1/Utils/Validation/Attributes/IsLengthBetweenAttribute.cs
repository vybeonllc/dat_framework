using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class IsLengthBetweenAttribute : ValidationAttribute
    {
        public long MinimumLength { get; set; }
        public long MaximumLength { get; set; }

        public IsLengthBetweenAttribute(string friendlyName, long minimumLength, long maximumLength, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, action)
        {
            MinimumLength = minimumLength;
            MaximumLength = maximumLength;
        }
    }
}
