using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class User : Base
    {
        enum StoredProcedures
        {
            Users_Insert,
            Users_Update,
            Users_Update_ByUserGuid,
            Users_Select_ByUserId,
            Users_Select_ByUserGuid,
            Users_Select_ByEmailAddress,
            Users_Select_All,
            Users_Authenticate

        }

        #region >>-- CONSTRUCTORS                                                 -->>--

        public User()
        {
            //Blank
        }

        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- Properties                                                   -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "UserId", IsPrimaryKey = true)]
        public long UserId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreatedBy")]
        public Guid CreatedBy { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "UserGuid")]
        public Guid UserGuid { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "EmailAddress")]
        public string EmailAddress { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "FirstName")]
        public string FirstName { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "LastName")]
        public string LastName { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Password")]
        public string Password { get; set; }

        public string FullName { get { return ((FirstName ?? string.Empty) + " " + (LastName ?? string.Empty)).Trim(); } }

        [Utils.Data.Attributes.Mapping(TargetName = "RoleId")]
        public int RoleId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Active")]
        public bool Active { get; set; }

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
                UserId = mySql.ExecuteScalar(StoredProcedures.Users_Insert.ToString(),
                    "_CreatedBy", CreatedBy,
                    "_FirstName", FirstName,
                    "_LastName", LastName,
                    "_Password", Password,
                    "_EmailAddress", EmailAddress,
                    "_Active", Active,
                    "_RoleId", RoleId);
                success = UserId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving user.", ex); }
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
                UserId = mySql.ExecuteScalar(StoredProcedures.Users_Update.ToString(),
                    "_UserId", UserId,
                    "_EmailAddress", EmailAddress,
                    "_FirstName", FirstName,
                    "_LastName", LastName,
                    "_UserGuid", UserGuid,
                    "_Password", Password,
                    "_Active", Active,
                    "_RoleId", RoleId);
                success = UserId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving user.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;

        }
        protected override bool Remove()
        {
            throw new NotImplementedException();
        }
        public bool UpdateByUserGuid()
        {
            bool success = false;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                UserId = mySql.ExecuteScalar(StoredProcedures.Users_Update_ByUserGuid.ToString(),
                    "_UserGuid", UserGuid,
                    "_EmailAddress", EmailAddress,
                    "_FirstName", FirstName,
                    "_LastName", LastName,
                    "_Password", Password,
                    "_Active", Active,
                    "_RoleId", RoleId);
                success = UserId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving user.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;

        }
        public bool ByUserId(long userId)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                row = mySql.GetDataRow(StoredProcedures.Users_Select_ByUserId.ToString(),
                    "_UserId", userId);
                if (row != null)
                    success = row.TryParse<User>(this) && UserId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading user.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }
            return success;
        }
        public bool ByUserGuid(Guid userGuid)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                row = mySql.GetDataRow(StoredProcedures.Users_Select_ByUserGuid.ToString(),
                    "_UserGuid", userGuid);
                if (row != null)
                    success = row.TryParse<User>(this) && UserId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading user.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }
        public bool ByEmailAddress(string emailAddress)
        {
            bool success = false;
            DataRow row = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                row = mySql.GetDataRow(StoredProcedures.Users_Select_ByEmailAddress.ToString(),
                    "_EmailAddress", emailAddress);
                if (row != null)
                    success = row.TryParse<User>(this) && UserId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading user.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }
            return success;

        }

        public static IEnumerable<User> SelectAll()
        {
            IEnumerable<User> users = null;
            DataTable table = null;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(DatConnectionString);
                table = mySql.GetDataTable(StoredProcedures.Users_Select_All.ToString());
                if (table != null)
                    users = table.Rows.Select<User>();

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading user.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return users;
        }

        #endregion

        public static User Authenticate(string emailAddress, string password)
        {
            DataRow row = null;
            User user = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(DatConnectionString);
                row = mySql.GetDataRow(StoredProcedures.Users_Authenticate.ToString(),
                    "_EmailAddress", emailAddress,
                    "_Password", password);
                if (row == null)
                    return user;
                user = new User();
                if (!row.TryParse<User>(user) || user.UserId < 1)
                    user = null;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading user.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return user;
        }
    }
}
