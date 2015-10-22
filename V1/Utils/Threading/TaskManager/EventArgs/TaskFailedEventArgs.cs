using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dat.V1.Utils.Threading.TaskManager.EventArgs
{
    public class TaskFailedEventArgs<T> : TaskManagerEventArgs where T : IDisposable
    {
        public string Reason { get; set; }
        public Exception Exception { get; set; }
        public TaskInfo<T> Task { get; set; }

        public TaskFailedEventArgs(TaskInfo<T> task, Exception exception, string reason)
        {
            Task = task;
            Exception = exception;
            Reason = reason;
        }
    }
}
