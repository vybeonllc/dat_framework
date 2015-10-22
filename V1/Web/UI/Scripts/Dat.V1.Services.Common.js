Dat.V1.Services.Common = {
    Asset: "common",
    Resource: {
        Service: "resource",
        Template: function (options) {
            options.Asset = Dat.V1.Services.Common.Asset;
            options.Service = Dat.V1.Services.Common.Resource.Service;
            options.EndPoint = "template";
            options.Parameters = new Array();
            options.Parameters.push(options.TemplateName);
            Dat.V1.Utils.Ajax.Call({
                Url: "http" + (Dat.V1.Config.Secured ? "s" : "") + "://" + Dat.V1.Config.ApiUrl + "/" + options.Asset + "/" + Dat.V1.Config.Version + "/" + options.Service + "/" + options.EndPoint + "/" + options.TemplateName,
                OnSuccess: function (response) {
                    if (options.OnSuccess)
                        options.OnSuccess(response);
                },
                OnError: function (e) {
                    if (options.OnError)
                        options.OnError(e);
                }
            });
        },
        Content: function (options) {
            options.Asset = Dat.V1.Services.Common.Asset;
            options.Service = Dat.V1.Services.Common.Resource.Service;
            options.EndPoint = "content";
            options.Parameters = new Array();
            options.Parameters.push(options.Content);
            options.AuthenticationToken = Dat.V1.Config.Token();
            return Dat.V1.Services.Send(options);
        }
    },
    Name: function (options) {
        options.EndPoint = "name";
        return Dat.V1.Services.Suggestion.Suggest(options);
    },
    Keyword: function (options) {
        options.EndPoint = "keyword";
        return Dat.V1.Services.Suggestion.Suggest(options);
    },
    Synonym: function (options) {
        options.EndPoint = "synonym";
        return Dat.V1.Services.Suggestion.Suggest(options);
    },
    Address: function (options) {
        options.EndPoint = "address";
        return Dat.V1.Services.Suggestion.Suggest(options);
    },
    PhoneNumberInfo: function (options) {
        options.EndPoint = "phone_number_info";
        return Dat.V1.Services.Suggestion.Suggest(options);
    },
    Location: {
        Service: "location",
        PostalCode: function (options) {
            options.Asset = Dat.V1.Services.Standards.Asset;
            options.EndPoint = "postalcode";
            options.Service = Dat.V1.Services.Standards.Location.Service;
            return Dat.V1.Services.Suggestion.Suggest(options);
        }
    }
};