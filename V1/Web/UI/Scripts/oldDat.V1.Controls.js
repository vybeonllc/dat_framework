Dat.V1.Controls = {
    RegisteredControls: new Array("ListView"),
    UserControl: function (options) {
        var me = this;
        this.TagName = options.TagName;
        this.ControlName = options.ControlName;
        this.ID = options.ID;
        this.Container = options.Container;
        this.Definition = options.Definition;
        this.Control = null;
        this.Constructor = eval("Dat.V1.Controls." + me.ControlName + ".FromDefinition");
        if (options.AutoInit)
            me.Control = me.Constructor ? me.Constructor(me) : null;
    },
    ListView: function (options) {
        var me = this;
        this.TagName = options.TagName ? options.TagName : "ul";
        this.ControlName = "ListView";
        this.ID = Dat.V1.Utils.Element.GetUniqueID(me.TagName + "_" + options.ID);
        this.Element = jQuery("<" + me.TagName + ">").attr("id", me.ID);
        this.Control = new Dat.V1.Controls.UserControl({
            ID: me.ID,
            TagName: me.TagName,
            ControlName: me.ControlName,
            Container: me.Container
        });
        this.Header = null;
        this.FilterElement = options.FilterElement;
        this.Container = options.Container;
        this.Footer = null;
        this.Pager = null;
        this.DefaultPageSize = 50;
        this.TemplateData = null;
        this.TemplateName = options.TemplateName;
        this.Template = null;
        this.HeaderTemplate = null;
        this.OnHeaderInitialized = options.OnHeaderInitialized;
        this.FooterTemplate = null;
        this.OnFooterInitialized = options.OnFooterInitialized;
        this.ItemTemplate = null;
        this.PagerTemplate = null;
        this.OnPagerInitialized = options.OnPagerInitialized;
        this.OnChangingPage = options.OnChangingPage;
        this.FooterTemplate = null;
        this.OnItemDataBinding = options.OnItemDataBinding;
        this.OnItemDataBound = options.OnItemDataBound;
        this.OnError = null;
        this.OnDataBinding = options.OnDataBinding;
        this.OnDataBound = options.OnDataBound;
        this.OnSuggestionSelected = options.OnSuggestionSelected;
        this.OnReady = null;
        this.Sortable = options.Sortable;
        this.Draggable = options.Draggable;
        this.Droppable = options.Droppable;
        this.OnItemCommand = options.OnItemCommand;

        if (me.Droppable) {
            var droppable_drop = me.Droppable.drop;
            me.Droppable.drop = function (e, ui) {

                var $draggable = jQuery(ui.draggable[0]);
                var $droppable = jQuery(e.target);

                if (droppable_drop) {
                    droppable_drop(me, {
                        draggable: { dataitem: $draggable.data("dataitem"), element: $draggable },
                        droppable: { dataitem: $droppable.data("dataitem"), element: $droppable }
                    }, e, ui);
                }
            };
        }
        if (me.Sortable) {
            var sorting_start = me.Sortable.start,
                      sorting_stop = me.Sortable.stop,
                      sorting_change = me.Sortable.change;
            me.Sortable.start = function (e, ui) {
                var $target = jQuery(e.toElement),
                       target_dataitem = $target.data("dataitem"),
                     $prev = $target.prev().length == 0 ? null : $target.prev(),
                     prev_dataitem = $prev ? $prev.data("dataitem") : null;
                if (sorting_start)
                    sorting_start(me, {
                        target: { dataitem: target_dataitem, element: $target },
                        prev: $prev == null ? null : { dataitem: prev_dataitem, element: $prev }
                    }, e, ui);
            };
            me.Sortable.stop = function (e, ui) {
                var $target = jQuery(e.toElement),
                     target_dataitem = $target.data("dataitem"),
                     $prev = $target.prev().length == 0 ? null : $target.prev(),
                     prev_dataitem = $prev ? $prev.data("dataitem") : null;

                if (sorting_stop)
                    sorting_stop(me, {
                        target: { dataitem: target_dataitem, element: $target },
                        prev: $prev == null ? null : { dataitem: prev_dataitem, element: $prev }
                    }, e, ui);
            };
            me.Sortable.change = function (e, ui) {
                var $target = jQuery(e.toElement),
                       target_dataitem = $target.data("dataitem"),
                      $prev = $target.prev().length == 0 ? null : $target.prev(),
                     prev_dataitem = $prev ? $prev.data("dataitem") : null;

                if (sorting_change)
                    sorting_change(me, {
                        target: { dataitem: target_dataitem, element: $target },
                        prev: $prev == null ? null : { dataitem: prev_dataitem, element: $prev }
                    }, e, ui);
            };
        }
        this.Dispose = function () {
            if (me.DataBinder) me.DataBinder.Dispose();
        };
        var onDataBinding = function () {
            if (me.OnDataBinding)
                me.OnDataBinding(me);
        };
        var onDataBound = function () {
            if (me.OnDataBound)
                me.OnDataBound(me);
        };

        this.DataBinder = null;
        this.Items = new Array();

        this.Init = function () {

            if (me.Template.find("[role='headertemplate']").length != 0) {
                me.HeaderTemplate = new Dat.V1.Controls.ListViewTemplate();
                me.HeaderTemplate.Element = jQuery(me.Template.find("[role='headertemplate']")).removeAttr("role").clone(true);
                me.Header = new Dat.V1.Controls.ListViewItem();
                me.Header.ItemTemplate = me.HeaderTemplate.Element;
                me.Header.Element = me.HeaderTemplate.Element;
                me.Header.ItemType = Dat.V1.Controls.ListViewItemType.Header;
                me.Header.ID = me.ID + "_header";
                me.Header.Element.attr("id", me.Header.ID);

            };
            if (me.Template.find("[role='pagertemplate']").length != 0) {
                me.PagerTemplate = new Dat.V1.Controls.ListViewTemplate();
                me.PagerTemplate.Element = jQuery(me.Template.find("[role='pagertemplate']")).removeAttr("role").clone(true);
                me.Pager = new Dat.V1.Controls.ListViewItem();
                me.Pager.ItemTemplate = me.PagerTemplate.Element;
                me.Pager.Element = me.PagerTemplate.Element;
                me.Pager.ItemType = Dat.V1.Controls.ListViewItemType.Pager;
                me.Pager.ID = me.ID + "_pager";
                me.Pager.Element.attr("id", me.Pager.ID);

            };
            if (me.Template.find("[role='footertemplate']").length != 0) {
                me.FooterTemplate = new Dat.V1.Controls.ListViewTemplate();
                me.FooterTemplate.Element = jQuery(me.Template.find("[role='footertemplate']")).removeAttr("role").clone(true);
                me.Footer = new Dat.V1.Controls.ListViewItem();
                me.Footer.ItemTemplate = me.FooterTemplate.Element;
                me.Footer.Element = me.FooterTemplate.Element;
                me.Footer.ItemType = Dat.V1.Controls.ListViewItemType.Footer;
                me.Footer.ID = me.ID + "_footer";
                me.Footer.Element.attr("id", me.Footer.ID);
            }
            if (me.Template.find("[role='emptyitemtemplate']").length != 0) {
                me.EmptyItemTemplate = new Dat.V1.Controls.ListViewTemplate();
                me.EmptyItemTemplate.Element = jQuery(me.Template.find("[role='emptyitemtemplate']")).removeAttr("role").clone(true);
                me.EmptyItem = new Dat.V1.Controls.ListViewItem();
                me.EmptyItem.ItemTemplate = me.EmptyItemTemplate.Element;
                me.EmptyItem.Element = me.EmptyItemTemplate.Element;
                me.EmptyItem.ItemType = Dat.V1.Controls.ListViewItemType.EmptyItem;
                me.EmptyItem.ID = me.ID + "_emptyitem";
                me.EmptyItem.Element.attr("id", me.EmptyItem.ID);
            }
            if (me.Template.find("[role='itemtemplate']").length != 0) {
                me.ItemTemplate = new Dat.V1.Controls.ListViewTemplate();
                me.ItemTemplate.Element = jQuery(me.Template.find("[role='itemtemplate']")).removeAttr("role").clone(true);
            }

            me.DataBinder = new Dat.V1.Controls.ListViewDataBinder({
                DataProvider: options.DataBinder.DataProvider,
                StartIndex: options.DataBinder.StartIndex,
                PageSize: options.DataBinder.PageSize,
                Interval: options.DataBinder.Interval,
                FreezeMode: options.DataBinder.FreezeMode ? true : false,
                OnDataBinding: onDataBinding,
                OnError: me.OnError,
            });

            if (me.FilterElement) {
                jQuery(me.FilterElement.find("[data-filter]")).each(function (i, v) {
                    var $element = jQuery(v);
                    $element.bind($element.attr("data-filter"), function (e) {
                        window.setTimeout(function () {
                            me.DataBind();
                        }, 50);
                    }).removeAttr("data-filter");
                });
            }
            if (options.OnInit)
                options.OnInit(me);
        }
        this.DataBind = function (ondatabound) {
            me.DataBinder.OnDataBound = function () {
                me.Element.empty();
                if (me.Header) {
                    me.Header.AppendTo(me.Element);
                    if (me.OnHeaderInitialized)
                        me.OnHeaderInitialized(me.Header, me);
                }
                me.Element.data("databinder", me.DataBinder);
                var data_set = me.DataBinder.ResultSet.results;


                while (me.Items.length != 0) {
                    var item = me.Items.pop();
                    item.Element.remove();
                }



                if (!data_set || data_set.length == 0) {
                    me.EmptyItem.AppendTo(me.Element);
                    me.EmptyItem.Element.fadeIn();
                    if (me.OnItemDataBinding);
                    me.OnItemDataBinding(me.EmptyItem, me);
                    if (me.OnItemDataBound)
                        me.OnItemDataBound(me.EmptyItem, me);
                }
                else {
                    me.EmptyItem.Element.fadeOut();
                }

                for (var i = 0 ; i < data_set.length ; i++) {
                    var dataitem = data_set[i];
                    var listviewitem = new Dat.V1.Controls.ListViewItem();
                    listviewitem.ItemType = i % 2 == 0
                      ? Dat.V1.Controls.ListViewItemType.Item
                      : Dat.V1.Controls.ListViewItemType.AlternateItem;

                    listviewitem.ItemTemplate = me.ItemTemplate;
                    listviewitem.Container = me.Element;
                    listviewitem.Index = i;
                    listviewitem.ID = Dat.V1.Utils.Element.GetUniqueID(me.ID + "_" +
                           (listviewitem.ItemType == Dat.V1.Controls.ListViewItemType.Item
                       ? "item"
                       : "alternate_item") + "_" + listviewitem.Index);
                    listviewitem.DataItem = dataitem;

                    var element = jQuery(listviewitem.ItemTemplate.Element).clone(true);

                    if (listviewitem.ItemType == Dat.V1.Controls.ListViewItemType.AlternateItem)
                        element.addClass("alternaterowtemplate");
                    element.attr("id", listviewitem.ID);
                    listviewitem.Element = element;
                    listviewitem.Element.data("dataitem", dataitem);

                    for (var field in dataitem) {
                        var $fields = jQuery(listviewitem.Element.find("[data-field='" + field + "']"));
                        for (j = 0 ; j < $fields.length ; j++) {
                            var $field = jQuery($fields[j]);
                            var data_type = $field.attr("data-type");
                            var value = dataitem[field];
                            $field.removeAttr("data-field").removeAttr("data-type")
                            switch (data_type) {
                                case "date":
                                    value = Dat.V1.Utils.Text.Replace(value.toString(), "/", "");
                                    value = eval("new " + value);
                                    break;
                                default:
                                    break;
                            }
                            Dat.V1.Utils.Element.SetElementValue($field, value);
                        }
                        var $commandArgs = jQuery(listviewitem.Element.find("[CommandArg='" + field + "']"));
                        for (x = 0 ; x < $commandArgs.length ; x++) {
                            var $command_element = jQuery($commandArgs[x]);
                            var command_name = $command_element.attr("CommandName");
                            var value = dataitem[field];
                            $command_element.removeAttr("CommandArg").removeAttr("CommandName").data("eventarg", { ListViewItem: listviewitem, CommandName: command_name, CommandArg: value, ListView: me, Field: { Name: field, Element: $command_element } });
                            $command_element.click(function () {
                                if (me.OnItemCommand)
                                    me.OnItemCommand(jQuery(this).data("eventarg"));
                            });
                        }

                    }
                    //jQuery(listviewitem.Element.find("[suggestion-dataprovider]")).each(function (i, v) {
                    //    var $field = jQuery(v);
                    //    var suggestionelement_class = $field.attr("suggestion-elementclass"), suggestion_class = $field.attr("suggestion-class");

                    //    Dat.V1.Controls.SupportSuggestion({
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
                    //            field.Idea = TwentyNinePrime.UI.Utils.GetElementValue(field.Field);

                    //        },
                    //        OnSelect: function (obj, suggestion_element, dataitem, text) {
                    //            field.Idea = TwentyNinePrime.UI.Utils.GetElementValue($field, dataitem.text);

                    //            if (me.OnSuggestionSelected)
                    //                me.OnSuggestionSelected(me, { SuggestionObject: obj, SuggestionElement: suggestion_element, SuggestionDataItem: dataitem, Idea: text, ListViewItem: listviewitem });
                    //        },
                    //        OnOpen: function (obj) {
                    //            obj.SuggestionBox.insertAfter(obj.Element);
                    //        }
                    //    });
                    //    $field.removeAttr("suggestion-dataprovider")
                    //        .removeAttr("suggestion-class")
                    //        .removeAttr("suggestion-elementclass")
                    //        .removeAttr("suggestion-minumum_characters")
                    //        .removeAttr("suggestion-max_suggestions");
                    //});



                    if (me.OnItemDataBinding)
                        me.OnItemDataBinding(listviewitem, me);

                    if (me.Draggable)
                        listviewitem.Element.css("z-index", 99999999).draggable(me.Draggable);
                    if (me.Droppable)
                        listviewitem.Element.droppable(me.Droppable);

                    listviewitem.AppendTo(me.Element);

                    me.Items.push(listviewitem);
                    if (me.OnItemDataBound)
                        me.OnItemDataBound(listviewitem, me);
                };
                if (me.Pager) {
                    me.Pager.AppendTo(me.Element);

                    me.Pager.FindElement("[role='next']").click(function () {
                        if (me.DataBinder.IsLastPage())
                            return;
                        if (me.OnChangingPage)
                            me.OnChangingPage(jQuery(this));
                        me.DataBinder.GoToPage(me.DataBinder.CurrentPage() + 1);
                    });
                    me.Pager.FindElement("[role='prev']").click(function () {
                        if (me.DataBinder.IsFirstPage())
                            return;
                        if (me.OnChangingPage)
                            me.OnChangingPage(jQuery(this));
                        me.DataBinder.GoToPage(me.DataBinder.CurrentPage() - 1);
                    });
                    me.Pager.FindElement("[name='pagesize']").change(function (e) {
                        me.DataBinder.PageSize = this.value;
                    });
                    me.Pager.FindElement("[role='pagenum']").text(me.DataBinder.CurrentPage() + " of " + me.DataBinder.TotalPages());
                    me.Pager.FindElement("[role='total_results']").text(me.DataBinder.TotalResults + " result(s).");
                    if (me.OnPagerInitialized)
                        me.OnPagerInitialized(me.Pager, me);
                }

                if (me.Footer) {
                    me.Footer.AppendTo(me.Element);
                    if (me.OnFooterInitialized)
                        me.OnFooterInitialized(me.Footer, me);
                    //me.Footer.Element.text(me.DataBinder.ResultSet.total_results + " result(s)");
                }
                if (me.Sortable) {
                    me.Element.sortable(me.Sortable);
                    me.Element.disableSelection();
                }
                if (me.OnDataBound)
                    me.OnDataBound(me);
                if (ondatabound)
                    ondatabound();
            };
            me.DataBinder.DataBind();
        };
        this.Item = function (comparer) {
            for (var i = 0 ; i < me.Items.length; i++) {
                var item = me.Items[i];
                if (item.ItemType == Dat.V1.Controls.ListViewItemType.Item
                    || item.ItemType == Dat.V1.Controls.ListViewItemType.AlternateItem)
                    if (comparer && comparer(item))
                        return item;
            }
        };
        Dat.V1.Services.Common.Resource.Template({
            TemplateName: me.TemplateName,
            OnSuccess: function (template_response) {
                me.TemplateData = template_response;
                Dat.V1.Utils.Template.Init({
                    Template: template_response.result_set.results[0],
                    OnSuccess: function (content_response) {
                        me.Template = jQuery(content_response);
                        me.Template = jQuery("<div>").append(me.Template);
                        me.Init();
                        if (options.OnReady)
                            options.OnReady(me);
                    },
                    OnError: function (error) {
                    }
                });
            },
            OnError: function (state, error) {
            }
        });
    },
    ListViewItem: function () {

        var me = this;
        this.Container = null;
        this.ID = null;
        this.Appended = false;
        this.FindElement = function (selector) {
            return jQuery(jQuery(me.Element).find(selector));
        };
        this.AppendTo = function (container, callback) {
            me.Appended = true;
            container.append(me.Element);
            me.Element.animate(me.ItemTemplate.Animation, callback);

        }
        this.Element = null;
        this.ItemTemplate = null;
        this.DataItem = null;
        this.ItemType = null;
        this.Index = null;
    },
    ListViewTemplate: function () {
        var me = this;
        this.Element = null;
    },
    ListViewItemType: {
        Header: "header",
        Footer: "footer",
        Item: "item",
        AlternateItem: "alternate_item",
        EmptyItem: "emptyitem",
        Pager: "pager"
    },

    ListViewDataBinder: function (options) {
        var me = this;
        var onDataBound = null;
        this.DataProvider = options.DataProvider;
        this.OnError = options.OnError;
        this.ResultSet = null;
        this.PageSize = options.PageSize;
        this.Parameters = null;
        this.Filters = null;
        this.TotalResults = null;
        this.StartIndex = options.StartIndex;
        this.Thread = new Dat.V1.Utils.Threading.Thread({
            Interval: options.Interval,
            OnThick: function (thread, onReady) {
                bind(function () {
                    if (me.OnDataBound)
                        me.OnDataBound();
                    onReady();
                });
            }
        });
        this.FreezeMode = options.FreezeMode;
        this.Dispose = function () {
            me.FreezeMode = true;
            me.Thread.Pause = true;
            delete me.Thread;
        };
        this.CurrentPage = function () {
            return Math.ceil(((me.StartIndex == 0 ? 1 : me.StartIndex) + 1) / me.PageSize);
        };
        this.IsLastPage = function () {
            return me.CurrentPage() == me.TotalPages();
        };
        this.IsFirstPage = function () {
            return me.CurrentPage() == 1;
        };
        this.TotalPages = function () {
            return Math.ceil((me.TotalResults == 0 ? 1 : me.TotalResults) / me.PageSize);
        };
        this.OnDataBinding = options.OnDataBinding;
        this.OnDataBound = options.OnDataBound;
        this.GoToPage = function (page_num) {
            me.StartIndex = (page_num - 1) * me.PageSize;
            me.DataBind();
        };
        var bind = function (callback) {
            me.DataProvider({
                StartIndex: me.StartIndex,
                PageSize: me.PageSize,
                Filters: me.Filters,
                Parameters: me.Parameters,
                OnSuccess: function (response) {
                    me.ResultSet = response.result_set;
                    me.TotalResults = response.result_set.total_results;
                    if (callback)
                        callback();
                    else
                        me.OnDataBound(me);
                },
                OnError: function (state, error) {
                    me.OnError(state, error);
                }
            });
        }
        this.DataBind = function () {
            if (me.OnDataBinding)
                me.OnDataBinding(me);
            if (me.FreezeMode) {
                me.Thread.Pause = true;
                bind();
            }
            else if (me.Thread.Pause)
                me.Thread.Start();
            else
                bind();
        };
    },
};
Dat.V1.Controls.ListView.FromDefinition = function (control) {
    var $definition = control.Definition;
    var $container = control.Container;
    var $databinder = jQuery($definition.find("DataBinder"));
    var $databinder_parameters = jQuery($databinder.find("Parameters Parameter"));
    var parameters = new Array();
    if ($databinder_parameters.length != 0)
        $databinder_parameters.each(function (i, v) {
            parameters.push(jQuery(v).attr("Value"));
        });
    var $filterElement = jQuery(control.Container.find("#" + $definition.attr("FilterElement")));

    if ($filterElement.length == 0)
        $filterElement = null;


    return new Dat.V1.Controls.ListView({
        ID: control.ID,
        TagName: control.TagName,
        TemplateName: $definition.attr("TemplateName"),
        FilterElement: $filterElement,
        Container: $container,
        OnItemDataBinding: eval($definition.attr("OnItemDataBinding")),
        OnItemDataBound: eval($definition.attr("OnItemDataBound")),
        OnDataBinding: eval($definition.attr("OnDataBinding")),
        OnDataBound: eval($definition.attr("OnDataBound")),
        OnHeaderInitialized: eval($definition.attr("OnHeaderInitialized")),
        OnFooterInitialized: eval($definition.attr("OnFooterInitialized")),
        OnPagerInitialized: eval($definition.attr("OnPagerInitialized")),
        OnReady: eval($definition.attr("OnReady")),
        OnItemCommand: eval($definition.attr("OnItemCommand")),
        OnSuggestionSelected: eval($definition.attr("OnSuggestionSelected")),
        OnError: eval($definition.attr("OnError")),
        DataBinder: {
            PageSize: $databinder.attr("PageSize"),
            StartIndex: $databinder.attr("StartIndex"),
            Interval: $databinder.attr("Interval"),
            DataProvider: eval($databinder.attr("DataProvider")),
        },
        OnInit: function (listview) {
            listview.Container.append(listview.Element);
            listview.DataBinder.Parameters = parameters;
        },
        OnReady: function (listview) {
            listview.DataBind();
        },
    });

};