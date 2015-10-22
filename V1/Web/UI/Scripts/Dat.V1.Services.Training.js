Dat.V1.Services.Training = {
    Asset: "training",
    Base: {
        Service: "base",
        Course: {
            EndPoint: "course",
            Insert: function (options) {
                options.Service = Dat.V1.Services.Training.Base.Service;
                options.EndPoint = Dat.V1.Services.Training.Base.Course.EndPoint;
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Load: function (options) {
                options.Parameters = new Array();
                options.Parameters.push(options.Source);
                options.Service = Dat.V1.Services.Training.Base.Service;
                options.EndPoint = Dat.V1.Services.Training.Base.Course.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            }
        },
        CourseSchedule: {
            EndPoint: "course_schedule",
            Insert: function (options) {
                options.Service = Dat.V1.Services.Training.Base.Service;
                options.EndPoint = Dat.V1.Services.Training.Base.Course.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Load: function (options) {
                options.Parameters = new Array();
                options.Parameters.push(options.UserType);
                options.Service = Dat.V1.Services.Training.Base.Service;
                options.EndPoint = Dat.V1.Services.Training.Base.CourseSchedule.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            }
        },
        Trainee: {
            EndPoint: "trainee",
            Insert: function (options) {
                options.Service = Dat.V1.Services.Training.Base.Service;
                options.EndPoint = Dat.V1.Services.Training.Base.Trainee.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Load: function (options) {
                options.Parameters = new Array();
                options.Parameters.push(options.UserGuid);
                options.Service = Dat.V1.Services.Training.Base.Service;
                options.EndPoint = Dat.V1.Services.Training.Base.Trainee.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            }
        },
        TraineeCourse: {
            EndPoint: "trainee_course",
            Insert: function (options) {
                options.Service = Dat.V1.Services.Training.Base.Service;
                options.EndPoint = Dat.V1.Services.Training.Base.TraineeCourse.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            ApplyForNewClasses: function (options) {
                options.Service = Dat.V1.Services.Training.Base.Service;
                options.EndPoint = Dat.V1.Services.Training.Base.TraineeCourse.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.PUT;
                options.Data = { manifest: options.Data };
                return Dat.V1.Services.GetResponse(options);
            },
            Load: function (options) {
                options.Parameters = new Array();
                options.Parameters.push(options.UserGuid);
                options.Service = Dat.V1.Services.Training.Base.Service;
                options.EndPoint = Dat.V1.Services.Training.Base.TraineeCourse.EndPoint;
                options.AuthenticationToken = Dat.V1.Config.GetAuthToken();
                options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.GET;
                return Dat.V1.Services.GetResponse(options);
            }
        }
    },
};