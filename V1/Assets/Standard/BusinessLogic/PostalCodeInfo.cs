using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DAT.v1.Assets.Standards.BusinessLogic
{
    [Serializable]
    public class PostalCodeInfo : Base
    {
        #region >>-- CONSTRUCTORS                                                 -->>--

        public PostalCodeInfo()
        {
            //Blank
        }

        public PostalCodeInfo(String connectionString)
        {
            ConnectionString = connectionString;
        }

        public PostalCodeInfo(DataRow row, String connectionString)
        {
            ConnectionString = connectionString;
            Load(row);
        }

        public PostalCodeInfo(long postalcodeID, String connectionString)
        {
            ConnectionString = connectionString;
            Load(postalcodeID);
        }

        #endregion >>-- CONSTRUCTORS                                                 -->>--

        #region >>-- FIELDS                                                       -->>--


        #endregion >>-- FIELDS                                                       -->>--


        #region >>-- Properties                                                   -->>--

        public long PostalCodeID { get; set; }
        public string PostalCode { get; set; }
        public string TelephonePrefix { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Elevation { get; set; }
        public int UTC { get; set; }
        public bool DayLightSaving { get; set; }
        public string CityTown { get; set; }
        public string CityTownAlias { get; set; }
        public string Locality { get; set; }
        public string Region { get; set; }
        public int CountryCode { get; set; }
        public string Country { get; set; }


        #endregion

        #region >>-- Database Methods                                                   -->>--

        public Boolean Load(DataRow row)
        {

            Boolean success = false;

            try
            {

                if (row != null)
                {

                    PostalCodeID = long.Parse(row["id_postal_codes"].ToString());
                    PostalCode = row["postal_code"] is DBNull ? null : row["postal_code"].ToString();
                    TelephonePrefix = row["telephone_prefix"] is DBNull ? null : row["telephone_prefix"].ToString();
                    Latitude = row["latitude"] is DBNull ? 0 : double.Parse(row["latitude"].ToString());
                    Longitude = row["longitude"] is DBNull ? 0 : double.Parse(row["longitude"].ToString());
                    Elevation = row["elevation"] is DBNull ? 0 : int.Parse(row["elevation"].ToString());
                    UTC = row["utc"] is DBNull ? 0 : int.Parse(row["utc"].ToString());
                    DayLightSaving = row["daylight_savings"] is DBNull ? false : row["daylight_savings"].ToString() == "1";
                    CityTown = row["citytown"] is DBNull ? null : row["citytown"].ToString();
                    CityTownAlias = row["citytown_alias"] is DBNull ? null : row["citytown_alias"].ToString();
                    Locality = row["locality"] is DBNull ? null : row["locality"].ToString();
                    Region = row["region"] is DBNull ? null : row["region"].ToString();
                    CountryCode = row["country_code"] is DBNull ? 0 : int.Parse(row["country_code"].ToString());
                    Country = row["country"] is DBNull ? null : row["country"].ToString();

                    success = true;

                }

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading postal code data row.", ex); }

            return success;
        }

        public Boolean Load(long postalcode_id)
        {
            Boolean success = false;
            DataLayer.MySQL mySQL = null;
            DataRow row = null;

            try
            {

                mySQL = new DataLayer.MySQL(ConnectionString);
                row = mySQL.GetDataRow(DataLayer.StoredProcedures.postal_codes_std_get_id,
                    "@_id_postal_code", postalcode_id);

                if (row != null) success = Load(row);

            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading postal code.", ex); }
            finally { if (mySQL != null) mySQL.Dispose(); }

            return success;
        }

        #endregion

        #region >>-- Shared Methods                                                   -->>--

        public static List<PostalCodeInfo> LoadPostalCodes(string postal_code, string connectionString)
        {
            Boolean success = false;
            DataLayer.MySQL mySQL = null;
            DataTable dt = null;
            List<PostalCodeInfo> postalcodes = new List<PostalCodeInfo>(); ;

            try
            {

                mySQL = new DataLayer.MySQL(connectionString);
                dt = mySQL.GetDataTable(DataLayer.StoredProcedures.postal_codes_std_get_postal_code,
                    "@_postal_code", postal_code);
                if (dt != null)
                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        PostalCodeInfo postalcode = new PostalCodeInfo(connectionString);
                        if (postalcode.Load(row) && postalcode.PostalCodeID > 0)
                            postalcodes.Add(postalcode);
                    }
            }
            catch (Exception ex) { throw new Exceptions.BusinessLogicException("Error loading postal code.", ex); }
            finally { if (mySQL != null) mySQL.Dispose(); }

            return postalcodes;
        }

        #endregion
    }
}
