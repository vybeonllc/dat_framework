using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.Framework.HttpHandlers.Resource
{
    public class Template : Master<Dto.Resource.Template.Request, Dto.Resource.Template.Template>
    {

        public override void GET(Guid Parameter)
        {
            base.GET(Parameter);
            string path = Resource.Context.Server.MapPath("~/templates/" + Parameter + ".template");
            if (System.IO.File.Exists(path))
            {
                RequestHandled = true;
                Resource.Context.Response.Write(System.IO.File.ReadAllText(path));
            }
            else
                throw new Dat.V1.Framework.Exceptions.HttpException(System.Net.HttpStatusCode.NotFound, "Template Not Found");
        }
    }
}
