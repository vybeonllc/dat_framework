using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Exceptions
{
    public class FrameworkException : System.Exception
    {
        public FrameworkException() : base() { }
        public FrameworkException(string message) : base(message) { }
        public FrameworkException(string message, System.Exception ex) : base(message, ex) { }
    }
}
