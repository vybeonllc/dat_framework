using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Dto.Bom.Exceptions
{
    public class SerializationNotImplementedException : System.Exception
    {
        public SerializationNotImplementedException() : base() { }
        public SerializationNotImplementedException(string message) : base(message) { }
        public SerializationNotImplementedException(string message, System.Exception ex) : base(message, ex) { }
    }
}
