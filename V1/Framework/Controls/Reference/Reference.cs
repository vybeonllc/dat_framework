using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Dat.V1.Framework.Controls
{
    [ParseChildren(true), PersistChildren(false)]
    public class Reference : UserControl
    {

        public Reference()
        {
        }

        public string ReferenceName { get; set; }
        public string ReferenceNameSpace { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
      
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (string.IsNullOrWhiteSpace(ReferenceName) || string.IsNullOrWhiteSpace(ReferenceNameSpace)) return;
            Interpreter.AddDependency(new KeyValuePair<string, string>(ReferenceName, ReferenceNameSpace));
        }
    }
}
