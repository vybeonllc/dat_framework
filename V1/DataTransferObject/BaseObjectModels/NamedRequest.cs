using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract]
    public class NamedRequest : Dat.V1.Dto.Bom.Request, Dat.V1.Utils.Serialization.ISerializable
    {
        [DataContract]
        public class NamedRequestDto
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "data")]
            public string Data { get; set; }
        }
        
        [DataContract]
        public class Manifest
        {
            [DataMember(Name = "named_request")]
            public NamedRequestDto NamedRequestDto { get; set; }
        }

        [DataMember(Name = "manifest")]
        public Manifest manifest { get; set; }

        public NamedRequest()
        {
        }

        public string ToJson()
        {
            return base.ToJson();
        }

        public string ToXml()
        {
            return base.ToXml();
        }

        public string ToHtml()
        {
            return base.ToHtml();
        }

        public static Dat.V1.Dto.Bom.Response<T> ByName<T>(string name, 
            Guid subscriberAssetGuid, 
            string apiUrl, 
            string asset, 
            bool secured, 
            Dat.V1.Utils.Enumerations.HttpVerbs method, 
            Dat.V1.Utils.Serialization.ISerializable data,
            params string[] parameters)
        {          
            Dat.V1.Dto.Bom.RequestOptions options = new Dat.V1.Dto.Bom.RequestOptions()
            {
                AuthenticationToken = String.Empty,
                ApiUrl = apiUrl,
                Asset = asset,
                Service = "api",
                EndPoint = "named_request",
                Method = method,
                Parameters = parameters != null ? String.Join("/", parameters) : String.Empty,
                Data = data,
                Secured = secured,
                RequestType = Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON,
                ResponseType = Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON,
                SubscriberAssetGuid = subscriberAssetGuid
            };


            var RawResult = Dat.V1.Utils.Serialization.JSON.Serializer.Deserialize<Dat.V1.Dto.Bom.Response<string>>(Dat.V1.Dto.Bom.Request.Send(options));

            return new Dat.V1.Dto.Bom.Response<T>()
            {
                Result = RawResult.Status.Code == 200 ? Newtonsoft.Json.JsonConvert.DeserializeObject<T>(RawResult.Status.Message) : default(T),
                Status = RawResult.Status
            };
        }

        public static Dat.V1.Dto.Bom.Response<T> GetByName<T>(string name, Guid subscriberAssetGuid, string apiUrl, string asset, bool secured, params string[] parameters)
        {
            var Parameters = new List<string>(parameters);
            Parameters.Insert(0, name);

            return ByName<T>(name, subscriberAssetGuid, apiUrl, asset, secured, Dat.V1.Utils.Enumerations.HttpVerbs.GET, null, Parameters.ToArray());
        }

        public static Dat.V1.Dto.Bom.Response<T> PostByName<T>(string name, Guid subscriberAssetGuid, string apiUrl, string asset, bool secured, object data)
        {
            return ByName<T>(name, subscriberAssetGuid, apiUrl, asset, secured, Dat.V1.Utils.Enumerations.HttpVerbs.POST, new NamedRequest()
            {
                manifest = new Manifest()
                {
                    NamedRequestDto = new NamedRequestDto()
                    {
                        Data = Newtonsoft.Json.JsonConvert.SerializeObject(data),
                        Name = name
                    }
                }
            }, null);
        }
    }
}
