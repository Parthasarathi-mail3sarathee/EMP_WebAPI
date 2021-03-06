using Microsoft.AspNetCore.Mvc;
using System.Threading;
using WebApplication_Services.Service;
using WebApplication_Shared_Services.Model;
using WebApplication_WebAPI.Controllers;
using Xunit;

namespace XUnit_Employee_TestProject1
{
    public class EmployeeUnitTestController
    {

        private IEmployeeService service;

        public EmployeeUnitTestController()
        {

            service = new EmployeeService();

        }

        [Fact]
        public async void GetAll_Employee_Return_Ok()
        {
            //Arrange  
            var controller = new EmployeeController(service);
            CancellationToken ct;
            //Act  
            var data = await controller.GetAll(ct);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void GeByID_Employee_Return_Ok()
        {
            //Arrange  
            var controller = new EmployeeController(service);
            CancellationToken ct;
            int id = 1;

            //Act  
            var data = await controller.GeByID(id, ct);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }


        [Fact]
        public async void GeByID_Employee_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new EmployeeController(service);
            CancellationToken ct;
            int id = 4;

            //Act  
            var data = await controller.GeByID(id, ct);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void GeByID_Employee_Return_MatchResult()
        {
            //Arrange  
            var controller = new EmployeeController(service);
            CancellationToken ct;
            int id = 2;

            //Act  
            var data = await controller.GeByID(id, ct);

            var objectResponse = data as ObjectResult; //Cast to desired type
            var emp = objectResponse.Value as Employee;

            //Assert  

            Assert.Equal(200, objectResponse.StatusCode);
            Assert.Equal(2,emp.ID);
            Assert.Equal("Raman",emp.Name);
            Assert.Equal("Madurai",emp.Address);

        }
    }
}
