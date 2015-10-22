var Dat = {
    V1: {
        Config: {
            GetAuthToken: function () {
                return Dat.V1.Utils.Cookies.Get("DATXAUTH");
            },
            SetAuthToken: function (token) {
                return Dat.V1.Utils.Cookies.Set("DATXAUTH", token);
            },
            OnError: function () {
                alert("Some thing went wrong.");
                Dat.V1.Config.SetToken("");
                //window.location = document.location.origin + "/index.html";
            },
            ApiUrl: $(location).attr('host'),
            Version: "v1",
            Asset: "dat",
            Secured: false,
            RequestContentType: "json",
            RequestAccepts: "json",
            FileChunkSize: ""
        },
        Utils: {
        },
        Services: {
        },
        UI: {
        }
    }
};