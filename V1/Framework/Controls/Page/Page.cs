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
    public class Page : UserControl
    {

        public Page()
        {
            Events = new PageEvents(); 
        }

        public string Title { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public PageEvents Events { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Interpreter.Page.Title = Title;
            Interpreter.PageOnLoad = Events.OnLoad;
        }
    }
}
