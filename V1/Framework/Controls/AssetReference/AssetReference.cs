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
    public class AssetReference : UserControl
    {

        public AssetReference()
        {
            Interpreter.AddDependency(RegisteredControls.WebMessaging);
            TagName = "script";
            Interpreter.AssetPool = true;
        }

        public string AssetName { get; set; }
        public string TagName { get; set; }
        public string AssetUrl  { get; set; }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //string template_path = System.Web.HttpContext.Current.Server.MapPath("Templates\\" + ItemTemplate.Name);
            //if (!System.IO.Directory.Exists(template_path))
            //    throw new System.IO.DirectoryNotFoundException("Template directory not exists");
            //string tamptlte_content = template_path + "\\" + ItemTemplate.Name + ".html";
            //if (!System.IO.File.Exists(tamptlte_content))
            //    throw new System.IO.FileNotFoundException("Template not found");
            //Frame.InnerHtml += System.IO.File.ReadAllText(tamptlte_content);
            bool t = true;
        }
      
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
    }
}
