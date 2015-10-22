using System;
using System.Web;
using System.Collections.Generic;

namespace Dat.V1.Framework.HttpHandlers
{
    [Dat.V1.Utils.Documentation.Attributes.MemberInfo(FullName = "Master HttpHandler", Summary = "Defined the base handler class for framework.")]
    public class Master<TINCOMING, TOUTGOING> : IHttpHandler where TINCOMING : class,  Dat.V1.Utils.Serialization.ISerializable, new()
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members
        private string assetConnectionString;
        public string AssetConnectionString { get { return assetConnectionString; } }
        public Resources.Resource Resource { get { return (Resources.Resource)HttpContext.Current.Items["Resource"]; } private set { HttpContext.Current.Items["Resource"] = value; } }
        protected bool RequestHandled;
        public virtual bool MetRequirments()
        {
            return true;
        }
        public virtual bool IsReusable
        {
            get { return true; }
        }
        Dictionary<string, List<Dto.Bom.Filter>> filters = null;
        public Dictionary<string, List<Dat.V1.Dto.Bom.Filter>> Filters
        {
            get
            {
                if (filters != null) return filters;
                filters = new Dictionary<string, List<Dto.Bom.Filter>>();
                if (Resource.Filters == null || Resource.Filters.Filters == null)
                    return filters;
                Resource.Filters.Filters.ForEach(filter => filters.Add(filter.Name, filter.Filters));
                return filters;
            }
        }
        public string ConnectionString(string connectionName)
        {
            return Resource.ConnectionString(connectionName);
        }

        public void SetResponse()
        {
            SetResponse(string.Empty);
        }
        public void SetResponse(string message)
        {
            TOUTGOING __null = default(TOUTGOING);
            SetResponse(__null, message);
        }
        public void SetResponse(System.Net.HttpStatusCode code)
        {
            TOUTGOING __null = default(TOUTGOING);
            SetResponse(__null, code);
        }
        public void SetResponse(System.Net.HttpStatusCode code, string message)
        {
            TOUTGOING __null = default(TOUTGOING);
            SetResponse(__null, code, message);
        }
        public void SetResponse(TOUTGOING item)
        {
            SetResponse(item, System.Net.HttpStatusCode.OK);
        }
        public void SetResponse(TOUTGOING item, string message)
        {
            SetResponse(item, new Dat.V1.Dto.Bom.StatusInfo(System.Net.HttpStatusCode.OK) { Message = message });
        }

        public void SetResponse(TOUTGOING item, System.Net.HttpStatusCode code)
        {
            SetResponse(item, new Dat.V1.Dto.Bom.StatusInfo(code));
        }
        public void SetResponse(TOUTGOING item, System.Net.HttpStatusCode code, string message)
        {
            SetResponse(item, new Dat.V1.Dto.Bom.StatusInfo(code) { Message = message });
        }
        public void SetResponse(TOUTGOING item, Dat.V1.Dto.Bom.StatusInfo status)
        {
            RequestHandled = true;
            Resource.SetResponse<TOUTGOING>(new Dat.V1.Dto.Bom.Response<TOUTGOING>()
            {
                Result = item,
                Status = status
            });
        }
        public void SetResponseAsCollection()
        {
            SetResponseAsCollection(string.Empty);
        }
        public void SetResponseAsCollection(string message)
        {
            SetResponseAsCollection(null, message);
        }
        public void SetResponseAsCollection(System.Net.HttpStatusCode code)
        {
            SetResponseAsCollection(null, code);
        }
        public void SetResponseAsCollection(System.Net.HttpStatusCode code, string message)
        {
            SetResponseAsCollection(null, code, message);
        }
        public void SetResponseAsCollection(List<TOUTGOING> items)
        {
            SetResponseAsCollection(items, System.Net.HttpStatusCode.OK);
        }
        public void SetResponseAsCollection(List<TOUTGOING> items, string message)
        {
            SetResponseAsCollection(items, 0, new Dat.V1.Dto.Bom.StatusInfo(System.Net.HttpStatusCode.OK) { Message = message });
        }
        public void SetResponseAsCollection(List<TOUTGOING> items, Dat.V1.Dto.Bom.StatusInfo status)
        {
            SetResponseAsCollection(items,0,status);
        }
        public void SetResponseAsCollection(List<TOUTGOING> items, System.Net.HttpStatusCode code)
        {
            SetResponseAsCollection(items, new Dat.V1.Dto.Bom.StatusInfo(code));
        }
        public void SetResponseAsCollection(List<TOUTGOING> items, System.Net.HttpStatusCode code, string message)
        {
            SetResponseAsCollection(items, 0, new Dat.V1.Dto.Bom.StatusInfo(code) { Message = message });
        }
        public void SetResponseAsCollection(List<TOUTGOING> items, int total_results)
        {
            SetResponseAsCollection(items, total_results, System.Net.HttpStatusCode.OK);
        }
        public void SetResponseAsCollection(List<TOUTGOING> items, int total_results, string message)
        {
            SetResponseAsCollection(items, total_results, new Dat.V1.Dto.Bom.StatusInfo(System.Net.HttpStatusCode.OK) { Message = message });
        }
        public void SetResponseAsCollection(List<TOUTGOING> items, int total_results, System.Net.HttpStatusCode code)
        {
            SetResponseAsCollection(items, total_results, new Dat.V1.Dto.Bom.StatusInfo(code));
        }
        public void SetResponseAsCollection(List<TOUTGOING> items, int total_results, Dat.V1.Dto.Bom.StatusInfo status)
        {
            RequestHandled = true;
            items = items ?? new List<TOUTGOING>();
            Resource.SetResponse<TOUTGOING>(new Dat.V1.Dto.Bom.ResponseCollection<TOUTGOING>()
            {
                ResultSet = new Dto.Bom.ResultSet<TOUTGOING>()
                {
                    Results = items,
                    ReturnedResults = items.Count,
                    TotalResults = total_results == 0 ? items.Count : total_results,
                },
                Status = status
            });
        }

        void CallGET(HttpContext context)
        {
            try
            {
                GET(context);
                if (RequestHandled) return;
            }
            catch (Framework.Exceptions.HttpException ex)
            {
                throw ex;
            }
            catch (Framework.Exceptions.EndPointException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exceptions.EndPointException(ex);
            }
            if (Resource.Parameters.Count == 0)
            {
                try
                {
                    GET();
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
                return;
            }
            else if (Resource.Parameters.Count > 1)
            {
                try
                {
                    GET(Resource.Parameters);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
                return;
            }
            string param = Resource.Parameters[0];
            Guid guid_arg = Guid.Empty;
            decimal decimal_arg = 0;
            float float_arg = 0;
            DateTime datetime_arg = DateTime.MinValue;
            long long_arg;
            if (string.IsNullOrWhiteSpace(param))
                throw new Dat.V1.Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Empty parameter presented");
            else if (Guid.TryParse(param, out guid_arg))
            {
                if (guid_arg == Guid.Empty)
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid guid presented.");
                try
                {
                    GET(guid_arg);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
            }
            else if (long.TryParse(param, out long_arg))
            {
                if (long_arg == 0)
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid number presented.");
                try
                {
                    GET(long_arg);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
            }
            else if (float.TryParse(param, out float_arg))
            {
                if (float_arg == 0)
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid float number presented.");
                try
                {
                    GET(float_arg);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
            }
            else if (decimal.TryParse(param, out decimal_arg))
            {
                if (decimal_arg == 0)
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid decimal number presented.");
                try
                {
                    GET(decimal_arg);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
            }
            else if (DateTime.TryParse(param, out datetime_arg))
            {
                if (datetime_arg == DateTime.MinValue)
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid date presented.");
                try
                {
                    GET(datetime_arg);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
            }
            else
                try
                {
                    GET(param);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
        }
        public virtual void GET(string Parameter)
        {
        }
        public virtual void GET(float Parameter)
        {
        }
        public virtual void GET(decimal Parameter)
        {
        }
        public virtual void GET(DateTime Parameter)
        {
        }
        public virtual void GET(long Parameter)
        {
        }
        public virtual void GET(Guid Parameter)
        {
        }
        public virtual void GET(List<string> Parameters)
        {
        }
        public virtual void GET(HttpContext context)
        {
        }
        public virtual void GET()
        {
        }
        public virtual void POST(HttpContext context)
        {
        }
        public virtual void POST(TINCOMING request)
        {
        }
        public virtual void PUT(TINCOMING request)
        {
        }
        public virtual void PUT(HttpContext context)
        {
        }
        public virtual void HEAD(HttpContext context)
        {
        }
        public virtual void DELETE(HttpContext context)
        {
        }
        void CallDELETE(HttpContext context)
        {
            try
            {
                CallDELETE(context);
                if (RequestHandled) return;
            }
            catch (Framework.Exceptions.HttpException ex)
            {
                throw ex;
            }
            catch (Framework.Exceptions.EndPointException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exceptions.EndPointException(ex);
            }
            bool delete_has_data = !string.IsNullOrWhiteSpace(Resource.PostedData);
            TINCOMING delete_request = default(TINCOMING);
            if (delete_has_data)
            {
                delete_request = Resource.DeserializeData<TINCOMING>();
                try
                {
                    DELETE(delete_request);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
                return;
            }
            if (Resource.Parameters.Count == 0)
            {
                try
                {
                    DELETE();
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
                return;
            }
            else if (Resource.Parameters.Count > 1)
            {
                try
                {
                    DELETE(Resource.Parameters);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
                return;
            }
            string param = Resource.Parameters[0];
            Guid guid_arg = Guid.Empty;
            decimal decimal_arg = 0;
            float float_arg = 0;
            DateTime datetime_arg = DateTime.MinValue;
            long long_arg;
            if (string.IsNullOrWhiteSpace(param))
                throw new Dat.V1.Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Empty parameter presented");
            else if (Guid.TryParse(param, out guid_arg))
            {
                if (guid_arg == Guid.Empty)
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid guid presented.");
                try
                {
                    DELETE(guid_arg);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
            }
            else if (long.TryParse(param, out long_arg))
            {
                if (long_arg == 0)
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid number presented.");
                try
                {
                    DELETE(long_arg);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
            }
            else if (float.TryParse(param, out float_arg))
            {
                if (float_arg == 0)
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid float number presented.");
                try
                {
                    DELETE(float_arg);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
            }
            else if (decimal.TryParse(param, out decimal_arg))
            {
                if (decimal_arg == 0)
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid decimal number presented.");
                try
                {
                    DELETE(decimal_arg);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
            }
            else if (DateTime.TryParse(param, out datetime_arg))
            {
                if (datetime_arg == DateTime.MinValue)
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid datea presented.");
                try
                {
                    DELETE(decimal_arg);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
            }
            else
                try
                {
                    DELETE(param);
                }
                catch (Framework.Exceptions.HttpException ex)
                {
                    throw ex;
                }
                catch (Framework.Exceptions.EndPointException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new Exceptions.EndPointException(ex);
                }
        }
        public virtual void DELETE(TINCOMING request)
        {
        }
        public virtual void DELETE(string Parameter)
        {
        }
        public virtual void DELETE(float Parameter)
        {
        }
        public virtual void DELETE(decimal Parameter)
        {
        }
        public virtual void DELETE(DateTime Parameter)
        {
        }
        public virtual void DELETE(long Parameter)
        {
        }
        public virtual void DELETE(Guid Parameter)
        {
        }
        public virtual void DELETE(List<string> Parameters)
        {
        }
        public virtual void DELETE()
        {
        }
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                RequestHandled = false;
                if (context.Request.Url.AbsoluteUri.StartsWith("http://localhost:")) Resource = Resource ?? new Resources.Resource(context);
                //write your handler implementation here.
                try
                {
                    assetConnectionString = Resource.AssetConnectionString;
                }
                catch (Exception ex)
                {
                }
                if (!MetRequirments())
                    throw new Exceptions.HttpException(System.Net.HttpStatusCode.PreconditionFailed, "Request has not met requirements.");

                switch (Resource.Verb)
                {
                    case Dat.V1.Utils.Enumerations.HttpVerbs.GET:
                        CallGET(context);
                        break;
                    case Dat.V1.Utils.Enumerations.HttpVerbs.DELETE:
                        CallDELETE(context);
                        break;
                    case Dat.V1.Utils.Enumerations.HttpVerbs.HEAD:
                        try
                        {
                            HEAD(context);
                        }
                        catch (Framework.Exceptions.HttpException ex)
                        {
                            throw ex;
                        }
                        catch (Framework.Exceptions.EndPointException ex)
                        {
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            throw new Exceptions.EndPointException(ex);
                        }
                        break;
                    case Dat.V1.Utils.Enumerations.HttpVerbs.POST:
                        try
                        {
                            POST(context);
                        }
                        catch (Framework.Exceptions.HttpException ex)
                        {
                            throw ex;
                        }
                        catch (Framework.Exceptions.EndPointException ex)
                        {
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            throw new Exceptions.EndPointException(ex);
                        }
                        if (!RequestHandled)
                        {
                            TINCOMING post_request = Resource.DeserializeData<TINCOMING>();
                            try
                            {
                                POST(post_request);
                            }
                            catch (Framework.Exceptions.HttpException ex)
                            {
                                throw ex;
                            }
                            catch (Framework.Exceptions.EndPointException ex)
                            {
                                throw ex;
                            }
                            catch (Exception ex)
                            {
                                throw new Exceptions.EndPointException(ex);
                            }
                        }
                        break;
                    case Dat.V1.Utils.Enumerations.HttpVerbs.PUT:
                        try
                        {
                            PUT(context);
                        }
                        catch (Framework.Exceptions.HttpException ex)
                        {
                            throw ex;
                        }
                        catch (Framework.Exceptions.EndPointException ex)
                        {
                            throw ex;
                        }
                        catch (Exception ex)
                        {
                            throw new Exceptions.EndPointException(ex);
                        }
                        if (!RequestHandled)
                        {
                            TINCOMING put_request = Resource.DeserializeData<TINCOMING>();
                            try
                            {
                                PUT(put_request);
                            }
                            catch (Framework.Exceptions.HttpException ex)
                            {
                                throw ex;
                            }
                            catch (Framework.Exceptions.EndPointException ex)
                            {
                                throw ex;
                            }
                            catch (Exception ex)
                            {
                                throw new Exceptions.EndPointException(ex);
                            }
                        }
                        break;
                    case Dat.V1.Utils.Enumerations.HttpVerbs.UNKNOWN:
                    default:
                        break;
                }
                if (!RequestHandled)
                    throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.NotImplemented, "Not Implemented");
            }
            catch (Utils.Validation.Exceptions.ValidationException ex)
            {
                SetResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Framework.Exceptions.HttpException ex)
            {
                SetResponse(ex.StatusCode, ex.Message);
            }
            catch (Framework.Exceptions.EndPointException ex)
            {
                SetResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Framework.Exceptions.FrameworkException ex)
            {
                SetResponse(System.Net.HttpStatusCode.InternalServerError, "Framework Error");
            }
            catch (Exception ex)
            {
                SetResponse(System.Net.HttpStatusCode.InternalServerError, "Unexpected Error");
            }
            if (context.Request.Url.AbsoluteUri.StartsWith("http://localhost:")) Resource.Flush();

        }

        #endregion
    }
}
