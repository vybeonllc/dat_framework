using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.DataLayer.Exceptions
{
    public class DataLayerException : System.Exception
    {
        public DataLayerException() : base() { }
        public DataLayerException(string message) : base(message) { }
        public DataLayerException(string message, System.Exception ex) : base(message, ex) { }
    }
}
