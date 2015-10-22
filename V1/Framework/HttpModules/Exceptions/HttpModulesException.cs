using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.HttpModules.Exceptions
{
    public class HttpModulesException : Framework.Exceptions.HttpException
    {

        public HttpModulesException(string message)
            : base(message)
        {
            StatusCode = System.Net.HttpStatusCode.InternalServerError;
        }
        public HttpModulesException(System.Net.HttpStatusCode status)
            : base(status.ToString())
        {
            StatusCode = status;
        }
        public HttpModulesException(System.Net.HttpStatusCode status, string message)
            : base(message)
        {
            StatusCode = status;
        }
        public HttpModulesException(System.Net.HttpStatusCode status, System.Exception ex)
            : base(status, status.ToString(), ex)
        {
        }
        public HttpModulesException(System.Net.HttpStatusCode status, string message, System.Exception ex)
            : base(status, message, ex)
        {
            StatusCode = status;
        }
    }
}
