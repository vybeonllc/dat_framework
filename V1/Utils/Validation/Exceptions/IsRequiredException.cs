using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Exceptions
{
    public class IsRequiredException : ValidationException
    {
        public IsRequiredException() : base() { }
        public IsRequiredException(string message) : base(message) { }
        public IsRequiredException(string message, System.Exception ex) : base(message, ex) { }
        public IsRequiredException(object friendlyName)
            : base(string.Format(Constants.ExceptionMessages.IsRequiredException, friendlyName))
        {
        }
    }
}
