Dat.V1.Services = {
    SetPaging: function (options) {
        if (options.QueryStrings) {
            options.QueryStrings.push({ Key: "startindex", Value: options.StartIndex ? options.StartIndex : 0 });
            options.QueryStrings.push({ Key: "pagesize", Value: options.PageSize ? options.PageSize : 50 });
        }
        else
            options.QueryStrings = new Array({ Key: "startindex", Value: options.StartIndex ? options.StartIndex : 0 }, { Key: "pagesize", Value: options.PageSize ? options.PageSize : 50 });
    },
    SetFilters: function (options) {
        var strFilters = "";
        if (options.Filters)
            for (var i = 0; i < options.Filters.length; i++) {
                var filteredColumn = options.Filters[i];
                strFilters += (strFilters == "" ? "" : ";") + "column_name" + i + "=" + filteredColumn.name;
                for (var j = 0; j < filteredColumn.filters.length; j++) {
                    var filter = filteredColumn.filters[j];
                    strFilters += ";" + filter.type + i + "=" + filter.value;
                }
            }
        strFilters = strFilters.trim(';');
        if (strFilters == "")
            return;
        if (options.QueryStrings)
            options.QueryStrings.push({ Key: "filters", Value: strFilters });
        else
            options.QueryStrings = new Array({ Key: "filters", Value: strFilters });
    },
    GetResponse: function (options) {
        Dat.V1.Services.SetPaging(options);
        Dat.V1.Services.SetFilters(options);
        return Dat.V1.Services.Send(options);
    },
    Send: function (options) {
        options.Url = "http" + (options.Secured ? "s" : "") + "://"
            + Dat.V1.Config.ApiUrl
            + "/" + (options.Asset ? options.Asset : Dat.V1.Config.Asset)
            + "/" + Dat.V1.Config.Version
            + "/" + (options.Service ? options.Service : Dat.V1.Config.Service)
            + "/" + options.EndPoint;
        options.ContentType = Dat.V1.Config.RequestContentType;
        options.Accepts = Dat.V1.Config.RequestAccepts;

        return Dat.V1.Utils.Ajax.Call(options);
    },

};