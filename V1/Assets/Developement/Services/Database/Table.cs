using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT.v1.Assets.Developement.Services.Database
{
    public class Table : DAT.v1.Framework.HttpHandlers.Master
    {
        public override void GET(System.Web.HttpContext context)
        {
            base.GET(context);
            if (Resource.Parameters.Count < 1 || Resource.Parameters.Count > 2 || string.IsNullOrWhiteSpace(Resource.Parameters[0]))
                throw new DAT.v1.Framework.Resources.Exceptions.InvalidRequestException(DAT.v1.Framework.Resources.Errors.InvalidParameters);
            string databaseName = Resource.Parameters[0], tableName = Resource.Parameters.Count > 1 ? Resource.Parameters[1] : null;
            string cn = "Server=172.16.0.152;Database=Developement;Uid=Application;Pwd=1562Closed#;";
            DAT.v1.Assets.Developement.BusinessLogic.Table.TableController cntrlr = new BusinessLogic.Table.TableController(cn);
            List<DAT.v1.Assets.Developement.ValueObject.Table> tables = null;
            if (string.IsNullOrWhiteSpace(tableName))
                tables = cntrlr.SelectByDatabaseName(databaseName);
            else
            {
                var table = cntrlr.SelectByTableName(databaseName, tableName);
                if (table != null)
                    tables = new List<ValueObject.Table>() { table };
            }
            if (tables == null)
                Resource.SetResponse(new DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Table.Table>()
                {
                    ResultSet = new v1.DTO.BOM.ResultSet<DTO.Database.Table.Table>(),
                    Status = new v1.DTO.BOM.Status()
                              {
                                  Code = 999,
                                  Message = "Something went wrong",
                                  StatusCode = "failed"
                              }
                });
            else
            {
                List<DTO.Database.Table.Table> results = tables.Select<DAT.v1.Assets.Developement.ValueObject.Table, DTO.Database.Table.Table>(d =>
                    new DTO.Database.Table.Table()
                    {
                        DatabaseName = d.DatabaseName,
                        TableName = d.TableName
                    }
                ).ToList();
                Resource.SetResponse(new DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Table.Table>()
                {
                    ResultSet = new v1.DTO.BOM.ResultSet<DTO.Database.Table.Table>()
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
