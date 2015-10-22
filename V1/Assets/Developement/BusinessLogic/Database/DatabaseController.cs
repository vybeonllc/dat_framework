using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAT.v1.Assets.Developement.BusinessLogic.Database
{
    public class DatabaseController:Base
    {
          #region >>-- CONSTRUCTORS                                                 -->>--


        public DatabaseController()
        {
            //Blank
        }

        public DatabaseController(String connectionString)
            : base(connectionString)
        {
        }



        #endregion >>-- CONSTRUCTORS                                                 -->>--



        #region >>-- Database Methods                                                   -->>--

        public ValueObject.Database Fill(DataRow row)
        {
            ValueObject.Database database = null;
            try
            {
                if (row != null)
                    database = new ValueObject.Database()
                    {
                        DatabaseName = row["DatabaseName"].ToString(),
                    };

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading database data row.", ex); }

            return database;
        }
        #endregion

        #region >>-- Shared Methods                                                   -->>--


        public List<ValueObject.Database> SelectAll()
        {
            List<ValueObject.Database> databases = null;
            System.Data.DataTable table= null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                table = mySql.GetDataTable(StoredProcedures.Database_Select_All.ToString());

                if (table != null)
                {
                    databases = new List<ValueObject.Database>();
                    foreach (System.Data.DataRow row in table.Rows)
                        databases.Add(Fill(row));
                }

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading database.", ex); }
            finally { if (mySql != null) mySql.Dispose(); } 

            return databases;
        }

        #endregion
    }
}
