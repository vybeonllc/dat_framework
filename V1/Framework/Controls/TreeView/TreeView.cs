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
    public class TreeView : UserControl
    {

        public TreeView()
        {
            Interpreter.AddDependency(RegisteredControls.TreeView);
            ListView lv = new ListView();
            Events = new TreeViewEvents();
            TagName = "ul";
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public TreeViewEvents Events { get; set; }

        string itemTemplateContent, emptyItemTemplateContent;

        public string TemplateName { get; set; }

        [TemplateContainer(typeof(Dat.V1.Framework.Controls.ListViewItemTemplateContainer))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate ItemTemplate { get; set; }

        [TemplateContainer(typeof(Dat.V1.Framework.Controls.ListViewItemTemplateContainer))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate EmptyItemTemplate { get; set; }

        public DataBinder DataBinder { get; set; }

        public string TagName { get; set; }
        public string ChildrenContainer { get; set; }
        public string ChildPrimaryKeyField { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        void ProcessTemplates()
        {
            string rootTemplate = Context.Server.MapPath("~/Templates");
            if (!System.IO.Directory.Exists(rootTemplate))
                System.IO.Directory.CreateDirectory(rootTemplate);

            StringBuilder sbTemplate = new StringBuilder();

            itemTemplateContent = ProcessTemplate(rootTemplate, ItemTemplate);
            emptyItemTemplateContent = ProcessTemplate(rootTemplate, EmptyItemTemplate);

            if (!string.IsNullOrWhiteSpace(itemTemplateContent))
            {
                sbTemplate.AppendLine("<div role='itemtemplate' >");
                sbTemplate.Append(itemTemplateContent);
                sbTemplate.AppendLine("</div>");
            }
            if (!string.IsNullOrWhiteSpace(emptyItemTemplateContent))
            {
                sbTemplate.AppendLine("<div role='emptyitemtemplate' >");
                sbTemplate.Append(emptyItemTemplateContent);
                sbTemplate.AppendLine("</div>");
            }
            string templateGuid = Guid.NewGuid().ToString();
            while (System.IO.File.Exists(rootTemplate + @"\" + (templateGuid = Guid.NewGuid().ToString()) + ".template")) ;
            TemplateName = templateGuid;
            System.IO.File.WriteAllText(rootTemplate + @"\" + templateGuid + ".template", sbTemplate.ToString());
        }
        string ProcessTemplate(string rootTemplate, ITemplate template)
        {
            if (template == null)
                return null;
            Dat.V1.Framework.Controls.TreeViewItemTemplateContainer container = new Dat.V1.Framework.Controls.TreeViewItemTemplateContainer();
            template.InstantiateIn(container);

            StringBuilder sb = new StringBuilder();
            System.IO.StringWriter sWriter = new System.IO.StringWriter(sb);
            HtmlTextWriter hWriter = new HtmlTextWriter(sWriter);
            container.RenderControl(hWriter);
            return sb.ToString();
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ProcessTemplates();
        }
    }
}
