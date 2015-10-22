using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Exceptions
{
    public class NotLessThanValueException : ValidationException
    {
        public NotLessThanValueException() : base() { }
        public NotLessThanValueException(string message) : base(message) { }
        public NotLessThanValueException(string message, System.Exception ex) : base(message, ex) { }
        public NotLessThanValueException(object friendlyName, object minValue)
            : base(string.Format(Constants.ExceptionMessages.NotLessThanValueException, friendlyName, minValue))
        {
        }
    }
}
