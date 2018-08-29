using Microsoft.AspNetCore.Mvc;
using Sample.Web.Infrastructure.Identity;
using System.Linq;
using System.Security.Claims;

namespace Sample.Web.Features
{
    //[Authorize]
    public class BaseAuthorizeController : Controller
    {
        //private long _userId;

        //protected long UserId
        //{
        //    get
        //    {
        //        var claim = GetClaim(AdminClaimType.UserId);
        //        if (claim == null)
        //            return 0;

        //        long.TryParse(claim.Value, out _userId);
        //        return _userId;
        //    }
        //}

        //public string Auth0UserId
        //{
        //    get
        //    {
        //        var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        //        return claim == null ? string.Empty : claim.Value;
        //    }
        //}

        protected Claim GetClaim(AdminClaimType claim)
        {
            return User.Claims.FirstOrDefault(x => x.Type == claim.DisplayName);
        }
    }
}
