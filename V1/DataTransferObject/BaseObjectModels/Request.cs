using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract(Name = "request")]
    public class Request : Dat.V1.Utils.Serialization.ISerializable
    {
        [System.Xml.Serialization.XmlIgnore]
        protected RequestOptions Options;
        public Request()
        {
        }

        public Request(RequestOptions options)
        {
            Options = options;
        }


        public string ToJson()
        {
            return Dat.V1.Utils.Serialization.JSON.Serializer.Serialize(this);
        }

        public string ToXml()
        {
            return Dat.V1.Utils.Serialization.XML.Serializer.Serialize(this);
        }
        public string ToCSV()
        {
            return Dat.V1.Utils.Serialization.CSV.Serializer.Serialize(this);
        }
        public string ToHtml()
        {
            return Dat.V1.Utils.Serialization.HTML.Serializer.Serialize(this);
        }
        public static string Send(RequestOptions options)
        {
            bool hasContet = options.Method == Dat.V1.Utils.Enumerations.HttpVerbs.POST || options.Method == Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
            string queryString = ("startindex=" + options.StartIndex.ToString() + "&pagesize=" + options.PageSize.ToString() + "&filters=" + (options.Filters == null ? "" : options.Filters.ToString()) + "&" + options.QueryStrings).Trim('&');
            string url = "http" + (options.Secured ? "s" : "") + "://" + options.ApiUrl + "/" + (options.Asset ?? Constants.Asset) + "/" + Constants.Version + "/" + (options.Service ?? Constants.Service) + "/" + options.EndPoint + "/" + (options.Parameters ?? string.Empty) + "?" + queryString;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Headers.Add("HTTP-Auth", options.AuthenticationToken);
            if (options.SubscriberAssetGuid != Guid.Empty)
                req.Headers.Add("SubscriberAssetGuid", options.SubscriberAssetGuid.ToString());
            req.Headers.Add("Language", string.IsNullOrWhiteSpace(options.Language) ? Utils.Localization.Contants.SupportedLanguages.English_US : options.Language);
            req.Method = options.Method.ToString();
            req.ContentType = options.RequestType.ToString();
            req.Accept = options.ResponseType.ToString();
            if (options.Timeout != 0)
                req.Timeout = options.Timeout;

            if (hasContet && options.Data != null)
            {
                //new Utils.Validation.Validators.Validator(options.Data, options.Method == Utils.Enumerations.HttpVerbs.PUT
                //    ? Utils.Validation.Enumerations.Action.Create
                //    : Utils.Validation.Enumerations.Action.Update);
                string data = string.Empty;
                switch (options.RequestType)
                {
                    case Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON:
                        data = options.Data.ToJson();
                        break;
                    case Dat.V1.Utils.Enumerations.DataExchangeFormats.XML:
                        data = options.Data.ToXml();
                        break;
                    case Dat.V1.Utils.Enumerations.DataExchangeFormats.CSV:
                        data = options.Data.ToCSV();
                        break;
                    case Dat.V1.Utils.Enumerations.DataExchangeFormats.HTML:
                        data = options.Data.ToHtml();
                        break;
                    default:
                        throw new Exceptions.SerializationNotImplementedException("Format not supported.");
                }
                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                Stream dataStream = req.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }
            string strResponse = string.Empty;
            try
            {
                WebResponse res = req.GetResponse();
                using (System.IO.Stream stream = res.GetResponseStream())
                    strResponse = new System.IO.StreamReader(stream, Encoding.UTF8).ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exceptions.TransportException("Something happened on the target server.", ex);
            }
            return strResponse;
        }



    }
}