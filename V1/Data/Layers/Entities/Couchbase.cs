using Couchbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Data.Layers.Entities
{
    public class CallbackResult
    {
        public object Value { get; set; }
        public ulong Cas { get; set; }
    }

    [Serializable]
    public abstract class Couchbase : IDisposable
    {
        #region >>-- CONSTRUCTORS                                                 -->>--
        /// <summary>
        /// Initializes a new instance of the Entity class.
        /// </summary>
        public Couchbase() { }

        public Couchbase(object entity)
        {
            Value = entity;
        }
        #endregion

        #region >>-- ABSTRACTS FIELDS                                             -->>--

        protected abstract string EntityName { get; }
        protected abstract Type EntityType { get; }
        protected abstract string[] Keys { get; }

        #endregion

        #region >>-- PROPERTIES                                                   -->>--

        public abstract object Value
        {
            get;
            protected set;
        }

        private string _key;
        public string Key
        {
            get 
            { 
                if (_key == null)
                {
                    SetKey(Keys, true);
                }
                return _key; 
            }
            private set { _key = value; }
        }

        private ulong _cas;

        public Func<Couchbase>  CallGetValueCallback  { get; set; }

        #endregion

        #region >>-- PRIVATE METHODS                                              -->>--

        /// <summary>
        ///Generates a unique key based from an string array prefixed by th  e document type then sperated by double colons.
        ///<para>   Format: "entityType::key1::key2::key3"</para>
        /// </summary>
        /// <returns></returns>
        public void SetKey(string[] keys, bool prefixEntityName)
        {
            Key = (prefixEntityName ? EntityName + "::" : string.Empty) + string.Join("::", Keys);
        }

        public void SetValue(object obj)
        {
            if (obj.GetType() == EntityType)
            {
                Value = obj;
            }
            else { throw new Dat.V1.Data.Layers.Exceptions.DataLayerException("Invalid entity type exception, object must be of type " + EntityType.Name); }
        }

        //private string GetDocumentType()
        //{
        //    try
        //    {
        //        Attribute attrib = Attribute.GetCustomAttribute(Value.GetType(), typeof(DataContractAttribute));
        //        return ((DataContractAttribute)attrib).Name;
        //    }
        //    catch (Exception ex) { throw new Exceptions.DataLayerException("Error getting DocumentType: Entity 'DataMemberAttribute' " + Value.GetType().Name, ex); }
        //}
        #endregion

        #region >>-- PUBLIC METHODS                                               -->>--

        public T Cast<T>()
        {
            return (T)Value;
        }

        public void Save()
        {
            try
            {
                using (Layers.Couchbase.Connector cb = new Layers.Couchbase.Connector(
                    Constants.Servers,
                    Constants.Bucket))
                {
                    cb.Upsert(Key, Value);
                }
            }
            catch (Exception ex) { throw new Exceptions.DataLayerException("Error saving " + Key, ex); }
        }

        //Replace or insert a document that is deleted after a specified number of days
        public void Save(TimeSpan expires)
        {
            try
            {
                using (Layers.Couchbase.Connector cb = new Layers.Couchbase.Connector(
                    Constants.Servers,
                   Constants.Bucket))
                {
                    Value = cb.Upsert(Key, Value, expires).Value;
                }
            }
            catch (Exception ex) { throw new Exceptions.DataLayerException("Error saving " + Key, ex); }
        }

        public void SafeSave(Func<Couchbase> callback, long delay, int retryAttempts)
        {
            try
            {
                using (Layers.Couchbase.Connector cb = new Layers.Couchbase.Connector(
                    Constants.Servers,
                    Constants.Bucket))
                {

                    var result = cb.SafeUpsert(Key, Value, _cas);
                    while (!result.Success)
                    {
                        var val = callback();
                        result = cb.SafeUpsert(Key, val.Value, val._cas);
                    }
                    Value = result.Value;
                    _cas = result.Cas;
                }
            }
            catch (Exception ex) { throw new Exceptions.DataLayerException("Error saving " + Key, ex); }
        }

        //Replaces Value with null if document does not exist
        public void Load<T>() 
        {
            try
            {  
                using (Layers.Couchbase.Connector cb = new Layers.Couchbase.Connector(
                    Constants.Servers,
                    Constants.Bucket))
                {
                    var v = cb.Get<T>(Key);
                    Value = v.Value;
                    _cas = v.Cas;
                }
            }
            catch (Exception ex) { throw new Exceptions.DataLayerException("Error loading " + Key, ex); }
        }

        public ulong GetNextId()
        {
            try
            {
                using (Layers.Couchbase.Connector cb = new Layers.Couchbase.Connector(   
                    Constants.Servers,
                    Constants.Bucket))
                {
                    var result = cb.Increment(EntityName + "::count", 1);
                    if (result.Success)
                    {
                        return result.Value;
                    }
                    else
                    {
                        throw new Exception(result.Message);
                    }
                }
            }
            catch (Exception ex) { throw new Exceptions.DataLayerException("Error GetNextKey ", ex); }
        }

        #endregion


        #region STATIC METHODS

        #endregion

        #region >>-- DISPOSING                                                    -->>--
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() { }
        #endregion
    }
}
