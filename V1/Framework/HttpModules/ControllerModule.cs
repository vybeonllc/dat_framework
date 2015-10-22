using System;
using System.Web;
using System.Linq;
using Dat.V1.Utils.Extensions;
using System.Text;

namespace Dat.V1.Framework.HttpModules
{
    public class ControllerModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>

        static bool loggingHandlersAssigned;

        public Framework.Resources.Resource Resource { get { return (Framework.Resources.Resource)HttpContext.Current.Items["Resource"]; } set { HttpContext.Current.Items["Resource"] = value; } }
        BusinessLogic.Request Request { get; set; }
        BusinessLogic.User LoggedInUser { get; set; }
        BusinessLogic.Role AnonymousRole { get; set; }
        bool IsAnonymous { get { return LoggedInUser.RoleId == AnonymousRole.RoleId; } }

        bool Authenticate()
        {
            Dat.V1.Utils.Security.Token.TokenGenerator token = null;
            Guid userGuid = Guid.Empty;
            BusinessLogic.User _user = new BusinessLogic.User();
            AnonymousRole = new BusinessLogic.Role();
            AnonymousRole.ByRoleName("anonymous");

            if (string.IsNullOrWhiteSpace(Resource.AuthenticationToken))
            {
                if (_user.ByEmailAddress("anonymous@vybeon.com"))
                    token = new Dat.V1.Utils.Security.Token.TokenGenerator(_user.UserGuid.ToString(), DateTime.Now.AddDays(1));
                else
                    throw new Exceptions.HttpModulesException(System.Net.HttpStatusCode.NotFound, "Account not found.");
            }
            else
                token = Dat.V1.Utils.Security.Token.TokenGenerator.FromTokenString(Resource.AuthenticationToken, Dat.V1.Utils.Security.Token.TokenGenerator.PrivateKey);

            if (token == null)
                throw new Dat.V1.Framework.HttpModules.Exceptions.HttpModulesException(System.Net.HttpStatusCode.BadRequest, "Invalid token");
            //else if (token.Expires <= DateTime.Now)
            //    throw new Dat.V1.Utils.Security.Exceptions.TokenException(Errors.TokenExpired);
            else if (string.IsNullOrWhiteSpace(token.Value) || !Guid.TryParse(token.Value, out userGuid) || userGuid == Guid.Empty)
                throw new Dat.V1.Framework.HttpModules.Exceptions.HttpModulesException(System.Net.HttpStatusCode.BadRequest, "Invalid token");
            else if (!_user.ByUserGuid(userGuid))
                throw new Dat.V1.Framework.HttpModules.Exceptions.HttpModulesException(System.Net.HttpStatusCode.InternalServerError, "Retrieving user failed.");
            else if (_user.UserId < 1 | _user.UserGuid == Guid.Empty)
                throw new Dat.V1.Framework.HttpModules.Exceptions.HttpModulesException(System.Net.HttpStatusCode.NotFound, "Account not found.");
            else
            {
                LoggedInUser = _user;
                Resource.AuthenticatedUser = _user.UserGuid;
                Resource.IsAnonymous = IsAnonymous;
                return true;
            }
            throw new Dat.V1.Framework.HttpModules.Exceptions.HttpModulesException(System.Net.HttpStatusCode.PreconditionFailed, "Authentication Failed.");
        }

        #region IHttpModule Members
        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }

        public void Dispose()
        {
            //clean-up code here.
        }
        public new void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            context.AcquireRequestState += new EventHandler(AcquireRequestState);
            context.BeginRequest += new EventHandler(OnBeginRequest);
            context.EndRequest += new EventHandler(OnEndRequest);
            context.RequestCompleted += new EventHandler(OnRequestCompleted);
        }


        new void OnEndRequest(object s, EventArgs e)
        {
            HttpApplication context = s as HttpApplication;

            if (!context.Request.Url.AbsoluteUri.StartsWith("http://localhost:")) Resource.Flush();

            Request.Result = Resource.Result;
            Request.Update();


        }
        void OnRequestCompleted(object s, EventArgs e)
        {

        }
        public void AcquireRequestState(Object sender, EventArgs e)
        {
            //HttpContext Context = ((HttpApplication)sender).Context;
            //Context.Items.Add("Item", "Item Data");
        }


        public new void OnBeginRequest(Object s, EventArgs e)
        {
            HttpApplication context = s as HttpApplication;

            if (!loggingHandlersAssigned)
            {
                Dat.V1.Utils.BusinessLogic.Base.OnCreating += Base_OnCreating;
                Dat.V1.Utils.BusinessLogic.Base.OnCreated += Base_OnCreated;
                Dat.V1.Utils.BusinessLogic.Base.OnUpdating += Base_OnUpdating;
                Dat.V1.Utils.BusinessLogic.Base.OnUpdated += Base_OnUpdated;
                loggingHandlersAssigned = true;
            }

            //create request object insert incoming request in to the database.
            Request = new BusinessLogic.Request()
            {
                CreateDate = DateTime.Now,
                AcceptType = context.Request.AcceptTypes.ToDelimitedString(","),
                ContentType = context.Request.ContentType,
                Cookies = context.Request.Cookies.AllKeys.Select(k => k + ":" + context.Request.Cookies[k].Value).ToDelimitedString(","),
                HttpAuth = context.Request.Headers["Http-Auth"],
                Headers = context.Request.Headers.AllKeys.Select(k => k + ":" + context.Request.Headers[k]).ToDelimitedString(","),
                Language = context.Request.Headers["Language"],
                SubscriberAssetGuid = Guid.Parse(string.IsNullOrWhiteSpace(context.Request.Headers.Get("SubscriberAssetGuid")) ? Guid.Empty.ToString() : context.Request.Headers.Get("SubscriberAssetGuid")),
                IpAddress = string.IsNullOrWhiteSpace(context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ? context.Request.ServerVariables["REMOTE_ADDR"] : context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0],
                Referrer = context.Request.UrlReferrer == null ? null : context.Request.UrlReferrer.AbsoluteUri,
                UserAgent = context.Request.UserAgent,
                Url = context.Request.Url.AbsoluteUri,
                Method = context.Request.HttpMethod.ToUpper(),
                ServerIpAddress = Dat.V1.Utils.Net.Dns.GetServerIPAddress()
            };

            try
            {
                //create resource object which present the details of the current request.
                Resource = new Framework.Resources.Resource(System.Web.HttpContext.Current);
                Request.EndPoint = Resource.EndPoint;
                Request.Method = Resource.Verb.ToString();
                Request.Parameters = Resource.Parameters.ToDelimitedString("/");
                Request.Service = Resource.Service;
                Request.Url = Resource.Url.AbsoluteUri;
                Request.Version = versionCheck(Resource.Version);
                Request.InputStream = Resource.PostedData;
                Request.AssetCode = Resource.AssetCode;
            }
            catch (Exception ex)
            {
                Request.Result = ex.ToString();
                throw ex;
            }

            try
            {
                // in this method all of the authentications steps should be checked for now it only
                Authenticate(); //checks if authentication token is specified for incoming request  or not, if it is specified it will check if the user is in anonymous role 
                //if not it will assign the anonymous user token for the request.

                Request.UserGuid = Resource.AuthenticatedUser;
                try
                {
                    Resource.Validate(); //check if all parameters from incoming request are valid. we can take try catch out whenever we move all the assets services in one place and let them to only
                    // serve the api requests not the pictures and medias. they dont have version or service so it will throw exception
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
                Request.Result = ex.ToString();
                throw ex;
            }
            finally
            {
                Request.Create();
            }

            try
            {
                Resource.DistributedAssetTechnology = new Dto.Bom.DistributedAssetTechnology()
                {
                    Copyright = "Copyright © " + DateTime.Now.Year + " VybeOn, LLC and its suppliers. All rights reserved. This API cannot be accessed and the content and any results may not be used, reproduced or transmitted in any manner without express written permission from VybeOn, LLC.",
                    Logo = "http://www.vybeon.com/images/logo.png",
                    Version = "v1",
                    RequestInfo = new Dto.Bom.RequestInfo()
                    {
                        EndPoint = Resource.EndPoint,
                        RequestFormat = Resource.RequestFormat.ToString(),
                        ResponseFormat = Resource.ResponseFormat.ToString(),
                        Service = Resource.Service,
                        TraceId = Guid.NewGuid() + "|" + Request.RequestId,
                        AssetVersion = Request.Version
                    }
                };
                BusinessLogic.Asset asset = new BusinessLogic.Asset();
                if (asset.ByAssetCode(Resource.AssetCode))
                {
                    Resource.DistributedAssetTechnology.RequestInfo.Asset = asset.AssetName;
                    Resource.DistributedAssetTechnology.RequestInfo.AssetDescription = asset.AssetDescription;
                    Resource.DistributedAssetTechnology.RequestInfo.AssetLogo = asset.AssetLogo;
                    Resource.DistributedAssetTechnology.RequestInfo.AssetVersion = Resource.Version;
                }
            }
            catch (Exception ex)
            {
            }

        }


        #endregion

        #region Business Logic Logging

        string logType = null;
        string LogType
        {
            get
            {
                if (string.IsNullOrWhiteSpace(logType))
                    logType = new BusinessLogic.Log().GetType().FullName;
                return logType;
            }
        }

        void Base_OnUpdated(object sender, Utils.BusinessLogic.EventArgs.OnUpdatedEventArg e)
        {
            if (e.EntityName == LogType) return;
            if (e.Success) return;
            new BusinessLogic.Log()
            {
                LogType = 1,
                ReferenceKey = e.UniqueIdentifier.ToString(),
                RequestId = Request.RequestId,
                Text = e.Error,
                Title = e.EntityName,
                Action = 2,
                Success = e.Success
            }.Create();
        }

        void Base_OnUpdating(object sender, Utils.BusinessLogic.EventArgs.OnUpdatingEventArg e)
        {
            return;
        }

        void Base_OnCreated(object sender, Utils.BusinessLogic.EventArgs.OnCreatedEventArg e)
        {
            if (e.EntityName == LogType) return;
            if (e.Success) return;
            new BusinessLogic.Log()
            {
                LogType = 1,
                ReferenceKey = e.UniqueIdentifier.ToString(),
                RequestId = Request.RequestId,
                Text = e.Error,
                Title = e.EntityName,
                Action = 1,
                Success = e.Success
            }.Create();
        }
        void Base_OnCreating(object sender, Utils.BusinessLogic.EventArgs.OnCreatingEventArg e)
        {
            return;
        }

        #endregion Business Logic Logging


    #region -->> HELPERS                                                                 >>--

        private string versionCheck(string versionString) {

          string version = "v?";

          try {

            if (versionString != null)
              if (versionString.StartsWith("v"))
                version = versionString.Substring(0, 10);

          }
          catch {}

          return version;

        }

    #endregion

  }
}