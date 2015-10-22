using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Dat.V1.Dto.Bom.SharedObjects
{
    [DataContract()]
    public class Location : Dat.V1.Utils.Serialization.ISerializable
    {
        [DataMember(Name = "id")]
        public long ID { get; set; }

        [DataMember(Name = "primary")]
        public bool Primary { get; set; }

        [DataMember(Name = "latlong")]
        public LatLong LatLong { get; set; }


        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "county")]
        public string County { get; set; }

        [DataMember(Name = "unit")]
        public string Unit { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "state_abb")]
        public string StateAbb { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }

        [DataMember(Name = "zipcode")]
        public string ZipCode { get; set; }

        [DataMember(Name = "long_address")]
        public string LongAddress { get; set; }

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
