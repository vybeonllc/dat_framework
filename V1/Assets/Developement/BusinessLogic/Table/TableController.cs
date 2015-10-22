using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAT.v1.Assets.Developement.BusinessLogic.Table
{
    public class TableController:Base
    {
          #region >>-- CONSTRUCTORS                                                 -->>--


        public TableController()
        {
            //Blank
        }

        public TableController(String connectionString)
            : base(connectionString)
        {
        }



        #endregion >>-- CONSTRUCTORS                                                 -->>--



        #region >>-- Database Methods                                                   -->>--

        public ValueObject.Table Fill(DataRow row)
        {
            ValueObject.Table table = null;
            try
            {
                if (row != null)
                    table = new ValueObject.Table()
                    {
                        DatabaseName = row["DatabaseName"].ToString(),
                        TableName = row["TableName"].ToString()
                    };

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading table data row.", ex); }

            return table;
        }
        #endregion

        public ValueObject.Table SelectByTableName(string databaseName, string tableName)
        {
            ValueObject.Table table = null;
            System.Data.DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                row = mySql.GetDataRow(StoredProcedures.Table_Select_ByTableName.ToString(), "@DatabaseName", databaseName, "@TableName", tableName);

                if (row != null)
                    table = Fill(row);

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading tables.", ex); }
            finally { if (mySql != null) mySql.Dispose(); } 

            return table;
        }

        public List<ValueObject.Table> SelectByDatabaseName(string databaseName)
        {
            List<ValueObject.Table> tables = null;
            System.Data.DataTable table= null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                table = mySql.GetDataTable(StoredProcedures.Table_Select_ByDatabaseName.ToString(), "@DatabaseName", databaseName);

                if (table != null)
                {
                    tables = new List<ValueObject.Table>();
                    foreach (System.Data.DataRow row in table.Rows)
                        tables.Add(Fill(row));
                }

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading tables.", ex); }
            finally { if (mySql != null) mySql.Dispose(); } 

            return tables;
        }

    }
}
