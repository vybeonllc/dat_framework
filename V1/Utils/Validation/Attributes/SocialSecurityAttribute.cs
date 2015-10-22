using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class SocialSecurityAttribute : RegularExpressionAttribute
    {
        public bool ClearNumber{get;set;}
        public SocialSecurityAttribute(string friendlyName, bool isClearNumber, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, Constants.RegularExpressionPatterns.SocialSecurity, System.Text.RegularExpressions.RegexOptions.None, false, action)
        {
            if (isClearNumber)
                Pattern = Constants.RegularExpressionPatterns.SocialSecurity_Clear;
            else
                Pattern = Constants.RegularExpressionPatterns.SocialSecurity;
        }
    }
}
