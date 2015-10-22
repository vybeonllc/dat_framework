using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Security.Exceptions
{
    public class TokenException :SecurityException
    {
          public TokenException() : base() { }
          public TokenException(string message) : base(message) { }
        public TokenException(string message, System.Exception ex) : base(message, ex) { }
    }
}
