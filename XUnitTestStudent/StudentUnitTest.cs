using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_Services.Service;
using WebApplication_Shared_Services.Model;
using WebApplication_Shared_Services.Service;
using WebApplication_WebAPI.Controllers;
using WebApplication_WebAPI.Filters;
using Xunit;

namespace XUnit_Student_TestProject1
{
    public class StudentUnitTestController
    {
        private Mock<ILoggerManager> _logger;
        private Mock<IStudentService> _studentService;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private Mock<IBaseAuth> _baseAuth;
        private Mock<ILogger<StudentController>> _logger_1;

        public StudentUnitTestController()
        {
            _logger =  new Mock<ILoggerManager>();
            _studentService = new Mock<IStudentService>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _baseAuth = new Mock<IBaseAuth>();
            _logger_1 = new Mock<ILogger<StudentController>>();

    }

       

        [Fact]
        public void GeByID_Student_Return_Ok()
        {
            //Arrange  
            var controller = new StudentController(_logger_1.Object ,_logger.Object, _studentService.Object, _httpContextAccessor.Object, _baseAuth.Object);
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
            var controller = new StudentController(_logger_1.Object, _logger.Object, _studentService.Object, _httpContextAccessor.Object, _baseAuth.Object);
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
            var controller = new StudentController(_logger_1.Object, _logger.Object, _studentService.Object, _httpContextAccessor.Object, _baseAuth.Object);
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

        [Fact]
        public async void AddStudent_Return_BadRequest_ModalValidationn()
        {

            // Arrange
            var controller = new StudentController(_logger_1.Object, _logger.Object, _studentService.Object, _httpContextAccessor.Object, _baseAuth.Object);
            CancellationToken ct;
            controller.ModelState.AddModelError("fakeError", "fakeError");
            Student stud = new Student() { ID = 3, Address = "Chennai", Email = "ram@", Role = "Stud" };
            _studentService.Setup(x => x.AddStudent(stud, ct)).Returns(Task.FromResult(stud));

            // Act
            var response = await controller.AddStudent(stud,ct);

            // Assert
            //Assert.Null(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }

      
    }
}
