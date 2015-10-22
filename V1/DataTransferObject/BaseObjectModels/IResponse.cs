using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Dto.Bom
{
    public interface IResponse<T> : Dat.V1.Utils.Serialization.ISerializable
    {
        StatusInfo Status { get; set; }

        DistributedAssetTechnology DistributedAssetTechnology { get; set; }
    }
}
