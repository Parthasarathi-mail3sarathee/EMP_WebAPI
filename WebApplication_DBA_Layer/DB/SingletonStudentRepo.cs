using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Model;

namespace WebApplication_DBA_Layer.DB
{
    public sealed class SingletonStudentRepo
    {
        private static SingletonStudentRepo instance = null;
        private static readonly object padlock = new object();
        public List<Student> Students { get; set; }

        SingletonStudentRepo()
        {
            Students = new List<Student>();
            Students.Add(new Student { ID = 2, Address = "Madurai", Department = "IT", Name = "Raman", Role = "Lead", DOB = new DateTime(1984, 10, 20), DOJ = new DateTime(1984, 10, 20), IsActive = true, SkillSets = new List<string>() { } });
            Students.Add(new Student { ID = 1, Address = "Chennai", Department = "IT", Name = "Raju", Role = "Manager", DOB = new DateTime(1984, 10, 20), DOJ = new DateTime(1984, 10, 20), IsActive = true, SkillSets = new List<string>() { } });

        }

        public static SingletonStudentRepo Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SingletonStudentRepo();
                    }
                    return instance;
                }
            }
        }
    }
}
