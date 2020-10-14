using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Model;
using WebApplication_DBA_Layer.DB;

namespace WebApplication_Services.Service
{
    public sealed class StudentService : IStudentService
    {
        public SingletonStudentRepo repo { get; set; }
        public StudentService()
        {
            repo = SingletonStudentRepo.Instance;
          
        }

        public Task<List<Student>> GetAllStudentAsync(CancellationToken ct)
        {
            return Task.FromResult(repo.Students);
        }
        public Task<Student> GetStudentByIDAsync(int id, CancellationToken ct)
        {
            var res = repo.Students.Where(e => e.ID == id).FirstOrDefault();
            return Task.FromResult(res);
        }
        public Task<Student> AddStudent(Student value, CancellationToken ct)
        {
            if (repo.Students.Count > 0)
            {
                var res = repo.Students.Select(i => i.ID).Max();
                value.ID = res + 1;
            }
            else
            {
                value.ID =  1;
            }
            repo.Students.Add(value);
            return Task.FromResult(value);
        }
        public Task<Student> UpdateStudent(int id, Student value, CancellationToken ct)
        {

            var res = repo.Students.Where(e => e.ID == id).FirstOrDefault();
            value.ID = res.ID;
            repo.Students.Remove(res);
            repo.Students.Add(value);
            return Task.FromResult(value);
        }


        public Task<bool> DeleteStudent(int id, CancellationToken ct)
        {
            var res = repo.Students.Where(e => e.ID == id).FirstOrDefault();
            if (res != null)
            {
                repo.Students.Remove(res);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

    }
}
