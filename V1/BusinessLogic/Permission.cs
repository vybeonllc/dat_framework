using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class Permission :Base
    {
        enum StoredProcedures
        {
            Permissions_Insert,
            Permissions_Update,
            Permissions_Select_ByPermissionId,
            Permissions_Select_All
        }
        #region >>-- CONSTRUCTORS                                                 -->>--


        public Permission()
        {
            //Blank
        }
 

        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- PROPERTIES                                                 -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "PermissionId", IsPrimaryKey = true)]
        public int PermissionId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "PermissionName")]
        public string PermissionName { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "PermissionDescription")]
        public string PermissionDescription { get; set; }

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
                PermissionId = (int)mySql.ExecuteScalar(StoredProcedures.Permissions_Insert.ToString(),
                    "_PermissionName", PermissionName,
                    "_PermissionDescription", PermissionDescription,
                    "_CreatedBy", CreatedBy);
                success = PermissionId > 1;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving permission.", ex); }
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
                PermissionId = (int)mySql.ExecuteScalar(StoredProcedures.Permissions_Update.ToString(),
                   "_PermissionId", PermissionId,
                   "_PermissionName", PermissionName,
                   "_PermissionDescription", PermissionDescription);
                success = PermissionId > 1;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving asset.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;

        }
        protected override bool Remove()
        {
            throw new NotImplementedException();
        }
        public bool ByPermissionId(int permissionId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);

                row = mySql.GetDataRow(StoredProcedures.Permissions_Select_ByPermissionId.ToString(),
                    "_PermissionId", permissionId);

                if (row != null)
                    success = row.TryParse<Permission>(this) && PermissionId > 0;

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading permission.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }
       
        #endregion

        #region >>-- Shared Methods                                                   -->>--


        public static IEnumerable<Permission> Select_All()
        {
            IEnumerable<Permission> permissions = null;
            DataTable table = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(DatConnectionString);
                table = mySql.GetDataTable(StoredProcedures.Permissions_Select_All.ToString());
                if (table != null)
                    permissions = table.Rows.Select<Permission>();

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading permissions.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return permissions;
        }
 
        #endregion
    }
}
