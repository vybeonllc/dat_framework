using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.BusinessLogic.EventArgs
{
    public class OnDeletingEventArg:BusinessLogicEventArg
    {
        public bool Cancel { get; set; }
        public string EntityName { get; set; }
    }
}
