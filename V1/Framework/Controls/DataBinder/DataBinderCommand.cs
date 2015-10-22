using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Dat.V1.Framework.Controls
{
    public class DataBinderCommand  
    {
        public string Parameters { get; set; }

        public DataBinderCommandType CommandType { get; set; }

        public string Target { get; set; }

        public string DataItemPropertyName { get; set; }

        public DataBinderCommand()
        {
        }
        public DataBinderCommand(DataBinderCommandType commandType)
        {
            CommandType = commandType;
        }
        public string OnExecuting { get; set; }
        public string OnExecuted { get; set; }
 
    }
}
