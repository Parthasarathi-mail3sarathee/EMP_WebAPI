using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication_Services.Service;
using WebApplication_Shared_Services.Model;
using WebApplication_Shared_Services.Service;
using WebApplication_WebAPI.Controllers;
using WebApplication_WebAPI.Filters;
using Xunit;

namespace XUnitTestStudent
{
    public class ModelStateUnitTests
    {
        private Mock<ILoggerManager> _logger;
        private Mock<IStudentService> _studentService;
        private Mock<IHttpContextAccessor> _httpContextAccessor;
        private Mock<IBaseAuth> _baseAuth;
        private Mock<ILogger<StudentController>> _logger_1;

        public ModelStateUnitTests()
        {
            _logger = new Mock<ILoggerManager>();
            _studentService = new Mock<IStudentService>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _baseAuth = new Mock<IBaseAuth>();
            _logger_1 = new Mock<ILogger<StudentController>>();

        }

        [Fact]
        public void Unit_Test_CarModel_ModelState_validations_are_thrown()
        {
            // Arrange 
            var controller = new ModelStateTestController();


            Student stud = new Student() { ID = 3, Address = "Chennai", Email = "ram@", Role = "Stud" };

            // Act
            var result = controller.TestTryValidateModel(stud);

            // Assert
            //Assert.IsFalse(result);

            var modelState = controller.ModelState;

           // Assert.AreEqual(2, modelState.Keys.Count);

           // Assert.IsTrue(modelState.Keys.Contains("Purchased"));
           // Assert.IsTrue(modelState["Purchased"].Errors.Count == 1);
           // Assert.AreEqual("The Purchased field is required.", modelState["Purchased"].Errors[0].ErrorMessage);

           // Assert.IsTrue(modelState.Keys.Contains("Colour"));
           // Assert.IsTrue(modelState["Colour"].Errors.Count == 1);
           // Assert.AreEqual("The Colour field is required.", modelState["Colour"].Errors[0].ErrorMessage);
        }
    }
}
