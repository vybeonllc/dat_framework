using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.Framework.Controls
{
    public partial class Interpreter
    {
        public KeyValuePair<string, string> jQuery = new KeyValuePair<string, string>("jQuery", "jquery-1.10.2.min");
        public KeyValuePair<string, string> jQuery_UI = new KeyValuePair<string, string>("jQuery-UI", "jquery-ui");
        public KeyValuePair<string, string> Dat_V1 = new KeyValuePair<string, string>("Dat", "Dat.V1");
        public KeyValuePair<string, string> Dat_V1_Services = new KeyValuePair<string, string>("DatServices", "Dat.V1.Services");
        public KeyValuePair<string, string> Dat_V1_Services_Common = new KeyValuePair<string, string>("DatCommonService", "Dat.V1.Services.Common");
        public KeyValuePair<string, string> Dat_V1_Utilities = new KeyValuePair<string, string>("DatUtilities", "Dat.V1.Utils");
        public KeyValuePair<string, string> Dat_V1_AssetPool = new KeyValuePair<string, string>("DatAssetPool", "Dat.V1.AssetPool");
        public System.Web.UI.Page Page { get; set; }
        public System.Web.UI.HtmlControls.HtmlGenericControl Frame { get; set; }
        public System.Web.UI.Control PlaceHolder { get; set; }
        public bool DebugMode { get; set; }
        public string Path { get; set; }
        public string PageName { get; set; }
        public bool AssetPool { get; set; }
        public string OnAssetPoolInitialized { get; set; }
        private string _onassetpoolinitialized { get; set; }
        public string PageOnLoad { get; set; }
        internal Dictionary<string, string> Dependencies;
        public void AddDependency(KeyValuePair<string, string> dependency)
        {
            if (Dependencies.ContainsKey(dependency.Key)) return;
            Dependencies.Add(dependency.Key, dependency.Value);
        }

        public StringBuilder AssetPoolScripts { get; set; }
        public StringBuilder ControlScripts { get; set; }
        public StringBuilder DependenciesScripts { get; set; }
        public StringBuilder DirectoryScripts { get; set; }

        public Interpreter()
        {
#if DEBUG
            DebugMode = true;
#else
            DebugMode = false;
            
#endif
            _onassetpoolinitialized = "Dat.V1.OnAssetPoolInitialized";
            Dependencies = new Dictionary<string, string>();
            AddDependency(jQuery);
            AddDependency(jQuery_UI);
            AddDependency(Dat_V1);
            AddDependency(Dat_V1_Services);
            AddDependency(Dat_V1_Services_Common);
            AddDependency(Dat_V1_Utilities);
            AddDependency(Dat_V1_AssetPool);
            AssetPoolScripts = new StringBuilder();
            ControlScripts = new StringBuilder();
            DependenciesScripts = new StringBuilder();
            DirectoryScripts = new StringBuilder();
            PlaceHolder = new Control();
            Frame = new System.Web.UI.HtmlControls.HtmlGenericControl("div")
            {
                ID = "Dat"
            };
            PlaceHolder.Controls.Add(Frame);
            Page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
            PageName = System.IO.Path.GetFileName(Page.Request.PhysicalPath).LeftOf(".");
            Path = System.Web.HttpContext.Current.Server.MapPath("~/" + PageName);

            Page.PreRenderComplete += Page_PreRenderComplete;
        }

        void Page_PreRenderComplete(object sender, EventArgs e)
        {
            //Frame.InnerHtml = Script.ToString();
            ProcessReferences();
            System.Web.UI.HtmlControls.HtmlGenericControl body = Page.FindControl("body") as System.Web.UI.HtmlControls.HtmlGenericControl;
            string html = System.IO.File.ReadAllText(Path + @"\" + PageName + ".datx");
            LiteralControl lc = new LiteralControl(html);
            PlaceHolder.Controls.Add(lc);
            body.InnerHtml = string.Empty;
            body.Controls.Add(PlaceHolder);
            StringBuilder page_content = new StringBuilder();

            System.IO.StringWriter sWriter = new System.IO.StringWriter(page_content);
            HtmlTextWriter hWriter = new HtmlTextWriter(sWriter);
            Page.RenderControl(hWriter);

            System.IO.File.WriteAllText(Path + "/" + PageName + ".html", page_content.ToString());
        }
        public void Interprete(UserControl control)
        {
            Type type = control.GetType();
            if (type == typeof(ListView))
                Interprete(control as ListView);
            else if (type == typeof(TreeView))
                Interprete(control as TreeView);
            else if (type == typeof(FormView))
                Interprete(control as FormView);
            else if (type == typeof(AssetReference))
                Interprete(control as AssetReference);
            else if (type == typeof(AssetListener))
                Interprete(control as AssetListener);
            else if (type == typeof(TextBox))
                Interprete(control as TextBox);
            else if (type == typeof(Page))
                Interprete(control as Page);

        }

    }
}
