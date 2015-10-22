using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Dat.V1.Data.Layers.Entities {

  /// <summary>
  /// Entity class  that all of the classes which represent a database table inherit.
  /// </summary>
  [Serializable] public abstract class MySql : IDisposable {

    #region >>-- CONSTRUCTORS                                                 -->>--

      /// <summary>
      /// Initializes a new instance of the Entity class.
      /// </summary>
      public MySql() { }

    #endregion

    #region >>-- ABSTRACTS FIELDS                                             -->>--

      protected abstract string                     TableName       { get; }
      protected abstract Dictionary<string, object> LoadParameters  { get; }
      protected abstract Dictionary<string, object> SaveParameters  { get; }

    #endregion

    #region >>-- ABSTRACTS METHODS                                            -->>--

      protected abstract void Parse(Dictionary<string, string> data);

    #endregion

    #region >>-- METHODS                                                      -->>--

      public void Save() {
        
        try {
          using (Dat.V1.Data.Layers.MySql.Connector mySQL = new Dat.V1.Data.Layers.MySql.Connector(Constants.ConnectionString)) {
            load(mySQL.GetDataRow(TableName + "_save", SaveParameters.MergeToParametersArray(null)));
          }
        }
        catch (Exception ex) { throw new Exceptions.DataLayerException("Error saving " + TableName, ex); }

      }

    #endregion

    #region >>-- PROTECTED METHODS                                            -->>--

      protected IEnumerable<Dictionary<string, string>> LoadMany(params object[] parameters) {
        var Parameters = new List<object>(LoadParameters.MergeToParametersArray(parameters));

        Parameters.Add("LoadBy");
        Parameters.Add(parameters != null ?
            String.Join(String.Empty, parameters.ToStringIndexedDictionary().Keys.Select(Func => "#" + Func + "#").ToArray()) :
            "#ALL#");

        return genericLoadBy<DataTable>(Parameters.ToArray()).Rows
            .OfType<DataRow>()
            .Select(Func => Func.Table.Columns
                .OfType<DataColumn>()
                .ToDictionary(ColumnFunc => ColumnFunc.ColumnName, ColumnFunc => Func[ColumnFunc.ColumnName].ToString()));
      }

      protected void LoadBy(params object[] parameters)
      {
        var Parameters = new List<object>(LoadParameters.MergeToParametersArray(parameters));

        Parameters.Add("_load_by");
        Parameters.Add(String.Join(String.Empty, parameters.ToStringIndexedDictionary().Keys.Select(Func => "#" + Func + "#").ToArray()));

        load(genericLoadBy<DataRow>(Parameters.ToArray()));
      }

    #endregion

    #region >>-- HELPERS                                                      -->>--

      private void load(DataRow dataRow) {
        
        try {
          if (dataRow != null) { 
            Parse(dataRow.Table.Columns
                .OfType<DataColumn>()
                .ToDictionary(Func => Func.ColumnName, Func => dataRow[Func.ColumnName].ToString()));
          }
        }
        catch (Exception ex) { throw new Exceptions.DataLayerException("Error loading " + TableName + " data row.", ex); }

      }

      private T genericLoadBy<T>(params object[] parameters) where T : class {
        
        try {
          using (Dat.V1.Data.Layers.MySql.Connector mySQL = new Dat.V1.Data.Layers.MySql.Connector(Constants.ConnectionString)) {
            if      (typeof(T) == typeof(DataTable)) return mySQL.GetDataTable(TableName + "_load", parameters) as T;
            else if (typeof(T) == typeof(DataRow))   return mySQL.GetDataRow(TableName + "_load", parameters) as T;
            else                                     throw new Exception("Type " + typeof(T).Name + " not supported");
          }
        }
        catch (Exception ex) { throw new Exceptions.DataLayerException("Error loading " + TableName, ex); }

      }

    #endregion

    #region >>-- DISPOSING                                                    -->>--
   
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
          }

          // Free any unmanaged objects here. 

          disposed = true;
       }

    #endregion

  }
}
