using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class UserEvent : Base
    {
        enum StoredProcedures
        {
            UserEvents_Insert,
            UserEvents_Update,
            UserEvents_Select_ByUserEventId
        }

        #region >>-- CONSTRUCTORS                                                 -->>--

        public UserEvent()
        {
            //Blank
        }

        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- Properties                                                   -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "UserEventId")]
        public long UserEventId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "UserId")]
        public Guid UserGuid { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "EventId")]
        public int EventId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "UserEventReferenceKey")]
        public long UserEventReferenceKey { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Description")]
        public string Description { get; set; }

        #endregion

        #region >>-- Database Methods                                                   -->>--


        protected override bool Insert()
        {
            bool success = false;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                UserEventId = mySql.ExecuteScalar(StoredProcedures.UserEvents_Update.ToString(),
                    "_UserEventId", UserEventId,
                    "_UserEventReferenceKey", UserEventReferenceKey,
                    "_UserGuid", UserGuid,
                    "_EventId", EventId,
                    "_UserEventDescription", Description);
                success = UserEventId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving user event.", ex); }
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
                UserEventId = mySql.ExecuteScalar(StoredProcedures.UserEvents_Insert.ToString(),
                    "_UserEventReferenceKey", UserEventReferenceKey,
                    "_UserGuid", UserGuid,
                    "_EventId", EventId,
                    "_UserEventDescription", Description);
                success = UserEventId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving user event.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;

        }
        protected override bool Remove()
        {
            throw new NotImplementedException();
        }

        public bool ByUserEventId(long userEventID)
        {
            DataRow row = null;
            bool success = false;
            DataLayer.MySQL mySql = null;

            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                row = mySql.GetDataRow(StoredProcedures.UserEvents_Select_ByUserEventId.ToString(),
                    "_UserEventID", userEventID);

                if (row != null)
                    success = row.TryParse<UserEvent>(this) && UserEventId > 0;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading user event.", ex); }
            finally { if (mySql != null) mySql.Dispose(); }

            return success;
        }

        #endregion

        #region >>-- Shared Methods                                                   -->>--


        #endregion
    }
}
