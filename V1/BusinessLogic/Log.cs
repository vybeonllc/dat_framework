using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.BusinessLogic
{
    [Serializable]
    public class Log : Base
    {
        enum StoredProcedures
        {
            Logs_Insert,
            Logs_Update,
        }
        #region >>-- CONSTRUCTORS                                                 -->>--


        public Log()
        {
            //Blank
        }

        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- PROPERTIES                                                 -->>--

        [Utils.Data.Attributes.Mapping(TargetName = "LogId", IsPrimaryKey = true)]
        public long LogId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "RequestId")]
        public long RequestId { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "LogType")]
        public int LogType { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Title")]
        public string Title { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Text")]
        public string Text { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "ReferenceKey")]
        public string ReferenceKey { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Action")]
        public int Action { get; set; }

        [Utils.Data.Attributes.Mapping(TargetName = "Success")]
        public bool Success { get; set; }

        #endregion >>-- PROPERTIES                                                 -->>--

        #region >>-- Database Methods                                                   -->>--


        protected override bool Insert()
        {
            bool success = false;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                LogId = (int)mySql.ExecuteScalar(StoredProcedures.Logs_Insert.ToString(),
                    "_RequestId", RequestId,
                    "_LogType", LogType,
                    "_Title", Title,
                    "_Text", Text,
                    "_ReferenceKey", ReferenceKey,
                    "_Action", Action,
                    "_Success", Success);
                success = LogId > 1;
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error saving log.", ex); }
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

        #endregion

        #region >>-- Shared Methods                                                   -->>--


        #endregion
    }
}
