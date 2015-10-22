using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public class FormViewEvents
    {
        public string OnDataBound { get; set; }
        public string OnDataBinding { get; set; }
        public string OnItemCommand { get; set; }
        public string OnHeaderInitialized { get; set; }
        public string OnFooterInitialized { get; set; }
        public string OnFieldBound { get; set; }
        public string OnFieldBinding { get; set; }
    } 
}
