#region -->> TAGS                                                                   <<--

// -----------------------------------------------------------------------------
// TITILE:      Base Database Connectivity
// CREATED BY:  Jason Martin (YLM Lead Developer)
// CREATED ON:  1/29/2010
// -----------------------------------------------------------------------------
// UPDATED BY:  Jason Martin (YLM Lead Developer)
// UPDATED ON:  1/29/2010
// -----------------------------------------------------------------------------
// UPDATE NOTES:
//
// -----------------------------------------------------------------------------

#endregion

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Text;
using System.Configuration;

namespace Dat.Assets.Scraper.V1.DataLayer
{
    /// <summary>
    /// SQL Wrapper class
    /// </summary>
    /// 
    [Serializable]
    public class MSSQL : IDisposable
    {

        #region -->> CONSTRUCTORS                                                           <<--

        /// <summary>
        /// Initializes a new instance of the MSSQL class.
        /// </summary>
        public MSSQL()
        {
            // BLANK
        }

        /// <summary>
        /// Initializes a new instance of the MSSQL class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MSSQL(String connectionString)
        {
            ConnectionString = connectionString;
        }


        /// <summary>
        /// Initializes a new instance of the MSSQL class.
        /// </summary>
        /// <param name="connection">The SQL connection.</param>
        public MSSQL(SqlConnection connection)
        {
            Connection = connection;
        }

        #endregion

        #region -->> ENUMERATORS                                                            <<--


        #endregion

        #region -->> MEMBERS                                                                <<--

        private SqlConnection _connection;
        private Int32 _commandTimeout = 30;

        #endregion

        #region -->> PROPERTIES                                                             <<--

        /// <summary>
        /// Gets or sets the SQL connection.
        /// </summary>
        /// <value>
        /// The SQL connection.
        /// </value>
        public SqlConnection Connection
        {
            get { return _connection; }
            set
            {
                _connection = value;
                _connection.Open();
            }
        }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public String ConnectionString
        {
            get { return _connection.ToString(); }
            set
            {
                _connection = new SqlConnection(value);
                _connection.Open();
            }
        }

        public Int32 CommandTimeout
        {
            get { return _commandTimeout; }
            set { _commandTimeout = value; }
        }

        #endregion

        #region -->> METHODS                                                                <<--

        
        /// <summary>
        /// Gets a data table.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure by name (string).</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Data Table returned as a result of the stored procedure.</returns>
        public DataTable GetDataTable(String storedProcedure, params Object[] parameters)
        {

            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            DataSet data = null;
            DataTable table = null;

            try
            {

                command = new SqlCommand(storedProcedure.ToString(), _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = CommandTimeout;

                if (parameters != null)
                {
                    for (Int32 index = 0; index <= (parameters.Length - 1); index = index + 2)
                    {
                        Object value;
                        if (parameters[index + 1] == null) value = DBNull.Value;
                        else value = parameters[index + 1];
                        command.Parameters.AddWithValue(parameters[index].ToString(), value);
                    }
                }

                adapter = new SqlDataAdapter(command);
                data = new DataSet();
                adapter.Fill(data, "DataSet");

                if (data.Tables.Count == 1) table = data.Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exceptions.DataLayerException("Error executing the " + storedProcedure.ToString() + " stored procedure.", ex);
            }
            finally
            {
                if (Connection.State != ConnectionState.Closed) Connection.Close();
                Connection.Dispose();
            }
            return table;

        }

      
        /// <summary>
        /// Gets a data row.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure by name (string).</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The Data Row returned as a result of the stored procedure</returns>
        public DataRow GetDataRow(String storedProcedure, params Object[] parameters)
        {

            SqlCommand command = null;
            SqlDataAdapter adapter = null;
            DataSet data = null;
            DataTable table = null;
            DataRow row = null;

            try
            {

                command = new SqlCommand(storedProcedure, _connection);
                command.CommandType = CommandType.StoredProcedure;

                for (Int32 index = 0; index <= (parameters.Length - 1); index = index + 2)
                {
                    Object value;
                    if (parameters[index + 1] == null) value = DBNull.Value;
                    else value = parameters[index + 1];
                    command.Parameters.AddWithValue(parameters[index].ToString(), value);
                }

                adapter = new SqlDataAdapter(command);
                data = new DataSet();
                adapter.Fill(data, "DataSet");

                if (data.Tables.Count >= 1) table = data.Tables[0];
                if (table != null && table.Rows.Count >= 1) row = table.Rows[0];

            }
            catch (Exception ex)
            {
                throw new Exceptions.DataLayerException("Error executing the " + storedProcedure.ToString() + " stored procedure.", ex);
            }
            finally
            {
                if (Connection.State != ConnectionState.Closed) Connection.Close();
                Connection.Dispose();
            }
            return row;

        }

        /// <summary>
        /// Executes the specified stored procedure.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure as an enum.</param>
        /// <param name="parameters">The parameters.</param>
        public void Execute(string storedProcedure, params Object[] parameters)
        {

            SqlCommand command = null;

            try
            {

                command = new SqlCommand(storedProcedure.ToString(), _connection);
                command.CommandTimeout = 120;
                command.CommandType = CommandType.StoredProcedure;

                for (Int32 index = 0; index <= (parameters.Length - 1); index = index + 2)
                {
                    Object value;
                    if (parameters[index + 1] == null) value = DBNull.Value;
                    else value = parameters[index + 1];
                    command.Parameters.AddWithValue(parameters[index].ToString(), value);
                }

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exceptions.DataLayerException("Error executing the " + storedProcedure.ToString() + " stored procedure.", ex);
            }
            finally
            {
                if (Connection.State != ConnectionState.Closed) Connection.Close();
                Connection.Dispose();
            }
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure as an enum.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the Int64 result of the stored procedure</returns>
        public Int64 ExecuteScalar(string storedProcedure, params Object[] parameters)
        {

            Object result = null;
            Int64 id = -1;

            try
            {

                result = ExecuteScalarObject(storedProcedure, parameters);
                if (result != null)
                    id = Int64.Parse(result.ToString());

            }
            catch (Exception ex)
            {
                throw new Exceptions.DataLayerException("Error executing the " + storedProcedure.ToString() + " stored procedure.", ex);
            }
            finally
            {
                if (Connection.State != ConnectionState.Closed) Connection.Close();
                Connection.Dispose();
            }
            return id;

        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="storedProcedure">The stored procedure as an enum.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>the Object result of the stored procedure</returns>
        public Object ExecuteScalarObject(string storedProcedure, params Object[] parameters)
        {
            SqlCommand command = null;
            Object result = null;

            try
            {

                command = new SqlCommand(storedProcedure.ToString(), _connection);
                command.CommandType = CommandType.StoredProcedure;

                for (Int32 index = 0; index <= (parameters.Length - 1); index = index + 2)
                {
                    Object value;
                    if (parameters[index + 1] == null) value = DBNull.Value;
                    else value = parameters[index + 1];
                    command.Parameters.AddWithValue(parameters[index].ToString(), value);
                }

                result = command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                throw new Exceptions.DataLayerException("Error executing the " + storedProcedure.ToString() + " stored procedure.", ex);
            }
            finally
            {
                if (Connection.State != ConnectionState.Closed) Connection.Close();
                Connection.Dispose();
            }
            return result;

        }

        #endregion

        #region -->> DISPOSALS                                                              <<--

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed) Connection.Close();
            Connection.Dispose();
        }

        #endregion

    }
}