using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Filters;
using WebApplication1.Service;

namespace WebApplication1.Controllers.Base
{
    public class AuthBase : ControllerBase
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IBaseAuth _baseAuth;
        private readonly ILoggerManager _logger;
        public AuthBase(ILoggerManager logger, IHttpContextAccessor httpContextAccessor, IBaseAuth baseAuth)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _baseAuth = baseAuth;
        }
        public async Task<string> AuthenticationAndAuthorization(List<string> permissionto)
        {
            string msg = "Success";
            var accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            var userPrincpal = await _baseAuth.AuthenticateJwtToken(accessToken);
            if (userPrincpal == null)
            {
                msg = "Unauthorized User credentials is not a valid";

                _logger.LogWarn(msg);
                _logger.LogWarn("Remote IP Client: "+ GetRemoteIP());
                _logger.LogWarn("client useragent: " + GetUserAgent());
                _logger.LogWarn("client language: " + GetUserLanguage());
                 GetUnknown();//log custom header in a log file logHeader_Each_Request_
                _logger.LogError(msg);
                return msg;
            }
            var userRigths = await _baseAuth.AuthorizeUser(userPrincpal, permissionto);
            if (!userRigths)
            {
                msg = "Unauthorized User access to the resource is not a valid";
                return msg;
            }
            return msg;
        }

        public string GetRemoteIP()
        {
            ProcessIP._httpContextAccessor = this._httpContextAccessor;
            return ProcessIP.GetRequestIP();
        }
        public string GetUserAgent()
        {
            ProcessIP._httpContextAccessor = this._httpContextAccessor;
            return ProcessIP.UserAgent();
        }
        public string GetUserLanguage()
        {
            ProcessIP._httpContextAccessor = this._httpContextAccessor;
            return ProcessIP.ClientLanguage();
        }
        public void GetUnknown()
        {
            ProcessIP._httpContextAccessor = this._httpContextAccessor;
            ProcessIP.Unknown();
        }

    }
}
