Dat.V1.Services.Scraper = {
	Asset: "scraper",
	GoogleAdwords: {
		Service:"google_adwords",
		Queue: {
			EndPoint: "queue",
			Get: function (options) {
				options.Asset = Dat.V1.Services.Scraper.Asset;
				options.Service = Dat.V1.Services.Scraper.GoogleAdwords.Service;
				options.EndPoint = Dat.V1.Services.Scraper.GoogleAdwords.Queue.EndPoint;
				return Dat.V1.Services.Report(options);
			},
			Update: function (options) {
				options.Asset = Dat.V1.Services.Scraper.Asset;
				options.Service = Dat.V1.Services.Scraper.GoogleAdwords.Service;
				options.EndPoint = Dat.V1.Services.Scraper.GoogleAdwords.Queue.EndPoint;
				options.Method = Dat.V1.Utils.Enumerations.HttpVerbs.POST;
				return Dat.V1.Services.Report(options);
			}
		}
	}

};