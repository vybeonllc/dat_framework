using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Controls
{
    public partial class Interpreter
    {
        public void Interprete(FormView formview)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(function(){");
            string select_parameters_guid = "_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            string update_parameters_guid = "_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            string create_parameters_guid = "_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            string delete_parameters_guid = "_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
            sb.AppendFormat("var {0} = new Array();", select_parameters_guid);
            sb.AppendFormat("var {0} = new Array();", update_parameters_guid);
            sb.AppendFormat("var {0} = new Array();", create_parameters_guid);
            sb.AppendFormat("var {0} = new Array();", delete_parameters_guid);

            if (formview.DataBinder != null)
            {
                if (formview.DataBinder.SelectCommand != null && !string.IsNullOrWhiteSpace(formview.DataBinder.SelectCommand.Parameters))
                {
                    string[] parameters = formview.DataBinder.SelectCommand.Parameters.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();
                    foreach (string s in parameters)
                        sb.AppendFormat("{0}.push(\"{1}\");", select_parameters_guid, s);
                }
                if (formview.DataBinder.UpdateCommand != null && !string.IsNullOrWhiteSpace(formview.DataBinder.UpdateCommand.Parameters))
                {
                    string[] parameters = formview.DataBinder.UpdateCommand.Parameters.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();
                    foreach (string s in parameters)
                        sb.AppendFormat("{0}.push(\"{1}\");", update_parameters_guid, s);
                }
                if (formview.DataBinder.CreateCommand != null && !string.IsNullOrWhiteSpace(formview.DataBinder.CreateCommand.Parameters))
                {
                    string[] parameters = formview.DataBinder.CreateCommand.Parameters.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();
                    foreach (string s in parameters)
                        sb.AppendFormat("{0}.push(\"{1}\");", create_parameters_guid, s);
                }
                if (formview.DataBinder.DeleteCommand != null && !string.IsNullOrWhiteSpace(formview.DataBinder.DeleteCommand.Parameters))
                {
                    string[] parameters = formview.DataBinder.DeleteCommand.Parameters.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(w => !string.IsNullOrWhiteSpace(w)).ToArray();
                    foreach (string s in parameters)
                        sb.AppendFormat("{0}.push(\"{1}\");", delete_parameters_guid, s);
                }
            }


            sb.Append("return new Dat.V1.Controls.FormView({");
            sb.AppendFormat("ID: \"{0}\",", formview.ID);
            sb.AppendFormat("TagName: \"{0}\",", formview.TagName);
            sb.AppendFormat("TemplateName: \"{0}\",", formview.TemplateName);
            if (!string.IsNullOrWhiteSpace(formview.DataItemSchema))
                sb.Append("DataItemSchema: \"(function(){return " + formview.DataItemSchema + ";})();\",");



            if (!string.IsNullOrWhiteSpace(formview.FilterElement))
                sb.AppendFormat("FilterElement: {0},", formview.FilterElement);

            if (!string.IsNullOrWhiteSpace(formview.SubmitElement))
                sb.AppendFormat("SubmitElement: \"{0}\",", formview.SubmitElement);

            if (!string.IsNullOrWhiteSpace(formview.DeleteElement))
                sb.AppendFormat("DeleteElement: \"{0}\",", formview.DeleteElement);

            sb.AppendFormat("Container: \"{0}\",", string.IsNullOrWhiteSpace(formview.ContainerElement) ? "#Dat" : formview.ContainerElement);

            if (!string.IsNullOrWhiteSpace(formview.Events.OnFieldBound))
                sb.AppendFormat("OnFieldBound: eval({0}),", formview.Events.OnFieldBound);

            if (!string.IsNullOrWhiteSpace(formview.Events.OnFieldBinding))
                sb.AppendFormat("OnFieldBinding: eval({0}),", formview.Events.OnFieldBinding);

            if (!string.IsNullOrWhiteSpace(formview.Events.OnDataBinding))
                sb.AppendFormat("OnDataBinding: eval({0}),", formview.Events.OnDataBinding);

            if (!string.IsNullOrWhiteSpace(formview.Events.OnDataBound))
                sb.AppendFormat("OnDataBound: eval({0}),", formview.Events.OnDataBound);

            if (!string.IsNullOrWhiteSpace(formview.Events.OnHeaderInitialized))
                sb.AppendFormat("OnHeaderInitialized: eval({0}),", formview.Events.OnHeaderInitialized);

            if (!string.IsNullOrWhiteSpace(formview.Events.OnFooterInitialized))
                sb.AppendFormat("OnFooterInitialized: eval({0}),", formview.Events.OnFooterInitialized);


            if (!string.IsNullOrWhiteSpace(formview.OnReady))
                sb.AppendFormat("OnReady: eval({0}),", formview.OnReady);

            if (!string.IsNullOrWhiteSpace(formview.Events.OnItemCommand))
                sb.AppendFormat("OnItemCommand: eval({0}),", formview.Events.OnItemCommand);

            if (!string.IsNullOrWhiteSpace(formview.OnError))
                sb.AppendFormat("OnError: eval({0}),", formview.OnError);

            if (!string.IsNullOrWhiteSpace(formview.OnInitialized))
                sb.AppendFormat("OnInit: eval({0}),", formview.OnInitialized);

            if (formview.DataBinder != null)
            {
                sb.Append("DataBinder: {");
                sb.AppendFormat("AssetName: \"{0}\",", formview.DataBinder.AssetName);
                sb.AppendFormat("PageSize: {0},", formview.DataBinder.PageSize);
                sb.AppendFormat("FreezeMode: {0},", (formview.DataBinder.Interval < 1).ToString().ToLower());
                if (formview.DataBinder.Interval > 0)
                    sb.AppendFormat("Interval: {0},", formview.DataBinder.Interval);
                sb.AppendFormat("StartIndex: {0},", formview.DataBinder.StartIndex);

                if (!string.IsNullOrWhiteSpace(formview.DataBinder.PrimaryKey))
                    sb.AppendFormat("PrimaryKey: \"{0}\",", formview.DataBinder.PrimaryKey);

                if (!string.IsNullOrWhiteSpace(formview.DataBinder.OnError))
                    sb.AppendFormat("OnError: eval({0}),", formview.DataBinder.OnError);

                if (formview.DataBinder.SelectCommand != null)
                {
                    sb.AppendFormat("SelectCommand: \"{0}\",", formview.DataBinder.SelectCommand.Target);


                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.SelectCommand.OnExecuting))
                        sb.AppendFormat("OnSelecting: eval({0}),", formview.DataBinder.SelectCommand.OnExecuting);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.SelectCommand.OnExecuted))
                        sb.AppendFormat("OnSelected: eval({0}),", formview.DataBinder.SelectCommand.OnExecuted);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.SelectCommand.DataItemPropertyName))
                        sb.AppendFormat("SelectDataItemProperty: \"{0}\",", formview.DataBinder.SelectCommand.DataItemPropertyName);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.SelectCommand.Parameters))
                        sb.AppendFormat("SelectParameters: {0},", select_parameters_guid);

                }
                if (formview.DataBinder.UpdateCommand != null)
                {
                    sb.AppendFormat("UpdateCommand: \"{0}\",", formview.DataBinder.UpdateCommand.Target);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.UpdateCommand.OnExecuting))
                        sb.AppendFormat("OnUpdating: eval({0}),", formview.DataBinder.UpdateCommand.OnExecuting);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.UpdateCommand.OnExecuted))
                        sb.AppendFormat("OnUpdated: eval({0}),", formview.DataBinder.UpdateCommand.OnExecuted);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.UpdateCommand.DataItemPropertyName))
                        sb.AppendFormat("UpdateDataItemProperty: \"{0}\",", formview.DataBinder.UpdateCommand.DataItemPropertyName);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.UpdateCommand.Parameters))
                        sb.AppendFormat("UpdateParameters: {0},", update_parameters_guid);
                }
                if (formview.DataBinder.DeleteCommand != null)
                {
                    sb.AppendFormat("DeleteCommand: \"{0}\",", formview.DataBinder.DeleteCommand.Target);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.DeleteCommand.OnExecuting))
                        sb.AppendFormat("OnDeleting: eval({0}),", formview.DataBinder.DeleteCommand.OnExecuting);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.DeleteCommand.OnExecuted))
                        sb.AppendFormat("OnDeleted: eval({0}),", formview.DataBinder.DeleteCommand.OnExecuted);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.DeleteCommand.DataItemPropertyName))
                        sb.AppendFormat("DeleteDataItemProperty: \"{0}\",", formview.DataBinder.DeleteCommand.DataItemPropertyName);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.DeleteCommand.Parameters))
                        sb.AppendFormat("DeleteParameters: {0},", delete_parameters_guid);
                }
                if (formview.DataBinder.CreateCommand != null)
                {
                    sb.AppendFormat("CreateCommand: \"{0}\",", formview.DataBinder.CreateCommand.Target);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.CreateCommand.OnExecuting))
                        sb.AppendFormat("OnCreating: eval({0}),", formview.DataBinder.CreateCommand.OnExecuting);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.CreateCommand.OnExecuted))
                        sb.AppendFormat("OnCreated: eval({0}),", formview.DataBinder.CreateCommand.OnExecuted);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.CreateCommand.DataItemPropertyName))
                        sb.AppendFormat("CreateDataItemProperty: \"{0}\",", formview.DataBinder.CreateCommand.DataItemPropertyName);

                    if (!string.IsNullOrWhiteSpace(formview.DataBinder.CreateCommand.Parameters))
                        sb.AppendFormat("CreateParameters: {0},", create_parameters_guid);
                }
                sb.Append("},");
            }

            string script = sb.ToString().Trim(',').Trim() + "});";
            script += "})();";
            ControlScripts.Append(script);
        }
    }
}
