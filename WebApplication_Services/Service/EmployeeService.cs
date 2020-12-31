using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_DBA_Layer.DB;
using WebApplication_Shared_Services.Model;

namespace WebApplication_Services.Service
{
    public sealed class EmployeeService : IEmployeeService
    {
        public SingletonEmployeeRepo repo { get; set; }
        public EmployeeService()
        {
            repo = SingletonEmployeeRepo.Instance;
          
        }

        public Task<List<Employee>> GetAllEmployeeAsync(CancellationToken ct)
        {
            return Task.FromResult(repo.Employees);
        }
        public Task<Employee> GetEmployeeByIDAsync(int id, CancellationToken ct)
        {
            var res = repo.Employees.FirstOrDefault(e => e.ID == id);
            return Task.FromResult(res);
        }
        public Task<Employee> AddEmployee(Employee value, CancellationToken ct)
        {
            if (repo.Employees.Count > 0)
            {
                var res = repo.Employees.Select(i => i.ID).Max();
                value.ID = res + 1;
            }
            else
            {
                value.ID =  1;
            }
            repo.Employees.Add(value);
            return Task.FromResult(value);
        }
        public Task<Employee> UpdateEmployee(int id, Employee value, CancellationToken ct)
        {

            var res = repo.Employees.FirstOrDefault(e => e.ID == id);
            value.ID = res.ID;
            repo.Employees.Remove(res);
            repo.Employees.Add(value);
            return Task.FromResult(value);
        }


        public Task<bool> DeleteEmployee(int id, CancellationToken ct)
        {
            var res = repo.Employees.FirstOrDefault(e => e.ID == id);
            if (res != null)
            {
                repo.Employees.Remove(res);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

    }
}
