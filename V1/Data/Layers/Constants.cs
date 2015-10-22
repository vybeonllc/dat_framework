using System;
using System.Configuration;

namespace Dat.V1.Data.Layers {
  public class Constants {
    public static string ConnectionString { get { return System.Configuration.ConfigurationManager.ConnectionStrings["Dat.Db.Asset"].ToString(); } }
    public static string[] Servers { get { return System.Configuration.ConfigurationManager.AppSettings["Dat.Db.Servers"].ToString().Split(';'); } }
    public static string Bucket { get { return System.Configuration.ConfigurationManager.AppSettings["Dat.Db.Bucket"].ToString(); } }
  }
}
