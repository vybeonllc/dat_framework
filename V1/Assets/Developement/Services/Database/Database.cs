using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT.v1.Assets.Developement.Services.Database
{
    public class Database : DAT.v1.Framework.HttpHandlers.Master
    {
        public override void GET(System.Web.HttpContext context)
        {
            string cn = "Server=172.16.0.152;Database=Developement;Uid=Application;Pwd=1562Closed#;";
            DAT.v1.Assets.Developement.BusinessLogic.Database.DatabaseController cntrlr = new BusinessLogic.Database.DatabaseController(cn);
            var databases = cntrlr.SelectAll();
            if (databases == null)
                Resource.SetResponse(new DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Database.Database>()
                {
                    ResultSet = new v1.DTO.BOM.ResultSet<DTO.Database.Database.Database>()
                    {
                        ReturnedResults = 0,
                        TotalResults = 0
                    },
                    Status = new v1.DTO.BOM.Status()
                      {
                          Code = 999,
                          Message = "Something went wrong",
                          StatusCode = "failed"
                      }
                });
            else
            {
                List<DTO.Database.Database.Database> results = databases.Select<DAT.v1.Assets.Developement.ValueObject.Database, DTO.Database.Database.Database>(d =>
                    new DTO.Database.Database.Database()
                    {
                        DatabaseName = d.DatabaseName
                    }
                ).ToList();
                Resource.SetResponse(new DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Database.Database>()
                {
                    ResultSet = new v1.DTO.BOM.ResultSet<DTO.Database.Database.Database>()
                    {
                        Results = results,
                        ReturnedResults = results.Count,
                        TotalResults = results.Count
                    },
                    Status = new v1.DTO.BOM.Status()
                    {
                        Code = 200,
                        Message = "OK",
                        StatusCode = "ok"
                    }
                });
            }
        }
    }
}
