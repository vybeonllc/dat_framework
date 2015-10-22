using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.BusinessLogic.EventArgs
{
    public class OnCreatedEventArg:BusinessLogicEventArg
    {
        public long UniqueIdentifier { get; set; }
        public string EntityName { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }
}
