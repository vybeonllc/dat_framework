using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dat.V1.Utils.Threading.TaskManager
{
    public class TaskInfo<T> : IDisposable where T : IDisposable
    {
        #region Events

        public event EventHandler<EventArgs.TaskFailedEventArgs<T>> OnFailed;
        public event EventHandler<EventArgs.TaskCompletedEventArgs<T>> OnCompleted;

        #endregion Events

        #region Properties

        public string TaskID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public Action<TaskInfo<T>> Task { get; set; }
        public T DataItem { get; set; }
        public bool IsBusy { get; set; }
        public System.Threading.ManualResetEvent ResetEvent { get; set; }



        #endregion Properties

        #region Ctors

        public TaskInfo(Action<TaskInfo<T>> task)
        {
            TaskID = Guid.NewGuid().ToString();
            Task = task;
        }

        #endregion Ctors

        #region Task Info Methods


        void OnComplete()
        {
            FinishTime = DateTime.Now;
            IsBusy = false;
            ResetEvent.Set();
            if (OnCompleted != null)
                OnCompleted(this, new EventArgs.TaskCompletedEventArgs<T>(this));
        }

        public void Run()
        {
            StartTime = DateTime.Now;
            try
            {
                Task(this);
            }
            catch (Exception ex)
            {
                Error(ex.Message, ex);
            }

            OnComplete();
        }

        #endregion Task Info Methods

        #region Business Logic Methods



        void Error(string message, Exception ex)
        {
            FinishTime = DateTime.Now;
            IsBusy = false;
            ResetEvent.Set();
            Console.WriteLine("ERROR|" + message + "|" + ex.ToString());
            if (OnFailed != null)
                OnFailed(this, new EventArgs.TaskFailedEventArgs<T>(this, ex, message));
        }


        #endregion Business Logic Methods


        public void Dispose()
        {
            if (DataItem != null)
                DataItem.Dispose();
            Task = null;
        }
    }
}
