using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Threading.Exceptions
{
    public class ThreadingException : Utils.Exceptions.UtilsException
    {
        public ThreadingException() : base() { }
        public ThreadingException(string message) : base(message) { }
        public ThreadingException(string message, System.Exception ex) : base(message, ex) { }
    }
}
