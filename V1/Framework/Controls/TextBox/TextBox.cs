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
    public class TextBox : UserControl
    {
        public DataBinder DataBinder { get; set; }
        public bool AutoComplete { get; set; }
        public string DataText { get; set; }
        public string DataValue { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public TextBoxEvents Events { get; set; }

        public int MinimumCharacters { get; set; }
        public int Dealy { get; set; }
        public int MaximumSuggestions { get; set; }

        public TextBox()
        {
            Interpreter.AddDependency(RegisteredControls.TextBox);
            Events = new TextBoxEvents();
            TagName = "input";
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}
