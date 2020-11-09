using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication_Services.Service;
using WebApplication_Shared_Services.Service;

namespace WebApplication_WebAPI.Filters
{
    public class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string[] claims) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { claims };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly string[] _claim;
        //private readonly ILoggerManager _logger;

        public ClaimRequirementFilter(string[] claim)
        {
            _claim = claim;
            //_logger = new LoggerManager();
            //_baseAuth = new IdentityBasicAuthentication(new AuthService(_logger));
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string msg;
            var _logger = (ILoggerManager)context.HttpContext.RequestServices.GetService(typeof(ILoggerManager));
            var _baseAuth = (IBaseAuth)context.HttpContext.RequestServices.GetService(typeof(IBaseAuth));
            var accessToken = context.HttpContext.Request.Headers["Authorization"];
            var userPrincpal =  _baseAuth.AuthenticateJwtToken(accessToken).Result;
            if (userPrincpal == null)
            {
                msg = "Unauthorized User credentials is not a valid";
                ProcessIP._httpContextAccessor = new HttpContextAccessor();
                ProcessIP._httpContextAccessor.HttpContext = context.HttpContext;
                _logger.LogWarn(msg);
                _logger.LogWarn("Remote IP Client: " + ProcessIP.GetRequestIP());
                _logger.LogWarn("client useragent: " + ProcessIP.UserAgent());
                _logger.LogWarn("client language: " + ProcessIP.ClientLanguage());
                ProcessIP.Unknown();//log custom header in a log file log unauthorized request of each Header of Each Request
                _logger.LogError(msg);
                context.Result = new UnauthorizedResult();
                return;
            }
            var userRigths =  _baseAuth.AuthorizeUserClaim(userPrincpal, _claim.ToList());
            if (!userRigths)
            {
                msg = "Unauthorized User access to the resource is not a valid";
                context.Result = new NoContentResult();
                return;
            }

            //var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == _claim.Value);
            //if (!hasClaim)
            //{
            //    context.Result = new ForbidResult();
            //}
        }
    }


}
