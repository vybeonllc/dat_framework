using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Dto.Membership.Authentication
{
    [DataContract()]
    public class Request : Dat.V1.Dto.Bom.Request, Dat.V1.Utils.Serialization.ISerializable
    {
        [DataMember(Name = "manifest")]
        public Manifest Manifest { get; set; }

        public Request()
        {
        }
        public static Dat.V1.Dto.Bom.Response<AuthenticationInfo> Authenticate(string email_address, string password, string api_url, bool secured, Dat.V1.Utils.Enumerations.DataExchangeFormats format)
        {
            Dat.V1.Dto.Bom.RequestOptions options = new Dat.V1.Dto.Bom.RequestOptions()
            {
                AuthenticationToken = "",
                ApiUrl = api_url,
                Data = new Dat.V1.Dto.Membership.Authentication.Request()
                {
                    Manifest = new Manifest()
                    {
                        AuthenticationInfo = new AuthenticationInfo()
                        {
                            EmailAddress = email_address,
                            Password = password
                        }
                    }
                },
                Asset = Constants.Asset,
                Service = Constants.Service,
                EndPoint = Constants.EndPoint,
                Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT,
                Secured = secured,
                RequestType = format,
                ResponseType = format
            };
            switch (format)
            {
                case Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON:
                    return Dat.V1.Utils.Serialization.JSON.Serializer.Deserialize<Dat.V1.Dto.Bom.Response<AuthenticationInfo>>(Dat.V1.Dto.Bom.Request.Send(options));
                default:
                    throw new Dto.Bom.Exceptions.SerializationNotImplementedException("Format not supported.");
            };
        }
        public static Dat.V1.Dto.Bom.Response<AuthenticationInfo> Authenticate(Guid userGuid, string api_url, bool secured, Dat.V1.Utils.Enumerations.DataExchangeFormats format)
        {
            Dat.V1.Dto.Bom.RequestOptions options = new Dat.V1.Dto.Bom.RequestOptions()
            {
                AuthenticationToken = "",
                ApiUrl = api_url,
                Data = new Dat.V1.Dto.Membership.Authentication.Request()
                {
                    Manifest = new Manifest()
                    {
                        AuthenticationInfo = new AuthenticationInfo()
                        {
                            UserGuid = userGuid
                        }
                    }
                },
                Asset = Constants.Asset,
                Service = Constants.Service,
                EndPoint = Constants.EndPoint,
                Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT,
                Secured = secured,
                RequestType = format,
                ResponseType = format
            };
            switch (format)
            {
                case Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON:
                    return Dat.V1.Utils.Serialization.JSON.Serializer.Deserialize<Dat.V1.Dto.Bom.Response<AuthenticationInfo>>(Dat.V1.Dto.Bom.Request.Send(options));
                default:
                    throw new Dto.Bom.Exceptions.SerializationNotImplementedException("Format not supported.");
            };
        }
    }
}
