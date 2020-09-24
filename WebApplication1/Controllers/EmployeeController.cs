﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;
using WebApplication1.Model;
using System.Threading;
using Microsoft.AspNetCore.Cors;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllHeaders")]
    public class EmployeeController : ControllerBase
    {
        
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
          
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var empinfo = await _employeeService.GetAllEmployeeAsync(ct);
            if (empinfo == null)
            {
                return NotFound();
            }
            return Ok(empinfo);
        }

        [HttpGet]
        [Route("GetByID/{id}")]
        public async Task<IActionResult> GeByID(int id, CancellationToken ct)
        {
            var empinfo = await _employeeService.GetEmployeeByIDAsync(id,ct);
            if (empinfo == null)
            {
                return NotFound();
            }
            return Ok(empinfo);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee value, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var empinfo = await _employeeService.AddEmployee(value, ct);
            return Created("", empinfo);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee value, CancellationToken ct)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var empinfo = await _employeeService.UpdateEmployee(id,value, ct);
            return Ok(empinfo);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id, CancellationToken ct)
        {
            var empinfo = await _employeeService.GetEmployeeByIDAsync(id, ct);
            if (empinfo == null)
            {
                return NotFound();
            }
            var result = await _employeeService.DeleteEmployee(id, ct);
            return NoContent();
        }
    }
}