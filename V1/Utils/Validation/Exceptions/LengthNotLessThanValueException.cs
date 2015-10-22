using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Exceptions
{
    public class LengthNotLessThanValueException : ValidationException
    {
        public LengthNotLessThanValueException() : base() { }
        public LengthNotLessThanValueException(string message) : base(message) { }
        public LengthNotLessThanValueException(string message, System.Exception ex) : base(message, ex) { }
        public LengthNotLessThanValueException(object friendlyName, object minLength)
            : base(string.Format(Constants.ExceptionMessages.LengthNotLessThanValueException, friendlyName, minLength))
        {
        }
    }
}
