using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Model;

namespace WebApplication_WebAPI.Filters
{
    public interface IBaseAuth
    {
        Task<IPrincipal> AuthenticateJwtToken(string token);
        Task<bool> AuthorizeUser(IPrincipal user, List<string> permissionto);
        bool AuthorizeUserClaim(IPrincipal user, List<string> permissionto);
    }
}
