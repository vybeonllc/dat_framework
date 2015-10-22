Dat.V1.Services.Developement = {
    Asset: "developement",
    Database: {
        Service: "database",
        Database: function (options) {
            options.Asset = Dat.V1.Services.Developement.Asset;
            options.Service = Dat.V1.Services.Developement.Database.Service;
            options.EndPoint = "database";
            return Dat.V1.Services.Report(options);
        },
        Table: function (options) {
            options.Asset = Dat.V1.Services.Developement.Asset;
            options.Service = Dat.V1.Services.Developement.Database.Service;
            options.EndPoint = "table";
            return Dat.V1.Services.Report(options);
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