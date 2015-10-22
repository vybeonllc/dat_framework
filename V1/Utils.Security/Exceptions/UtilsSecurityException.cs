using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Security.Exceptions
{
    public class SecurityException : Dat.V1.Utils.Exceptions.UtilsException
    {
        public SecurityException() : base() { }
        public SecurityException(string message) : base(message) { }
        public SecurityException(string message, System.Exception ex) : base(message, ex) { }
    }
}
