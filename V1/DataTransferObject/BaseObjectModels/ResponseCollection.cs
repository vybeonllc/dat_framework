using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract(Name = "response")]
    public class ResponseCollection<T> : Utils.Serialization.ISerializable, IResponse<T>
    {
        [DataMember(Name = "status")]
        public StatusInfo Status { get; set; }

        [DataMember(Name = "result_set")]
        public ResultSet<T> ResultSet { get; set; }


        [DataMember(Name = "distributed_asset_technology")]
        public DistributedAssetTechnology DistributedAssetTechnology { get; set; }

        public string ToJson()
        {
            return Dat.V1.Utils.Serialization.JSON.Serializer.Serialize(this);
        }

        public string ToXml()
        {
            return Dat.V1.Utils.Serialization.XML.Serializer.Serialize(this);
        }
        public string ToCSV()
        {
            return Dat.V1.Utils.Serialization.CSV.Serializer.Serialize(this);
        }
        public string ToHtml()
        {
            return Dat.V1.Utils.Serialization.HTML.Serializer.Serialize(this);
        }
    }
}
