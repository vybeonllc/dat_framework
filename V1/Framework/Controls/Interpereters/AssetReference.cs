using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public partial class Interpreter
    {
        public void Interprete(Controls.AssetReference assetReference)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(function(){");
            sb.AppendFormat("if(Dat.V1.AssetPool.Assets.{0}) return;", assetReference.AssetName);
            sb.Append("Dat.V1.AssetPool.Total = (Dat.V1.AssetPool.Total || 0) + 1;");
            sb.AppendFormat("Dat.V1.AssetPool.Assets.{0} = new Dat.V1.Utils.WebMessaging.Messenger(", assetReference.AssetName);
            sb.Append("{");
            sb.AppendFormat("Asset: \"{0}\",", assetReference.AssetName);
            sb.AppendFormat("AssetUrl: \"{0}\",", assetReference.AssetUrl);

            sb.Append("OnReady: function(){ Dat.V1.AssetPool.Count++; if(!Dat.V1.AssetPool.AssetPoolInitialized) if(Dat.V1.AssetPool.Total == Dat.V1.AssetPool.Count) {Dat.V1.AssetPool.AssetPoolInitialized = true;" + _onassetpoolinitialized + "();}");
            if (!string.IsNullOrWhiteSpace(assetReference.OnReady))
                sb.AppendFormat("{0}();", assetReference.OnReady);
            sb.Append("},");

            if (!string.IsNullOrWhiteSpace(assetReference.OnError))
                sb.AppendFormat("OnError: {0},", assetReference.OnError);
            sb.Append("});})();");
            AssetPoolScripts.Append(sb.ToString());
        }
    }
}
