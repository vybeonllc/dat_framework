using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Exceptions
{
    public class EndPointException : FrameworkException
    {
        public System.Net.HttpStatusCode StatusCode { get { return System.Net.HttpStatusCode.InternalServerError; } }

        public EndPointException()
            : base("Unexpected error while retrieving data.")
        {

        }


        public EndPointException(System.Exception ex)
            : base(ex.Message, ex)
        {

        }

    }
}
