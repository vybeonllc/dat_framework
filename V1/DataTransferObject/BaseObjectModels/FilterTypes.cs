using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Bom
{
    [DataContract()]
    public enum FilterTypes
    {
        [EnumMember(Value = "equal")]
        Equal = 1,

        [EnumMember(Value = "not_equal")]
        NotEqual = 2,

        [EnumMember(Value = "less_than_or_equal")]
        LessThanOrEqual = 3,

        [EnumMember(Value = "greater_than_or_equal")]
        GreaterThanOrEqual = 4,

        [EnumMember(Value = "less_than")]
        LessThan = 5,

        [EnumMember(Value = "greater_than")]
        GreaterThan = 6,

        [EnumMember(Value = "contains")]
        Contains = 7,

        [EnumMember(Value = "not_contains")]
        NotContains = 8,

        [EnumMember(Value = "starts_with")]
        StartsWith = 9,

    }
}
