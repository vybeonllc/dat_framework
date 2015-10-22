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

namespace Dat.V1.Utils.Serialization.CSV
{
    public class Serializer
    {

        public static string Serialize(object obj)
        {
            return "";
        }
        public static T Deserialize<T>(string csv) where T : new()
        {
            return new T();
        }
    }
}
