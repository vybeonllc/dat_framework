using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Threading.TaskManager.Exceptions
{
    public class TaskManagerException : Utils.Threading.Exceptions.ThreadingException
    {
        public TaskManagerException () : base() { }
        public TaskManagerException (string message) : base(message) { }
        public TaskManagerException(string message, System.Exception ex) : base(message, ex) { }
    }
}
