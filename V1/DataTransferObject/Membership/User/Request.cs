using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Dto.Membership.User
{
    [DataContract()]
    public class Request : Dat.V1.Dto.Bom.Request, Dat.V1.Utils.Serialization.ISerializable
    {
        [DataMember(Name = "manifest")]
        public Manifest Manifest { get; set; }

        public Request()
        {
        }

        public static Dat.V1.Dto.Bom.Response<User> Create(User user, string authToken, string api_url, bool secured, Dat.V1.Utils.Enumerations.DataExchangeFormats format)
        {
            Dat.V1.Dto.Bom.RequestOptions options = new Dat.V1.Dto.Bom.RequestOptions()
            {
                AuthenticationToken = authToken,
                ApiUrl = api_url,
                Data = new Dat.V1.Dto.Membership.User.Request()
                {
                    Manifest = new Manifest()
                    {
                        User = user
                    }
                },
                Secured = secured,
                Asset = Dat.V1.Dto.Membership.Constants.Asset,
                Service = Membership.Constants.Service,
                EndPoint = Constants.EndPoint,
                Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT,
                RequestType = format,
                ResponseType = format
            };
            switch (format)
            {
                case Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON:
                    return Dat.V1.Utils.Serialization.JSON.Serializer.Deserialize<Dat.V1.Dto.Bom.Response<User>>(Dat.V1.Dto.Bom.Request.Send(options));
                default:
                    throw new Dto.Bom.Exceptions.SerializationNotImplementedException("Format not supported.");
            };
        }
        public static Dat.V1.Dto.Bom.Response<User> Update(User user, string authToken, string api_url, bool secured, Dat.V1.Utils.Enumerations.DataExchangeFormats format)
        {
            Dat.V1.Dto.Bom.RequestOptions options = new Dat.V1.Dto.Bom.RequestOptions()
            {
                AuthenticationToken = authToken,
                ApiUrl = api_url,
                Data = new Dat.V1.Dto.Membership.User.Request()
                {
                    Manifest = new Manifest()
                    {
                        User = user
                    }
                },
                Asset = Dat.V1.Dto.Membership.Constants.Asset,
                Service = Membership.Constants.Service,
                EndPoint = Constants.EndPoint,
                Method = Dat.V1.Utils.Enumerations.HttpVerbs.POST,
                Secured = secured,
                RequestType = format,
                ResponseType = format
            };
            switch (format)
            {
                case Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON:
                    return Dat.V1.Utils.Serialization.JSON.Serializer.Deserialize<Dat.V1.Dto.Bom.Response<User>>(Dat.V1.Dto.Bom.Request.Send(options));
                default:
                    throw new Dto.Bom.Exceptions.SerializationNotImplementedException("Format not supported.");
            };
        }
        public static Dat.V1.Dto.Bom.Response<User> Select_ByEmailAddress(string emailAddress, string authToken, bool secured, string api_url, Dat.V1.Utils.Enumerations.DataExchangeFormats format)
        {
            Dat.V1.Dto.Bom.RequestOptions options = new Dat.V1.Dto.Bom.RequestOptions()
            {
                AuthenticationToken = authToken,
                ApiUrl = api_url,
                Parameters = emailAddress,
                Asset = Dat.V1.Dto.Membership.Constants.Asset,
                Service = Membership.Constants.Service,
                EndPoint = Constants.EndPoint,
                Secured = secured,
                Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET,
                RequestType = format,
                ResponseType = format
            };
            switch (format)
            {
                case Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON:
                    return Dat.V1.Utils.Serialization.JSON.Serializer.Deserialize<Dat.V1.Dto.Bom.Response<User>>(Dat.V1.Dto.Bom.Request.Send(options));
                default:
                    throw new Dto.Bom.Exceptions.SerializationNotImplementedException("Format not supported.");
            };
        }
        public static Dat.V1.Dto.Bom.Response<User> Select_ByGuid(Guid guid, string authToken, string api_url, bool secured, Dat.V1.Utils.Enumerations.DataExchangeFormats format)
        {
            Dat.V1.Dto.Bom.RequestOptions options = new Dat.V1.Dto.Bom.RequestOptions()
            {
                AuthenticationToken = authToken,
                ApiUrl = api_url,
                Parameters = guid.ToString(),
                Asset = Dat.V1.Dto.Membership.Constants.Asset,
                Service = Membership.Constants.Service,
                EndPoint = Constants.EndPoint,
                Secured = secured,
                Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET,
                RequestType = format,
                ResponseType = format
            };
            switch (format)
            {
                case Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON:
                    return Dat.V1.Utils.Serialization.JSON.Serializer.Deserialize<Dat.V1.Dto.Bom.Response<User>>(Dat.V1.Dto.Bom.Request.Send(options));
                default:
                    throw new Dto.Bom.Exceptions.SerializationNotImplementedException("Format not supported.");
            };
        }
    }
}
