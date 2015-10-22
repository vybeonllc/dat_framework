using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.Framework.Controls
{
    public partial class Interpreter
    {
        void ProcessReferences()
        {
            ProcessScripts();
            ProcessStyles();
        }
        void ProcessScripts()
        {

            foreach (KeyValuePair<string, string> script in Dependencies)
            {
                string relativePath = "~/Scripts/" + script.Value + ".js";
                DependenciesScripts.Append(System.IO.File.ReadAllText(Page.Request.MapPath(relativePath)));
            }

            List<string> scripts_files = System.IO.Directory.GetFiles(Path, "*.datx.js").ToList();


            scripts_files.ForEach(s => DirectoryScripts.Append(System.IO.File.ReadAllText(s)));


            StringBuilder scripts = new StringBuilder();
            scripts.Append(DependenciesScripts.ToString());
            scripts.Append(DirectoryScripts.ToString());
            scripts.Append("jQuery(window).load(function(){");
            scripts.Append(_onassetpoolinitialized + " = function(){ ");
            if (!string.IsNullOrWhiteSpace(OnAssetPoolInitialized))
                scripts.Append(OnAssetPoolInitialized + "();");
            if (!string.IsNullOrWhiteSpace(PageOnLoad))
                scripts.Append(PageOnLoad + "();");
            scripts.Append(ControlScripts);
            scripts.Append("};");
            scripts.Append(AssetPoolScripts.ToString());
            scripts.Append("});");


            string script_path = Path + "/" + PageName + ".js";
            System.IO.File.WriteAllText(script_path, scripts.ToString());

            System.Web.UI.HtmlControls.HtmlGenericControl script_tag = new System.Web.UI.HtmlControls.HtmlGenericControl("script");

            script_tag.Attributes["src"] = Page.ResolveUrl("~/" + PageName + "/" + PageName + ".js");

            Page.Header.Controls.Add(script_tag);
        }
        void ProcessStyles()
        {
            StringBuilder styles = new StringBuilder();


            List<string> style_sheets = System.IO.Directory.GetFiles(Path, "*.datx.css").ToList();


            style_sheets.ForEach(s => styles.Append(System.IO.File.ReadAllText(s)));
            string style_path = Path + "/" + PageName + ".css";
            System.IO.File.WriteAllText(style_path, styles.ToString());

            System.Web.UI.HtmlControls.HtmlGenericControl style_tag = new System.Web.UI.HtmlControls.HtmlGenericControl("link");

            style_tag.Attributes["rel"] = "stylesheet";
            style_tag.Attributes["type"] = "text/css";
            style_tag.Attributes["href"] = Page.ResolveUrl("~/" + PageName + "/" + PageName + ".css");

            Page.Header.Controls.Add(style_tag);
        }
    }
}
