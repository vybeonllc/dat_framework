using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DAT.v1.Assets.Developement.BusinessLogic.Column
{
    public class ColumnController : Base
    {
        #region >>-- CONSTRUCTORS                                                 -->>--


        public ColumnController()
        {
            //Blank
        }

        public ColumnController(String connectionString)
            : base(connectionString)
        {
        }



        #endregion >>-- CONSTRUCTORS                                                 -->>--



        #region >>-- Database Methods                                                   -->>--

        public ValueObject.Column Fill(DataRow row)
        {
            ValueObject.Column table = null;
            try
            {
                if (row != null)
                    table = new ValueObject.Column()
                    {
                        DatabaseName = row["DatabaseName"].ToString(),
                        TableName = row["TableName"].ToString(),
                        IsNullable = bool.Parse(row["IsNullable"].ToString()),
                        AutoIncrement = bool.Parse(row["AutoIncrement"].ToString()),
                        ColumnName = row["ColumnName"].ToString(),
                        OrdinalPosition = int.Parse(row["OrdinalPosition"].ToString()),
                        PrimaryKey = bool.Parse(row["PrimaryKey"].ToString())
                    };

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading column data row.", ex); }

            return table;
        }
        #endregion

        public ValueObject.Column SelectByColumneName(string databaseName, string tableName, string columnName)
        {
            ValueObject.Column column = null;
            System.Data.DataRow row = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                row = mySql.GetDataRow(StoredProcedures.Column_Select_ByColumnName.ToString(), "@DatabaseName", databaseName, "@TableName", tableName, "@ColumnName", columnName);

                if (row != null)
                    column = Fill(row);

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading column.", ex); }
            finally { if (mySql != null) mySql.Dispose(); } 

            return column;
        }
        public List<ValueObject.Column> SelectByTableName(string databaeName, string tableName)
        {
            List<ValueObject.Column> columns = null;
            System.Data.DataTable table = null;
            DataLayer.MySQL mySql = null;
            try
            {
                mySql = new DataLayer.MySQL(ConnectionString);
                table = mySql.GetDataTable(StoredProcedures.Column_Select_ByTableName.ToString(), "@DatabaseName", databaeName, "@TableName", tableName);

                if (table != null)
                {
                    columns = new List<ValueObject.Column>();
                    foreach (System.Data.DataRow row in table.Rows)
                        columns.Add(Fill(row));
                }

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading columns.", ex); }
            finally { if (mySql != null) mySql.Dispose(); } 

            return columns;
        }

    }
}
