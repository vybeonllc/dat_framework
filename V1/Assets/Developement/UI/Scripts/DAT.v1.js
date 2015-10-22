var Dat = {
    V1: {
        Config: {
            Token: function () {
                return Dat.V1.Utils.Cookies.Get("token");
            },
            SetToken: function (token) {
                return Dat.V1.Utils.Cookies.Set("token", token);
            },
            OnError: function () {
                alert("Some thing went wrong.");
                Dat.V1.Config.SetToken("");
                //window.location = document.location.origin + "/index.html";
            },
            ApiUrl: "localhost:1883",
            Version: "v1",
            Asset: "dat",
            RequestContentType: "json",
            RequestAccepts: "json",
            FileChunkSize: ""
        },
        Utils: {
        },
        Services: {
        },
    }
}