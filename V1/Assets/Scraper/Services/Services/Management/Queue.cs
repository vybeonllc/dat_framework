using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.Assets.Scraper.V1.Services.Management
{
    public class Queue : Dat.V1.Framework.HttpHandlers.Master<Dat.Assets.Scraper.V1.Dto.Management.Queue.Request, Dat.Assets.Scraper.V1.Dto.Management.Queue.QueueInfo>
    {
        string cn = "Data Source=172.16.0.243;Initial Catalog=Scraping;User ID=sa;Password=1562Closed#;Max Pool Size=200;Timeout=240;Packet Size=7999";
        public override void GET(long Parameter)
        {
            base.GET(Parameter);
            List<Dat.Assets.Scraper.V1.BusinessLogic.Queue> db_queues = null;
           
            db_queues = BusinessLogic.Queue.ByServerId((int)Parameter, cn).ToList();

            if (db_queues == null)
            {
                SetResponse(System.Net.HttpStatusCode.NotFound);
                return;
            }
            SetResponseAsCollection(db_queues.Select<BusinessLogic.Queue, Dat.Assets.Scraper.V1.Dto.Management.Queue.QueueInfo>(q =>
                        new Dat.Assets.Scraper.V1.Dto.Management.Queue.QueueInfo()
                        {
                            Attempt = q.Attempt,
                            DequeuedOn = q.DequeuedOn,
                            DownloadedOn = q.DownloadedOn,
                            EnqueuedOn = q.EnqueuedOn,
                            Error = q.Error,
                            FinishedOn = q.FinishedOn,
                            Keyword = q.Keyword,
                            KeywordId = q.KeywordId,
                            Location = q.Location,
                            LocationId = q.LocationId,
                            LocationType = q.LocationType,
                            ParsedOn = q.ParsedOn,
                            Priority = q.Priority,
                            QueueId = q.QueueId,
                            Results = q.Results,
                            ServerId = q.ServerId,
                            StartedOn = q.StartedOn,
                            Status = q.Status,
                            Url = q.Url
                        }
                    ).ToList());
        }
        public override void POST(Dat.Assets.Scraper.V1.Dto.Management.Queue.Request request)
        {
            base.POST(request);
            List<Dat.Assets.Scraper.V1.BusinessLogic.Queue> db_queues = null;
            if (Resource.Parameters.Count == 0 || string.IsNullOrWhiteSpace(Resource.Parameters[0]))
                throw new Dat.V1.Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid parameter passed."); 
            int serverId;
            string strServerId = Resource.Parameters[0];
            if (!int.TryParse(strServerId, out serverId))
                throw new Dat.V1.Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Server id is not valid..");


            db_queues = BusinessLogic.Queue.SelectNext(serverId, cn).ToList();

            if (db_queues == null)
            {
                SetResponse(System.Net.HttpStatusCode.NotFound);
                return;
            }
            SetResponseAsCollection(db_queues.Select<BusinessLogic.Queue, Dat.Assets.Scraper.V1.Dto.Management.Queue.QueueInfo>(q =>
                         new Dat.Assets.Scraper.V1.Dto.Management.Queue.QueueInfo()
                         {
                             Attempt = q.Attempt,
                             DequeuedOn = q.DequeuedOn,
                             DownloadedOn = q.DownloadedOn,
                             EnqueuedOn = q.EnqueuedOn,
                             Error = q.Error,
                             FinishedOn = q.FinishedOn,
                             Keyword = q.Keyword,
                             KeywordId = q.KeywordId,
                             Location = q.Location,
                             LocationId = q.LocationId,
                             LocationType = q.LocationType,
                             ParsedOn = q.ParsedOn,
                             Priority = q.Priority,
                             QueueId = q.QueueId,
                             Results = q.Results,
                             ServerId = q.ServerId,
                             StartedOn = q.StartedOn,
                             Status = q.Status,
                             Url = q.Url
                         }
                     ).ToList());

        }
    }
}
