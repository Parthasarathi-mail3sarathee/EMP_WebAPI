using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using WebApplication1.Controllers;
using WebApplication1.Model;
using WebApplication1.Service;
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
            Assert.Equal(emp.ID,2);
            Assert.Equal(emp.Name, "Raman");
            Assert.Equal(emp.Address, "Madurai");

        }
    }
}
