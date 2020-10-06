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

•	Xunit , MOQ unit testing.


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




1)	Getting token for the above users

https://localhost:44379/api/token


Sankar(Role: Student)

Raman(Role: Student, Leader)

Raju(Role: Teacher, Staff, SuperUser)




request
For user Sankar

POST /api/token HTTP/1.1
Host: localhost:44379
Content-Type: application/json
Cache-Control: no-cache
Postman-Token: b27b8044-6888-7867-1c45-60554fea180a

   {
        "username": "sankar@test.com",
        "password": "student"
    }
    
    
C#



var client = new RestClient("https://localhost:44379/api/token");
var request = new RestRequest(Method.POST);
request.AddHeader("postman-token", "c8ba5f06-7282-3fde-c23d-40e45c070b2b");
request.AddHeader("cache-control", "no-cache");
request.AddHeader("content-type", "application/json");
request.AddParameter("application/json", "   {\n        \"username\": \"sankar@test.com\",\n        \"password\": \"student\"\n    }", ParameterType.RequestBody);
IRestResponse response = client.Execute(request);



Response 

Status OK(200)


Header:
access-control-allow-origin →*

content-type →application/json; charset=utf-8

date →Tue, 06 Oct 2020 13:32:05 GMT

server →Kestrel

transfer-encoding →chunked

x-powered-by →ASP.NET

x-sourcefiles →=?UTF-8?B?QzpcVXNlcnNcQWRtaW5cc291cmNlXHJlcG9zXEVNUF9XZWJBUElcV2ViQXBwbGljYXRpb24xXGFwaVx0b2tlbg==?=




Body:
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJjYzYxZjA1MC05ZGQyLTQxZTEtODg2Zi1iZjI0NGI3NmFhNTEiLCJuYW1lIjoic2Fua2FyQHRlc3QuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhbmthckB0ZXN0LmNvbSIsImV4cCI6MTYwMjA3NzUyNSwiaXNzIjoiaHR0cDovL215c2l0ZS5jb20iLCJhdWQiOiJodHRwOi8vbXlzaXRlLmNvbSJ9.i9ZFmc5i-rgQVSKQiI-g4gk5WppIytzg_4cnGm-wM_Q"
}

request

For user Raman


POST /api/token HTTP/1.1
Host: localhost:44379
Content-Type: application/json
Cache-Control: no-cache
Postman-Token: 691cac72-18e8-61c5-716d-50ebf01e6e7a

   {
        "username": "test@test.com",
        "password": "Lead"
    }



C#
var client = new RestClient("https://localhost:44379/api/token");
var request = new RestRequest(Method.POST);
request.AddHeader("postman-token", "c7b8f384-2f2f-4662-bc37-5e9fd4dc8fd5");
request.AddHeader("cache-control", "no-cache");
request.AddHeader("content-type", "application/json");
request.AddParameter("application/json", "   {\n        \"username\": \"test@test.com\",\n        \"password\": \"Lead\"\n    }", ParameterType.RequestBody);
IRestResponse response = client.Execute(request);





Response 

Status OK(200)



Header:

access-control-allow-origin →*
content-type →application/json; charset=utf-8
date →Tue, 06 Oct 2020 13:34:52 GMT
server →Kestrel
transfer-encoding →chunked
x-powered-by →ASP.NET
x-sourcefiles →=?UTF-8?B?QzpcVXNlcnNcQWRtaW5cc291cmNlXHJlcG9zXEVNUF9XZWJBUElcV2ViQXBwbGljYXRpb24xXGFwaVx0b2tlbg==?=



Body:


{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJjYmMyNGExNS05YWYxLTQyYjUtYjMwNC03Mjk2ODQxNjM2OTIiLCJuYW1lIjoidGVzdEB0ZXN0LmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ0ZXN0QHRlc3QuY29tIiwiZXhwIjoxNjAyMDc3NjkyLCJpc3MiOiJodHRwOi8vbXlzaXRlLmNvbSIsImF1ZCI6Imh0dHA6Ly9teXNpdGUuY29tIn0.lcfzyVZRDoz9Me850Zb-P5HlCUuYMLBRZJrqOia3bKQ"
}




request

For user Raju


POST /api/token HTTP/1.1
Host: localhost:44379
Content-Type: application/json
Cache-Control: no-cache
Postman-Token: ef93bb9a-16fa-2e44-838f-b5a10e07be19

   {
        "username": "test1@test.com",
        "password": "Teacher"
    }




In C#


var client = new RestClient("https://localhost:44379/api/token");
var request = new RestRequest(Method.POST);
request.AddHeader("postman-token", "821de5f5-88e7-b83f-7f9e-468aa619b902");
request.AddHeader("cache-control", "no-cache");
request.AddHeader("content-type", "application/json");
request.AddParameter("application/json", "   {\n        \"username\": \"test1@test.com\",\n        \"password\": \"Teacher\"\n    }", ParameterType.RequestBody);
IRestResponse response = client.Execute(request);





Response 

Status OK(200)

Header:

access-control-allow-origin →*
content-type →application/json; charset=utf-8
date →Tue, 06 Oct 2020 13:39:21 GMT
server →Kestrel
transfer-encoding →chunked
x-powered-by →ASP.NET
x-sourcefiles →=?UTF-8?B?QzpcVXNlcnNcQWRtaW5cc291cmNlXHJlcG9zXEVNUF9XZWJBUElcV2ViQXBwbGljYXRpb24xXGFwaVx0b2tlbg==?=




Body:

{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI0N2FkZGJjNS03ZWI3LTQ2ZWItOTA1OC0yMmIyNjFhY2I2NGEiLCJuYW1lIjoidGVzdDFAdGVzdC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdDFAdGVzdC5jb20iLCJleHAiOjE2MDIwNzc5NjEsImlzcyI6Imh0dHA6Ly9teXNpdGUuY29tIiwiYXVkIjoiaHR0cDovL215c2l0ZS5jb20ifQ.t2y-_zqH1nSm9Vk82DVYPjhH7polMim8sYB-VfSFmDg"
}






2)	Getting get all students for the controller  for the different users from the response toke https://localhost:44379/api/Student/Getall



Sankar(Role: Student)
Raman(Role: Student, Leader)
Raju(Role: Teacher, Staff, SuperUser)



For user Sankar


request


GET /api/Student/Getall HTTP/1.1
Host: localhost:44379
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJjYzYxZjA1MC05ZGQyLTQxZTEtODg2Zi1iZjI0NGI3NmFhNTEiLCJuYW1lIjoic2Fua2FyQHRlc3QuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhbmthckB0ZXN0LmNvbSIsImV4cCI6MTYwMjA3NzUyNSwiaXNzIjoiaHR0cDovL215c2l0ZS5jb20iLCJhdWQiOiJodHRwOi8vbXlzaXRlLmNvbSJ9.i9ZFmc5i-rgQVSKQiI-g4gk5WppIytzg_4cnGm-wM_Q
Cache-Control: no-cache
Postman-Token: 3af7cb93-33af-33a7-47a1-9626fad34542



In C#


var client = new RestClient("https://localhost:44379/api/Student/Getall");
var request = new RestRequest(Method.GET);
request.AddHeader("postman-token", "9d2738c3-d07d-6fcd-2f00-d1b5a2bd9a62");
request.AddHeader("cache-control", "no-cache");
request.AddHeader("authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJjYzYxZjA1MC05ZGQyLTQxZTEtODg2Zi1iZjI0NGI3NmFhNTEiLCJuYW1lIjoic2Fua2FyQHRlc3QuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNhbmthckB0ZXN0LmNvbSIsImV4cCI6MTYwMjA3NzUyNSwiaXNzIjoiaHR0cDovL215c2l0ZS5jb20iLCJhdWQiOiJodHRwOi8vbXlzaXRlLmNvbSJ9.i9ZFmc5i-rgQVSKQiI-g4gk5WppIytzg_4cnGm-wM_Q");
request.AddHeader("content-type", "application/json");
request.AddParameter("application/json", "   {\n        \"username\": \"test@test.com\",\n        \"password\": \"Lead\"\n    }", ParameterType.RequestBody);
IRestResponse response = client.Execute(request




Response 

Status Unauthroized(401)

Header

content-length →0
date →Tue, 06 Oct 2020 13:44:45 GMT
server →Kestrel
x-powered-by →ASP.NET
x-sourcefiles →=?UTF-8?B?QzpcVXNlcnNcQWRtaW5cc291cmNlXHJlcG9zXEVNUF9XZWJBUElcV2ViQXBwbGljYXRpb24xXGFwaVxTdHVkZW50XEdldGFsbA==?=



Body:



request

For user Raman


GET /api/Student/Getall HTTP/1.1
Host: localhost:44379
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJjYmMyNGExNS05YWYxLTQyYjUtYjMwNC03Mjk2ODQxNjM2OTIiLCJuYW1lIjoidGVzdEB0ZXN0LmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ0ZXN0QHRlc3QuY29tIiwiZXhwIjoxNjAyMDc3NjkyLCJpc3MiOiJodHRwOi8vbXlzaXRlLmNvbSIsImF1ZCI6Imh0dHA6Ly9teXNpdGUuY29tIn0.lcfzyVZRDoz9Me850Zb-P5HlCUuYMLBRZJrqOia3bKQ
Cache-Control: no-cache
Postman-Token: 52e5ac87-e437-70f3-869f-17a7a86f462f



In c#


var client = new RestClient("https://localhost:44379/api/Student/Getall");
var request = new RestRequest(Method.GET);
request.AddHeader("postman-token", "6220a8d7-dbfc-0006-cce1-660aa3ffdaeb");
request.AddHeader("cache-control", "no-cache");
request.AddHeader("authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJjYmMyNGExNS05YWYxLTQyYjUtYjMwNC03Mjk2ODQxNjM2OTIiLCJuYW1lIjoidGVzdEB0ZXN0LmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJ0ZXN0QHRlc3QuY29tIiwiZXhwIjoxNjAyMDc3NjkyLCJpc3MiOiJodHRwOi8vbXlzaXRlLmNvbSIsImF1ZCI6Imh0dHA6Ly9teXNpdGUuY29tIn0.lcfzyVZRDoz9Me850Zb-P5HlCUuYMLBRZJrqOia3bKQ");
request.AddHeader("content-type", "application/json");
request.AddParameter("application/json", "   {\n        \"username\": \"test@test.com\",\n        \"password\": \"Lead\"\n    }", ParameterType.RequestBody);
IRestResponse response = client.Execute(request);




Response 

Status Ok(200)

Header:

content-type →application/json; charset=utf-8
date →Tue, 06 Oct 2020 13:50:04 GMT
server →Kestrel
transfer-encoding →chunked
x-powered-by →ASP.NET
x-sourcefiles →=?UTF-8?B?QzpcVXNlcnNcQWRtaW5cc291cmNlXHJlcG9zXEVNUF9XZWJBUElcV2ViQXBwbGljYXRpb24xXGFwaVxTdHVkZW50XEdldGFsbA==?=




Body:

[
    {
        "name": "Raman",
        "id": 2,
        "address": "Madurai",
        "role": "Lead",
        "department": "IT",
        "email": null,
        "skillSets": [],
        "dob": "1984-10-20T00:00:00",
        "doj": "1984-10-20T00:00:00",
        "isActive": true
    },
    {
        "name": "Raju",
        "id": 1,
        "address": "Chennai",
        "role": "Manager",
        "department": "IT",
        "email": null,
        "skillSets": [],
        "dob": "1984-10-20T00:00:00",
        "doj": "1984-10-20T00:00:00",
        "isActive": true
    }
]





request

For user Raju


GET /api/Student/Getall HTTP/1.1
Host: localhost:44379
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI0N2FkZGJjNS03ZWI3LTQ2ZWItOTA1OC0yMmIyNjFhY2I2NGEiLCJuYW1lIjoidGVzdDFAdGVzdC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdDFAdGVzdC5jb20iLCJleHAiOjE2MDIwNzc5NjEsImlzcyI6Imh0dHA6Ly9teXNpdGUuY29tIiwiYXVkIjoiaHR0cDovL215c2l0ZS5jb20ifQ.t2y-_zqH1nSm9Vk82DVYPjhH7polMim8sYB-VfSFmDg
Cache-Control: no-cache
Postman-Token: 40676b5a-6910-deac-53b3-f303297280aa




In C#


var client = new RestClient("https://localhost:44379/api/Student/Getall");
var request = new RestRequest(Method.GET);
request.AddHeader("postman-token", "97c02cea-2ed1-9e61-82c9-9b4afc0ad377");
request.AddHeader("cache-control", "no-cache");
request.AddHeader("authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiI0N2FkZGJjNS03ZWI3LTQ2ZWItOTA1OC0yMmIyNjFhY2I2NGEiLCJuYW1lIjoidGVzdDFAdGVzdC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdDFAdGVzdC5jb20iLCJleHAiOjE2MDIwNzc5NjEsImlzcyI6Imh0dHA6Ly9teXNpdGUuY29tIiwiYXVkIjoiaHR0cDovL215c2l0ZS5jb20ifQ.t2y-_zqH1nSm9Vk82DVYPjhH7polMim8sYB-VfSFmDg");
request.AddHeader("content-type", "application/json");
request.AddParameter("application/json", "   {\n        \"username\": \"test@test.com\",\n        \"password\": \"Lead\"\n    }", ParameterType.RequestBody);
IRestResponse response = client.Execute(request);



Response 

Status Ok(200)

Header:

content-type →application/json; charset=utf-8
date →Tue, 06 Oct 2020 13:56:11 GMT
server →Kestrel
transfer-encoding →chunked
x-powered-by →ASP.NET
x-sourcefiles →=?UTF-8?B?QzpcVXNlcnNcQWRtaW5cc291cmNlXHJlcG9zXEVNUF9XZWJBUElcV2ViQXBwbGljYXRpb24xXGFwaVxTdHVkZW50XEdldGFsbA==?=




Body:

[
    {
        "name": "Raman",
        "id": 2,
        "address": "Madurai",
        "role": "Lead",
        "department": "IT",
        "email": null,
        "skillSets": [],
        "dob": "1984-10-20T00:00:00",
        "doj": "1984-10-20T00:00:00",
        "isActive": true
    },
    {
        "name": "Raju",
        "id": 1,
        "address": "Chennai",
        "role": "Manager",
        "department": "IT",
        "email": null,
        "skillSets": [],
        "dob": "1984-10-20T00:00:00",
        "doj": "1984-10-20T00:00:00",
        "isActive": true
    }
]
















