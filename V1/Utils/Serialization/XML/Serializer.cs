using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.Utils.Serialization.XML
{
    public class Serializer
    {
        public static string Serialize<T>(T item)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
            settings.Indent = false;
            settings.OmitXmlDeclaration = false;
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("data", "http://data.synclistings.com");

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, item);
                }
                return textWriter.ToString();
            }
        }
        public static T Deserialize<T>(string data) where T : new()
        {
            T xml = new T();
            XmlSerializer ser = new XmlSerializer(typeof(T));
            xml = (T)ser.Deserialize(data.ToStream());
            return xml;
        }
    }
}
