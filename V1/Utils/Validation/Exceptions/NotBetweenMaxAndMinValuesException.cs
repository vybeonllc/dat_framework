using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Exceptions
{
    public class NotBetweenMaxAndMinValuesException : ValidationException
    {
        public NotBetweenMaxAndMinValuesException() : base() { }
        public NotBetweenMaxAndMinValuesException(string message) : base(message) { }
        public NotBetweenMaxAndMinValuesException(string message, System.Exception ex) : base(message, ex) { }
        public NotBetweenMaxAndMinValuesException(object friendlyName, object minValue, object maxValue)
            : base(string.Format(Constants.ExceptionMessages.NotBetweenMaxAndMinValuesException, friendlyName, minValue, maxValue))
        {
        }
    }
}
