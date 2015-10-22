using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public class UserControl : System.Web.UI.Control
    {
        protected Interpreter Interpreter = Environment.Interpreter;

        public UserControl()
        {
            Interpreter.AddDependency(RegisteredControls.UserControl);
            ControlSource = Environment.ControlSource;
        }
        protected override void OnPreRender(System.EventArgs e)
        {
            Interpreter.Interprete(this);
        }
        public string Name { get; set; }
        public string TagName { get; set; }
        public string ID { get; set; }
        public UserControl Container { get; set; }
        public string ContainerElement { get; set; }
        public string OnError { get; set; }
        public string OnInitialized { get; set; }
        public string OnReady { get; set; }

        public ControlsSources ControlSource { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}
