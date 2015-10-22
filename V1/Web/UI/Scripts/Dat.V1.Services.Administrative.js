Dat.V1.Services.Administrative = {
    Tracking: {
		Service:"tracking",
		Request: {
			EndPoint: "request",
			Get: function (options) {
			    options.Service = Dat.V1.Services.Administrative.Tracking.Service;
			    options.EndPoint = Dat.V1.Services.Administrative.Tracking.Request.EndPoint;
				return Dat.V1.Services.Report(options);
			},
		}
	}

};