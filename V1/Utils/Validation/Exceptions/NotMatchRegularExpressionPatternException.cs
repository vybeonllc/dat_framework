using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Exceptions
{
    public class NotMatchRegularExpressionPatternException : ValidationException
    {
        public NotMatchRegularExpressionPatternException() : base() { }
        public NotMatchRegularExpressionPatternException(string message) : base(message) { }
        public NotMatchRegularExpressionPatternException(string message, System.Exception ex) : base(message, ex) { }
        public NotMatchRegularExpressionPatternException(object friendlyName)
            : base(string.Format(Constants.ExceptionMessages.NotMatchRegularExpressionPatternException, friendlyName))
        {
        }
    }
}
