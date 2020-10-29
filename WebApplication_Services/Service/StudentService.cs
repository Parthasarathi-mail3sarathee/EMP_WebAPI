using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Model;
using WebApplication_DBA_Layer.DB;
using WebApplication_Shared_Services.Service;

namespace WebApplication_Services.Service
{
    public sealed class StudentService : IStudentService
    {
        public SingletonStudentRepo repo { get; set; }
        

        private readonly IOperationTransient _transientOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationScoped _scopedOperation;


        public StudentService(

                      IOperationTransient transientOperation,
                      IOperationScoped scopedOperation,
                      IOperationSingleton singletonOperation
                      )
        {
            repo = SingletonStudentRepo.Instance;

            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
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

        public Task<string>  GetTrancientScopedSingleton()
        {

            string str = "{ ServiceMethod: { Transient: " + _transientOperation.OperationId + " "
                          + ", Scoped: " + _scopedOperation.OperationId + " "
                          + ", Singleton: " + _singletonOperation.OperationId + " } ";

            return Task.FromResult(str); ;
        }
    }
}
