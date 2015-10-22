using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class RolePermission : Base
    {
        enum StoredProcedures
        {
            RolePermissions_Insert,
            RolePermissions_Select_ByRolePermissionId,
            RolePermissions_Select_ByRoleId,
            RolePermissions_Select_ByPermissionId,
        }
        #region >>-- CONSTRUCTORS                                                 -->>--


        public RolePermission()
        {
            //Blank
        }

 
        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- PROPERTIES                                                 -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "RolePermissionId", IsPrimaryKey = true)]
        public long RolePermissionId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "PermissionId")]
        public int PermissionId { get; set; }

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
                RolePermissionId = (int)mySql.ExecuteScalar(StoredProcedures.RolePermissions_Insert.ToString(),
                    "_RoleId", RoleId,
                    "_PermissionId", PermissionId,
                    "_CreatedBy", CreatedBy);
                success = RolePermissionId > 1;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving role permission.", ex); }
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
        public bool ByRolePermissionId(long rolePermissionId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);

                row = mySql.GetDataRow(StoredProcedures.RolePermissions_Select_ByRolePermissionId.ToString(),
                    "_RolePermissionId", rolePermissionId);

                if (row != null)
                    success = row.TryParse<RolePermission>(this) && RolePermissionId > 0;

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading role permission.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }
       
        #endregion

        #region >>-- Shared Methods                                                   -->>--

          public static IEnumerable<RolePermission> ByRoleId(int roleId )
        {
            IEnumerable<RolePermission> rolePermissions = null;
            DataTable table = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(DatConnectionString);
                table = mySql.GetDataTable(StoredProcedures.RolePermissions_Select_ByRoleId.ToString(),"_RoleId",roleId);
                if (table != null)
                    rolePermissions = table.Rows.Select<RolePermission>( );

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset permissions.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return rolePermissions;
        }

          public static IEnumerable<RolePermission> ByPermissionId(int permissionId )
          {
              IEnumerable<RolePermission> rolePermissions = null;
              DataTable table = null;
              DataLayer.MySQL mySql = null;

              try
              {
                  mySql = new DataLayer.MySQL(DatConnectionString);
                  table = mySql.GetDataTable(StoredProcedures.RolePermissions_Select_ByPermissionId.ToString(), "_PermissionId", permissionId);
                  if (table != null)
                      rolePermissions = table.Rows.Select<RolePermission>( );

              }
              catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading asset permissions.", ex); }
              finally { if (mySql != null) mySql.Dispose(); }

              return rolePermissions;
          }

        #endregion
    }
}
