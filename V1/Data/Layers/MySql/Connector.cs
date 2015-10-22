using System;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Dat.V1.Data.Layers.MySql {

  [Serializable] public class Connector : IDisposable {

    #region -->> CONSTRUCTORS                                                           <<--

      /// <summary>
      /// Initializes a new instance of the MSSQL class.
      /// </summary>
      public Connector() { }

      /// <summary>
      /// Initializes a new instance of the MSSQL class.
      /// </summary>
      /// <param name="connectionString">The connection string.</param>
      public Connector(String connectionString) {
        ConnectionString = connectionString;
      }

      /// <summary>
      /// Initializes a new instance of the MSSQL class.
      /// </summary>
      /// <param name="connection">The SQL connection.</param>
      public Connector(MySqlConnection connection) {
        Connection = connection;
      }

    #endregion

    #region -->> MEMBERS                                                                <<--

      [NonSerialized] private MySqlConnection _connection;
      [NonSerialized] private Int32           _commandTimeout = 30;

    #endregion

    #region -->> PROPERTIES                                                             <<--

      /// <summary>
      /// Gets or sets the SQL connection.
      /// </summary>
      /// <value>
      /// The SQL connection.
      /// </value>
      public MySqlConnection Connection {
        get { return _connection; }
        set {
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
      public String ConnectionString {
        get { return _connection.ToString(); }
        set {
          _connection = new MySqlConnection(value);
          _connection.Open();
        }
      }

      /// <summary>
      /// Gets or sets the database connection timeout
      /// </summary>
      /// <value>
      /// Timeout in milliseconds.
      /// </value>
      public Int32 CommandTimeout {
        get { return _commandTimeout; }
        set { _commandTimeout = value; }
      }

    #endregion

    #region -->> METHODS                                                                <<--

      /// <summary>
      /// Gets a data table.
      /// </summary>
      /// <param name="storedProcedure">The stored procedure as an enum.</param>
      /// <param name="parameters">The parameters.</param>
      /// <returns>The Data Table returned as a result of the stored procedure.</returns>
      public DataTable GetDataTable(string storedProcedure, params Object[] parameters) {
        int totalResults = 0;
        return GetDataTable(storedProcedure.ToString(), out totalResults, parameters);
      }

      /// <summary>
      /// Gets a data table.
      /// </summary>
      /// <param name="storedProcedure">The stored procedure by name (string).</param>
      /// <param name="parameters">The parameters.</param>
      /// <returns>The Data Table returned as a result of the stored procedure.</returns>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
      public DataTable GetDataTable(string storedProcedure, out int totalResults, params Object[] parameters) {

        MySqlCommand      command = null;
        MySqlDataAdapter  adapter = null;
        DataSet           data    = null;
        DataTable         result  = null;

        totalResults = 0;

        try {

          command                 = new MySqlCommand(storedProcedure, _connection);
          command.CommandType     = CommandType.StoredProcedure;
          command.CommandTimeout  = CommandTimeout;

          if (parameters != null) {
            for (Int32 index = 0; index <= (parameters.Length - 1); index = index + 2) {

              Object value;

              if (parameters[index + 1] == null)  value = DBNull.Value;
              else                                value = parameters[index + 1];

              command.Parameters.AddWithValue(parameters[index].ToString(), value);

            }
          }

          adapter = new MySqlDataAdapter(command);
          data    = new DataSet();

          adapter.Fill(data, "DataSet");

          if (data.Tables.Count > 0) result       = data.Tables[0];
          if (data.Tables.Count > 1) totalResults = int.Parse(data.Tables[1].Rows[0]["TotalResults"].ToString());

        }
        catch (Exception ex) {
          throw new Exceptions.DataLayerException("Error executing the " + storedProcedure.ToString() + " stored procedure.", ex);
        }
        finally {
          if (Connection.State != ConnectionState.Closed) Connection.Close();
          Connection.Dispose();
        }

        return result;

      }

      /// <summary>
      /// Gets a data row.
      /// </summary>
      /// <param name="storedProcedure">The stored procedure by name (string).</param>
      /// <param name="parameters">The parameters.</param>
      /// <returns>The Data Row returned as a result of the stored procedure</returns>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
      public DataRow GetDataRow(string storedProcedure, params Object[] parameters) {

        MySqlCommand      command = null;
        MySqlDataAdapter  adapter = null;
        DataSet           data    = null;
        DataTable         table   = null;
        DataRow           result  = null;

        try {

          command             = new MySqlCommand(storedProcedure, _connection);
          command.CommandType = CommandType.StoredProcedure;

          for (Int32 index = 0; index <= (parameters.Length - 1); index = index + 2) {

            Object value;

            if (parameters[index + 1] == null)  value = DBNull.Value;
            else                                value = parameters[index + 1];

            command.Parameters.AddWithValue(parameters[index].ToString(), value);

          }

          adapter = new MySqlDataAdapter(command);
          data    = new DataSet();

          adapter.Fill(data, "DataSet");

          if (data.Tables.Count >= 1)                 table   = data.Tables[0];
          if (table != null && table.Rows.Count >= 1) result  = table.Rows[0];

        }
        catch (Exception ex) {
            throw new Exceptions.DataLayerException("Error executing the " + storedProcedure.ToString() + " stored procedure.", ex);
        }
        finally {
          if (Connection.State != ConnectionState.Closed) Connection.Close();
          Connection.Dispose();
        }

        return result;

      }

      /// <summary>
      /// Executes the specified stored procedure.
      /// </summary>
      /// <param name="storedProcedure">The stored procedure as an enum.</param>
      /// <param name="parameters">The parameters.</param>
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
      public void Execute(string storedProcedure, params Object[] parameters) {

        MySqlCommand command = null;

        try {

          command                 = new MySqlCommand(storedProcedure, _connection);
          command.CommandTimeout  = 120;
          command.CommandType     = CommandType.StoredProcedure;

          for (Int32 index = 0; index <= (parameters.Length - 1); index = index + 2) {

            Object value;

            if (parameters[index + 1] == null)  value = DBNull.Value;
            else                                value = parameters[index + 1];

            command.Parameters.AddWithValue(parameters[index].ToString(), value);

          }

          command.ExecuteNonQuery();

        }
        catch (Exception ex) {
          throw new Exceptions.DataLayerException("Error executing the " + storedProcedure.ToString() + " stored procedure.", ex);
        }
        finally {
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
      public Int64 ExecuteScalar(string storedProcedure, params Object[] parameters) {

        Object  result  = null;
        Int64   id      = -1;

        try {
          result = ExecuteScalarObject(storedProcedure, parameters);
          if (result != null) id = Int64.Parse(result.ToString());
        }
        catch (Exception ex) {
          throw new Exceptions.DataLayerException("Error executing the " + storedProcedure.ToString() + " stored procedure.", ex);
        }
        finally {
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
      [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
      public Object ExecuteScalarObject(string storedProcedure, params Object[] parameters) {

        MySqlCommand  command = null;
        Object        result  = null;

        try {

          command             = new MySqlCommand(storedProcedure, _connection);
          command.CommandType = CommandType.StoredProcedure;

          for (Int32 index = 0; index <= (parameters.Length - 1); index = index + 2) {
                
            Object value;

            if (parameters[index + 1] == null)  value = DBNull.Value;
            else                                value = parameters[index + 1];

            command.Parameters.AddWithValue(parameters[index].ToString(), value);

          }

          result = command.ExecuteScalar();

        }
        catch (Exception ex) {
          throw new Exceptions.DataLayerException("Error executing the " + storedProcedure.ToString() + " stored procedure.", ex);
        }
        finally {
          if (Connection.State != ConnectionState.Closed) Connection.Close();
          Connection.Dispose();
        }

        return result;

      }

    #endregion

    #region -->> DISPOSALS                                                              <<--

      [NonSerialized] bool        disposed  = false;                                  // Flag: Has Dispose already been called? 
      [NonSerialized] SafeHandle  handle    = new SafeFileHandle(IntPtr.Zero, true);  // Instantiate a SafeHandle instance.

      /// <summary>
      /// Public implementation of Dispose pattern callable by consumers. 
      /// </summary>
      public void Dispose() { 
        Dispose(true);
        GC.SuppressFinalize(this);           
      }

      /// <summary>
      /// Protected implementation of Dispose pattern. 
      /// </summary>
      /// <param name="disposing">Are we already Disposing?</param>
      protected virtual void Dispose(bool disposing) {
          
        if (disposed) return; 

        if (disposing) {

          handle.Dispose();
              
          // Free any other managed objects here. 
          if (_connection.State != ConnectionState.Closed) _connection.Close();
          _connection.Dispose();

        }

        // Free any unmanaged objects here. 
        disposed = true;

      }

    #endregion

  }

}