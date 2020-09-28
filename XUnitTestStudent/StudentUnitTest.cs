using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using WebApplication1.Controllers;
using WebApplication1.Model;
using WebApplication1.Service;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using WebApplication1.Filters;
using System.Threading.Tasks;

namespace XUnit_Student_TestProject1
{
    public class StudentUnitTestController
    {
        private Mock<ILoggerManager> _logger;
        private Mock<IStudentService> _studentService;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private Mock<IBaseAuth> _baseAuth;

        public StudentUnitTestController()
        {
            _logger =  new Mock<ILoggerManager>();
            _studentService = new Mock<IStudentService>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _baseAuth = new Mock<IBaseAuth>();


        }

        [Fact]
        public async void GetAll_Student_Return_Ok()
        {
            //Arrange  
            var controller = new StudentController(_logger.Object,_studentService.Object, _httpContextAccessor.Object, _baseAuth.Object);
            CancellationToken ct;

            //Act  
           // var data = await controller.GetAll(ct);

            //Assert  
           // Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GeByID_Student_Return_Ok()
        {
            //Arrange  
            var controller = new StudentController(_logger.Object, _studentService.Object, _httpContextAccessor.Object, _baseAuth.Object);
            CancellationToken ct;
            int id = 1;
            Student stud = new Student() { ID = 1, Name = "RAM", Address = "Chennai", Email = "ram@test.com", Role = "Stud" };
            _studentService.Setup(x => x.GetStudentByIDAsync(id, ct)).Returns(Task.FromResult(stud));

            //Act  
            var data = controller.GeByID(id, ct).Result;

            //Assert  
            var objectResponse = data as ObjectResult; //Cast to desired type
                                                       // var stud = objectResponse.Value as Student;
            Assert.IsType<OkObjectResult>(data);
        }


        [Fact]
        public  void GeByID_Student_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new StudentController(_logger.Object, _studentService.Object, _httpContextAccessor.Object, _baseAuth.Object);
            CancellationToken ct;
            int id = 4;
            _studentService.Setup(x => x.GetStudentByIDAsync(id, ct)).Returns(Task.FromResult<Student>(null));

            //Act  
            var data =  controller.GeByID(id, ct).Result;

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void GeByID_Student_Return_MatchResult()
        {
            //Arrange  
            var controller = new StudentController(_logger.Object, _studentService.Object, _httpContextAccessor.Object, _baseAuth.Object);
            CancellationToken ct;
            int id = 2;
            Student stud = new Student() { ID = 2, Name = "RAM", Address = "Chennai", Email = "ram@test.com", Role = "Stud" };
            _studentService.Setup(x => x.GetStudentByIDAsync(id, ct)).Returns(Task.FromResult(stud));

            //Act  
            var data = await controller.GeByID(id, ct);

            var objectResponse = data as ObjectResult; //Cast to desired type
            var studres = objectResponse.Value as Student;

            //Assert  

            Assert.Equal(200, objectResponse.StatusCode);
            Assert.Equal(stud.ID, studres.ID);
            Assert.Equal(stud.Name, studres.Name);
            Assert.Equal(stud.Address, studres.Address);

        }
    }
}