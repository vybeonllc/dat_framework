using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Services.Administrative.Tracking
{
    public class Request : Dat.V1.Framework.HttpHandlers.Master<Dat.V1.Dto.Administrative.RequestInfo.Request, Dat.V1.Dto.Administrative.RequestInfo.RequestInfo>
    {
        public override void GET()
        {
            base.GET();
            var requests = Dat.V1.BusinessLogic.Request.SelectAll().Select<Dat.V1.BusinessLogic.Request, Dat.V1.Dto.Administrative.RequestInfo.RequestInfo>(c =>
                new Dat.V1.Dto.Administrative.RequestInfo.RequestInfo()
                {
                    AcceptType = c.AcceptType,
                    AssetGuid = Guid.Empty,
                    ContentType = c.ContentType,
                    Cookies = c.Cookies,
                    CreateDate = c.CreateDate,
                    EndPoint = c.EndPoint,
                    Headers = c.Headers,
                    HttpAuth = c.HttpAuth,
                    InputStream = c.InputStream,
                    IpAddress = c.IpAddress,
                    Language = c.Language,
                    Method = c.Method,
                    Parameters = c.Parameters,
                    Referrer = c.Referrer,
                    RequestId = c.RequestId,
                    Result = c.Result,
                    Service = c.Service,
                    Url = c.Url,
                    UserAgent = c.UserAgent,
                    UserGuid = c.UserGuid,
                    Version = c.Version,
                }).ToList();
            SetResponseAsCollection(requests);
        }
    }
}
