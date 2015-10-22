using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace Dat.V1.Utils.Serialization.JSON
{
    public class Serializer
    {

        public static string Serialize(object obj)
        {
            MemoryStream ms = new MemoryStream();
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());

            serializer.WriteObject(ms, obj);

            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        public static T Deserialize<T>(string json) where T : new()
        {
            using (MemoryStream stream = new MemoryStream(Encoding.Unicode.GetBytes(json.Trim())))
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
        }
    }
}
