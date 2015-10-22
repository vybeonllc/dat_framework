using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class AssetRole : Base
    {
        enum StoredProcedures
        {
            AssetRoles_Insert,
            AssetRoles_Select_ByAssetRoleId,
            AssetRoles_Select_ByAssetGuid,
            AssetRoles_Select_ByRoleId,
        }
        #region >>-- CONSTRUCTORS                                                 -->>--


        public AssetRole()
        {
            //Blank
        }

        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- PROPERTIES                                                 -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "AssetRoleId", IsPrimaryKey = true)]
        public long AssetRoleId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "AssetGuid")]
        public Guid AssetGuid { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "RoleId")]
        public int RoleId { get; set; }

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
                AssetRoleId = (int)mySql.ExecuteScalar(StoredProcedures.AssetRoles_Insert.ToString(),
                    "_RoleId", RoleId,
                    "_AssetGuid", AssetGuid,
                    "_CreatedBy", CreatedBy);
                success = AssetRoleId > 1;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving asset role.", ex); }
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
        public bool ByAssetRoleId(long assetRoleId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);

                row = mySql.GetDataRow(StoredProcedures.AssetRoles_Select_ByAssetRoleId.ToString(),
                    "_AssetRoleId", assetRoleId);

                if (row != null)
                    success = row.TryParse<AssetRole>(this) && AssetRoleId > 0;

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset role.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }

        #endregion

        #region >>-- Shared Methods                                                   -->>--

        public static IEnumerable<AssetRole> ByAssetGuid(Guid assetGuid)
        {
            IEnumerable<AssetRole> assetRoles = null;
            DataTable table = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(DatConnectionString);
                table = mySql.GetDataTable(StoredProcedures.AssetRoles_Select_ByAssetGuid.ToString(), "_AssetGuid", assetGuid);
                if (table != null)
                    assetRoles = table.Rows.Select<AssetRole>();

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset roles.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return assetRoles;
        }

        public static IEnumerable<AssetRole> ByRoleId(int roleId)
        {
            IEnumerable<AssetRole> assetRoles = null;
            DataTable table = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(DatConnectionString);
                table = mySql.GetDataTable(StoredProcedures.AssetRoles_Select_ByAssetGuid.ToString(), "_RoleId", roleId);
                if (table != null)
                    assetRoles = table.Rows.Select<AssetRole>();

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset roles.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return assetRoles;
        }

        #endregion
    }
}
