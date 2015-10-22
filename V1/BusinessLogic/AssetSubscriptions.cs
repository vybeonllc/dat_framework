using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class AssetSubscription :Base
    {
        enum StoredProcedures
        {
            AssetSubscriptions_Insert,
            AssetSubscriptions_Select_ByAssetSubscriptionId,
            AssetSubscriptions_Select_BySubscriberAssetGuid,
            AssetSubscriptions_Select_ByPublisherAssetGuid
        }
        #region >>-- CONSTRUCTORS                                                 -->>--


        public AssetSubscription()
        {
            //Blank
        }
 
        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- PROPERTIES                                                 -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "AssetSubscriptionId", IsPrimaryKey = true)]
        public long AssetSubscriptionId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "SubscriberAssetGuid")]
        public Guid SubscriberAssetGuid { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "PublisherAssetGuid")]
        public Guid PublisherAssetGuid { get; set; }

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
                AssetSubscriptionId = (int)mySql.ExecuteScalar(StoredProcedures.AssetSubscriptions_Insert.ToString(),
                    "_SubscriberAssetGuid", SubscriberAssetGuid,
                    "_PublisherAssetGuid", PublisherAssetGuid,
                    "_CreatedBy", CreatedBy);
                success = AssetSubscriptionId > 1;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving asset subscription.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;

        }
        protected override bool Save()
        {
            throw new NotImplementedException();
        }
        public bool ByAssetSubscriptionId(long assetSubscriptionId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);

                row = mySql.GetDataRow(StoredProcedures.AssetSubscriptions_Select_ByAssetSubscriptionId.ToString(),
                    "_AssetSubscriptionId", assetSubscriptionId);

                if (row != null)
                    success = row.TryParse<AssetSubscription>(this) && AssetSubscriptionId > 0;

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset subscription.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }
        protected override bool Remove()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region >>-- Shared Methods                                                   -->>--

        public static IEnumerable<AssetSubscription> BySubscriberAssetGuid(Guid subscriberAssetGuid)
        {
            IEnumerable<AssetSubscription> assetSubscriptions = null;
            DataTable table = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                table = mySql.GetDataTable(StoredProcedures.AssetSubscriptions_Select_BySubscriberAssetGuid.ToString(), "_SubscriberAssetGuid", subscriberAssetGuid);
                if (table != null)
                    assetSubscriptions = table.Rows.Select<AssetSubscription>( );

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset subscriptions.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return assetSubscriptions;
        }

        public static IEnumerable<AssetSubscription> ByPublisherAssetGuid(Guid publisherAssetGuid)
        {
            IEnumerable<AssetSubscription> assetSubscriptions = null;
            DataTable table = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                table = mySql.GetDataTable(StoredProcedures.AssetSubscriptions_Select_ByPublisherAssetGuid.ToString(), "_PublisherAssetGuid", publisherAssetGuid);
                if (table != null)
                    assetSubscriptions = table.Rows.Select<AssetSubscription>( );

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset subscriptions.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return assetSubscriptions;
        }
        #endregion
    }
}
