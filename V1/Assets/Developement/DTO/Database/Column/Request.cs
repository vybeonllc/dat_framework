using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace DAT.v1.Assets.Developement.DTO.Database.Column
{
    [DataContract(Name = "request")]
    public class Request  
    {

        public static DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Column.Column> SelectByColumnName(string databaseName, string tableName, string columnName, string authToken, string api_url, DAT.v1.Utils.Enumerations.DataExchangeFormats format)
        {
            DAT.v1.DTO.BOM.RequestOptions options = new DAT.v1.DTO.BOM.RequestOptions()
                   {
                       AuthenticationToken = authToken,
                       Parameters = databaseName + "/" + tableName + "/" + columnName,
                       ApiUrl = api_url,
                       EndPoint = Constants.EndPoint,
                       Method = DAT.v1.Utils.Enumerations.HttpVerbs.GET,
                       RequestType = format,
                       ResponseType = format
                   };
            switch (format)
            {
                case DAT.v1.Utils.Enumerations.DataExchangeFormats.JSON:
                    return Utils.Serialization.JSON.Serializer.Deserialize<DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Column.Column>>(DAT.v1.DTO.BOM.Request.Send(options));
                case DAT.v1.Utils.Enumerations.DataExchangeFormats.XML:
                    return Utils.Serialization.XML.Serializer.Deserialize<DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Column.Column>>(DAT.v1.DTO.BOM.Request.Send(options));
                default:
                    throw new DAT.v1.DTO.BOM.Exceptions.SerializationNotImplementedException("Format not supported.");
            };
        }
        public static DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Column.Column> SelectByTableName(string databaseName, string tableName, string authToken, string api_url, DAT.v1.Utils.Enumerations.DataExchangeFormats format)
        {
            DAT.v1.DTO.BOM.RequestOptions options = new DAT.v1.DTO.BOM.RequestOptions()
            {
                AuthenticationToken = authToken,
                Parameters = databaseName + "/" + tableName,
                ApiUrl = api_url,
                EndPoint = Constants.EndPoint,
                Method = DAT.v1.Utils.Enumerations.HttpVerbs.GET,
                RequestType = format,
                ResponseType = format
            };
            switch (format)
            {
                case DAT.v1.Utils.Enumerations.DataExchangeFormats.JSON:
                    return Utils.Serialization.JSON.Serializer.Deserialize<DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Column.Column>>(DAT.v1.DTO.BOM.Request.Send(options));
                case DAT.v1.Utils.Enumerations.DataExchangeFormats.XML:
                    return Utils.Serialization.XML.Serializer.Deserialize<DAT.v1.DTO.BOM.Response<DAT.v1.Assets.Developement.DTO.Database.Column.Column>>(DAT.v1.DTO.BOM.Request.Send(options));
                default:
                    throw new DAT.v1.DTO.BOM.Exceptions.SerializationNotImplementedException("Format not supported.");
            };
        }

    }
}