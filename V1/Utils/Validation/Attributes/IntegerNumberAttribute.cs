using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class IntegerNumberAttribute : RegularExpressionAttribute
    {
        public IntegerNumberAttribute(string friendlyName, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, Constants.RegularExpressionPatterns.IntegerNumbers, System.Text.RegularExpressions.RegexOptions.None, false, action)
        {

        }
    }
}
