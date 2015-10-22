Dat.V1.Services = {
    Report: function (options) {
        if (!options.Service)
            options.Service = "report";
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
        options.QueryStrings = new Array({ Key: "startindex", Value: options.StartIndex ? options.StartIndex : 0 }, { Key: "pagesize", Value: options.PageSize ? options.PageSize : 50 }, { Key: "filters", Value: strFilters });
        return Dat.V1.Services.Send(options);
    },
    Suggestion: {
        Service: "suggestion",
        Suggest: function (options) {
            options.Service = options.Service ? options.Service : Dat.V1.Services.Suggestion.Service;
            options.Parameters = new Array();
            options.Parameters.push(encodeURIComponent(options.Idea));
            options.QueryStrings = new Array({ Key: "max_suggestions", Value: options.MaxSuggestions ? options.MaxSuggestions : 10 });
            options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
            return Dat.V1.Services.Send(options);
        },
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