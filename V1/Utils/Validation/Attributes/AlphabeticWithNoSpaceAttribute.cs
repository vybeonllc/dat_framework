using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class AlphabeticWithNoSpaceAttribute : RegularExpressionAttribute
    {
        public AlphabeticWithNoSpaceAttribute(string friendlyName, Enumerations.Action action= Enumerations.Action.All)
            : base(friendlyName, Constants.RegularExpressionPatterns.AlphabeticWithOutSpace, System.Text.RegularExpressions.RegexOptions.None, false,action)
        {

        }
    }
}
