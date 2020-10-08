using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Service
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
