using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.BusinessLogic.Exceptions
{
    public class BusinessLogicException : System.Exception
    {
        public BusinessLogicException() : base() { }
        public BusinessLogicException(string message) : base(message) { }
        public BusinessLogicException(string message, System.Exception ex) : base(message, ex) { }
    }
}
