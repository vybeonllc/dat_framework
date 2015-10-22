using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Exceptions
{
    public class LengthNotBetweenMaxAndMinValuesException : ValidationException
    {
        public LengthNotBetweenMaxAndMinValuesException() : base() { }
        public LengthNotBetweenMaxAndMinValuesException(string message) : base(message) { }
        public LengthNotBetweenMaxAndMinValuesException(string message, System.Exception ex) : base(message, ex) { }
        public LengthNotBetweenMaxAndMinValuesException(object friendlyName, object minLength, object maxLength)
            : base(string.Format(Constants.ExceptionMessages.LengthNotBetweenMaxAndMinValuesException, friendlyName, minLength, maxLength))
        {
        }
    }
}
