Dat.V1.Services.Dat = {
    Asset: "dat",
    Membership: {
        Service: "membership",
        SignOut: function (options) {
            Dat.V1.Config.SetAuthToken(null);
            if (options.OnSuccess)
                options.OnSuccess();
        },
        Authenticate: {
            EndPoint: "authenticate",
            Authenticate: function (options) {
                if (!options.Data) options.Data = { authentication_info: { token: Dat.V1.Config.GetAuthToken() } };
                options.Service = Dat.V1.Services.Dat.Membership.Service;
                var onSuccess = options.OnSuccess;
                options.OnSuccess = function (response) {
                    Dat.V1.Config.SetAuthToken(response.result.token);
                    if (onSuccess(response))
                        onSuccess();
                }
                options.EndPoint = Dat.V1.Services.Dat.Membership.Authenticate.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
        },
        User: {
            EndPoint: "user",
            Select: function (options) {
                options.Service = Dat.V1.Services.Dat.Membership.Service;
                options.EndPoint = Dat.V1.Services.Dat.Membership.User.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                options.Parameters = new Array();
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Parameters.push(options.EmailAddress);
                return Dat.V1.Services.GetResponse(options);
            },
            SelectByGuid: function (options) {
                options.Service = Dat.V1.Services.Dat.Membership.Service;
                options.EndPoint = Dat.V1.Services.Dat.Membership.User.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                return Dat.V1.Services.GetResponse(options);
            },
            Create: function (options) {
                options.Service = Dat.V1.Services.Dat.Membership.Service;
                options.EndPoint = Dat.V1.Services.Dat.Membership.User.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Update: function (options) {
                options.Service = Dat.V1.Services.Dat.Membership.Service;
                options.EndPoint = Dat.V1.Services.Dat.Membership.User.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.POST;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            }
        }
    },
    Resource: {
        Service: "resource",
        Template: function (options) {
            options.Asset = Dat.V1.Services.Dat.Asset;
            options.Service = Dat.V1.Services.Dat.Resource.Service;
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
            options.Asset = Dat.V1.Services.Dat.Asset;
            options.Service = Dat.V1.Services.Dat.Resource.Service;
            options.EndPoint = "content";
            options.Parameters = new Array();
            options.Parameters.push(options.Content);
            options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
            return Dat.V1.Services.Send(options);
        }
    }
};