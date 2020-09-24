using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Filters
{
    public interface IBaseAuth
    {
        Task<IPrincipal> AuthenticateJwtToken(string token);
        Task<bool> AuthorizeUser(IPrincipal user, List<string> permissionto);
    }
}
