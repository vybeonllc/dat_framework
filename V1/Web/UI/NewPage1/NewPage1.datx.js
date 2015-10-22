Dat.V1.UI.Prospectfuel = {
    Step1: {
        ListView1: {
            EventHandlers: {
                OnInit: function (lv) {
                    lv.Container.append(lv.Element);
                },
                OnReady: function (lv) {
                    lv.DataBind();
                }
            }
        },
        FormView1: {
            EventHandlers: {
                OnInit: function (fv) {
                    fv.Container.append(fv.Element);
                },
                OnReady: function (fv) {
                    fv.DataBind();
                }
            }
        }
    }
};