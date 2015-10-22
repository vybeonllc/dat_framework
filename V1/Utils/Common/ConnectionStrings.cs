using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Common
{
    public class ConnectionStrings
    {
        public static string AssetConnectionString { get { return ConnectionString("AssetConnectionString"); } }
        public static string ConnectionString(string connectionName) { return System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString; }
    }
}
