using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dat.V1.Utils.Threading.TaskManager.EventArgs
{
    public class TaskCompletedEventArgs<T> :TaskManagerEventArgs where T :  IDisposable
    {
        public TaskInfo<T> Task { get; set; }
        public TaskCompletedEventArgs(TaskInfo<T> task)
        {
            Task = task;
        }
    }
}
