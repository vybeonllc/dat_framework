Dat.V1.Controls.FormView = function (options) {
    var me = this;
    this.ControlName = "FormView";
    this.TagName = options.TagName ? options.TagName : "div";
    this.ID = Dat.V1.Utils.Element.GetUniqueID(me.TagName + "_" + options.ID);
    this.Header = null;
    this.FilterElement = options.FilterElement;
    this.Container = jQuery(options.Container);
    this.Footer = null;
    this.TemplateData = null;
    this.TemplateName = options.TemplateName;
    this.Template = null;
    this.HeaderTemplate = null;
    this.OnHeaderInitialized = options.OnHeaderInitialized;
    this.FooterTemplate = null;
    this.OnFooterInitialized = options.OnFooterInitialized;
    this.FooterTemplate = null;
    this.ItemTemplate = null;
    this.DataBinder = null;
    this.Element = jQuery("<" + me.TagName + ">").attr("id", me.ID);
    me.Element.data("control", me);
    this.DataItem = null;
    this.DataItemSchema = options.DataItemSchema;
    this.FormViewItem = null;
    this.FormViewMode = Dat.V1.Controls.FormViewMode.Modify;
    this.SubmitElement = options.SubmitElement ? options.SubmitElement : "#" + me.ID + " [name='data-submit']";
    this.DeleteElement = options.DeleteElement ? options.DeleteElement : "#" + me.ID + " [name='data-delete']";
    this.OnError = options.OnError;
    this.OnFieldBound = options.OnFieldBound;
    this.OnFieldBinding = options.OnFieldBinding;
    this.OnDataBinding = options.OnDataBinding;
    this.OnSuggestionSelected = options.OnSuggestionSelected;
    this.OnDataBound = options.OnDataBound;
    this.OnItemCommand = options.OnItemCommand;
    this.OnReady = null;
    this.Dispose = function () {
        if (me.DataBinder) me.DataBinder.Dispose();
    };
    var submitElement = null;
    var deleteElement = null;
    var onDataBinding = function () {
        if (me.OnDataBinding)
            me.OnDataBinding(me);
    };
    var onDataBound = function () {
        if (me.OnDataBound)
            me.OnDataBound(me);
    };
    this.Update = function () {
        console.log("saving.");
        if (me.DataItem == null)
            return;
        var primary_key = me.DataItem[me.DataBinder.PrimaryKey];
        if (!primary_key || primary_key === 0 || primary_key === "00000000-0000-0000-0000-000000000000") {
            me.FillDataItem();
            me.DataBinder.Create(me.DataItem);
        }
        else if (!primary_key || primary_key !== 0) {
            me.FillDataItem();
            me.DataBinder.Update(me.DataItem);
        }
    };
    this.Delete = function () {
        if (me.DataItem == null)
            return;
        var primary_key = me.DataItem[me.DataBinder.PrimaryKey];
        if (!primary_key || primary_key === 0)
            return;
        else
            me.DataBinder.Delete(me.DataItem);
    };
    this.Init = function () {
        if (me.Template.find("[role='headertemplate']").length != 0) {
            me.HeaderTemplate = new Dat.V1.Controls.FormViewTemplate();
            me.HeaderTemplate.Element = jQuery(me.Template.find("[role='headertemplate']")).removeAttr("role").clone(true);
            me.Header = new Dat.V1.Controls.FormViewItem();
            me.Header.ItemTemplate = me.HeaderTemplate.Element;
            me.Header.Element = me.HeaderTemplate.Element;
            me.Header.ItemType = Dat.V1.Controls.FormViewItemType.Header;
            me.Header.ID = me.ID + "_header";
            me.Header.Element.attr("id", me.Header.ID);
            me.Header.AppendTo(me.Element);
        };
        if (me.Template.find("[role='footertemplate']").length != 0) {
            me.FooterTemplate = new Dat.V1.Controls.FormViewgTemplate();
            me.FooterTemplate.Element = jQuery(me.Template.find("[role='footertemplate']")).removeAttr("role").clone(true);
            me.Footer = new Dat.V1.Controls.FormViewItem();
            me.Footer.ItemTemplate = me.FooterTemplate.Element;
            me.Footer.Element = me.FooterTemplate.Element;
            me.Footer.ItemType = Dat.V1.Controls.FormViewItemType.Footer;
            me.Footer.ID = me.ID + "_footer";
            me.Footer.Element.attr("id", me.Footer.ID);
        }
        if (me.Template.find("[role='emptyitemtemplate']").length != 0) {
            me.EmptyItemTemplate = new Dat.V1.Controls.FormViewTemplate();
            me.EmptyItemTemplate.Element = jQuery(me.Template.find("[role='emptyitemtemplate']")).removeAttr("role").clone(true);
            me.EmptyItem = new Dat.V1.Controls.FormViewItem();
            me.EmptyItem.ItemTemplate = me.EmptyItemTemplate.Element;
            me.EmptyItem.Element = me.EmptyItemTemplate.Element;
            me.EmptyItem.ItemType = Dat.V1.Controls.FormViewItemType.EmptyItem;
            me.EmptyItem.ID = me.ID + "_emptyitem";
            me.EmptyItem.Element.attr("id", me.EmptyItem.ID);
        }
        if (me.Template.find("[role='itemtemplate']").length != 0) {
            me.ItemTemplate = new Dat.V1.Controls.FormViewTemplate();
            me.ItemTemplate.Element = jQuery(me.Template.find("[role='itemtemplate']")).removeAttr("role").clone(true);
        }
        me.DataBinder = new Dat.V1.Controls.DataBinder({
            IsSingleObject: true,
            AssetName: options.DataBinder.AssetName,
            SelectCommand: options.DataBinder.SelectCommand,
            UpdateCommand: options.DataBinder.UpdateCommand,
            CreateCommand: options.DataBinder.CreateCommand,
            DeleteCommand: options.DataBinder.DeleteCommand,
            OnCreating: function (databiner, event_arg) { if (options.DataBinder.OnCreating) options.DataBinder.OnCreating(me, event_arg); },
            OnCreated: function (databiner, event_arg) { me.DataItem = event_arg; if (options.DataBinder.OnCreated) options.DataBinder.OnCreated(me, event_arg); },
            OnUpdating: function (databiner, event_arg) { if (options.DataBinder.OnUpdating) options.DataBinder.OnUpdating(me, event_arg); },
            OnUpdated: function (databiner, event_arg) { me.DataItem = event_arg; if (options.DataBinder.OnUpdated) options.DataBinder.OnUpdated(me, event_arg); },
            OnDeleting: function (databiner, event_arg) { if (options.DataBinder.OnDeleting) options.DataBinder.OnDeleting(me, event_arg); },
            OnDeleted: function (databiner, event_arg) { if (options.DataBinder.OnDeleted) options.DataBinder.OnDeleted(me, event_arg); },
            OnSelecting: function (databinder) { if (options.DataBinder.OnSelecting) options.DataBinder.OnSelecting(me); },
            OnSelected: function (databiner, event_arg) { if (options.DataBinder.OnSelected) options.DataBinder.OnSelected(me, event_arg); },
            SelectParameters: options.DataBinder.SelectParameters,
            UpdateParameters: options.DataBinder.UpdateParameters,
            CreateParameters: options.DataBinder.CreateParameters,
            DeleteParameters: options.DataBinder.DeleteParameters,
            PrimaryKey: options.DataBinder.PrimaryKey,
            Interval: options.DataBinder.Interval,
            FreezeMode: options.DataBinder.FreezeMode ? true : false,
            SelectDataItemProperty: options.DataBinder.SelectDataItemProperty,
            UpdateDataItemProperty: options.DataBinder.UpdateDataItemProperty,
            CreateDataItemProperty: options.DataBinder.CreateDataItemProperty,
            OnDataBinding: onDataBinding,
            OnError: me.OnError
        });


        if (options.OnInit)
            options.OnInit(me);

    }
    this.FillControls = function () {
        for (i = 0 ; i < me.FormViewItem.Fields.length; i++) {
            var datafield = me.FormViewItem.Fields[i];
            var value = me.DataItem[datafield.Name];
            switch (datafield.Elements.DataType) {
                case "date":
                    value = Dat.V1.Utils.Text.Replace(value.toString(), "/", "");
                    value = eval("new " + value);
                    break;
                case "bool":
                    value = value ? "true" : "false";
                default:
                    break;
            }
            for (j = 0 ; j < datafield.Elements.length ; j++) {
                //if (me.OnFieldBinding)
                //    me.OnFieldBinding(field, value, datafield.Elements, me.FormViewItem, me);
                var $field = jQuery(datafield.Elements[j]);
                Dat.V1.Utils.Element.SetElementValue($field, value);
                //if (me.OnFieldBound)
                //    me.OnFieldBound(field, value, $field, me.FormViewItem, me);
            }
        }
    };
    this.DataBind = function (ondatabound) {
        me.DataBinder.OnDataBound = function () {
            me.Element.empty();
            if (me.Header) {
                me.Header.AppendTo(me.Element);
                if (me.OnHeaderInitialized)
                    me.OnHeaderInitialized(me.Header, me);
            }

            me.Element.data("databinder", me.DataBinder);
            me.DataItem = me.FormViewMode == Dat.V1.Controls.FormViewMode.Create ? eval(me.DataItemSchema) : me.DataBinder.Result;


            if (me.OnDataBinding)
                me.OnDataBinding(me);

            if (!me.DataItem) {
                me.EmptyItem.AppendTo(me.Element);
                me.EmptyItem.Element.fadeIn();
            }
            else
                me.EmptyItem.Element.fadeOut();

            me.FormViewItem = new Dat.V1.Controls.FormViewItem();
            me.FormViewItem.ItemType = Dat.V1.Controls.FormViewItemType.Item;

            me.FormViewItem.ItemTemplate = me.ItemTemplate;
            me.FormViewItem.Container = me.Element;
            me.FormViewItem.ID = Dat.V1.Utils.Element.GetUniqueID(me.ID + "_item");
            me.FormViewItem.DataItem = me.DataItem;

            var element = jQuery(me.FormViewItem.ItemTemplate.Element).clone(true);
            element.attr("id", me.FormViewItem.ID);
            me.FormViewItem.Element = element;
            me.FormViewItem.Element.data("dataitem", me.DataItem);

            //var suggestions = new Array();
            //jQuery(formviewitem.Element.find("[suggestion-dataprovider]")).each(function (i, v) {
            //    var $field = jQuery(v);
            //    var suggestionelement_class = $field.attr("suggestion-elementclass"), suggestion_class = $field.attr("suggestion-class");
            //    var fieldName = $field.attr("data-field");
            //    var suggestion = new Dat.V1.Controls.SupportSuggestion({
            //        Element: $field,
            //        DataProvider: eval($field.attr("suggestion-dataprovider")),
            //        MinimumCharacters: $field.attr("suggestion-minumum_characters"),
            //        MaximumSuggestions: $field.attr("suggestion-max_suggestions"),
            //        SuggestionBoxClass: suggestion_class,
            //        SuggestionBoxElementClass: suggestionelement_class,
            //        OnItemDataBound: function (obj, suggestion_element, dataitem) {
            //            suggestion_element.text(dataitem.text);
            //        },
            //        OnIdeaRequested: function (field) {
            //            field.Idea = TwentyNinePrime.UI.Utils.GetElementValue($field);
            //        },
            //        OnSelect: function (obj, suggestion_element, dataitem, text) {
            //            TwentyNinePrime.UI.Utils.SetElementValue($field, dataitem.text);

            //            if (me.OnSuggestionSelected)
            //                me.OnSuggestionSelected(me, { Suggestion: { SuggestionObject: obj, SuggestionElement: suggestion_element, DataItem: dataitem, Idea: text }, EventInfo: { DataField: { Text: fieldName, Value: formviewitem.DataItem[fieldName], Field: $field }, FormViewItem: formviewitem } });
            //        },
            //        OnReady: function (obj) {
            //            obj.SuggestionBox.insertAfter(obj.Element);
            //        }
            //    });
            //    suggestions.push(suggestion);
            //    $field.removeAttr("suggestion-dataprovider")
            //        .removeAttr("suggestion-class")
            //        .removeAttr("suggestion-elementclass")
            //        .removeAttr("suggestion-minumum_characters")
            //        .removeAttr("suggestion-max_suggestions");
            //});
            //formviewitem.Element.data("suggestions", suggestions);

            for (var field in me.DataItem) {
                var $fields = jQuery(me.FormViewItem.Element.find("[data-field='" + field + "']"));
                me.FormViewItem.Fields.push({ Name: field, Elements: $fields, DataType: $fields.length == 0 ? null : jQuery($fields[0]).attr("data-type"), Nullable: $fields.length == 0 ? null : jQuery($fields[0]).attr("data-nullable") });
                for (j = 0 ; j < $fields.length ; j++) {
                    var $field = jQuery($fields[j]);
                    var data_type = $field.attr("data-type");
                    var value = me.DataItem[field];
                    if (me.OnFieldBinding)
                        me.OnFieldBinding(field, value, $field, me.FormViewItem, me);
                    $field.removeAttr("data-field").removeAttr("data-type").removeAttr("data-nullable");
                    switch (data_type) {
                        case "date":
                            value = Dat.V1.Utils.Text.Replace(value.toString(), "/", "");
                            value = eval("new " + value);
                            break;
                        case "bool":
                            value = value ? "true" : "false";
                        default:
                            break;
                    }

                    Dat.V1.Utils.Element.SetElementValue($field, value);

                    if (me.OnFieldBound)
                        me.OnFieldBound(field, value, $field, me.FormViewItem, me);
                }
                var $commandArgs = jQuery(me.FormViewItem.Element.find("[CommandArg='" + field + "']"));
                for (x = 0 ; x < $commandArgs.length ; x++) {
                    var $command_element = jQuery($commandArgs[x]);
                    var command_name = $command_element.attr("CommandName");
                    var value = me.DataItem[field];
                    $command_element.removeAttr("CommandArg").removeAttr("CommandName").data("eventarg", { FormViewItem: me.FormViewItem, CommandName: command_name, CommandArg: value, FormView: me, Field: { Name: field, Element: $command_element } });
                    $command_element.click(function () {
                        if (me.OnItemCommand)
                            me.OnItemCommand(jQuery(this).data("eventarg"));
                    });
                }
            }



            me.FormViewItem.AppendTo(me.Element);

            if (me.Footer) {
                me.Footer.AppendTo(me.Element);
                if (me.OnFooterInitialized)
                    me.OnFooterInitialized(me.Footer, me);
                //me.Footer.Element.text(me.DataBinder.ResultSet.total_results + " result(s)");
            }

            submitElement = jQuery(me.SubmitElement);
            deleteElement = jQuery(me.DeleteElement);


            if (submitElement && submitElement.length > 0)
                submitElement.click(function () {
                    me.Update();
                });
            if (deleteElement && deleteElement.length > 0)
                deleteElement.click(function () {
                    me.Delete();
                });
            if (me.OnDataBound)
                me.OnDataBound(me);
            if (ondatabound)
                ondatabound();
            if (me.FormViewMode == Dat.V1.Controls.FormViewMode.Create) {
                if (deleteElement && deleteElement.length != 0)
                    deleteElement.hide();
            }
            else
                if (deleteElement && deleteElement.length != 0)
                    deleteElement.show();
        };
        if (me.FormViewMode == Dat.V1.Controls.FormViewMode.Create)
            me.DataBinder.OnDataBound();
        else
            me.DataBinder.DataBind();
    };
    this.FillDataItem = function () {
        jQuery(me.FormViewItem.Fields).each(function (i, v) {
            if (v.Elements.length == 0)
                return;
            var val = Dat.V1.Utils.Element.GetElementValue(jQuery(v.Elements[0]));;
            if (v.Nullable && (!val || val == ""))
                val = null;
            else
                switch (v.DataType) {
                    case "date":
                        var dt = new Date(val);
                        val = '/Date(' + new Date(Date.UTC(dt.getFullYear(), dt.getMonth(), dt.getDate(), dt.getHours(), dt.getMinutes(), dt.getSeconds(), dt.getMilliseconds())).getTime() + ')/';
                        break;
                    case "bool":
                        val = val || val == false || null;
                    case "guid":
                        val = val || "00000000-0000-0000-0000-000000000000";
                    default:
                        break;
                };
            me.DataItem[v.Name] = val;
        });
    }
    Dat.V1.Services.Common.Resource.Template({
        TemplateName: me.TemplateName,
        OnSuccess: function (content_response) {
            me.Template = jQuery(content_response);
            me.Template = jQuery("<div>").append(me.Template);
            me.Init();
            if (options.OnReady)
                options.OnReady(me);
        },
        OnError: function (error) {
            me.OnError("OnCreatingTemplate", error);
        }
    });
};