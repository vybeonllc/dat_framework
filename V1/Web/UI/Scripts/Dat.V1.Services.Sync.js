Dat.V1.Services.Sync = {
    Asset: "sync",
    Information: {
        Service: "information",
        Listing: {
            EndPoint: "listing",
            Add: function (options) {
                options.Service = Dat.V1.Services.Sync.Information.Service;
                options.EndPoint = Dat.V1.Services.Sync.Information.Listing.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.Data = { manifest: options.data };
                return Dat.V1.Services.GetResponse(options);
            }
        }
    },
    Scraping: {
        Service: "scraping",
        Queue: {
            EndPoint: "queue",
            Dequeue: function (options) {
                options.Service = Dat.V1.Services.Sync.Scraping.Service;
                options.EndPoint = Dat.V1.Services.Sync.Scraping.Queue.EndPoint;
                options.QueryStrings = [{ Key: "ServerId", Value: options.ServerId }];
                return Dat.V1.Services.GetResponse(options);
            },
            ByQueueId: function (options) {
                options.Service = Dat.V1.Services.Sync.Scraping.Service;
                options.EndPoint = Dat.V1.Services.Sync.Scraping.Queue.EndPoint;
                options.Parameters = [options.QueueId];
                return Dat.V1.Services.GetResponse(options);
            },
            Update: function (options) {
                options.Service = Dat.V1.Services.Sync.Scraping.Service;
                options.EndPoint = Dat.V1.Services.Sync.Scraping.Queue.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.POST;
                options.Data = { manifest: options.data };
                return Dat.V1.Services.GetResponse(options);
            },
            ByRequestGuid: function (options) {
                options.Service = Dat.V1.Services.Sync.Scraping.Service;
                options.EndPoint = Dat.V1.Services.Sync.Scraping.Queue.EndPoint;
                options.Parameters = [options.RequestGuid];
                return Dat.V1.Services.GetResponse(options);
            }
        }
    }
    //Database: {
    //    Service: "database",
    //    Database: function (options) {
    //        options.Asset = Dat.V1.Services.Developement.Asset;
    //        options.Service = Dat.V1.Services.Developement.Database.Service;
    //        options.EndPoint = "database";
    //        return Dat.V1.Services.GetResponse(options);
    //    },
    //    Table: function (options) {
    //        options.Asset = Dat.V1.Services.Developement.Asset;
    //        options.Service = Dat.V1.Services.Developement.Database.Service;
    //        options.EndPoint = "table";
    //        return Dat.V1.Services.GetResponse(options);
    //    }
    //},
    //Name: function (options) {
    //    options.EndPoint = "name";
    //    return Dat.V1.Services.Suggestion.Suggest(options);
    //},
    //Keyword: function (options) {
    //    options.EndPoint = "keyword";
    //    return Dat.V1.Services.Suggestion.Suggest(options);
    //},
    //Synonym: function (options) {
    //    options.EndPoint = "synonym";
    //    return Dat.V1.Services.Suggestion.Suggest(options);
    //},
    //Address: function (options) {
    //    options.EndPoint = "address";
    //    return Dat.V1.Services.Suggestion.Suggest(options);
    //},
    //PhoneNumberInfo: function (options) {
    //    options.EndPoint = "phone_number_info";
    //    return Dat.V1.Services.Suggestion.Suggest(options);
    //},
    //Location: {
    //    Service: "location",
    //    PostalCode: function (options) {
    //        options.Asset = Dat.V1.Services.Standards.Asset;
    //        options.EndPoint = "postalcode";
    //        options.Service = Dat.V1.Services.Standards.Location.Service;
    //        return Dat.V1.Services.Suggestion.Suggest(options);
    //    }
    //}
};