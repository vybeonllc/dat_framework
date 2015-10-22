using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Dto.Bom.Exceptions
{
    public class TransportException : BomException
    {
        public TransportException() : base() { }
        public TransportException(string message) : base(message) { }
        public TransportException(string message, System.Exception ex) : base(message, ex) { }
    }
}
