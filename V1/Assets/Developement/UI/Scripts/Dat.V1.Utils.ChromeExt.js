Dat.V1.Utils.ChromeExt = {
    Connections: {
        Callback: function (action, onSuccess, onFailed) {
            this.Action = action;
            this.onSuccess = onSuccess;
            this.onFailed = onFailed;
        },
        Message: function (action, arg, status) {
            this.Action = action;
            this.Argument = arg;
            this.Status = status;
        },
        Open: function (name, callBacks) {
            var port = chrome.extension.connect({ name: name });
            port.onMessage.addListener(function (msg) {
                for (var i = 0; i < callBacks.length; i++) {
                    var callBack = callBacks[i];
                    if (callBack.Action != msg.Action)
                        continue;
                    if (msg.Status == "failed")
                        callBack.onFailed(msg);
                    else
                        callBack.onSuccess(msg);
                    return;
                }
                throw ("command not found");
            });
            return port;
        },
        PostMessage: function (port, message) {
            port.postMessage(message);
        }
    },
    CreateTab: function (index, windowId, url, selected, callBack) {
        chrome.tabs.create({
            'index': index,
            'windowId': windowId,
            'url': url,
            'selected': selected
        }, callBack ? callBack : function () { });
    },
    ClearCookies: function (since) {
        chrome.browsingData.remove({
            "since": since
        }, {
            "appcache": true,
            "cache": true,
            "cookies": true,
            "downloads": true,
            "fileSystems": true,
            "formData": true,
            "history": true,
            "indexedDB": true,
            "localStorage": true,
            "serverBoundCertificates": true,
            "pluginData": true,
            "passwords": true,
            "webSQL": true
        });
    }
},
};