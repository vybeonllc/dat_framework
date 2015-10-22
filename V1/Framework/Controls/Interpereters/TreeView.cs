using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public partial class Interpreter
    {
        public void Interprete(TreeView treeview)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("var t= (function(){");
            string parameters_guid = "_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            sb.AppendFormat("var {0} = new Array();", parameters_guid);
            if (treeview.DataBinder != null && treeview.DataBinder.SelectCommand != null && !string.IsNullOrWhiteSpace(treeview.DataBinder.SelectCommand.Parameters))
            {
                string[] parameters = treeview.DataBinder.SelectCommand.Parameters.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();
                foreach (string s in parameters)
                    sb.AppendFormat("{0}.push(\"{1}\");", parameters_guid, s);
            }
            sb.Append("return new Dat.V1.Controls.TreeView({");
            sb.AppendFormat("ID: \"{0}\",", treeview.ID);
            sb.AppendFormat("TagName: \"{0}\",", treeview.TagName);
            sb.AppendFormat("TemplateName: \"{0}\",", treeview.TemplateName);
            sb.AppendFormat("ChildPrimaryKeyField: \"{0}\",", treeview.ChildPrimaryKeyField);
            
            sb.AppendFormat("Container: \"{0}\",", string.IsNullOrWhiteSpace(treeview.ContainerElement) ? "#Dat" : treeview.ContainerElement);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnItemDataBinding))
                sb.AppendFormat("OnItemDataBinding: eval({0}),", treeview.Events.OnItemDataBinding);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnItemDataBound))
                sb.AppendFormat("OnItemDataBound: eval({0}),", treeview.Events.OnItemDataBound);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnDataBinding))
                sb.AppendFormat("OnDataBinding: eval({0}),", treeview.Events.OnDataBinding);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnDataBound))
                sb.AppendFormat("OnDataBound: eval({0}),", treeview.Events.OnDataBound);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnCollapsed))
                sb.AppendFormat("OnCollapsed: eval({0}),", treeview.Events.OnCollapsed);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnCollapsing))
                sb.AppendFormat("OnCollapsing: eval({0}),", treeview.Events.OnCollapsing);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnDeSelected))
                sb.AppendFormat("OnDeSelected: eval({0}),", treeview.Events.OnDeSelected);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnDeSelecting))
                sb.AppendFormat("OnDeSelecting: eval({0}),", treeview.Events.OnDeSelecting);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnExpanded))
                sb.AppendFormat("OnExpanded: eval({0}),", treeview.Events.OnExpanded);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnExpanding))
                sb.AppendFormat("OnExpanding: eval({0}),", treeview.Events.OnExpanding);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnInitEach))
                sb.AppendFormat("OnInitEach: eval({0}),", treeview.Events.OnInitEach);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnReadyEach))
                sb.AppendFormat("OnReadyEach: eval({0}),", treeview.Events.OnReadyEach);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnSelected))
                sb.AppendFormat("OnSelected: eval({0}),", treeview.Events.OnSelected);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnSelecting))
                sb.AppendFormat("OnSelecting: eval({0}),", treeview.Events.OnSelecting);

            if (!string.IsNullOrWhiteSpace(treeview.OnReady))
                sb.AppendFormat("OnReady: eval({0}),", treeview.OnReady);

            if (!string.IsNullOrWhiteSpace(treeview.Events.OnItemCommand))
                sb.AppendFormat("OnItemCommand: eval({0}),", treeview.Events.OnItemCommand);

            if (!string.IsNullOrWhiteSpace(treeview.OnError))
                sb.AppendFormat("OnError: eval({0}),", treeview.OnError);

            if (!string.IsNullOrWhiteSpace(treeview.OnInitialized))
                sb.AppendFormat("OnInit: eval({0}),", treeview.OnInitialized);
            if (treeview.DataBinder != null)
            {
                sb.Append("DataBinder: {");
                sb.AppendFormat("AssetName: \"{0}\",", treeview.DataBinder.AssetName);
                sb.AppendFormat("FreezeMode: {0},", (treeview.DataBinder.Interval < 1).ToString().ToLower());

                if (!string.IsNullOrWhiteSpace(treeview.DataBinder.OnError))
                    sb.AppendFormat("OnError: eval({0}),", treeview.DataBinder.OnError);

                if (!string.IsNullOrWhiteSpace(treeview.DataBinder.PrimaryKey))
                    sb.AppendFormat("PrimaryKey: \"{0}\",", treeview.DataBinder.PrimaryKey);

                if (treeview.DataBinder.Interval > 0)
                    sb.AppendFormat("Interval: {0},", treeview.DataBinder.Interval);
                sb.AppendFormat("StartIndex: {0},", treeview.DataBinder.StartIndex);
                if (treeview.DataBinder.SelectCommand != null)
                {
                    sb.AppendFormat("SelectCommand: \"{0}\",", treeview.DataBinder.SelectCommand.Target);
                    sb.AppendFormat("SelectParameters: {0},", parameters_guid);
                }
                sb.Append("}");
            }
            string script = sb.ToString().Trim(',').Trim() + "});";
            script += "})();";
            ControlScripts.Append(script);
        }
    }
}
