using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Model;

namespace WebApplication_Services.Service
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentAsync(CancellationToken ct);
        Task<Student> GetStudentByIDAsync(int id, CancellationToken ct);
        Task<Student> AddStudent(Student value, CancellationToken ct);
        Task<Student> UpdateStudent(int id, Student value, CancellationToken ct);
        Task<bool> DeleteStudent(int id, CancellationToken ct);
    }
}
