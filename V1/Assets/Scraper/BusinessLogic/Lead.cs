using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;
using Dat.V1.Utils;
namespace Dat.Assets.Scraper.V1.BusinessLogic
{
    [Serializable]
    public class Lead : Base
    {
        enum StoredProcedures
        {
            Leads_Insert,
            Leads_Update,
            Leads_Select_ByLeadId,
            Leads_Select_ByQueueId,
        }

        #region >>-- CONSTRUCTORS                                                 -->>--

        public Lead()
        {
            //Blank
        }

        public Lead(String connectionString)
            : base(connectionString)
        {
        }



        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- Properties                                                   -->>--

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "LeadId")]
        public long LeadId { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Slogan")]
        public string Slogan { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Website")]
        public string Website { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Creative")]
        public string Creative { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "PhoneNumber")]
        public long PhoneNumber { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Address")]
        public string Address { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Link")]
        public string Link { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Latitude")]
        public float Latitude { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Longitude")]
        public float Longitude { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "QueueId")]
        public long QueueId { get; set; }


        #endregion

        #region >>-- Database Methods                                                   -->>--


        public Boolean Create()
        {
            Boolean success = false;
            DataLayer.MSSQL mssql = null;
            try
            {
                mssql = new DataLayer.MSSQL(ConnectionString);
                LeadId = mssql.ExecuteScalar(StoredProcedures.Leads_Insert.ToString(),
                    "@Slogan", Slogan,
                    "@Website", Website,
                    "@Creative", Creative,
                    "@PhoneNumber", PhoneNumber,
                    "@Address,", Address,
                    "@Link", Link,
                    "@Latitude", Latitude,
                    "@Longitude", Longitude,
                    "@QueueId", QueueId);
                success = LeadId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving lead.", ex); }
            finally { if (mssql != null) mssql.Dispose(); }

            return success;

        }
        public Boolean Update()
        {
            Boolean success = false;
            DataLayer.MSSQL mssql = null;
            try
            {
                mssql = new DataLayer.MSSQL(ConnectionString);
                LeadId = mssql.ExecuteScalar(StoredProcedures.Leads_Insert.ToString(),
                    "@LeadId", LeadId,
                    "@Slogan", Slogan,
                    "@Website", Website,
                    "@Creative", Creative,
                    "@PhoneNumber", PhoneNumber,
                    "@Address,", Address,
                    "@Link", Link,
                    "@Latitude", Latitude,
                    "@Longitude", Longitude,
                    "@QueueId", QueueId);
                success = LeadId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving lead.", ex); }
            finally { if (mssql != null) mssql.Dispose(); }

            return success;

        }

        public bool ByLeadId(long leadId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MSSQL mssql = null;

            try
            {
                mssql = new DataLayer.MSSQL(ConnectionString);
                row = mssql.GetDataRow(StoredProcedures.Leads_Select_ByLeadId.ToString(),
                    "@LeadId", leadId);
                if (row != null)
                    success = row.TryParse<Lead>(this) && LeadId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading lead.", ex); }
            finally { if (mssql != null) mssql.Dispose(); }
            return success;
        }
        public static IEnumerable<Lead> ByQueueId(long queueId, string connectionString)
        {
            IEnumerable<Lead> leads = null;
            DataTable table = null;
            DataLayer.MSSQL mssql = null;

            try
            {
                mssql = new DataLayer.MSSQL(connectionString);
                table = mssql.GetDataTable(StoredProcedures.Leads_Select_ByQueueId.ToString(), "@QueueId", queueId);
                if (table != null)
                    leads = table.Rows.Select<Lead>(new object[] { connectionString });

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading leads.", ex); }
            finally { if (mssql != null) mssql.Dispose(); }

            return leads;
        }

        #endregion


    }
}
