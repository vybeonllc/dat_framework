DAT.v1.Services.Standards = {
    Asset: "standards",
    Name: function (options) {
        options.EndPoint = "name";
        return DAT.v1.Services.Suggestion.Suggest(options);
    },
    Keyword: function (options) {
        options.EndPoint = "keyword";
        return DAT.v1.Services.Suggestion.Suggest(options);
    },
    Synonym: function (options) {
        options.EndPoint = "synonym";
        return DAT.v1.Services.Suggestion.Suggest(options);
    },
    Address: function (options) {
        options.EndPoint = "address";
        return DAT.v1.Services.Suggestion.Suggest(options);
    },
    PhoneNumberInfo: function (options) {
        options.EndPoint = "phone_number_info";
        return DAT.v1.Services.Suggestion.Suggest(options);
    },
    Location: {
        Service: "location",
        PostalCode: function (options) {
            options.Asset = DAT.v1.Services.Standards.Asset;
            options.EndPoint = "postalcode";
            options.Service = DAT.v1.Services.Standards.Location.Service;
            return DAT.v1.Services.Suggestion.Suggest(options);
        }
    }
};