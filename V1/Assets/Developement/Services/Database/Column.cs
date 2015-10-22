using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT.v1.Assets.Developement.Services.Database
{
    public class Column : DAT.v1.Framework.HttpHandlers.Master
    {
        public override void GET(System.Web.HttpContext context)
        {
            if (Resource.Parameters.Count < 2 || Resource.Parameters.Count > 3)
                throw new DAT.v1.Framework.Resources.Exceptions.InvalidRequestException(DAT.v1.Framework.Resources.Errors.InvalidParameters);
            string databaseName = Resource.Parameters[0], tableName = Resource.Parameters[1], columnName = Resource.Parameters.Count > 2 ? Resource.Parameters[2] : null;
            string cn = "Server=172.16.0.152;Database=Developement;Uid=Application;Pwd=1562Closed#;";
            DAT.v1.Assets.Developement.BusinessLogic.Column.ColumnController cntrlr = new BusinessLogic.Column.ColumnController(cn);
            List<DAT.v1.Assets.Developement.ValueObject.Column> columns = null;
            if (string.IsNullOrWhiteSpace(columnName))
                columns = cntrlr.SelectByTableName(databaseName, tableName);
            else
            {
                var column = cntrlr.SelectByColumneName(databaseName, tableName, columnName);
                if (column != null)
                    columns = new List<ValueObject.Column>() { column };
            }
            if (columns == null)
                Resource.SetResponse(new DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Column.Column>()
                {
                    ResultSet = new v1.DTO.BOM.ResultSet<DTO.Database.Column.Column>()
                    {
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
                List<DTO.Database.Column.Column> results = columns.Select<DAT.v1.Assets.Developement.ValueObject.Column, DTO.Database.Column.Column>(d =>
                    new DTO.Database.Column.Column()
                    {
                        DatabaseName = d.DatabaseName,
                        TableName = d.TableName,
                        AutoIncrement = d.AutoIncrement,
                        ColumnName = d.ColumnName,
                        ColumnType = d.ColumnType,
                        IsNullable = d.IsNullable,
                        OrdinalPosition = d.OrdinalPosition,
                        PrimaryKey = d.PrimaryKey,
                    }
                ).ToList();
                Resource.SetResponse(new DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Column.Column>()
                {
                    ResultSet = new v1.DTO.BOM.ResultSet<DTO.Database.Column.Column>()
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
