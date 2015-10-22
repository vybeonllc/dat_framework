using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.BusinessLogic
{
    /// <summary>
    /// Entity class  that all of the classes which represent a database table inherit.
    /// </summary>
    [Serializable]
    public abstract class Base : IDisposable
    {

        #region >>-- CONSTRUCTORS                                                 -->>--


        /// <summary>
        /// Initializes a new instance of the Entity class.
        /// </summary>
        public Base()
        {
            // BLANK
        }


        #endregion

        #region Events

        static event EventHandler<EventArgs.OnCreatingEventArg> onCreating;
        public static event EventHandler<EventArgs.OnCreatingEventArg> OnCreating
        {
            add
            {
                if (onCreating == null || !onCreating.GetInvocationList().Contains(value))
                {
                    onCreating += value;
                }
            }
            remove
            {
                onCreating -= value;
            }
        }
        static event EventHandler<EventArgs.OnCreatedEventArg> onCreated;
        public static event EventHandler<EventArgs.OnCreatedEventArg> OnCreated
        {
            add
            {
                if (onCreated == null || !onCreated.GetInvocationList().Contains(value))
                {
                    onCreated += value;
                }
            }
            remove
            {
                onCreated -= value;
            }
        }

        static event EventHandler<EventArgs.OnUpdatingEventArg> onUpdating;
        public static event EventHandler<EventArgs.OnUpdatingEventArg> OnUpdating
        {
            add
            {
                if (onUpdating == null || !onUpdating.GetInvocationList().Contains(value))
                {
                    onUpdating += value;
                }
            }
            remove
            {
                onUpdating -= value;
            }
        }
        static event EventHandler<EventArgs.OnUpdatedEventArg> onUpdated;
        public static event EventHandler<EventArgs.OnUpdatedEventArg> OnUpdated
        {
            add
            {
                if (onUpdated == null || !onUpdated.GetInvocationList().Contains(value))
                {
                    onUpdated += value;
                }
            }
            remove
            {
                onUpdated -= value;
            }
        }

        static event EventHandler<EventArgs.OnDeletingEventArg> onDeleting;
        public static event EventHandler<EventArgs.OnDeletingEventArg> OnDeleting
        {
            add
            {
                if (onDeleting == null || !onDeleting.GetInvocationList().Contains(value))
                {
                    onDeleting += value;
                }
            }
            remove
            {
                onDeleting -= value;
            }
        }
        static event EventHandler<EventArgs.OnDeletedEventArg> onDeleted;
        public static event EventHandler<EventArgs.OnDeletedEventArg> OnDeleted
        {
            add
            {
                if (onDeleted == null || !onDeleted.GetInvocationList().Contains(value))
                {
                    onDeleted += value;
                }
            }
            remove
            {
                onDeleted -= value;
            }
        }
        #endregion Events

        #region >>-- FIELDS AND PROPERTIES                                        -->>--

        string typeFullName;
        protected string TypeFullName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(typeFullName))
                    typeFullName = this.GetType().FullName;
                return typeFullName;
            }
        }

        protected static string connectionString;
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    try
                    {
                        connectionString = Dat.V1.Utils.Common.ConnectionStrings.AssetConnectionString;
                    }
                    catch (Exception ex)
                    {
                        connectionString = null;
                    }
                }
                return connectionString;
            }
        }


        #endregion

        #region >>-- METHODS                                                      -->>--

        protected abstract bool Insert();
        protected abstract bool Save();
        protected abstract bool Remove();

        public bool Create()
        {
            try
            {
                EventArgs.OnCreatingEventArg arg = new EventArgs.OnCreatingEventArg()
                {
                    EntityName = TypeFullName,
                };
                if (onCreating != null)
                    onCreating(this, arg);
                if (arg.Cancel)
                    return false;
                if (Insert())
                {
                    if (onCreated != null)
                        onCreated(this, new EventArgs.OnCreatedEventArg()
                        {
                            EntityName = TypeFullName,
                            Success = true,
                            UniqueIdentifier = Dat.V1.Utils.Reflection.MemberInfoClass.GetPrimrayKey(this)
                        });
                    return true;
                }
                else
                {
                    if (onCreated != null)
                        onCreated(this, new EventArgs.OnCreatedEventArg()
                        {
                            EntityName = TypeFullName,
                            Error = "Procedure called but it retrieved id as 0.",
                            UniqueIdentifier = Dat.V1.Utils.Reflection.MemberInfoClass.GetPrimrayKey(this)
                        });
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (onCreated != null)
                    onCreated(this, new EventArgs.OnCreatedEventArg()
                    {
                        EntityName = TypeFullName,
                        Error = ex.ToString(),
                        UniqueIdentifier = Dat.V1.Utils.Reflection.MemberInfoClass.GetPrimrayKey(this)
                    });
                throw ex;
            }
        }
        public bool Update()
        {
            try
            {
                EventArgs.OnUpdatingEventArg arg = new EventArgs.OnUpdatingEventArg()
                {
                    EntityName = TypeFullName,
                };
                if (onUpdating != null)
                    onUpdating(this, arg);

                if (arg.Cancel)
                    return false;

                if (Save())
                {
                    if (onUpdated != null)
                        onUpdated(this, new EventArgs.OnUpdatedEventArg()
                        {
                            EntityName = TypeFullName,
                            Success = true,
                            UniqueIdentifier = Dat.V1.Utils.Reflection.MemberInfoClass.GetPrimrayKey(this)
                        });
                    return true;
                }
                else
                {
                    if (onUpdated != null)
                        onUpdated(this, new EventArgs.OnUpdatedEventArg()
                        {
                            EntityName = TypeFullName,
                            Error = "Procedure called but it retrieved id as 0.",
                            UniqueIdentifier = Dat.V1.Utils.Reflection.MemberInfoClass.GetPrimrayKey(this)
                        });
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (onUpdated != null)
                    onUpdated(this, new EventArgs.OnUpdatedEventArg()
                    {
                        EntityName = TypeFullName,
                        Error = ex.ToString(),
                        UniqueIdentifier = Dat.V1.Utils.Reflection.MemberInfoClass.GetPrimrayKey(this)
                    });
                throw ex;
            }
        }
        public bool Delete()
        {
            try
            {
                EventArgs.OnDeletingEventArg arg = new EventArgs.OnDeletingEventArg()
                {
                    EntityName = TypeFullName,
                };
                if (onDeleting != null)
                    onDeleting(this, arg);

                if (arg.Cancel)
                    return false;

                if (Remove())
                {
                    if (onDeleted != null)
                        onDeleted(this, new EventArgs.OnDeletedEventArg()
                        {
                            EntityName = TypeFullName,
                            Success = true,
                            UniqueIdentifier = Dat.V1.Utils.Reflection.MemberInfoClass.GetPrimrayKey(this)
                        });
                    return true;
                }
                else
                {
                    if (onDeleted != null)
                        onDeleted(this, new EventArgs.OnDeletedEventArg()
                        {
                            EntityName = TypeFullName,
                            Error = "Procedure called but it retrieved id as 0.",
                            UniqueIdentifier = Dat.V1.Utils.Reflection.MemberInfoClass.GetPrimrayKey(this)
                        });
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (onDeleted != null)
                    onDeleted(this, new EventArgs.OnDeletedEventArg()
                    {
                        EntityName = TypeFullName,
                        Error = ex.ToString(),
                        UniqueIdentifier = Dat.V1.Utils.Reflection.MemberInfoClass.GetPrimrayKey(this)
                    });
                throw ex;
            }
        }
        #endregion

        #region >>-- DISPOSING                                                    -->>--

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {

        }

        #endregion                                  -->>--


    }
}
