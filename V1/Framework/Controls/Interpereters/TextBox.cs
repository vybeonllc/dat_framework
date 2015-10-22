using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public partial class Interpreter
    {
        public void Interprete(TextBox textbox)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("var t= (function(){ var y='creatingtextbox';})();");
            ControlScripts.Append(sb.ToString());
        }
    }
}
