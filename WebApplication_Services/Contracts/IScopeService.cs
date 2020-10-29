using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Model;

namespace WebApplication_Services.Service
{
    public interface IScopeService
    {
        Task<string> GetTrancientScopedSingleton();
    }
}
