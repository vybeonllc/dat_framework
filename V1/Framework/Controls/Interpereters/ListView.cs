using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public partial class Interpreter
    {
        public void Interprete(ListView listview)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("var t= (function(){");
            string parameters_guid = "_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            sb.AppendFormat("var {0} = new Array();", parameters_guid);
            if (listview.DataBinder != null && listview.DataBinder.SelectCommand != null && !string.IsNullOrWhiteSpace(listview.DataBinder.SelectCommand.Parameters))
            {
                string[] parameters = listview.DataBinder.SelectCommand.Parameters.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();
                foreach (string s in parameters)
                    sb.AppendFormat("{0}.push(\"{1}\");", parameters_guid, s);
            }
            sb.Append("return new Dat.V1.Controls.ListView({");
            sb.AppendFormat("ID: \"{0}\",", listview.ID);
            sb.AppendFormat("TagName: \"{0}\",", listview.TagName);
            sb.AppendFormat("TemplateName: \"{0}\",", listview.TemplateName);

            if (!string.IsNullOrWhiteSpace(listview.FilterElement))
                sb.AppendFormat("FilterElement: {0},", listview.FilterElement);


            sb.AppendFormat("Container: \"{0}\",", string.IsNullOrWhiteSpace(listview.ContainerElement) ? "#Dat" : listview.ContainerElement);

            if (!string.IsNullOrWhiteSpace(listview.Events.OnItemDataBinding))
                sb.AppendFormat("OnItemDataBinding: eval({0}),", listview.Events.OnItemDataBinding);

            if (!string.IsNullOrWhiteSpace(listview.Events.OnItemDataBound))
                sb.AppendFormat("OnItemDataBound: eval({0}),", listview.Events.OnItemDataBound);

            if (!string.IsNullOrWhiteSpace(listview.Events.OnDataBinding))
                sb.AppendFormat("OnDataBinding: eval({0}),", listview.Events.OnDataBinding);

            if (!string.IsNullOrWhiteSpace(listview.Events.OnDataBound))
                sb.AppendFormat("OnDataBound: eval({0}),", listview.Events.OnDataBound);

            if (!string.IsNullOrWhiteSpace(listview.Events.OnHeaderInitialized))
                sb.AppendFormat("OnHeaderInitialized: eval({0}),", listview.Events.OnHeaderInitialized);

            if (!string.IsNullOrWhiteSpace(listview.Events.OnFooterInitialized))
                sb.AppendFormat("OnFooterInitialized: eval({0}),", listview.Events.OnFooterInitialized);

            if (listview.Pager != null && !string.IsNullOrWhiteSpace(listview.Pager.OnPagerInitialized))
                sb.AppendFormat("OnPagerInitialized: eval({0}),", listview.Pager.OnPagerInitialized);

            if (!string.IsNullOrWhiteSpace(listview.OnReady))
                sb.AppendFormat("OnReady: eval({0}),", listview.OnReady);

            if (!string.IsNullOrWhiteSpace(listview.Events.OnItemCommand))
                sb.AppendFormat("OnItemCommand: eval({0}),", listview.Events.OnItemCommand);

            if (!string.IsNullOrWhiteSpace(listview.OnError))
                sb.AppendFormat("OnError: eval({0}),", listview.OnError);

            if (!string.IsNullOrWhiteSpace(listview.OnInitialized))
                sb.AppendFormat("OnInit: eval({0}),", listview.OnInitialized);
            if (listview.DataBinder != null)
            {
                sb.Append("DataBinder: {");
                sb.AppendFormat("AssetName: \"{0}\",", listview.DataBinder.AssetName);
                sb.AppendFormat("PageSize: {0},", listview.DataBinder.PageSize);
                sb.AppendFormat("FreezeMode: {0},", (listview.DataBinder.Interval < 1).ToString().ToLower());

                if (!string.IsNullOrWhiteSpace(listview.DataBinder.PrimaryKey))
                    sb.AppendFormat("PrimaryKey: \"{0}\",", listview.DataBinder.PrimaryKey);

                if (!string.IsNullOrWhiteSpace(listview.DataBinder.OnError))
                    sb.AppendFormat("OnError: eval({0}),", listview.DataBinder.OnError);

                if (listview.DataBinder.Interval > 0)
                    sb.AppendFormat("Interval: {0},", listview.DataBinder.Interval);
                sb.AppendFormat("StartIndex: {0},", listview.DataBinder.StartIndex);
                if (listview.DataBinder.SelectCommand != null)
                {
                    sb.AppendFormat("SelectCommand: \"{0}\",", listview.DataBinder.SelectCommand.Target);
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
