using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public partial class Interpreter
    {
        public void Interprete(Controls.AssetListener assetListener)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(function(){");
            sb.AppendFormat("Dat.V1.Config.Asset = \"{0}\";", assetListener.AssetName);
            sb.AppendFormat("new Dat.V1.Utils.WebMessaging.Listener();");
            sb.Append("})();");
            ControlScripts.Append(sb.ToString());
        }
    }
}
