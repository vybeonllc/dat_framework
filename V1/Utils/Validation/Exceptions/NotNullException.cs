using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Exceptions
{
    public class NotNullException : ValidationException
    {
        public NotNullException() : base() { }
        public NotNullException(string message) : base(message) { }
        public NotNullException(string message, System.Exception ex) : base(message, ex) { }
        public NotNullException(object friendlyName)
            : base(string.Format(Constants.ExceptionMessages.NotNullException, friendlyName))
        {
        }
    }
}
