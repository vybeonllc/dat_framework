using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Enumerations
{
    public enum Action
    {
        All = Create | Update | Select,
        Create = 0,
        Update = 1,
        Select = 2
    }
}
