using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.DB
{
    public sealed class SingletonUserRepo
    {
        private static SingletonUserRepo instance = null;
        private static readonly object padlock = new object();
        public List<User> Users { get; set; }
        public List<UserRole> UserRoles {get;set; }
        public List<Role> Roles { get; set; }

        SingletonUserRepo()
        {
            Users = new List<User>();
            Roles = new List<Role>();
            UserRoles = new List<UserRole>();
            //Master Info
            Roles.Add(new Role { ID = 1, Name = "Student", Description = "" });
            Roles.Add(new Role { ID = 2, Name = "Leader", Description = "" });
            Roles.Add(new Role { ID = 3, Name = "Teacher", Description = "" });
            Roles.Add(new Role { ID = 4, Name = "Staff", Description = "" });
            Roles.Add(new Role { ID = 5, Name = "SuperUser", Description = "" });
            //UserRole Info transaction table

            UserRoles.Add(new UserRole { ID = 6, roleID = 1, userID = 3 });// Name = "Sankar"

            UserRoles.Add(new UserRole { ID = 1, roleID = 1, userID = 2 });// Name = "Raman"
            UserRoles.Add(new UserRole { ID = 2, roleID = 2, userID = 2 });// Name = "Raman"

            UserRoles.Add(new UserRole { ID = 3, roleID = 3, userID = 1 });// Name = "Raju"
            UserRoles.Add(new UserRole { ID = 4, roleID = 4, userID = 1 });// Name = "Raju"
            UserRoles.Add(new UserRole { ID = 5, roleID = 5, userID = 1 });// Name = "Raju"

            //User Info  
            Users.Add(new User { ID = 3,  Name = "Sankar", Password = "student", Email = "sankar@test.com", IsActive = true });
            Users.Add(new User { ID = 2,  Name = "Raman", Password = "Lead", Email = "test@test.com", IsActive = true });
            Users.Add(new User { ID = 1,  Name = "Raju", Password = "Teacher", Email = "test1@test.com", IsActive = true });
    
        }

        public static SingletonUserRepo Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SingletonUserRepo();
                    }
                    return instance;
                }
            }
        }
    }
}
