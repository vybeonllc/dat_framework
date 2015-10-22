using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.BusinessLogic
{
    public abstract class Base : Dat.V1.Utils.BusinessLogic.Base
    {
       
        public Base()
        {
            connectionString = DatConnectionString;
        }
        protected static string DatConnectionString { get { return Dat.V1.Utils.Common.ConnectionStrings.ConnectionString("DatConnectionString"); } }
    }
}
