using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Service
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
