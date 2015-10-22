Dat.V1.AssetPool = {
    Manager: {
        Call: function (asset, target, options) {
            var func = eval("Dat.V1.AssetPool.Assets." + asset + ".Post");
            func({ Target: target, Options: options });
        }
    },
    Assets: {
    },
    Count: 0,
    Total: 0
};