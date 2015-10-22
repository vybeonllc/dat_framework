using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT.v1.Web.Services.Standards.Location
{
    public class PostalCode : DAT.v1.Framework.HttpHandlers.Suggestion
    {
        public override void Suggest()
        {
            List<DAT.v1.DTO.Suggestion.Suggestion<DAT.v1.DTO.Standards.Location.PostalCode.PostalCodeInfo>> results = new List<DAT.v1.DTO.Suggestion.Suggestion<DAT.v1.DTO.Standards.Location.PostalCode.PostalCodeInfo>>();

            results = DAT.v1.Assets.Standards.BusinessLogic.PostalCodeInfo.LoadPostalCodes(SuggestionIdea, "Server=172.16.0.152;Database=standards;Uid=Application;Pwd=1562Closed#;")
            .Select<DAT.v1.Assets.Standards.BusinessLogic.PostalCodeInfo, DAT.v1.DTO.Suggestion.Suggestion<DAT.v1.DTO.Standards.Location.PostalCode.PostalCodeInfo>>(suggestion =>
                new DAT.v1.DTO.Suggestion.Suggestion<DAT.v1.DTO.Standards.Location.PostalCode.PostalCodeInfo>()
                {
                    Data = new DAT.v1.DTO.Standards.Location.PostalCode.PostalCodeInfo()
                    {
                        CityTown = suggestion.CityTown,
                        CityTownAlias = suggestion.CityTownAlias,
                        Country = suggestion.Country,
                        CountryCode = suggestion.CountryCode,
                        DayLightSaving = suggestion.DayLightSaving,
                        Elevation = suggestion.Elevation,
                        Latitude = suggestion.Latitude,
                        Locality = suggestion.Locality,
                        Longitude = suggestion.Longitude,
                        PostalCode = suggestion.PostalCode,
                        PostalCodeID = suggestion.PostalCodeID,
                        Region = suggestion.Region,
                        TelephonePrefix = suggestion.TelephonePrefix,
                        UTC = suggestion.UTC
                    },
                    Text = suggestion.PostalCode,
                    Value = suggestion.PostalCodeID.ToString()
                }).ToList();
            Resource.SetResponse(new DAT.v1.DTO.BOM.Response<DAT.v1.DTO.Suggestion.Suggestion<DAT.v1.DTO.Standards.Location.PostalCode.PostalCodeInfo>>()
            {
                ResultSet = new DTO.BOM.ResultSet<DAT.v1.DTO.Suggestion.Suggestion<DAT.v1.DTO.Standards.Location.PostalCode.PostalCodeInfo>>()
                {
                    Results = results,
                    ReturnedResults = results.Count,
                    TotalResults = results.Count
                },
                Status = new DTO.BOM.Status()
                {
                    Code = 200,
                    Message = "OK",
                    StatusCode = "ok"
                }
            });
        }
    }
}
