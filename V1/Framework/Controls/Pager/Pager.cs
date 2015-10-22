using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace Dat.V1.Framework.Controls
{
    [ParseChildren(true), PersistChildren(false)]
    public class Pager : UserControl
    {
        [TemplateContainer(typeof(Dat.V1.Framework.Controls.ListViewItemTemplateContainer))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate PagerTemplate { get; set; }  public string OnPagerInitialized { get; set; }

        public string OnChangingPage { get; set; }
        public string DefaultPageSize { get; set; }
    }
}
