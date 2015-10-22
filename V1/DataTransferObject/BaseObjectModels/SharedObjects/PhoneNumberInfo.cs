using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Dat.V1.Dto.Bom.SharedObjects
{
    [DataContract()]
    public class PhoneNumberInfo : Dat.V1.Utils.Serialization.ISerializable
    {
        [DataMember(Name = "id")]
        public long ID { get; set; }

        [DataMember(Name = "line_type")]
        public string LineType { get; set; }

        [DataMember(Name = "carrier")]
        public string Carrier { get; set; }

        [DataMember(Name = "owner_name")]
        public string OwnerName { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "phone")]
        public Phone Phone { get; set; }

        public new string ToJson()
        {
            return Dat.V1.Utils.Serialization.JSON.Serializer.Serialize(this);
        }
        public new string ToXml()
        {
            return Dat.V1.Utils.Serialization.XML.Serializer.Serialize(this);
        }
        public new string ToCSV()
        {
            return Dat.V1.Utils.Serialization.CSV.Serializer.Serialize(this);
        }
        public new string ToHtml()
        {
            return Dat.V1.Utils.Serialization.HTML.Serializer.Serialize(this);
        }
    }
}
