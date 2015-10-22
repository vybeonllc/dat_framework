using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dat.V1.Utils.Threading.TaskManager
{
    public class TaskManager<T> where T :   IDisposable
    {
        #region Events

        public event EventHandler<EventArgs.TaskFailedEventArgs<T>> OnTaskFailed;
        public event EventHandler<EventArgs.TaskCompletedEventArgs<T>> OnTaskCompleted;
        public event EventHandler<EventArgs.TaskAdded<T>> OnTaskAdded;

        #endregion Events

        #region Properties

        public int MaxThreads;
        public int CurrentThreads { get { int result = 0; lock (Tasks) result = Tasks.Count(task => task.IsBusy); return result; } }
        public int TotalActive { get { return CurrentThreads; } }
        public int Total { get { return Tasks.Count; } }
        List<System.Threading.ManualResetEvent> resetEvents = new List<System.Threading.ManualResetEvent>();
        public bool Stop;
        public List<TaskInfo<T>> Tasks { get; set; }

        #endregion Properties

        #region Ctors

        public TaskManager()
        {
            MaxThreads = 2;
            Tasks = new List<TaskInfo<T>>();
        }

        #endregion Ctors

        #region Event Handlers

        void task_OnFailed(object sender, EventArgs.TaskFailedEventArgs<T> e)
        {
            if (OnTaskFailed != null)
                OnTaskFailed(sender, e);
            lock (Tasks)
                Tasks.RemoveAll(t => t.TaskID == e.Task.TaskID);
            if (e.Task != null)
                e.Task.Dispose();
        }
        void task_OnCompleted(object sender, EventArgs.TaskCompletedEventArgs<T> e)
        {
            if (OnTaskCompleted != null)
                OnTaskCompleted(sender, e);
            lock (Tasks)
                Tasks.RemoveAll(t => t.TaskID == e.Task.TaskID);
            if (e.Task != null)
                e.Task.Dispose();
        }

        #endregion Event Handlers

        #region Methods

        public bool HasRoom
        {
            get
            {
                return MaxThreads - CurrentThreads > 0;
            }
        }
        public bool Add(TaskInfo<T> task)
        {
            while (!HasRoom) ;

            DateTime creating = DateTime.Now;
            task.OnCompleted += new EventHandler<EventArgs.TaskCompletedEventArgs<T>>(task_OnCompleted);
            task.OnFailed += new EventHandler<EventArgs.TaskFailedEventArgs<T>>(task_OnFailed);
            System.Threading.ManualResetEvent resetEvent = new System.Threading.ManualResetEvent(false);
            task.ResetEvent = resetEvent;

            resetEvents.Add(resetEvent);
            task.IsBusy = true;
            Tasks.Add(task);
            System.Threading.ThreadPool.QueueUserWorkItem(x => task.Run());
            if (OnTaskAdded != null)
                OnTaskAdded(this, new EventArgs.TaskAdded<T>(task));
            return true;
        }

        #endregion Methods
    }
}
