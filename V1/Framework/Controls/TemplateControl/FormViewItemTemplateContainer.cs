using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public class FormViewItemTemplateContainer:TemplateContainer
    {
        public string TemplateName { get; set; }
        public List<string> StyleSheets { get; set; }
        public List<string> JavaScripts { get; set; }

    }
}
