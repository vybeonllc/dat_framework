using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Net
{
    public class Dns
    {
        public static string GetServerIPAddress()
        {
            string strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipHostInfo = System.Net.Dns.Resolve(System.Net.Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }
    }

}
