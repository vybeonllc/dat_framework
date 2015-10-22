using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Exceptions {

  public class UtilsException : System.Exception {

    public UtilsException() : base() { }
    public UtilsException(string message) : base(message) { }
    public UtilsException(string message, System.Exception ex) : base(message, ex) { }

    public static Exception GetLastException(Exception ex) {

      var finalException = new Exception();

      if (ex != null) {
        if (ex.InnerException != null) finalException = GetLastException(ex.InnerException);
        else                           finalException = ex;
      }

      return finalException;

    }

  }
}
