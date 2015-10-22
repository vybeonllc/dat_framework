using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Services.Membership.Authentication
{
    public class Authenticate : Dat.V1.Framework.HttpHandlers.Master<Dto.Membership.Authentication.Request, Dto.Membership.Authentication.AuthenticationInfo>
    {
        public override void PUT(Dto.Membership.Authentication.Request request)
        {
            base.PUT(request);
            Dat.V1.Dto.Membership.Authentication.Request req = Resource.DeserializeData<Dat.V1.Dto.Membership.Authentication.Request>();
            if (req == null || req.Manifest == null)
                throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.BadRequest, "Invalid data.");
            BusinessLogic.User user = new BusinessLogic.User();
            if (!string.IsNullOrWhiteSpace(req.Manifest.AuthenticationInfo.EmailAddress) && !string.IsNullOrWhiteSpace(req.Manifest.AuthenticationInfo.Password))
                user = Dat.V1.BusinessLogic.User.Authenticate(req.Manifest.AuthenticationInfo.EmailAddress, req.Manifest.AuthenticationInfo.Password);
            else if (req.Manifest.AuthenticationInfo.UserGuid != Guid.Empty) //has to be removed, make sure ask pos asset if using this.
            {
                if (!user.ByUserGuid(req.Manifest.AuthenticationInfo.UserGuid))
                    user = null;
            }
            else if (!string.IsNullOrWhiteSpace(req.Manifest.AuthenticationInfo.Token))
            {
                Guid userGuid = Guid.Empty;
                Dat.V1.Utils.Security.Token.TokenGenerator old_token = Dat.V1.Utils.Security.Token.TokenGenerator.FromTokenString(req.Manifest.AuthenticationInfo.Token, Dat.V1.Utils.Security.Token.TokenGenerator.PrivateKey);

                if (old_token == null)
                    throw new Dat.V1.Utils.Security.Exceptions.TokenException("Invalid Token");
                //else if (old_token.Expires <= DateTime.Now)
                //    throw new Dat.V1.Utils.Security.Exceptions.TokenException("Token Expired");
                else if (string.IsNullOrWhiteSpace(old_token.Value) || !Guid.TryParse(old_token.Value, out userGuid) || userGuid == Guid.Empty)
                    throw new Dat.V1.Utils.Security.Exceptions.TokenException("Invalid Token");
                else if (!user.ByUserGuid(userGuid))
                    throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.NotFound, "Unexpected Error");
                else if (user.UserId < 1)
                    throw new Framework.Exceptions.HttpException(System.Net.HttpStatusCode.NotFound, "Account Not Exists");
            }
            if (user == null || user.UserGuid == Guid.Empty)
            {
                SetResponse(System.Net.HttpStatusCode.PreconditionFailed);
                return;
            }
            Dat.V1.Utils.Security.Token.TokenGenerator token = new Utils.Security.Token.TokenGenerator(user.UserGuid.ToString(), DateTime.Now.AddMinutes(15));
            SetResponse(new Dat.V1.Dto.Membership.Authentication.AuthenticationInfo()
            {
                EmailAddress = user.EmailAddress,
                Token = token.GetTokenString(Utils.Security.Token.TokenGenerator.PrivateKey)
            });
        }

    }
}
