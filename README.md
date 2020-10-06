# EMP_WebAPI

We have a sample web application :

https://github.com/Parthasarathi-mail3sarathee/EMP_WebAPI

To summarize the above things. It is a sample back-end application of student REST API in .Net WebAPI Core.
Implementations are

•	Rest API in .Net WebAPI Core (Get,POST,PUT,DELETE) (Student Controller)

•	singleton in memory database (Users and its role access to student controller )

•	Token Based authentication

•	Sample authorization and authentication for single Get Method

•	Logging the request headers of unauthorized access for security purpose.

•	MOQ unit testing.


It is a singleton in memory database 
https://github.com/Parthasarathi-mail3sarathee/EMP_WebAPI/tree/master/WebApplication1/DB


Let’s consider the working of Student REST API
https://github.com/Parthasarathi-mail3sarathee/EMP_WebAPI/blob/master/WebApplication1/Controllers/StudentController.cs


In our application we have 5 different roles they are; (Student, Leader, Teacher, Staff, SuperUser)
In our application we have 3 users with the role combination


Sankar(Role: Student)

Raman(Role: Student, Leader)

Raju(Role: Teacher, Staff, SuperUser)


in memory database for users
https://github.com/Parthasarathi-mail3sarathee/EMP_WebAPI/blob/master/WebApplication1/DB/SingletonUserRepo.cs


User Repository contains the following users
Roles in our application are in code

	            //Role Mater Info  
	            Roles.Add(new Role { ID = 1, Name = "Student", Description = "" });;
	            Roles.Add(new Role { ID = 2, Name = "Leader", Description = "" });
		    Roles.Add(new Role { ID = 3, Name = "Teacher", Description = "" });
		    Roles.Add(new Role { ID = 4, Name = "Staff", Description = "" });
		    Roles.Add(new Role { ID = 5, Name = "SuperUser", Description = "" });
		    


	            //User Info  
	            Users.Add(new User { ID = 3,  Name = "Sankar", Password = "student", Email = "sankar@test.com", IsActive = true });
	            Users.Add(new User { ID = 2,  Name = "Raman", Password = "Lead", Email = "test@test.com", IsActive = true });
	            Users.Add(new User { ID = 1,  Name = "Raju", Password = "Teacher", Email = "test1@test.com", IsActive = true });
	    


//UserRole Info transaction table
	
	UserRoles.Add(new UserRole { ID = 6, roleID = 1, userID = 3 });// Name = "Sankar", Role=”Student”

	
	UserRoles.Add(new UserRole { ID = 1, roleID = 1, userID = 2 });// Name = "Raman", Role=” Student”
	UserRoles.Add(new UserRole { ID = 2, roleID = 2, userID = 2 });// Name = "Raman"", Role=” Leader”
	
	UserRoles.Add(new UserRole { ID = 3, roleID = 3, userID = 1 });// Name = "Raju", Role=” Teacher”
	UserRoles.Add(new UserRole { ID = 4, roleID = 4, userID = 1 });// Name = "Raju", Role=” Staff”
	UserRoles.Add(new UserRole { ID = 5, roleID = 5, userID = 1 });// Name = "Raju", Role=” SuperUser”




