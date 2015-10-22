using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class RegularExpressionAttribute : ValidationAttribute
    {
        public string Pattern { get; set; }
        public System.Text.RegularExpressions.RegexOptions Options { get; set; }
        public bool EscapeCharacters { get; set; }
        public RegularExpressionAttribute(string friendlyName, string pattern, System.Text.RegularExpressions.RegexOptions options, bool escapeCharacters, Enumerations.Action action = Enumerations.Action.All)
            : base(friendlyName, action)
        {
            Pattern = pattern;
            Options = options;
            EscapeCharacters = escapeCharacters;
        }
    }
}
