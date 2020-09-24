﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Service
{
    public interface IAuthService
    {
        Task<User> RegisterUser(User userModel);
        Task<User> FindUser(string userName, string password);
        Task<bool> ValidateUser(string userName, string password);
        Task<string> GenerateToken(string userName);
        ClaimsPrincipal GetPrincipal(string token);
        Task<List<Role>> GetUserRoles(string userName);
    }
}
