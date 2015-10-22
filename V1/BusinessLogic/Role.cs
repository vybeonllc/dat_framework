using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class Role :Base
    {
        enum StoredProcedures
        {
            Roles_Insert,
            Roles_Update_ByRoleId,
            Roles_Select_All,
            Roles_Select_ByRoleName,
            Roles_Select_ByRoleId,
        }


        #region >>-- CONSTRUCTORS                                                 -->>--

        public Role()
        {
            //Blank
        }
 
        #endregion >>-- CONSTRUCTORS                                                 -->>--


        #region >>-- Properties                                                   -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "RoleId", IsPrimaryKey = true)]
        public int RoleId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreatedBy")]
        public long CreatedBy { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "RoleName")]
        public string RoleName { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "RoleDescription")]
        public string RoleDescription { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreateDate")]
        public DateTime CreateDate { get; set; }

        #endregion

        #region >>-- Database Methods                                                   -->>--


        protected override bool Insert()
        {
            bool success = false;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                RoleId = (int)mySql.ExecuteScalar(StoredProcedures.Roles_Insert.ToString(),
                    "_RoleName", RoleName,
                    "_CreatedBy", CreatedBy,
                    "_RoleDescription", RoleDescription);
                success = RoleId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving role.", ex); }
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
                RoleId = (int)mySql.ExecuteScalar(StoredProcedures.Roles_Update_ByRoleId.ToString(),
                    "_RoleName", RoleName,
                    "_RoleDescription", RoleDescription);

                success = RoleId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error role user.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;

        }
          protected override bool Remove()
          {
              throw new NotImplementedException();
          }
        public bool ByRoleId(int roleId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                row = mySql.GetDataRow(StoredProcedures.Roles_Select_ByRoleId.ToString(),
                    "_RoleId", roleId);

                if (row != null)
                    success = row.TryParse<Role>(this) && RoleId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading role.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }
        public bool ByRoleName(string roleName)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                row = mySql.GetDataRow(StoredProcedures.Roles_Select_ByRoleName.ToString(),
                    "_RoleName", roleName);

                if (row != null)
                    success = row.TryParse<Role>(this) && RoleId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading role.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }

        #endregion

        public static IEnumerable<Role> SelectAll()
        {
            IEnumerable<Role> roles = null;
            DataTable table = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(DatConnectionString);
                table = mySql.GetDataTable(StoredProcedures.Roles_Select_All.ToString());
                if (table != null)
                    roles = table.Rows.Select<Role>( );

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading roles.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return roles;
        }
    }
}
