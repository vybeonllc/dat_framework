using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom.SharedObjects
{
    [DataContract()]
    public class TimeZone
    {
        [DataMember(Name = "name_abb")]
        public string NameAbbreviation { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "offset")]
        public double Offset { get; set; }

        public static TimeZone GetTimeZoneOffset(string name_abb)
        {
            Enumerations.TimeZoneAbbrevations timezone_abb = Enumerations.TimeZoneAbbrevations.Unknown;
            int offset = (int)Enumerations.TimeZoneAbbrevations.Unknown;
            if (Enum.TryParse<Enumerations.TimeZoneAbbrevations>(name_abb, out timezone_abb) && timezone_abb != Enumerations.TimeZoneAbbrevations.Unknown)
                offset = (int)timezone_abb;
            string name = Enum.GetName(typeof(Enumerations.TimeZones), offset);
            return new TimeZone()
            {
                Name = name,
                NameAbbreviation = timezone_abb.ToString(),
                Offset = TimeZoneInfo.FindSystemTimeZoneById(name.Replace("_", " ")).BaseUtcOffset.TotalHours
            };
        }
    }
}
