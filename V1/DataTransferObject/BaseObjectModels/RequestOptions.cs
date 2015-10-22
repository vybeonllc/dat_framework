using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Dto.Bom
{
    public struct RequestOptions
    {
        public Dat.V1.Utils.Enumerations.DataExchangeFormats RequestType { get; set; }
        public Dat.V1.Utils.Enumerations.DataExchangeFormats ResponseType { get; set; }
        public Dat.V1.Utils.Enumerations.HttpVerbs Method { get; set; }
        public Dat.V1.Utils.Serialization.ISerializable Data { get; set; }


        public string ApiUrl { get; set; }
        public bool Secured { get; set; }
        public string Asset { get; set; }
        public string Service { get; set; }
        public string EndPoint { get; set; }
        public string QueryStrings { get; set; }
        public string Language { get; set; }
        public string Parameters { get; set; }
        public string AuthenticationToken { get; set; }
        public Guid SubscriberAssetGuid { get; set; }
        public int StartIndex { get; set; }
        public int PageSize { get; set; }
        public Bom.Filtering Filters { get; set; }
        public int Timeout { get; set; }
    }
}
