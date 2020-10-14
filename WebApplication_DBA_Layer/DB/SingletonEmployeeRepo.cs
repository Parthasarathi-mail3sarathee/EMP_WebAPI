using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Model;

namespace WebApplication_DBA_Layer.DB
{
    public sealed class SingletonEmployeeRepo
    {
        private static SingletonEmployeeRepo instance = null;
        private static readonly object padlock = new object();
        public List<Employee> Employees { get; set; }

        SingletonEmployeeRepo()
        {
            Employees = new List<Employee>();
            Employees.Add(new Employee { ID = 2, Address = "Madurai", Department = "IT", Name = "Raman", Role = "Lead", DOB = new DateTime(1984, 10, 20), DOJ = new DateTime(1984, 10, 20), IsActive = true, SkillSets = new List<string>() { } });
            Employees.Add(new Employee { ID = 1, Address = "Chennai", Department = "IT", Name = "Raju", Role = "Manager", DOB = new DateTime(1984, 10, 20), DOJ = new DateTime(1984, 10, 20), IsActive = true, SkillSets = new List<string>() { } });

        }

        public static SingletonEmployeeRepo Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SingletonEmployeeRepo();
                    }
                    return instance;
                }
            }
        }
    }
}
