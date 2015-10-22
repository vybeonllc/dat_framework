Dat.V1.Utils.WebMessaging = {
    Messages: {},
    Messenger: function (options) {
        try {
            var me = this;
            var AssetUrl = options.AssetUrl ? options.AssetUrl : "dat.29prime.com";
            this.Frame = null;
            this.Asset = options.Asset;
            this.OnError = options.OnError;
            this.OnReady = options.OnReady;
            this.Post = function (post_options) {
                var message_id = Dat.V1.Utils.Element.GetUniqueID("dat");
                while (eval("Dat.V1.Utils.WebMessaging.Messages." + message_id)) message = Dat.V1.Utils.Element.GetUniqueID("dat");
                var message = eval("Dat.V1.Utils.WebMessaging.Messages." + message_id + " = {};");
                message.Asset = me.Asset;
                message.MessageId = message_id;
                message.Status = "Sending";
                message.Target = post_options.Target;
                message.Options = post_options.Options;
                me.Frame[0].contentWindow.postMessage(JSON.stringify(message), location.protocol + "//" + AssetUrl);
                message.Status = "Sent";
            };
            me.Frame = jQuery("<iframe>");
            window.addEventListener("message", function (evt) {
                var message_data = JSON.parse(evt.data);
                if (message_data.Asset != me.Asset)
                    return;
                if (message_data.Status == "Initialized") {
                    me.OnReady();
                    return;
                }
                var message_id = message_data.MessageId,
                    message = eval("Dat.V1.Utils.WebMessaging.Messages." + message_id),
                    onsuccess = message.Options.OnSuccess,
                    onerror = message.Options.OnError,
                    onredirect = message.Options.OnRedirect;
                message = message_data;
                message.Options.OnSuccess = onsuccess;
                message.Options.OnError = onerror;
                message.Options.OnRedirect = onredirect;
                if (message.Status == "Succeed")
                    message.Options.OnSuccess(message.Result);
                else if (message.Status == "Redirect") {
                    var canRedirect = location.href.indexOf(message.Result.status.destination) == -1;
                    var arg = { Result: message.Result, CanRedirect: canRedirect };
                    if (message.Options.OnRedirect)
                        message.Options.OnRedirect(arg);
                    else if (canRedirect && message.Result.status.message)
                        alert("You are about to leave this website because " + message.Result.status.message);
                    canRedirect = arg.CanRedirect;
                    if (!canRedirect)
                        return;
                  
                    if (message.Result.status.destination)
                        window.location = message.Result.status.destination;
                }
                else
                    message.Options.OnError(message.Result);
            }, false);
            var frame_loaded = null;
            me.Frame.attr("src", location.protocol + "//" + AssetUrl + (me.Asset.toLowerCase() == "dat" ? "" : "/" + me.Asset.toLowerCase()) + "/api.html");
            me.Frame.hide();
            frame_loaded = function (callback) {
                if (me.Frame[0].contentWindow.document.readyState === "complete" && me.Frame[0].contentWindow) {
                    me.Frame.load(function () { callback(); });
                    return;
                }
                window.setTimeout(function () {
                    frame_loaded();
                }, 100);
            };
            me.Frame.appendTo(jQuery("body"));
            frame_loaded(function () {
                me.Frame[0].contentWindow.postMessage(JSON.stringify({ Asset: me.Asset, Status: "HandShake" }), location.protocol + "//" + AssetUrl);
            });

        }
        catch (e) {
            me.OnError(e);
        }
    }
};