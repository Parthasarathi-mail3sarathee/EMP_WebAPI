using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication_DBA_Layer.DB;
using WebApplication_Shared_Services;
using WebApplication_Shared_Services.Model;
using WebApplication_Shared_Services.Service;

namespace WebApplication_Services.Service
{
    public sealed class AuthService : IAuthService
    {
        public SingletonUserRepo repo { get; set; }
        private string key { get; set; }
        private readonly ILoggerManager _logger;
        public AuthService(ILoggerManager logger)
        {
            _logger = logger;
            repo = SingletonUserRepo.Instance;
            key = "my_secret_key_12345";
        }

        public Task<User> RegisterUser(User userModel)
        {
            if (repo.Users.Count > 0)
            {
                var res = repo.Users.Select(i => i.ID).Max();
                userModel.ID = res + 1;
            }
            else userModel.ID = 1;
            // Hash
            var hash = SecurePasswordHasher.Hash(userModel.Password);
            userModel.Password = hash;
            
            repo.Users.Add(userModel);
            return Task.FromResult(userModel);
        }

        public Task<List<Role>> GetUserRoles(string userName)
        {
            var resuser = repo.Users.Where(u => u.Email == userName).FirstOrDefault();
            var resuseRole = repo.UserRoles.Where(ur => ur.userID == resuser.ID);

            var rle = from ur in resuseRole
                      join r in repo.Roles
                       on ur.roleID equals r.ID
                      select r;

            return Task.FromResult(rle.ToList());
        }


        public Task<User> FindUser(string userName, string password)
        {
            var res = repo.Users.Where(e => e.Email == userName && e.Password == password).FirstOrDefault();
            return Task.FromResult(res);
        }

        public Task<bool> ValidateUser(string userName, string password)
        {
            var res = repo.Users.Where(e => e.Email == userName && SecurePasswordHasher.Verify(password, e.Password)).FirstOrDefault();
            if (res == null) return Task.FromResult(false);
            else return Task.FromResult(true);
        }

        public Task<string> GenerateToken(string userName)
        {
            try
            {
                //byte[] key = Convert.FromBase64String(Secret);
                //Secret key which will be used later during validation    
                var issuer = "http://mysite.com";  //normally this will be your site URL    

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //Create a List of Claims, Keep claims name short    
                var permClaims = new List<Claim>();
                permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                permClaims.Add(new Claim("name", userName));
                permClaims.Add(new Claim(ClaimTypes.Name, userName));

                //Create Security Token object by giving required parameters    
                var token = new JwtSecurityToken(issuer, //Issure    
                                issuer,  //Audience    
                                permClaims,
                                expires: DateTime.Now.AddDays(1),
                                signingCredentials: credentials);
                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
                return Task.FromResult(jwt_token);
            }
            catch (Exception ex)
            {
                _logger.LogError("Message: "+ ex.Message);
                _logger.LogError("StackTrace: " + ex.StackTrace);
                return null;
            }
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null) return null;
                byte[] keybyte = Encoding.UTF8.GetBytes(key);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(keybyte)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch (Exception ex)
            {
                _logger.LogError("Message: " + ex.Message);
                _logger.LogError("StackTrace: " + ex.StackTrace);
                return null;
            }
        }
    }
}
