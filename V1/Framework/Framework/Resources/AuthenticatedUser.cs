using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Framework.Resources
{
    public class AuthenticatedUser
    {
        public Guid UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return ((FirstName ?? string.Empty) + " " + (LastName ?? string.Empty)).Trim(); } }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
