using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Exceptions
{
    public class HttpException : FrameworkException
    {
        public System.Net.HttpStatusCode StatusCode { get; set; }

        public HttpException(string message)
            : base(message)
        {
            StatusCode = System.Net.HttpStatusCode.InternalServerError;
        }
        public HttpException(System.Net.HttpStatusCode status)
            : base(status.ToString())
        {
            StatusCode = status;
        }
        public HttpException(System.Net.HttpStatusCode status, string message)
            : base(message)
        {
            StatusCode = status;
        }
        public HttpException(System.Net.HttpStatusCode status, System.Exception ex)
            : this(status, status.ToString(), ex)
        {
        }
        public HttpException(System.Net.HttpStatusCode status, string message, System.Exception ex)
            : base(message, ex)
        {
            StatusCode = status;
        }

    }
}
