using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Model;

namespace WebApplication_Services.Service
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeeAsync(CancellationToken ct);
        Task<Employee> GetEmployeeByIDAsync(int id, CancellationToken ct);
        Task<Employee> AddEmployee(Employee value, CancellationToken ct);
        Task<Employee> UpdateEmployee(int id, Employee value, CancellationToken ct);
        Task<bool> DeleteEmployee(int id, CancellationToken ct);
    }
}
