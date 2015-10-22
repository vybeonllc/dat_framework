using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dat.V1.Dto.Membership.User
{
    [DataContract()]
    public class User
    {

        [DataMember(Name = "user_guid")]
        [Dat.V1.Utils.Validation.Attributes.Required("User Guid", Utils.Validation.Enumerations.Action.Update)]
        public Guid UserGuid { get; set; }

        [DataMember(Name = "password")]
        [Dat.V1.Utils.Validation.Attributes.Password("Password", Utils.Validation.Enumerations.PasswordType.Strong)]
        [Dat.V1.Utils.Validation.Attributes.LengthNotGreaterThan("Password Length", 500)]
        public string Password { get; set; }

        [DataMember(Name = "role_id")]
        [Dat.V1.Utils.Validation.Attributes.PositiveIntegerNumber("Role", Utils.Validation.Enumerations.Action.Update | Utils.Validation.Enumerations.Action.Create)]
        public int RoleId { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "full_name")]
        public string FullName { get; set; }

        [DataMember(Name = "email_address")]
        [Dat.V1.Utils.Validation.Attributes.EmailAddress("Email Address", Utils.Validation.Enumerations.Action.Update | Utils.Validation.Enumerations.Action.Create)]
        public string EmailAddress { get; set; }

        [DataMember(Name = "create_date")]
        public DateTime CreateDate { get; set; }

        [DataMember(Name = "active")]
        public bool Active { get; set; }

        [DataMember(Name = "created_by")]
        [Dat.V1.Utils.Validation.Attributes.Required("Creator", Utils.Validation.Enumerations.Action.Update | Utils.Validation.Enumerations.Action.Create)]
        public Guid CreatedBy { get; set; }
    }
}
