Dat.V1.Controls.TreeView = function (options) {
    var me = this;
    this.ID = options.ID;
    this.Element = null;
    this.OnDataBound = options.OnDataBound;
    this.OnDataBinding = options.OnDataBinding;
    this.OnItemDataBound = options.OnItemDataBound;
    this.OnItemDataBinding = options.OnItemDataBinding;
    this.OnItemCommand = options.OnItemCommand;
    this.Container = jQuery(options.Container);
    this.ChildrenContainer = options.ChildrenContainer;
    this.OnInit = options.OnInit;
    this.ChildPrimaryKeyField = options.ChildPrimaryKeyField;
    this.OnInitEach = options.OnInitEach;
    this.OnExpanding = options.OnExpanding;
    this.OnExpanded = options.OnExpanded;
    this.OnCollapsing = options.OnCollapsing;
    this.OnCollapsed = options.OnCollapsed;
    this.OnDeSelecting = options.OnDeSelecting;
    this.OnDeSelected = options.OnDeSelected;
    this.OnSelecting = options.OnSelecting;
    this.OnSelected = options.OnSelected;
    this.OnError = options.OnError;
    this.OnReadyEach = options.OnReadyEach;
    this.OnReady = options.OnReady;
    this.Sortable = options.Sortable;
    this.Draggable = options.Draggable;
    this.Droppable = options.Droppable;
    this.AssetName = options.AssetName;
    this.TemplateName = options.TemplateName;
    this.SelectedNode = null;
    this.Parent = options.Parent;
    this.Root = options.Root || me;
    var select_parameters = options.DataBinder.SelectParameters;
    this.DataBinder = new Dat.V1.Controls.DataBinder({
        AssetName: options.DataBinder.AssetName,
        OnError: me.OnError,
        Interval: options.DataBinder.Interval,
        SelectCommand: options.DataBinder.SelectCommand,
        SelectParameters: select_parameters,
        PrimaryKey: options.DataBinder.PrimaryKey,
        FreezeMode: true
    });
    this.Collapse = function (collapse_arg) {
        onCollapsing(collapse_arg);
        if (collapse_arg.Cancel === true) return;
        var $children = collapse_arg.Node.FindElement(".children");
        if ($children.length == 0) {
            onCollapsed(collapse_arg);
            if (collapse_arg.OnCollapsed)
                collapse_arg.OnCollapsed(collapse_arg);
            return;
        }
        $children.slideUp(function () {
            onCollapsed(collapse_arg);
            if (collapse_arg.OnCollapsed)
                collapse_arg.OnCollapsed(collapse_arg);
            $children.remove();
        })

    };
    this.Select = function (select_arg) {
        if (!select_arg.Node)
            if (me.DataBinder.PrimaryKey && select_arg[me.DataBinder.PrimaryKey]) {
                me.Root.ListView.Element.find("li").each(function (i, v) {
                    var $data = jQuery(v).data("listviewitem");
                    if ($data && $data.DataItem[me.DataBinder.PrimaryKey] == select_arg[me.DataBinder.PrimaryKey])
                        select_arg = { Node: $data, TreeView: me, Cancel: false };
                });
            }
            else return;
        if (!select_arg.Node) return;
        onSelecting(select_arg);
        if (select_arg.Cancel === true) return;
        me.Root.SelectedNode = select_arg.Node;
        onSelected(select_arg);
    };
    this.DeSelect = function () {
        console.log("deselect");
        var $selected = jQuery(me.Root.ListView.Element.find(".selected")).removeClass("selected").addClass("not-selected");
        jQuery(me.Root.ListView.Element.find(".selected-node")).removeClass("selected-node");
        var listviewitem = $selected.parent().data("listviewitem");
        var deselect_arg = { Node: listviewitem, TreeView: me, Cancel: false };
        onDeSelecting(deselect_arg);
        if (deselect_arg.Cancel === true) return;
        onDeSelected(deselect_arg);
        me.Root.SelectedNode = null;
    };
    this.Expand = function (expand_arg) {
        onExpanding(expand_arg);
        if (expand_arg.Cancel === true) return;
        var children_id = Dat.V1.Utils.Element.GetUniqueID(expand_arg.Node.ID);
        var $children = jQuery("<div>").addClass("children").attr("id", children_id);
        expand_arg.Node.FindElement(".children").remove();
        $children.hide().slideUp();
        expand_arg.Node.Element.append($children);
        var parameters = new Array();
        parameters.push(expand_arg.Node.DataItem[me.ChildPrimaryKeyField]);
        var children = new Dat.V1.Controls.TreeView({
            ID: children_id + "_TreeView",
            Container: "#" + children_id,
            TemplateName: me.TemplateName,
            ChildPrimaryKeyField: me.ChildPrimaryKeyField,
            OnInitEach: me.OnInitEach,
            OnError: me.OnError,
            OnReadyEach: me.OnReadyEach,
            OnDataBound: function (lv) {
                if ($children.length == 0) {
                    onExpanded(expand_arg);
                    if (expand_arg.OnExpanded)
                        expand_arg.OnExpanded(expand_arg);
                    return;
                }
                $children.slideDown(500, function () {
                    onExpanded(expand_arg);
                    if (expand_arg.OnExpanded)
                        expand_arg.OnExpanded(expand_arg);
                });
            },
            Root: me.Parent ? me.Parent.Root : me,
            OnSelecting: onSelecting,
            OnSelected: onSelected,
            DataBinder: {
                OnError: me.OnError,
                AssetName: me.DataBinder.AssetName,
                OnError: me.OnError,
                Interval: me.DataBinder.Interval,
                SelectCommand: me.DataBinder.SelectCommand,
                SelectParameters: parameters,
                PrimaryKey: me.DataBinder.PrimaryKey,
                FreezeMode: true
            },
            Parent: expand_arg.TreeView
        });
        $children.data("control", children);

    };

    onExpanded = function (expanded_arg) {
        expanded_arg.Node.Element.addClass("expanded");
        expanded_arg.Node.NodeButton.removeClass("expand-button");
        expanded_arg.Node.NodeButton.addClass("collapse-button");
        if (me.OnExpanded)
            me.OnExpanded(expanded_arg);
    };
    onExpanding = function (expanding_arg) {
        if (me.OnExpanding)
            me.OnExpanding(expanding_arg);
    };
    onSelected = function (selected_arg) {
        selected_arg.Node.Element.removeClass("not-selected");
        selected_arg.Node.Element.addClass("selected");
        selected_arg.Node.NodeText.addClass("selected-node");
        if (me.OnSelected)
            me.OnSelected(selected_arg);
    };
    onSelecting = function (selecting_arg) {
        if (me.OnSelecting)
            me.OnSelecting(selecting_arg);
    };
    onDeSelected = function (deselected_arg) {
        if (me.OnDeSelected)
            me.OnDeSelected(deselected_arg);
    };
    onDeSelecting = function (deselected_arg) {
        if (me.OnDeSelecting)
            me.OnDeSelecting(deselected_arg);
    };
    onCollapsed = function (collapsed_arg) {
        collapsed_arg.Node.Element.removeClass("expanded");
        collapsed_arg.Node.NodeButton.addClass("expand-button");
        collapsed_arg.Node.NodeButton.removeClass("collapse-button");
        if (me.OnCollapsed)
            me.OnCollapsed(collapsed_arg);
    };
    onCollapsing = function (collpsing_arg) {
        if (me.OnCollapsing)
            me.OnCollapsing(collpsing_arg);
    };
    me.ListView = new Dat.V1.Controls.ListView({
        ID: me.ID,
        OnDataBound: me.OnDataBound,
        OnDataBinding: me.OnDataBinding,
        OnItemDataBound: me.OnItemDataBound,
        TagName: "ul",
        TemplateName: me.TemplateName,
        OnInit: me.OnInitEach,
        Container: options.Container,
        OnItemCommand: me.OnItemCommand,
        OnError: me.OnError,
        OnReady: function (listview) {
            if (!me.Parent)
                if (me.OnReady)
                    me.OnReady(me)
            if (me.OnReadyEach)
                me.OnReadyEach(listview);
            listview.DataBind();
        },
        OnInit: function (lv) {
            lv.Container.append(lv.Element);
            lv.Element.css("list-style-type", "none");
            if (!me.Parent)
                if (me.OnInit)
                    me.OnInit(me);
            if (me.OnInitEach)
                me.OnInitEach(lv);
        },
        OnItemDataBinding: function (listviewitem, listview) {
            listviewitem.NodeText = listviewitem.FindElement("[data-role='node-text']");
            listviewitem.NodeButton = listviewitem.FindElement("[data-role='node-button']");
            listviewitem.NodeButton.addClass("expand-button");
            listviewitem.NodeButton.removeAttr("data-role");
            listviewitem.NodeText.removeAttr("data-role");
            listviewitem.TreeView = me;
            if (me.OnItemDataBinding)
                me.OnItemDataBinding(listviewitem.listview);
        },
        OnItemCommand: function (arg) {
            switch (arg.CommandName.toLowerCase()) {
                case "expand": {
                    var expand_arg = { Node: arg.ListViewItem, TreeView: me, Cancel: false };
                    if (expand_arg.Node.Element.hasClass("expanded"))
                        me.Collapse(expand_arg);
                    else
                        me.Expand(expand_arg);
                    break;
                }
                case "click": {
                    var selected_arg = { Node: arg.ListViewItem, TreeView: me, Cancel: false };
                    me.DeSelect();
                    me.Select(selected_arg);
                    break;
                }
            }
            if (me.OnItemCommand)
                me.OnItemCommand(arg);
        },
        DataBinder: me.DataBinder
    });
};