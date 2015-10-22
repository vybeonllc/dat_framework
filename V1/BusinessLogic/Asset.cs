using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class Asset : Base
    {
        enum StoredProcedures
        {
            Assets_Insert,
            Assets_Update,
            Assets_Select_ByAssetId,
            Assets_Select_ByAssetCode,
            Assets_Select_ByAssetGuid,
        }
        #region >>-- CONSTRUCTORS                                                 -->>--


        public Asset()
        {
            //Blank
        }

        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- PROPERTIES                                                 -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "AssetId", IsPrimaryKey = true)]
        public int AssetId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "AssetGuid")]
        public Guid AssetGuid { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "AssetCode")]
        public string AssetCode { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "AssetName")]
        public string AssetName { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "AssetLogo")]
        public string AssetLogo { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "AssetDescription")]
        public string AssetDescription { get; set; }

        #endregion >>-- PROPERTIES                                                 -->>--

        #region >>-- Database Methods                                                   -->>--


        protected override bool Insert()
        {
            bool success = false;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                AssetId = (int)mySql.ExecuteScalar(StoredProcedures.Assets_Insert.ToString(),
                    "_AssetName", AssetName,
                    "_AssetGuid", AssetGuid,
                    "_CreatedBy", CreatedBy,
                    "_AssetCode", AssetCode);
                success = AssetId > 1;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving asset.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;

        }
        protected override bool Save()
        {
            bool success = false;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                AssetId = (int)mySql.ExecuteScalar(StoredProcedures.Assets_Update.ToString(),
                    "_AssetName", AssetName,
                    "_AssetGuid", AssetGuid,
                    "_AssetCode", AssetCode);
                success = AssetId > 1;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving asset.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;

        }
        protected override bool Remove()
        {
            throw new NotImplementedException();
        }
        public bool ByAssetId(int assetId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);

                row = mySql.GetDataRow(StoredProcedures.Assets_Select_ByAssetId.ToString(),
                    "_AssetId", assetId);

                if (row != null)
                    success = row.TryParse<Asset>(this) && AssetId > 0;

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }
        public bool ByAssetGuid(Guid assetGuid)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                row = mySql.GetDataRow(StoredProcedures.Assets_Select_ByAssetGuid.ToString(),
                    "_AssetGuid", assetGuid);

                if (row != null)
                    success = row.TryParse<Asset>(this) && AssetId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }
        public bool ByAssetCode(string assetCode)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);

                row = mySql.GetDataRow(StoredProcedures.Assets_Select_ByAssetCode.ToString(),
                    "_AssetCode", assetCode);

                if (row != null)
                    success = row.TryParse<Asset>(this) && AssetId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }
        #endregion

        #region >>-- Shared Methods                                                   -->>--


        #endregion
    }
}
