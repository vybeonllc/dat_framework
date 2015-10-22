using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Administrative.AssetRole
{
    [DataContract()]
    public class Manifest
    {
        [DataMember(Name = "asset_role")]
        public AssetRole AssetRole { get; set; }

        public Manifest()
        {
        }

    }
}
