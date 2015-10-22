using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class PasswordAttribute : RegularExpressionAttribute
    {
        public Enumerations.PasswordType PasswordType { get; set; }
        public PasswordAttribute(string friendlyName, Enumerations.PasswordType passwordType, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, Constants.RegularExpressionPatterns.BadPassword, System.Text.RegularExpressions.RegexOptions.None, false, action)
        {
            PasswordType = passwordType;
            switch (PasswordType)
            {
                case Enumerations.PasswordType.Best:
                    Pattern = Constants.RegularExpressionPatterns.BestPassword;
                    break;
                case Enumerations.PasswordType.Strong:
                    Pattern = Constants.RegularExpressionPatterns.StrongPassword;
                    break;
                case Enumerations.PasswordType.Weak:
                    Pattern = Constants.RegularExpressionPatterns.WeakPassword;
                    break;
            };
        }
    }
}
