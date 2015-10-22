using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dat.V1.Utils.Extensions;

namespace Dat.V1.Services.Membership
{
    public class User : Dat.V1.Framework.HttpHandlers.Master<Dto.Membership.User.Request, Dto.Membership.User.User>
    { 
        public override void GET(Guid user_guid)
        {
            base.GET(user_guid);
            Dat.V1.BusinessLogic.User user = new BusinessLogic.User();
            if (!user.ByUserGuid(user_guid) || user.UserGuid == Guid.Empty)
            {
                SetResponse(System.Net.HttpStatusCode.NotFound);
                return;
            }
            SetResponse(new Dat.V1.Dto.Membership.User.User()
                         {
                             Active = user.Active,
                             CreateDate = user.CreateDate,
                             CreatedBy = user.CreatedBy,
                             EmailAddress = user.EmailAddress,
                             FullName = user.FullName,
                             FirstName = user.FirstName,
                             LastName = user.LastName,
                             RoleId = user.RoleId,
                             UserGuid = user.UserGuid
                         });

        }
        public override void GET(string email_address)
        {
            base.GET(email_address);

            Dat.V1.BusinessLogic.User user = new BusinessLogic.User();
            if (!user.ByEmailAddress(email_address) || user.UserGuid == Guid.Empty)
            {
                SetResponse(System.Net.HttpStatusCode.NotFound);
                return;
            }
            SetResponse(new Dat.V1.Dto.Membership.User.User()
            {
                Active = user.Active,
                CreateDate = user.CreateDate,
                CreatedBy = user.CreatedBy,
                EmailAddress = user.EmailAddress,
                FullName = user.FullName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleId = user.RoleId,
                UserGuid = user.UserGuid
            });
        }

        public override void PUT(Dto.Membership.User.Request request)
        {
            base.PUT(request);

            Dto.Membership.User.Request req = Resource.DeserializeData<Dto.Membership.User.Request>();
            new Utils.Validation.Validators.Validator(req, Utils.Validation.Enumerations.Action.Create);

            List<Dat.V1.Dto.Membership.User.User> users = new List<Dto.Membership.User.User>();

            Dat.V1.BusinessLogic.User u = new BusinessLogic.User()
            {
                Active = true,
                CreatedBy = Resource.AuthenticatedUser,
                EmailAddress = req.Manifest.User.EmailAddress,
                FirstName = req.Manifest.User.FirstName,
                LastName = req.Manifest.User.LastName,
                Password = req.Manifest.User.Password,
            };
            if (string.IsNullOrWhiteSpace(u.Password))
                u.Password = new Random().RandomString(20);
            if (u.Create() && u.ByUserId(u.UserId))
            {
                SetResponse(new Dat.V1.Dto.Membership.User.User()
                                {
                                    Active = u.Active,
                                    CreateDate = u.CreateDate,
                                    CreatedBy = u.CreatedBy,
                                    EmailAddress = u.EmailAddress,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    FullName = u.FullName,
                                    RoleId = u.RoleId,
                                    UserGuid = u.UserGuid
                                });
                return;
            }
            SetResponse(System.Net.HttpStatusCode.InternalServerError);
        }
        public override void POST(Dto.Membership.User.Request request)
        {
            base.PUT(request);

            Dto.Membership.User.Request req = Resource.DeserializeData<Dto.Membership.User.Request>();

            List<Dat.V1.Dto.Membership.User.User> users = new List<Dto.Membership.User.User>();

            Dat.V1.BusinessLogic.User u = new BusinessLogic.User()
            {
                UserGuid = req.Manifest.User.UserGuid,
                FirstName = req.Manifest.User.FirstName,
                LastName = req.Manifest.User.LastName,
                EmailAddress = req.Manifest.User.EmailAddress,
                Password = req.Manifest.User.Password,
                RoleId = req.Manifest.User.RoleId,
                Active = req.Manifest.User.Active
            };
            if (u.UpdateByUserGuid() && u.ByUserId(u.UserId))
            {
                SetResponse(new Dat.V1.Dto.Membership.User.User()
                                {
                                    Active = u.Active,
                                    CreateDate = u.CreateDate,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    FullName = u.FullName,
                                    CreatedBy = u.CreatedBy,
                                    EmailAddress = u.EmailAddress,
                                    UserGuid = u.UserGuid
                                });
                return;
            }
            SetResponse(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
