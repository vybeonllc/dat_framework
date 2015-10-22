using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.Assets.Scraper.V1.Services.Management
{
    public class Lead : Dat.V1.Framework.HttpHandlers.Master<Dat.Assets.Scraper.V1.Dto.Management.Lead.Request, Dat.Assets.Scraper.V1.Dto.Management.Lead.LeadInfo>
    {
        string cn = "Data Source=172.16.0.243;Initial Catalog=Scraping;User ID=sa;Password=1562Closed#;Max Pool Size=200;Timeout=240;Packet Size=7999";
        public override void GET()
        {
            base.GET();
            List<Dat.Assets.Scraper.V1.Dto.Management.Lead.LeadInfo> users = new List<Dat.Assets.Scraper.V1.Dto.Management.Lead.LeadInfo>();
            List<Dat.Assets.Scraper.V1.BusinessLogic.Lead> db_leads = Dat.Assets.Scraper.V1.BusinessLogic.Lead.ByQueueId(0, cn).ToList();
            SetResponseAsCollection(db_leads.Select<Dat.Assets.Scraper.V1.BusinessLogic.Lead, Dat.Assets.Scraper.V1.Dto.Management.Lead.LeadInfo>(lead =>
                new Dat.Assets.Scraper.V1.Dto.Management.Lead.LeadInfo()
                       {
                           Address = lead.Address,
                           CreateaDate = lead.CreateDate,
                           Creative = lead.Creative,
                           Latitude = lead.Latitude,
                           LeadId = lead.LeadId,
                           Link = lead.Link,
                           Longitude = lead.Longitude,
                           PhoneNumber = lead.PhoneNumber,
                           QueueId = lead.QueueId,
                           Slogan = lead.Slogan,
                           Website = lead.Website
                       }).ToList());
        }
        public override void GET(long Parameter)
        {
            base.GET(Parameter);
            List<Dat.Assets.Scraper.V1.Dto.Management.Lead.LeadInfo> users = new List<Dat.Assets.Scraper.V1.Dto.Management.Lead.LeadInfo>();
            Dat.Assets.Scraper.V1.BusinessLogic.Lead lead = new BusinessLogic.Lead(cn);
            if (lead.ByLeadId(Parameter))
            {
                SetResponse(new Dat.Assets.Scraper.V1.Dto.Management.Lead.LeadInfo()
                       {
                           Address = lead.Address,
                           CreateaDate = lead.CreateDate,
                           Creative = lead.Creative,
                           Latitude = lead.Latitude,
                           LeadId = lead.LeadId,
                           Link = lead.Link,
                           Longitude = lead.Longitude,
                           PhoneNumber = lead.PhoneNumber,
                           QueueId = lead.QueueId,
                           Slogan = lead.Slogan,
                           Website = lead.Website
                       });
            }
            else
                SetResponse(System.Net.HttpStatusCode.NotFound);
        }
       
        public override void PUT(Dat.Assets.Scraper.V1.Dto.Management.Lead.Request request)
        {
            base.PUT(request);

            new Dat.V1.Utils.Validation.Validators.Validator(request, Dat.V1.Utils.Validation.Enumerations.Action.Create);

            List<Dat.Assets.Scraper.V1.Dto.Management.Lead.LeadInfo> leads = new List<Dto.Management.Lead.LeadInfo>();
            request.Manifest.Leads.ForEach(lead =>
            {
                BusinessLogic.Lead l = new BusinessLogic.Lead(cn)
                {
                    Address = lead.Address,
                    Creative = lead.Creative,
                    Latitude = lead.Latitude,
                    LeadId = lead.LeadId,
                    Link = lead.Link,
                    Longitude = lead.Longitude,
                    PhoneNumber = lead.PhoneNumber,
                    QueueId = lead.QueueId,
                    Slogan = lead.Slogan,
                    Website = lead.Website
                };
                if (l.Create() && l.ByLeadId(l.LeadId))
                    leads.Add(new Dto.Management.Lead.LeadInfo()
                                {
                                    Address = l.Address,
                                    CreateaDate = l.CreateDate,
                                    Creative = l.Creative,
                                    Latitude = l.Latitude,
                                    LeadId = l.LeadId,
                                    Link = l.Link,
                                    Longitude = l.Longitude,
                                    PhoneNumber = l.PhoneNumber,
                                    QueueId = l.QueueId,
                                    Slogan = l.Slogan,
                                    Website = l.Website
                                });
            });
            if (leads.Count == 0)
                SetResponse(System.Net.HttpStatusCode.InternalServerError);
            else
                SetResponseAsCollection(leads);
        }
    }
}
