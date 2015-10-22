Dat.V1.Services.Leads = {
    Asset: "leads",
    Leads: {
        Service: "leads",
        Organization: {
            EndPoint: "organization",
            Select: function (options) {
                options.Service = Dat.V1.Services.Leads.Leads.Service;
                options.EndPoint = Dat.V1.Services.Leads.Leads.Organization.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            },
        }
    },
    Reports: {
        Service: "reports",
        PageAccess: {
            EndPoint: "page_access",
            Load: function (options) {
                options.Service = Dat.V1.Services.Leads.Reports.Service;
                options.EndPoint = Dat.V1.Services.Leads.Reports.PageAccess.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            }
        },
        FormCompletion: {
            EndPoint: "form_completion",
            Run: function (options) {
                options.Service = Dat.V1.Services.Leads.Reports.Service;
                options.EndPoint = Dat.V1.Services.Leads.Reports.FormCompletion.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            }
        }
    },
    Membership: {
        Service: "membership",
        Authorization: {
            EndPoint: "authorize",
            Authorize: function (options) {
                options.Service = Dat.V1.Services.Leads.Membership.Service;
                options.EndPoint = Dat.V1.Services.Leads.Membership.Authorize.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                if (options.UserGuid) {
                    options.Parameters = new Array();
                    options.Parameters.push(options.User_Guid);
                }
                return Dat.V1.Services.GetResponse(options);
            },
        },
        PhoneServer: {
            EndPoint: "phone_server",
            Select: function (options) {
                options.Service = Dat.V1.Services.Leads.Membership.Service;
                options.EndPoint = Dat.V1.Services.Leads.Membership.PhoneServer.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                return Dat.V1.Services.GetResponse(options);
            }
        },
        SipSettings: {
            EndPoint: "sip_settings",
            Insert: function (options) {
                options.Service = Dat.V1.Services.Leads.Membership.Service;
                options.EndPoint = Dat.V1.Services.Leads.Membership.SipSettings.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.Data = { manifest: options.Data };
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                return Dat.V1.Services.GetResponse(options);
            }
        },
        User: {
            EndPoint: "user",
            Select: function (options) {
                options.Service = Dat.V1.Services.Leads.Membership.Service;
                options.EndPoint = Dat.V1.Services.Leads.Membership.User.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                if (options.User_Guid) {
                    options.Parameters = new Array();
                    options.Parameters.push(options.User_Guid);
                }
                return Dat.V1.Services.GetResponse(options);
            },
            Delete: function (options) {
                options.Service = Dat.V1.Services.Leads.Membership.Service;
                options.EndPoint = Dat.V1.Services.Leads.Membership.User.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.DELETE;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                if (options.User_Guid) {
                    options.Parameters = new Array();
                    options.Parameters.push(options.User_Guid);
                }
                return Dat.V1.Services.GetResponse(options);
            },
            SelectTree: function (options) {
                options.Service = Dat.V1.Services.Leads.Membership.Service;
                options.EndPoint = Dat.V1.Services.Leads.Membership.User.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.QueryStrings = new Array();
                options.QueryStrings.push({ Key: "UserTree", Value: "yes" });
                if (options.User_Guid) {
                    options.Parameters = new Array();
                    options.Parameters.push(options.User_Guid);
                }
                return Dat.V1.Services.GetResponse(options);
            },
            Create: function (options) {
                options.Service = Dat.V1.Services.Leads.Membership.Service;
                options.EndPoint = Dat.V1.Services.Leads.Membership.User.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Update: function (options) {
                options.Service = Dat.V1.Services.Leads.Membership.Service;
                options.EndPoint = Dat.V1.Services.Leads.Membership.User.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.POST;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            }
        }
    },
    Questionnaire: {
        Service: "questionnaire",
        Step1: {
            EndPoint: "step1",
            Create: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step1.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Update: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step1.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.POST;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Select: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step1.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            }
        },
        Step2: {
            EndPoint: "step2",
            Create: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step2.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Update: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step2.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.POST;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Select: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step2.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            }
        },
        Step3: {
            EndPoint: "step3",
            Create: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step3.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Update: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step3.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.POST;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Select: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step3.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            }
        },
        Step4: {
            EndPoint: "step4",
            Create: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step4.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Update: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step4.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.POST;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Select: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step4.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            }
        },
        Step5: {
            EndPoint: "step5",
            Select: function (options) {
                options.Service = Dat.V1.Services.Leads.Questionnaire.Service;
                options.EndPoint = Dat.V1.Services.Leads.Questionnaire.Step5.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            }
        }
    }
};