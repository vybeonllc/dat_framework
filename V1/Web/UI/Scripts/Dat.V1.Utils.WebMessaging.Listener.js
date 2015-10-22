if (!Dat.V1.Utils.WebMessaging)
    Dat.V1.Utils.WebMessaging = {};
Dat.V1.Utils.WebMessaging.Listener = function () {
    var me = this;
    var port = null;
    var postback = function (origin, message) {
        parent.postMessage(JSON.stringify(message), origin);
    };
    window.addEventListener("message", function (evt) {
        console.log(evt);
        var message = JSON.parse(evt.data);
        try {
            if (message.Status == "HandShake") {
                postback(evt.origin, { Asset: message.Asset, Status: "Initialized" });
                return;
            }
            message.Status = "Processing";
            message.Options.OnSuccess = function (response) {
                message.Status = "Succeed";
                message.Result = response;
                postback(evt.origin, message);
            };
            message.Options.OnRedirect = function (response) {
                message.Status = "Redirect";
                message.Result = response;
                postback(evt.origin, message);
            };
            message.Options.OnError = function (state, error) {
                message.Status = "Failed";
                message.Result = { State: state, Error: error };
                postback(evt.origin, message);
            };
            eval(message.Target + "(message.Options)");
        }
        catch (e) {
            message.Status = "Failed";
            message.Result = e;
            postback(evt.origin, message);
        }
    }, false);
};
