using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.Framework.Resources
{
    public class Resource
    {

        #region IResource Members


        public string AssetConnectionString { get { return Dat.V1.Utils.Common.ConnectionStrings.AssetConnectionString; } }
        public System.Web.HttpCookieCollection Cookies { get; set; }
        public string EndPoint { get; set; }
        public System.Web.HttpContext Context { get; set; }
        public System.Collections.Specialized.NameValueCollection QueryStrings { get; set; }
        public System.Collections.Specialized.NameValueCollection Headers { get; set; }
        public List<string> Parameters { get; private set; }
        public string PostedData { get { return postedData; } }
        public string AssetCode { get; set; }
        public string Language { get { return Context.Request.Headers.Get("Language"); } }
        public string AuthenticationToken { get { return Context.Request.Headers.Get("HTTP-Auth"); } }
        public Guid SubscriberAssetGuid { get { return Guid.Parse(string.IsNullOrWhiteSpace(Context.Request.Headers.Get("SubscriberAssetGuid")) ? Guid.Empty.ToString() : Context.Request.Headers.Get("SubscriberAssetGuid")); } }
        public Guid AuthenticatedUser { get; set; }
        public string Service { get; set; }
        public string Version { get; set; }
        public string ApiUrl { get; set; }
        public string Result { get; set; }
        public bool IsAnonymous { get; set; }
        public Dat.V1.Dto.Bom.DistributedAssetTechnology DistributedAssetTechnology { get; set; }
        public DateTime CreateDate { get; set; }

        public Uri Url { get; set; }
        public System.IO.Stream DataStream { get; set; }
        public Dat.V1.Utils.Enumerations.DataExchangeFormats ResponseFormat { get; set; }
        public Dat.V1.Utils.Enumerations.DataExchangeFormats RequestFormat { get; set; }
        public Dat.V1.Utils.Enumerations.HttpVerbs Verb { get; set; }
        Dat.V1.Dto.Bom.Filtering filters = new Dat.V1.Dto.Bom.Filtering();
        protected int StartIndex { get { return startindex; } }
        protected int PageSize { get { return pagesize; } }
        public Dat.V1.Dto.Bom.Filtering Filters { get { return filters; } }

        int startindex, pagesize;
        string postedData;

        public string ConnectionString(string connectionName) {  return Dat.V1.Utils.Common.ConnectionStrings.ConnectionString(connectionName);  }

        #endregion IResource Members
        public Resource(System.Web.HttpContext ctx)
        {
            Context = ctx;
            Url = Context.Request.Url;

            List<string> raw = Context.Request.Path.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (raw == null)
                raw = new List<string>();
            //Url.Segments.ToList().ForEach(seg => { string strSeg = seg.Replace("/", string.Empty); if (!string.IsNullOrWhiteSpace(strSeg)) raw.Add(strSeg); });

            SetVerb();
            SetRequestContentType();
            SetResponseFormat();
            SetPostedData();

            Headers = ctx.Request.Headers;
            Cookies = ctx.Request.Cookies;
            DataStream = Context.Request.InputStream;
            Version = raw.Count > 1 ? raw[1].ToLower() : null;
            AssetCode = raw.FirstOrDefault(); ;
            Service = raw.Count > 2 ? raw[2].ToLower() : null;
            EndPoint = raw.Count > 3 ? raw[3].ToLower() : null;
            Parameters = System.Web.HttpUtility.UrlDecode(Context.Request.PathInfo ?? "").Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();// : new List<string>(); //raw.Count > 4 ? raw.GetRange(4, raw.Count - 4).Select(p => System.Web.HttpUtility.UrlDecode(p)).ToList() : new List<string>();
            QueryStrings = Context.Request.QueryString;
            if (!string.IsNullOrWhiteSpace(Version))
                ApiUrl = Url.AbsoluteUri.LeftOf(Context.Request.Path);// Url.AbsoluteUri.LeftOf(Version);

            if (QueryStrings != null)
            {
                if (!string.IsNullOrWhiteSpace(QueryStrings["startindex"]))
                    int.TryParse(QueryStrings["startindex"], out startindex);

                if (!string.IsNullOrWhiteSpace(QueryStrings["pagesize"]))
                    int.TryParse(QueryStrings["pagesize"], out pagesize);

                if (!string.IsNullOrWhiteSpace(QueryStrings["filters"]))
                    filters = Dat.V1.Dto.Bom.Filtering.Parse(QueryStrings["filters"]);
            }
            if (StartIndex < 0) startindex = 0;
            if (PageSize < 1 || PageSize > 100)
                pagesize = 1;
        }
        void SetPostedData()
        {
            byte[] PostData = Context.Request.BinaryRead(Context.Request.ContentLength);
            postedData = Encoding.UTF8.GetString(PostData);
        }
        void SetRequestContentType()
        {
            string strContentType = string.IsNullOrWhiteSpace(Context.Request.ContentType) ? "json" : Context.Request.ContentType;
            if (strContentType.ToLower().Contains("xml"))
                RequestFormat = Dat.V1.Utils.Enumerations.DataExchangeFormats.XML;
            else if (strContentType.ToLower().Contains("json"))
                RequestFormat = Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON;
            else if (strContentType.ToLower().Contains("html"))
                RequestFormat = Dat.V1.Utils.Enumerations.DataExchangeFormats.HTML;
            else RequestFormat = Dat.V1.Utils.Enumerations.DataExchangeFormats.UNKNOWN;
        }
        public System.Data.DataTable FiltersToTableType()
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("DataType", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Value", typeof(string));

            if (Filters.Filters == null) return table;

            Filters.Filters.ForEach(c =>
            {
                if (c.Filters != null)
                {
                    c.Filters.ForEach(f =>
                    {
                        System.Data.DataRow row = table.NewRow();
                        row["Name"] = c.Name;
                        row["DataType"] = string.Empty;
                        row["Type"] = f.Type.ToString();
                        row["Value"] = f.Value;
                        table.Rows.Add(row);
                    });
                }
            });

            return table;
        }
        void SetResponseFormat()
        {
            string strAcceptType = Context.Request.AcceptTypes == null || Context.Request.AcceptTypes.Any(a => a.ToLower().Contains("html")) ? "json" : Context.Request.AcceptTypes.FirstOrDefault() ?? string.Empty;
            if (strAcceptType.ToLower().Contains("json"))
                ResponseFormat = Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON;
            else if (strAcceptType.ToLower().Contains("xml"))
                ResponseFormat = Dat.V1.Utils.Enumerations.DataExchangeFormats.XML;
            else if (strAcceptType.ToLower().Contains("csv"))
                ResponseFormat = Dat.V1.Utils.Enumerations.DataExchangeFormats.CSV;
            else if (strAcceptType.ToLower().Contains("html"))
                ResponseFormat = Dat.V1.Utils.Enumerations.DataExchangeFormats.HTML;
            else ResponseFormat = Dat.V1.Utils.Enumerations.DataExchangeFormats.UNKNOWN;
        }


        void SetVerb()
        {
            switch (Context.Request.HttpMethod.ToLower())
            {
                case "get":
                    Verb = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                    break;
                case "post":
                    Verb = Dat.V1.Utils.Enumerations.HttpVerbs.POST;
                    break;
                case "put":
                    Verb = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                    break;
                case "delete":
                    Verb = Dat.V1.Utils.Enumerations.HttpVerbs.DELETE;
                    break;
                case "head":
                    Verb = Dat.V1.Utils.Enumerations.HttpVerbs.HEAD;
                    break;
                default:
                    Verb = Dat.V1.Utils.Enumerations.HttpVerbs.UNKNOWN;
                    break;
            }
        }
        public void Validate()
        {
            if (Verb == Dat.V1.Utils.Enumerations.HttpVerbs.UNKNOWN)
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "No request method has been set.");
            if (RequestFormat == Dat.V1.Utils.Enumerations.DataExchangeFormats.UNKNOWN)
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Request serialization type has not been set.");
            if (ResponseFormat == Dat.V1.Utils.Enumerations.DataExchangeFormats.UNKNOWN)
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Response serialization type has not been set.");
            if (string.IsNullOrWhiteSpace(AssetCode))
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Asset has not been specified.");

            if (string.IsNullOrWhiteSpace(Service))
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Service has not been specified.");
            if (string.IsNullOrWhiteSpace(EndPoint))
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "End Point has not been specified.");
            if (string.IsNullOrWhiteSpace(Version))
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Version has not been specified.");
            if (Version.ToLower().IndexOf("v") != 0 || Version.Length != 2) // should replace with regular expression matching v1,v2,v3
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Version is not exists.");
        }
        public T DeserializeData<T>() where T : Utils.Serialization.ISerializable, new()
        {
            try
            {
                switch (ResponseFormat)
                {
                    case Utils.Enumerations.DataExchangeFormats.JSON:
                        return Utils.Serialization.JSON.Serializer.Deserialize<T>(PostedData);
                    case Utils.Enumerations.DataExchangeFormats.XML:
                        return Utils.Serialization.XML.Serializer.Deserialize<T>(PostedData);
                    default:
                        throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.NotImplemented, "Deserialization method not supported");
                }
            }
            catch (Framework.Exceptions.HttpException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.InternalServerError, "Deserializing data failed.");
            }
        }
        public void SetResponse<T>(Dat.V1.Dto.Bom.ResponseCollection<T> response)
        {
            if (response == null)
            {
                Result = Context.Response.ToString();
                return;
            }
          
            response.ResultSet.Filtering = Filters;
            response.ResultSet.Paging = new Dto.Bom.Paging()
            {
                PageSize = PageSize,
                StartIndex = StartIndex
            };
            SetResponse<T>((Dat.V1.Dto.Bom.IResponse<T>)response);
        }
        void SetResponse<T>(Dat.V1.Dto.Bom.Response<T> response)
        {
            if (response == null)
            {
                Result = Context.Response.ToString();
                return;
            }

            response.Filtering = Filters;
            SetResponse<T>((Dat.V1.Dto.Bom.IResponse<T>)response);
        }
        public void SetResponse<T>(Dat.V1.Dto.Bom.IResponse<T> response)
        {
            try
            {
                if (DistributedAssetTechnology != null)
                {
                    TimeSpan duration = DateTime.Now.Subtract(Context.Timestamp);
                    DistributedAssetTechnology.RequestInfo.TimeTaken = string.Format("{0}:{1}:{2}.{3}", duration.Hours, duration.Minutes, duration.Seconds, duration.Milliseconds);
                }
                response.DistributedAssetTechnology = DistributedAssetTechnology;
                switch (ResponseFormat)
                {
                    case Dat.V1.Utils.Enumerations.DataExchangeFormats.JSON:
                        Result = response.ToJson();
                        break;
                    case Dat.V1.Utils.Enumerations.DataExchangeFormats.XML:
                        Result = response.ToXml();
                        break;
                    case Dat.V1.Utils.Enumerations.DataExchangeFormats.HTML:
                        Result = response.ToHtml();
                       break;
                    //default:
                    //    throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.NotImplemented, "Serialization method not supported");
                }
            }
            catch (Framework.Exceptions.HttpException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.InternalServerError, "Serializing data failed.");
            }
        }
        public void Flush()
        {
            Context.Response.Write(Result);
            try
            {
                Context.Response.Flush();
            }
            catch (Exception ex)
            {
                Result = ex.ToString();
            }
        }
    }
}
