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
    public class DataBinder : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public DataBinderCommand UpdateCommand { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public DataBinderCommand SelectCommand { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public DataBinderCommand DeleteCommand { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public DataBinderCommand CreateCommand { get; set; }

        public int PageSize { get; set; }
        public int StartIndex { get; set; }
        public string AssetName { get; set; }
         public int Interval { get; set; }

         public string PrimaryKey { get; set; }

         public string OnDataBinding { get; set; }
        public string OnDataBound { get; set; }

        public DataBinder()
        {
            Interpreter.AddDependency(RegisteredControls.DataBinder);
            StartIndex = 0;
            PageSize = 10;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

    }
}
