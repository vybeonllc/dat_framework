using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class NegativeNumberAttribute : RegularExpressionAttribute
    {
        public NegativeNumberAttribute(string friendlyName, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, Constants.RegularExpressionPatterns.NegativeNumber, System.Text.RegularExpressions.RegexOptions.None, false, action)
        {

        }
    }
}
