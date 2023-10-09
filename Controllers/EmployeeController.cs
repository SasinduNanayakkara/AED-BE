using AED_BE.Models;
using AED_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace AED_BE.Controllers
{
        [Route("api/employee")]
        [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee newEmployee)
        {
            await _employeeService.CreateAsync(newEmployee);
            return CreatedAtAction(nameof(Get), new { id = newEmployee.Id }, newEmployee);
        }

        [HttpGet]
        public async Task<List<Employee>> Get()
        {
            return await _employeeService.GetEmployeesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetOneEmployeeById(string id)
        {
            Employee employee = await _employeeService.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
