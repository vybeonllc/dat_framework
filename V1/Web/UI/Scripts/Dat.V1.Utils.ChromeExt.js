Dat.V1.Utils.ChromeExt = {
    LocalStorage: {
        Set: function (name, value) {
            localStorage[name] = value;
        },
        Get: function (name) {
            return localStorage[name];
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
    CreateWindow: function (url, additional_data, callback) {
        additional_data = additional_data == null ? {} : additional_data;
        additional_data.url = url;
        chrome.windows.create(additional_data, callback);
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
    },
    Proxy: {
        Utils: {
            SetProxy: function (arg, success, failed) {
                try {
                    var rules = new Array();
                    rules.push(new Dat.V1.Utils.ChromeExt.Proxy.ProxyRule(
                        Dat.V1.Utils.ChromeExt.Proxy.ProxyRuleTypes.SingleProxy,
                        new Dat.V1.Utils.ChromeExt.Proxy.ProxyServer(arg.ProxySchema, arg.ProxyAddress, proxy.ProxyPort))
                    );
                    var configuration = new Dat.V1.Utils.ChromeExt.Proxy.Configuration(Dat.V1.Utils.ChromeExt.Proxy.ProxyModes.FixedServers,
                        rules,
                        arg.BypassList); //Bypass list array new Array("localhost")g
                    Dat.V1.Utils.ChromeExt.Proxy.SetProxy(configuration,
                        Dat.V1.Utils.ChromeExt.Proxy.ProxyScope.IncognitoPersistent,
                        function () {
                            success(arg);
                        });
                }
                catch (e) {
                    failed(e);
                }
            },
            RemoveProxy: function (arg, success, failed) {
                try {
                    var configuration = new Dat.V1.Utils.ChromeExt.Proxy.Configuration(Dat.V1.Utils.ChromeExt.Proxy.ProxyModes.System,
                        null,
                        new Array()); //Bypass list
                    Dat.V1.Utils.ChromeExt.Proxy.SetProxy(configuration,
                        Dat.V1.Utils.ChromeExt.Proxy.ProxyScope.IncognitoPersistent,
                        function () { success(arg); });
                }
                catch (e) {
                    failed(e);
                }
            }
        },
        ProxyModes: { //A ProxyConfig object's mode attribute determines the overall behavior of Chrome with regards to proxy usage. It can take the following values:
            Direct: "direct", // Never use a proxy.     In direct mode all connections are created directly, without any proxy involved. This mode allows no further parameters in the ProxyConfig object.
            AutoDetect: "auto_detect", // Auto detect proxy settings.        In auto_detect mode the proxy configuration is determined by a PAC script that can be downloaded at http://wpad/wpad.dat. This mode allows no further parameters in the ProxyConfig object.
            PAC: "pac_script", // Use specified PAC script.          In pac_script mode the proxy configuration is determined by a PAC script that is either retrieved from the URL specified in the PacScript object or taken literally from the data element specified in the PacScript object. Besides this, this mode allows no further parameters in the ProxyConfig object.
            FixedServers: "fixed_servers", //Manually specify proxy servers.            In fixed_servers mode the proxy configuration is codified in a ProxyRules object. Its structure is described in Proxy rules. Besides this, the fixed_servers mode allows no further parameters in the ProxyConfig object.
            System: "system" //Use system proxy settings.           In system mode the proxy configuration is taken from the operating system. This mode allows no further parameters in the ProxyConfig object. Note that the system mode is different from setting no proxy configuration. In the latter case, Chrome falls back to the system settings only if no command-line options influence the proxy configuration.
        },
        ProxyScope: {
            Regular: "regular",
            RegularOnly: "regular_only",
            IncognitoPersistent: "incognito_persistent",
            IncognitoSessionOnly: "incognito_session_only"
        },
        ProxyRuleTypes: {
            SingleProxy: "singleProxy", //The proxy server to be used for all per-URL requests (that is http, https, and ftp).
            HttpProxy: "proxyForHttp", //The proxy server to be used for HTTP requests.
            HttpsProxy: "proxyForHttps", //The proxy server to be used for HTTPS requests.
            FtpProxy: "proxyForFtp", //The proxy server to be used for FTP requests.
            FallbackProxy: "fallbackProxy" //The proxy server to be used for everthing else or if any of the specific proxyFor... is not specified.
        },
        ProxySchemes: {
            HTTP: {
                Name: "http",
                Port: 80
            },
            HTTPS: {
                Name: "https",
                Port: 443
            },
            SOCKS4: {
                Name: "socks4",
                Port: 1080
            },
            SOCKS5: {
                Name: "socks5",
                Port: 1080
            }
        },
        ProxyServer: function (scheme, host, port) {
            var me = this;
            this.Scheme = scheme ? scheme : Dat.V1.Utils.ChromeExt.Proxy.ProxySchemes.HTTP.Name;
            this.Host = host;
            if (port)
                this.Port = port;
            else {
                switch (me.Scheme) {
                    case Dat.V1.Utils.ChromeExt.Proxy.ProxySchemes.HTTPS.Name:
                        this.Port = Dat.V1.Utils.ChromeExt.Proxy.ProxySchemes.HTTPS.Port;
                        break;
                    case Dat.V1.Utils.ChromeExt.Proxy.ProxySchemes.SOCKS4.Name:
                        this.Port = Dat.V1.Utils.ChromeExt.Proxy.ProxySchemes.SOCKS4.Port;
                        break;
                    case Dat.V1.Utils.ChromeExt.Proxy.ProxySchemes.SOCKS5.Name:
                        this.Port = Dat.V1.Utils.ChromeExt.Proxy.ProxySchemes.SOCKS5.Port;
                        break;
                    case Dat.V1.Utils.ChromeExt.Proxy.ProxySchemes.HTTP.Name:
                    default:
                        this.Port = Dat.V1.Utils.ChromeExt.Proxy.ProxySchemes.HTTP.Port;
                        break;
                }
            }
        },

        ProxyRule: function (proxyRuleType, proxyServer) {
            this.ProxyRuleType = proxyRuleType;
            this.ProxyServer = proxyServer;
        },
        Configuration: function (proxyMode, rules, bypassList) {
            this.Rules = rules;
            this.BypassList = bypassList ? bypassList : new Array();
            this.ProxyMode = proxyMode ? proxyMode : Dat.V1.Utils.ChromeExt.Proxy.ProxyModes.SingleProxy;
        },
        SetProxy: function (configuration, scope, callback) {
            var config = new Object();
            config.rules = new Object();
            config.mode = configuration.ProxyMode;
            config.rules.bypassList = configuration.BypassList;
            if (configuration.ProxyMode == Dat.V1.Utils.ChromeExt.Proxy.ProxyModes.FixedServers)
                for (var i = 0; i < configuration.Rules.length; i++) {
                    var rule = configuration.Rules[i];
                    switch (rule.ProxyRuleType) {
                        case Dat.V1.Utils.ChromeExt.Proxy.ProxyRuleTypes.SingleProxy:
                            config.rules.singleProxy = { scheme: rule.ProxyServer.Scheme, host: rule.ProxyServer.Host, port: rule.ProxyServer.Port };
                            break;
                        case Dat.V1.Utils.ChromeExt.Proxy.ProxyRuleTypes.HttpProxy:
                            config.rules.proxyForHttp = { scheme: rule.ProxyServer.Scheme, host: rule.ProxyServer.Host, port: rule.ProxyServer.Port };
                            break;
                        case Dat.V1.Utils.ChromeExt.Proxy.ProxyRuleTypes.HttpsProxy:
                            config.rules.proxyForHttps = { scheme: rule.ProxyServer.Scheme, host: rule.ProxyServer.Host, port: rule.ProxyServer.Port };
                            break;
                        case Dat.V1.Utils.ChromeExt.Proxy.ProxyRuleTypes.FtpProxy:
                            config.rules.proxyForFtp = { scheme: rule.ProxyServer.Scheme, host: rule.ProxyServer.Host, port: rule.ProxyServer.Port };
                            break;
                        case Dat.V1.Utils.ChromeExt.Proxy.ProxyRuleTypes.FallbackProxy:
                            config.rules.fallbackProxy = { scheme: rule.ProxyServer.Scheme, host: rule.ProxyServer.Host, port: rule.ProxyServer.Port };
                            break;
                    };
                }
            chrome.proxy.settings.set({
                value: config,
                scope: scope
            }, callback);
        }
    },
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
};