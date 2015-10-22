using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DAT.v1.DTO.Standards.Location.PostalCode
{
    [DataContract()]
    public class PostalCodeInfo : DAT.v1.Utils.Serialization.ISerializable
    {
        [DataMember(Name = "id")]
        public long PostalCodeID { get; set; }

        [DataMember(Name = "postalcode")]
        public string PostalCode { get; set; }

        [DataMember(Name = "telephone_prefix")]
        public string TelephonePrefix { get; set; }

        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        [DataMember(Name = "elevation")]
        public int Elevation { get; set; }

        [DataMember(Name = "utc")]
        public int UTC { get; set; }

        [DataMember(Name = "daylight_saving")]
        public bool DayLightSaving { get; set; }

        [DataMember(Name = "city_town")]
        public string CityTown { get; set; }

        [DataMember(Name = "city_town_alias")]
        public string CityTownAlias { get; set; }

        [DataMember(Name = "locality")]
        public string Locality { get; set; }

        [DataMember(Name = "region")]
        public string Region { get; set; }

        [DataMember(Name = "country_code")]
        public int CountryCode { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        public new string ToJson()
        {
            return DAT.v1.Utils.Serialization.JSON.Serializer.Serialize(this);
        }
        public new string ToXml()
        {
            return DAT.v1.Utils.Serialization.XML.Serializer.Serialize(this);
        }
        public new string ToHtml()
        {
            return DAT.v1.Utils.Serialization.HTML.Serializer.Serialize(this);
        }
        public new string ToCSV()
        {
            return DAT.v1.Utils.Serialization.CSV.Serializer.Serialize(this);
        }
    }
}
