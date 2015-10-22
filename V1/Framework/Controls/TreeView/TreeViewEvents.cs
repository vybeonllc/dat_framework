using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public class TreeViewEvents
    {
        public string OnDataBound { get; set; }
        public string OnDataBinding { get; set; }
        public string OnItemCommand { get; set; }
        public string OnItemDataBound { get; set; }
        public string OnItemDataBinding { get; set; }
        public string OnInitEach { get; set; }
        public string OnExpanding { get; set; }
        public string OnExpanded { get; set; }
        public string OnCollapsing { get; set; }
        public string OnCollapsed { get; set; }
        public string OnDeSelecting { get; set; }
        public string OnDeSelected { get; set; }
        public string OnSelecting { get; set; }
        public string OnSelected { get; set; }
        public string OnReadyEach { get; set; }
    } 
}
