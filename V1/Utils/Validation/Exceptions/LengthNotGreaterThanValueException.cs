using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Exceptions
{
    public class LengthNotGreaterThanValueException : ValidationException
    {
        public LengthNotGreaterThanValueException() : base() { }
        public LengthNotGreaterThanValueException(string message) : base(message) { }
        public LengthNotGreaterThanValueException(string message, System.Exception ex) : base(message, ex) { }
        public LengthNotGreaterThanValueException(object friendlyName, object maxLength)
            : base(string.Format(Constants.ExceptionMessages.LengthNotGreaterThanValueException, friendlyName, maxLength))
        {
        }
    }
}
