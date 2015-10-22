using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Exceptions
{
    public class NotGreaterThanValueException : ValidationException
    {
        public NotGreaterThanValueException() : base() { }
        public NotGreaterThanValueException(string message) : base(message) { }
        public NotGreaterThanValueException(string message, System.Exception ex) : base(message, ex) { }
        public NotGreaterThanValueException(object friendlyName, object maxValue)
            : base(string.Format(Constants.ExceptionMessages.NotGreaterThanValueException, friendlyName, maxValue))
        {
        }
    }
}
