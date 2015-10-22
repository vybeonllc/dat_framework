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
    public class Queue : Base
    {
        enum StoredProcedures
        {
            Queues_Insert,
            Queues_Update,
            Queues_Select_ByQueueId,
            Queues_Select_Next,
            Queues_Select_ByServerId,
        }

        #region >>-- CONSTRUCTORS                                                 -->>--

        public Queue()
        {
            //Blank
        }

        public Queue(String connectionString)
            : base(connectionString)
        {
        }



        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- Properties                                                   -->>--

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "QueueId")]
        public long QueueId { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "ServerId")]
        public int ServerId { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Error")]
        public int Error { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Priority")]
        public int Priority { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Status")]
        public string Status { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Url")]
        public string Url { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "EnqueuedOn")]
        public DateTime EnqueuedOn { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "DequeuedOn")]
        public DateTime DequeuedOn { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "StartedOn")]
        public DateTime StartedOn { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "DownloadedOn")]
        public DateTime DownloadedOn { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "ParsedOn")]
        public DateTime ParsedOn { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "FinishedOn")]
        public DateTime FinishedOn { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Attempt")]
        public int Attempt { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Results")]
        public int Results { get; set; }
         
        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Keyword")]
        public string Keyword { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "KeywordId")]
        public int KeywordId { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "LocationId")]
        public int LocationId { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "Location")]
        public string Location { get; set; }

        [Dat.V1.Utils.Data.Attributes.Mapping(TargetName = "LocationType")]
        public int LocationType { get; set; }

        #endregion

        #region >>-- Database Methods                                                   -->>--


        public Boolean Create()
        {
            Boolean success = false;
            DataLayer.MSSQL mssql = null;
            try
            {
                mssql = new DataLayer.MSSQL(ConnectionString);
                QueueId = mssql.ExecuteScalar(StoredProcedures.Queues_Insert.ToString(),
                    "@ServerId", ServerId,
                    "@Error,", Error,
                    "@Priority", Priority,
                    "@Status", Status,
                    "@Url", Url,
                    "@DequeuedOn", DequeuedOn,
                    "@StartedOn", StartedOn,
                    "@DownloadedOn", DownloadedOn,
                    "@ParsedOn", ParsedOn,
                    "@FinishedOn", FinishedOn,
                    "@Attempt", Attempt);
                success = QueueId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving queue.", ex); }
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
                QueueId = mssql.ExecuteScalar(StoredProcedures.Queues_Update.ToString(),
                    "@QueueId", QueueId,
                    "@ServerId", ServerId,
                    "@Error,", Error,
                    "@Priority", Priority,
                    "@Status", Status,
                    "@Url", Url,
                    "@DequeuedOn", DequeuedOn,
                    "@StartedOn", StartedOn,
                    "@DownloadedOn", DownloadedOn,
                    "@ParsedOn", ParsedOn,
                    "@FinishedOn", FinishedOn,
                    "@Attempt", Attempt,
                    "@Results", Results);
                success = QueueId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving queue.", ex); }
            finally { if (mssql != null) mssql.Dispose(); }

            return success;

        }

        public bool ByQueueId(long queueId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MSSQL mssql = null;

            try
            {
                mssql = new DataLayer.MSSQL(ConnectionString);
                row = mssql.GetDataRow(StoredProcedures.Queues_Select_ByQueueId.ToString(),
                    "@QueueId", queueId);
                if (row != null)
                    success = row.TryParse<Queue>(this) && QueueId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading queue.", ex); }
            finally { if (mssql != null) mssql.Dispose(); }
            return success;
        }

        public static IEnumerable<Queue> SelectNext(int serverId, string connectionString)
        {
            IEnumerable<Queue> queues = null;
            DataTable table = null;
            DataLayer.MSSQL mssql = null;

            try
            {
                mssql = new DataLayer.MSSQL(connectionString);
                table = mssql.GetDataTable(StoredProcedures.Queues_Select_Next.ToString(), "@ServerId", serverId);
                if (table != null)
                    queues = table.Rows.Select<Queue>(new object[] { connectionString });

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading queue.", ex); }
            finally { if (mssql != null) mssql.Dispose(); }

            return queues;
        }
        public static IEnumerable<Queue> ByServerId(int serverId, string connectionString)
        {
            IEnumerable<Queue> queues = null;
            DataTable table = null;
            DataLayer.MSSQL mssql = null;

            try
            {
                mssql = new DataLayer.MSSQL(connectionString);
                table = mssql.GetDataTable(StoredProcedures.Queues_Select_ByServerId.ToString(), "@ServerId", serverId);
                if (table != null)
                    queues = table.Rows.Select<Queue>(new object[] { connectionString });

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading queue.", ex); }
            finally { if (mssql != null) mssql.Dispose(); }

            return queues;
        }

        #endregion


    }
}
