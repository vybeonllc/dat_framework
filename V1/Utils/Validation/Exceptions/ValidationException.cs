using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Exceptions
{
    public class ValidationException : Utils.Exceptions.UtilsException
    {
        public ValidationException() : base() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, System.Exception ex) : base(message, ex) { }
    }
}
