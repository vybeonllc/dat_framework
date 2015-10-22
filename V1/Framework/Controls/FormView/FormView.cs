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
    public class FormView : UserControl
    {

        public FormView()
        {
            Interpreter.AddDependency(RegisteredControls.FormView);
            Interpreter.AddDependency(RegisteredControls.FormViewItemType);
            Interpreter.AddDependency(RegisteredControls.FormViewTemplate);
            Interpreter.AddDependency(RegisteredControls.FormViewItem); 
            Interpreter.AddDependency(RegisteredControls.FormViewMode); 
            Events = new FormViewEvents();
            TagName = "ul";
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public FormViewEvents Events { get; set; }

        string itemTemplateContent, headerTemplateContent, footerTemplateContent, emptyItemTemplateContent;

        public string TemplateName { get; set; }

        public string FilterElement { get; set; }
        public string SubmitElement { get; set; }
        public string DeleteElement { get; set; }

        public string DataItemSchema { get; set; }

        [TemplateContainer(typeof(Dat.V1.Framework.Controls.FormViewItemTemplateContainer))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate HeaderTemplate { get; set; }

        [TemplateContainer(typeof(Dat.V1.Framework.Controls.FormViewItemTemplateContainer))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate ItemTemplate { get; set; }

        [TemplateContainer(typeof(Dat.V1.Framework.Controls.FormViewItemTemplateContainer))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate EmptyItemTemplate { get; set; }


        [TemplateContainer(typeof(Dat.V1.Framework.Controls.FormViewItemTemplateContainer))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate FooterTemplate { get; set; }

        public DataBinder DataBinder { get; set; }


        public string TagName { get; set; }

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
        void ProcessTemplates()
        {
            string rootTemplate = Context.Server.MapPath("~/Templates");
            if (!System.IO.Directory.Exists(rootTemplate))
                System.IO.Directory.CreateDirectory(rootTemplate);


            StringBuilder sbTemplate = new StringBuilder();

            itemTemplateContent = ProcessTemplate(rootTemplate, ItemTemplate);
            headerTemplateContent = ProcessTemplate(rootTemplate, HeaderTemplate);
            footerTemplateContent = ProcessTemplate(rootTemplate, FooterTemplate);
            emptyItemTemplateContent = ProcessTemplate(rootTemplate, EmptyItemTemplate);

            if (!string.IsNullOrWhiteSpace(itemTemplateContent))
            {
                sbTemplate.AppendLine("<div role='itemtemplate' >");
                sbTemplate.Append(itemTemplateContent);
                sbTemplate.AppendLine("</div>");
            }
            if (!string.IsNullOrWhiteSpace(headerTemplateContent))
            {
                sbTemplate.AppendLine("<div role='headertemplate' >");
                sbTemplate.Append(headerTemplateContent);
                sbTemplate.AppendLine("</div>");
            }
            if (!string.IsNullOrWhiteSpace(footerTemplateContent))
            {
                sbTemplate.AppendLine("<div role='footertemplate' >");
                sbTemplate.Append(footerTemplateContent);
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
            Dat.V1.Framework.Controls.FormViewItemTemplateContainer container = new Dat.V1.Framework.Controls.FormViewItemTemplateContainer();
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
