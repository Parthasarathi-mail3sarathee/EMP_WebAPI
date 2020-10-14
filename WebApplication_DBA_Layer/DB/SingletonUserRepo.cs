using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Shared_Services.Model;

namespace WebApplication_DBA_Layer.DB
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
            //Password = "student"
            Users.Add(new User { ID = 3,  Name = "Sankar", Password = "$MYHASH$V1$10000$FgmAy/FK2WM16yUegb2kZg28NsvuI2Hs8DqFNl9/1U9rBLZN", Email = "sankar@test.com", IsActive = true });
            //Password = "Lead"
            Users.Add(new User { ID = 2,  Name = "Raman", Password = "$MYHASH$V1$10000$MbQju3j+kx2kFWGFW7+9eMR9349K0bK4Sx5i4hfEGOqpbYIV", Email = "test@test.com", IsActive = true });
            //Password = "Teacher"
            Users.Add(new User { ID = 1,  Name = "Raju", Password = "$MYHASH$V1$10000$dCK1B9K44bExIgsBSVBh8u9dY03ptfIZpiEbhjbIB9R9ZshE", Email = "test1@test.com", IsActive = true });
    
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
