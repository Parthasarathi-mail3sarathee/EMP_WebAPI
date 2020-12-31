using System.Threading.Tasks;

namespace WebApplication_Services.Service
{
    public interface IScopeService
    {
        Task<string> GetTrancientScopedSingleton();
    }
}
