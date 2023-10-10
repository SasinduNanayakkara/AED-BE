using AED_BE.Models;
using AED_BE.Services;
using Microsoft.AspNetCore.Mvc;

namespace AED_BE.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController
    {
        private readonly ClientService _clientService;
        private readonly EmployeeService _employeeService;

        public LoginController(ClientService clientService, EmployeeService employeeService)
        {
            _clientService = clientService;
            _employeeService = employeeService;
        }

        //[HttpPost]
        //public async Task<ActionResult<Object>> Post(string email, string password)
        //{

        //}

    }
}
