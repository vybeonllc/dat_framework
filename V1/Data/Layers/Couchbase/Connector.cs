using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using Couchbase;
using Couchbase.Configuration.Client;
using System.Linq;
using System.Collections.Generic;
using Couchbase.Core;
using Dat.V1.Data.Layers.Exceptions;


namespace Dat.V1.Data.Layers.Couchbase {

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
    public Connector(string[] servers, string bucket)
    {
        Servers = servers;
        Bucket = bucket;
        Config = CreateConfiguration();
    }

    #endregion

    #region -->> MEMBERS                                                                <<--

      [NonSerialized] private string[] _servers;
      [NonSerialized] private string _bucket;
      [NonSerialized] private ClientConfiguration _config;
      [NonSerialized] private Int32  _commandTimeout = 30;

    #endregion

    #region -->> PROPERTIES                                                             <<--

    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    /// <value>
    /// The connection string.
    /// </value>
    public string[] Servers {
    get { return _servers; }
    set { _servers = value;           }
    }

    public string Bucket
    {
        get { return _bucket; }
        set { _bucket = value; }
    }

    public ClientConfiguration Config
    {
        get { return _config; }
        set { _config = value; }
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

    private ClientConfiguration CreateConfiguration()
    {
        ClientConfiguration config = new ClientConfiguration
        {
            Servers = Servers.Select(x => new Uri(x)).ToList<Uri>(),
            UseSsl = false,
            DefaultOperationLifespan = 1000,
            BucketConfigs = new Dictionary<string, BucketConfiguration>
                {
                    { Bucket, new BucketConfiguration
                    {
                        BucketName = Bucket,
                        UseSsl = false,
                        Password = "",
                        DefaultOperationLifespan = 2000,
                        PoolConfiguration = new PoolConfiguration
                        {
                            MaxSize = 10,
                            MinSize = 5,
                            SendTimeout = 12000
                        }
                    }}
                }
        };
        return config;
    }

    public IOperationResult<T> Get<T>(string key)
    {
        Cluster cluster = new Cluster(Config);
        IBucket bucket = cluster.OpenBucket(Bucket);
        try
        {
            return bucket.Get<T>(key);
        }
        catch (Exception ex)
        {
            throw new DataLayerException("Error getting key " + key, ex);
        }
        finally
        {
            if (bucket != null)
            {
                cluster.CloseBucket(bucket);
                cluster.Dispose();
            }
        }
    }

    public IOperationResult<object> Insert(string key, object value)
    {
        Cluster cluster = new Cluster(Config);
        IBucket bucket = cluster.OpenBucket(Bucket);
        try
        {
            return bucket.Insert(key, value);
        }
        catch (Exception ex)
        {
            throw new DataLayerException("Error inserting key " + key, ex);
        }
        finally
        {
            if (bucket != null)
            {
                cluster.CloseBucket(bucket);
                cluster.Dispose();
            }
        }
    }

    public IOperationResult<object> Upsert(string key, object value)
    {
        Cluster cluster = new Cluster(Config);
        IBucket bucket = cluster.OpenBucket(Bucket);
        try
        {
            return bucket.Upsert(key, value);
        }
        catch (Exception ex)
        {
            throw new DataLayerException("Error inserting key " + key, ex);
        }
        finally
        {
            if (bucket != null)
            {
                cluster.CloseBucket(bucket);
                cluster.Dispose();
            }
        }
    }
    public IOperationResult<object> Upsert(string key, object value, TimeSpan expiration)
    {
        Cluster cluster = new Cluster(Config);
        IBucket bucket = cluster.OpenBucket(Bucket);
        try
        {
            return bucket.Upsert(key, value, expiration);
        }
        catch (Exception ex)
        {
            throw new DataLayerException("Error inserting key " + key, ex);
        }
        finally
        {
            if (bucket != null)
            {
                cluster.CloseBucket(bucket);
                cluster.Dispose();
            }
        }
    }
           
    //public IOperationResult<object> SafeUpsert(string key, object value, ulong cas)
    //{
    //    Cluster cluster = new Cluster(Config);
    //    IBucket bucket = cluster.OpenBucket(Bucket);
    //    try
    //    {

    //        IOperationResult<object> output = bucket.Upsert(key, value, cas);
    //        while (!output.Success)
    //        {
    //            //Thread.Sleep(1000);
    //            var newVal = getValueCallback();
    //            output = bucket.Upsert(key, newVal.Value, newVal.Cas);
    //        }
    //        return output;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new DataLayerException("Error updating key " + key, ex);
    //    }
    //    finally
    //    {
    //        if (bucket != null)
    //        {
    //            cluster.CloseBucket(bucket);
    //            cluster.Dispose();
    //        }
    //    }
    //}

    public IOperationResult<object> SafeUpsert(string key, object value, ulong cas)
    {
        Cluster cluster = new Cluster(Config);
        IBucket bucket = cluster.OpenBucket(Bucket);
        try
        {
            return bucket.Upsert(key, value, cas);
        }
        catch (Exception ex)
        {
            throw new DataLayerException("Error updating key " + key, ex);
        }
        finally
        {
            if (bucket != null)
            {
                cluster.CloseBucket(bucket);
                cluster.Dispose();
            }
        }
    }

    public IOperationResult<ulong> Increment(string key, ulong delta)
    {
        Cluster cluster = new Cluster(Config);
        IBucket bucket = cluster.OpenBucket(Bucket);
        try
        {
            return bucket.Increment(key, delta);
        }
        catch (Exception ex)
        {
            throw new DataLayerException("Error incrementing key " + key, ex);
        }
        finally
        {
            if (bucket != null)
            {
                cluster.CloseBucket(bucket);
                cluster.Dispose();
            }
        }
    }

    public IOperationResult<ulong> Decrement(string key, ulong delta)
    {
        Cluster cluster = new Cluster(Config);
        IBucket bucket = cluster.OpenBucket(Bucket);
        try
        {
            return bucket.Increment(key, delta);
        }
        catch (Exception ex)
        {
            throw new DataLayerException("Error decrementing key " + key, ex);
        }
        finally
        {
            if (bucket != null)
            {
                cluster.CloseBucket(bucket);
                cluster.Dispose();
            }
        }
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


    }

    // Free any unmanaged objects here. 
    disposed = true;

    }

    #endregion

    }

    public class CallbackResult
    {
        public object Value { get; set; }
        public ulong Cas { get; set; }
    }
  
}
