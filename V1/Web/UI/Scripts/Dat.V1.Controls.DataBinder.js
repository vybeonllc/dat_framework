Dat.V1.Controls.DataBinder = function (options) {
    var me = this;
    var onDataBound = null;
    this.AssetName = options.AssetName;
    this.SelectCommand = options.SelectCommand;
    this.CreateCommand = options.CreateCommand;
    this.DeleteCommand = options.DeleteCommand;
    this.UpdateCommand = options.UpdateCommand;
    this.SelectParameters = options.SelectParameters;
    this.CreateParameters = options.CreateParameters;
    this.UpdateParameters = options.UpdateParameters;
    this.CreateParameters = options.CreateParameters;
    this.SelectDataItemProperty = options.SelectDataItemProperty;
    this.CreateDataItemProperty = options.CreateDataItemProperty;
    this.UpdateDataItemProperty = options.UpdateDataItemProperty;
    this.DeleteDataItemProperty = options.DeleteDataItemProperty;
    this.PrimaryKey = options.PrimaryKey;
    this.OnUpdating = options.OnUpdating;
    this.OnUpdated = options.OnUpdated;
    this.OnCreating = options.OnCreating;
    this.OnCreated = options.OnCreated;
    this.OnDeleting = options.OnDeleting;
    this.OnDeleted = options.OnDeleted;
    this.OnSelecting = options.OnSelecting;
    this.OnSelected = options.OnSelected;
    this.Result = null;
    this.IsSingleObject = options.IsSingleObject && options.IsSingleObject == true ? true : false;
    this.Update = function (DataItem) {
        if (!me.UpdateCommand)
            return;
        if (me.OnUpdating) {
            var event_arg = { DataItem: DataItem, Cancel: false };
            me.OnUpdating(me, event_arg);
            if (event_arg.Cancel)
                return;
        }
        var data = {};
        if (me.UpdateDataItemProperty)
            data[me.UpdateDataItemProperty] = DataItem;
        else data = DataItem;
        Dat.V1.AssetPool.Manager.Call(me.AssetName, me.UpdateCommand, {
            Parameters: me.UpdateParameters,
            Data: data,
            OnSuccess: function (response) {
                if (me.OnUpdated)
                    me.OnUpdated(me, me.IsSingleObject ? response.result : response.result_set.results[0]);
            },
            OnError: function (state, error) {
                me.OnError(state, error);
            }
        });
    };
    this.Delete = function (DataItem) {
        if (!me.DeleteCommand)
            return;
        if (me.OnDeleting) {
            var event_arg = { DataItem: DataItem, Cancel: false };
            me.OnDeleting(me, event_arg);
            if (event_arg.Cancel)
                return;
        }
        me.DeleteParameters = new Array();
        me.DeleteParameters.push(DataItem[me.PrimaryKey]);
        Dat.V1.AssetPool.Manager.Call(me.AssetName, me.DeleteCommand, {
            Parameters: me.DeleteParameters,
            OnSuccess: function (response) {
                if (me.OnDeleted)
                    me.OnDeleted(me, response.status);
            },
            OnError: function (state, error) {
                me.OnError(state, error);
            }
        });
    };
    this.Create = function (DataItem) {
        if (!me.CreateCommand)
            return;
        if (me.OnCreating) {
            var event_arg = { DataItem: DataItem, Cancel: false };
            me.OnCreating(me, event_arg);
            if (event_arg.Cancel)
                return;
        }
        var data = {};
        if (me.CreateDataItemProperty)
            data[me.CreateDataItemProperty] = DataItem;
        else data = DataItem;
        Dat.V1.AssetPool.Manager.Call(me.AssetName, me.CreateCommand, {
            Parameters: me.CreateParametersd,
            Data: data,
            OnSuccess: function (response) {
                DataItem = me.IsSingleObject ? response.result : response.result_set.results[0];
                if (me.OnCreated)
                    me.OnCreated(me, DataItem);
            },
            OnError: function (state, error) {
                me.OnError(state, error);
            }
        });
    };
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
        if (me.OnSelecting) {
            var event_arg = { Cancel: false };
            me.OnSelecting(me, event_arg);
            if (event_arg.Cancel)
                return;
        }
        Dat.V1.AssetPool.Manager.Call(me.AssetName, me.SelectCommand, {
            StartIndex: me.StartIndex,
            PageSize: me.PageSize,
            Filters: me.Filters,
            Parameters: me.SelectParameters,
            OnSuccess: function (response) {
                me.Result = me.IsSingleObject ? response.result : null;
                me.ResultSet = me.IsSingleObject ? null : response.result_set;
                me.TotalResults = me.IsSingleObject ? 1 : response.result_set.total_results;
                if (me.OnSelected)
                    me.OnSelected(me, { DataItem: me.IsSingleObject ? response.result : response.result_set.results[0] });
                if (callback)
                    callback();
                else if (me.OnDataBound)
                    me.OnDataBound(me);
            },
            OnError: function (state, error) {
                me.OnError(state, error);
            }
        });
    };
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
};