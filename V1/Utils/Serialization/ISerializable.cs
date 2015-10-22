using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Serialization
{
    public interface ISerializable 
    {
        string ToJson();
        string ToXml();
        string ToCSV();
        string ToHtml();
    }
}
