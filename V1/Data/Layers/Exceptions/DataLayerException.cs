using System;

namespace Dat.V1.Data.Layers.Exceptions {
 
  [Serializable] public class DataLayerException : System.Exception {

    #region -->> CONSTRUCTORS                                                           <<--

      public DataLayerException()                                     : base()            { }
      public DataLayerException(string message)                       : base(message)     { }
      public DataLayerException(string message, System.Exception ex)  : base(message, ex) { }

    #endregion

  }

}