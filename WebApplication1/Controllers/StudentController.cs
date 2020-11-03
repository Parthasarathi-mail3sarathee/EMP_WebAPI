using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApplication_WebAPI.Controllers.Base;
using WebApplication_WebAPI.Filters;
using WebApplication_Services.Service;
using WebApplication_Shared_Services.Model;
using WebApplication_Shared_Services.Service;
using WebApplication_Shared_Services.Contracts;
using Microsoft.Extensions.Logging;

namespace WebApplication_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllHeaders")]
    //[IdentityBasicAuthentication]
    public class StudentController : AuthBase
    {

        private readonly ILoggerManager _logger;

        private readonly ILogger<StudentController> _logger_1;
        private readonly IStudentService _studentService;



        public StudentController(ILogger<StudentController> logger_1, ILoggerManager logger,
                      IStudentService studentService, IHttpContextAccessor httpContextAccessor, IBaseAuth baseAuth) : base(logger, httpContextAccessor, baseAuth)
        {
            _logger_1 = logger_1;
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var ipaddress = base.GetRemoteIP();
            var permission = new List<string>() { "Leader", "Teacher", "Staff", "SuperUser" };
            var msg = await base.AuthenticationAndAuthorization(permission);
            if (msg != "Success") return Unauthorized();

            _logger_1.LogDebug(1, "NLog injected into HomeController");
            _logger_1.LogInformation("NLog injected into HomeController");

            _logger.LogInfo("Here is info message from the controller.");
            _logger.LogDebug("Here is debug message from the controller.");
            _logger.LogWarn("Here is warn message from the controller.");
            _logger.LogError("Here is error message from the controller.");


            var studinfo = await _studentService.GetAllStudentAsync(ct);
            if (studinfo == null)
            {
                return NotFound();
            }
            return Ok(studinfo);
        }

        [HttpGet]
        [ClaimRequirement(new string[] { "Leader", "Teacher" })]
        [Route("GetByID/{id}")]
        public async Task<IActionResult> GeByID(int id, CancellationToken ct)
        {
            var studinfo = await _studentService.GetStudentByIDAsync(id, ct);
            if (studinfo == null)
            {
                return NotFound();
            }
            return Ok(studinfo);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student value, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var studinfo = await _studentService.AddStudent(value, ct);
            return Created("", studinfo);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Student value, CancellationToken ct)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var empinfo = await _studentService.UpdateStudent(id, value, ct);
            return Ok(empinfo);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken ct)
        {
            var studinfo = await _studentService.GetStudentByIDAsync(id, ct);
            if (studinfo == null)
            {
                return NotFound();
            }
            var result = await _studentService.DeleteStudent(id, ct);
            return NoContent();
        }

       
    }
}