using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ZipCodeAttribute : RegularExpressionAttribute
    {
        public bool IsClear { get; set; }
        public ZipCodeAttribute(string friendlyName, bool isClear, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, Constants.RegularExpressionPatterns.ZipCode, System.Text.RegularExpressions.RegexOptions.None, false, action)
        {
            IsClear = isClear;
            if (IsClear)
                Pattern = Constants.RegularExpressionPatterns.ZipCode_Clear;
            else
                Pattern = Constants.RegularExpressionPatterns.ZipCode;
        }
    }
}
