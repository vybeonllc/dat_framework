Dat.V1.Services.Standards = {
    Asset: "standards",
    GeoLocation: {
        Service: "geo_location",
        PostalCode: {
            EndPoint: "postal_code",
            Get: function (options) {
                options.Asset = Dat.V1.Services.Standards.Asset;
                options.Service = Dat.V1.Services.Standards.GeoLocation.Service;
                options.EndPoint = Dat.V1.Services.Standards.GeoLocation.PostalCode.EndPoint;
                return Dat.V1.Services.GetResponse(options);
            },
            Put: function (options) {
                options.Asset = Dat.V1.Services.Standards.Asset;
                options.Service = Dat.V1.Services.Standards.GeoLocation.Service;
                options.EndPoint = Dat.V1.Services.Standards.GeoLocation.PostalCode.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Post: function (options) {
                options.Asset = Dat.V1.Services.Standards.Asset;
                options.Service = Dat.V1.Services.Standards.GeoLocation.Service;
                options.EndPoint = Dat.V1.Services.Standards.GeoLocation.PostalCode.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.POST;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            }
        }
    },
};