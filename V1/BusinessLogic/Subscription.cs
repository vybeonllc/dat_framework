using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class Subscription : Base
    {
        enum StoredProcedures
        {
            Subscriptions_Insert,
            Subscriptions_Update,
            Subscriptions_Select_BySubscriptionId,
            Subscriptions_Select_ByUserGuid,
            Subscriptions_IsSubscribed
        }
        #region >>-- CONSTRUCTORS                                                 -->>--


        public Subscription()
        {
            //Blank
        }

        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- PROPERTIES                                                 -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "SubscriptionId", IsPrimaryKey = true)]
        public long SubscriptionId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "UserGuid")]
        public Guid UserGuid { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "AssetGuid")]
        public Guid AssetGuid { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreateDate")]
        public DateTime CreateDate { get; set; }

        #endregion >>-- PROPERTIES                                                 -->>--

        #region >>-- Database Methods                                                   -->>--


        protected override bool Insert()
        {
            bool success = false;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                SubscriptionId = (int)mySql.ExecuteScalar(StoredProcedures.Subscriptions_Insert.ToString(),
                    "_UserGuid", UserGuid,
                    "_AssetGuid", AssetGuid,
                    "_CreatedBy", CreatedBy);
                success = SubscriptionId > 1;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving subscription.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;

        }
        protected override bool Save()
        {
            throw new NotImplementedException();
        }
        protected override bool Remove()
        {
            throw new NotImplementedException();
        }
        public bool BySubscriptionId(long subscriptionId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);

                row = mySql.GetDataRow(StoredProcedures.Subscriptions_Select_BySubscriptionId.ToString(),
                    "_SubscriptionId", subscriptionId);

                if (row != null)
                    success = row.TryParse<Subscription>(this) && SubscriptionId > 0;

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading subscription.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }
        
        #endregion

        #region >>-- Shared Methods                                                   -->>--

        public static IEnumerable<Subscription> ByUserGuid(Guid userGuid)
        {
            IEnumerable<Subscription> subscriptions = null;
            DataTable table = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(DatConnectionString);
                table = mySql.GetDataTable(StoredProcedures.Subscriptions_Select_ByUserGuid.ToString(), "_UserGuid", userGuid);
                if (table != null)
                    subscriptions = table.Rows.Select<Subscription>();

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading subscriptions.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return subscriptions;
        }

        public static bool IsSubscribed(Guid assetGuid,Guid userGuid)
        {
            bool success = false;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(DatConnectionString);
                return (bool)mySql.ExecuteScalarObject(StoredProcedures.Subscriptions_Insert.ToString(),
                    "_AssetGuid", assetGuid,
                    "_UserGuid", userGuid);
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving subscription.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }
        }
        #endregion
    }
}
