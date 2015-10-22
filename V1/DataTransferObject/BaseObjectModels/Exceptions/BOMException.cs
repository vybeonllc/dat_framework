using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Dto.Bom.Exceptions
{
    public class BomException : System.Exception
    {
        public BomException() : base() { }
        public BomException(string message) : base(message) { }
        public BomException(string message, System.Exception ex) : base(message, ex) { }
    }
}
