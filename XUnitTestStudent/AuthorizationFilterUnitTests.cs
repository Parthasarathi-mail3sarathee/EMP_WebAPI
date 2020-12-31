using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Service;
using WebApplication_WebAPI.Filters;
using Xunit;

namespace XUnitTestStudent
{
    public class AuthorizationFilterUnitTests
    {
        public AuthorizationFilterUnitTests()
        {

        }

        [Fact]
        public void Authorization_OnSuccess()
        {

            //Arrange  

            var perm = new List<string>() { "Leader", "Teacher" };
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock
              .Setup(a => a.Request.Headers["Authorization"])
              .Returns("Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI2NDJiNDE4Yi05MWFjLTQ5NTQtODgxMC01ZDc1OWJhMjAxNTAiLCJuYW1lIjoidGVzdEB0ZXN0LmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ0ZXN0QHRlc3QuY29tIiwiZXhwIjoxNjA2ODI5OTE4LCJpc3MiOiJodHRwOi8vbXlzaXRlLmNvbSIsImF1ZCI6Imh0dHA6Ly9teXNpdGUuY29tIn0.76s1wSHNCqyhqyXkDajD3YYcvyjxopa91VSPLwUr-AQ");

            // var httpContextMock = new Mock<HttpContext>();

            // Create the userValidatorMock 
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "fdssdfsf")
                    };
            var identity = new ClaimsIdentity(claims, "Jwt");
            IPrincipal userPrincpal = new ClaimsPrincipal(identity);
            var baseauth = new Mock<IBaseAuth>();
            baseauth.Setup(bauth => bauth
                // For any parameter passed to IsValid
                .AuthenticateJwtToken(It.IsAny<string>())
                ).Returns(Task.FromResult(userPrincpal));
            baseauth.Setup(bauth => bauth
                // For any parameter passed to IsValid
                .AuthorizeUserClaim(userPrincpal, perm)
                ).Returns(true);

            var logger = new Mock<ILoggerManager>();
            logger.Setup(lgr => lgr
                // For any parameter passed to IsValid
                .LogInfo(It.IsAny<string>())
            );

            var serviceProviderMocklog = new Mock<IServiceProvider>();
            serviceProviderMocklog.Setup(provider => provider.GetService(typeof(ILoggerManager)))
                .Returns(logger.Object);
        
            var serviceProviderMockbAuth = new Mock<IServiceProvider>();
            serviceProviderMockbAuth.Setup(provider => provider.GetService(typeof(IBaseAuth)))
                .Returns(baseauth.Object);

            httpContextMock.SetupGet(context => context.RequestServices)
            .Returns(serviceProviderMocklog.Object);

            httpContextMock.SetupGet(context => context.RequestServices)
            .Returns(serviceProviderMockbAuth.Object);

            ActionContext fakeActionContext =
                 new ActionContext(httpContextMock.Object,
                 new Microsoft.AspNetCore.Routing.RouteData(),
                 new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());

            AuthorizationFilterContext fakeAuthFilterContext =
                  new AuthorizationFilterContext(fakeActionContext,
                       new List<IFilterMetadata> { });

            //Act  
            ClaimRequirementFilter claimReqFilter =
                         new ClaimRequirementFilter(new string[] { "Leader", "Teacher" });
            claimReqFilter.OnAuthorization(fakeAuthFilterContext);


            //Assert  


            Assert.Null(fakeAuthFilterContext.Result);

        }
    }
}
