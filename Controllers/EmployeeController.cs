using AED_BE.DTO.RequestDto;
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
        private readonly LoginService _loginService;

        public EmployeeController(EmployeeService employeeService, LoginService loginService)
        {
            _employeeService = employeeService;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] EmployeeLoginRequest loginRequest)
        {
            IActionResult response = Unauthorized();
            String token = await _loginService.EmployeeLogin(loginRequest);
            if (token != null)
            {
                response = Ok(new { access_token = token });
            }
            return response;

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
