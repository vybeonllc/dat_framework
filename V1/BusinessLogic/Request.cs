using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class Request : Base
    {
        enum StoredProcedures
        {
            Requests_Insert,
            Requests_Update,
            Requests_Select_ByRequestId,
            Requests_Select_All
        }
        #region >>-- CONSTRUCTORS                                                 -->>--

        public Request()
        {
            //Blank
        }

 
        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- Properties                                                   -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "Version")]
        public string Version { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "RequestId", IsPrimaryKey = true)]
        public long RequestId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "AssetCode")]
        public string AssetCode { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Service")]
        public string Service { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "EndPoint")]
        public string EndPoint { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Parameters")]
        public string Parameters { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Method")]
        public string Method { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "UserGuid")]
        public Guid UserGuid { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "IpAddress")]
        public string IpAddress { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "ServerIpAddress")]
        public string ServerIpAddress { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Url")]
        public string Url { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Headers")]
        public string Headers { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "HttpAuth")]
        public string HttpAuth { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Language")]
        public string Language { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Referrer")]
        public string Referrer { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "UserAgent")]
        public string UserAgent { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Cookies")]
        public string Cookies { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "InputStream")]
        public string InputStream { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "ContentType")]
        public string ContentType { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "AcceptType")]
        public string AcceptType { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Result")]
        public string Result { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "SubscriberAssetGuid")]
        public Guid SubscriberAssetGuid { get; set; }

        #endregion

        #region >>-- Database Methods                                                   -->>--


        protected override bool Insert()
        {
            bool success = false;
            DataLayer.MySQL mySql = null;
            string truncatedResult = Result;
            
            try {

              if (Result != null)
                if (Result.ToCharArray().Length >= 20000) truncatedResult = Result.Substring(0, 20000);

                mySql = new DataLayer.MySQL(ConnectionString);
                RequestId = mySql.ExecuteScalar(StoredProcedures.Requests_Insert.ToString(),
                            "_Version", Version,
                            "_AssetCode", AssetCode,
                            "_Service", Service,
                            "_EndPoint", EndPoint,
                            "_Parameters", Parameters,
                            "_Method", Method,
                            "_UserGuid", UserGuid,
                            "_IpAddress", IpAddress,
                            "_Url", Url,
                            "_Headers", Headers,
                            "_HttpAuth", HttpAuth,
                            "_Language", Language,
                            "_Referrer", Referrer,
                            "_UserAgent", UserAgent,
                            "_Cookies", Cookies,
                            "_InputStream", InputStream,
                            "_ContentType", ContentType,
                            "_SubscriberAssetGuid", SubscriberAssetGuid,
                            "_AcceptType", AcceptType,
                            "_Result", truncatedResult,
                            "_ServerIpAddress", ServerIpAddress);
                success = RequestId > 0;

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving request.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }
          protected override bool Save()
        {
            bool success = false;
            DataLayer.MySQL mySql = null;
            string truncatedResult = Result;
            
            try {

              if (Result != null)
                if (Result.ToCharArray().Length >= 20000) truncatedResult = Result.Substring(0, 20000);

              mySql = new DataLayer.MySQL(ConnectionString);
              RequestId = mySql.ExecuteScalar(StoredProcedures.Requests_Update.ToString(),
                          "_RequestId", RequestId,
                          "_Result", truncatedResult );
              success = RequestId > 0;

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving request.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;

        }
          protected override bool Remove()
          {
              throw new NotImplementedException();
          }
        public bool ByRequestId(long requestId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);

                row = mySql.GetDataRow(StoredProcedures.Requests_Select_ByRequestId.ToString(),
                    "_RequestId", requestId);

                if (row != null)
                    success = row.TryParse<Request>(this) && RequestId > 0;

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading request.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }


        #endregion

        #region >>-- Shared Methods                                                   -->>--

        public static IEnumerable<Request> SelectAll()
        {
            IEnumerable<Request> requests = null;
            DataTable table = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(DatConnectionString);
                table = mySql.GetDataTable(StoredProcedures.Requests_Select_All.ToString());
                if (table != null)
                    requests = table.Rows.Select<Request>( );

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading Request.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return requests;
        }

        #endregion
    }
}
