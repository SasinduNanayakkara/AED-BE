/**
 * @Author E.M.S.D. Ekanayake
 * @Created 10/1/2023
 * @Description Implement Client API Controllers
 **/

using AED_BE.DTO.RequestDto;
using AED_BE.DTO.ResponseDto;
using AED_BE.Models;
using AED_BE.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AED_BE.Controllers
{

    [Route("api/client")] //Route
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly ClientService _clientService;
        private readonly LoginService _loginService;

        public ClientController(ClientService clientService, LoginService loginService) //Constructor
        {
            _clientService = clientService;
            _loginService = loginService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] ClientLoginRequest loginRequest) //Login
        {
            IActionResult response = Unauthorized();
            LoginResponse user = await _loginService.ClientLogin(loginRequest);
            if (user.accessToken != null)
            {
                response = Ok(new { access_token = user.accessToken, user = user.userDetails });
            }
            return response;

        }

        // GET: api/client
        [HttpGet]
        public async Task<List<Client>> Get() //Get all Clients
        {
            return await _clientService.GetClientsAsync();
        }

        // GET api/client/5
        [HttpGet("{nic}")]
        public async Task<ActionResult<Client>> GetOneClient(string nic) //Get one client
        {
            Client client = await _clientService.GetClientAsync(nic);
            if (client == null)
            {
                return NotFound();
            }
            return client;
        }

        // POST api/client
        [HttpPost]
        public async Task<ActionResult<Client>> Post(Client newClient) // Create Client
        {
            await _clientService.CreateAsync(newClient);
            return CreatedAtAction(nameof(Get), new { id = newClient.Id }, newClient);
        }

        // PUT api/client/5
        [HttpPut("{nic}")]
        public async Task<ActionResult> Put(string nic, Client updatedClient) //update Client
        {
            Client client = await _clientService.GetClientAsync(nic);
            if (client == null)
            {
                return NotFound();
            }

            updatedClient.Id = client.Id;
            await _clientService.UpdateAsync(nic, updatedClient);

            return Ok(updatedClient);
        }

        // DELETE api/client/5
        [HttpDelete("{nic}")]
        public async Task<ActionResult> Delete(string nic) // Delete Client
        {
            Client client = await _clientService.GetClientAsync(nic);
            if (client == null)
            {
                return NotFound("There is no client with this nic: " + nic);
            }

            await _clientService.DeleteAsync(nic);
            return Ok("Deleted successfully");
        }

        [HttpGet("deactivate/{nic}")]
        public async Task<ActionResult> Deactivate(string nic)
        {
            await _clientService.DeactivateClient(nic);
            return Ok("Deactivated");
        }


        [HttpGet("activate/{nic}")]
        public async Task<ActionResult> Activate(string nic)
        {
            await _clientService.ActivateClient(nic);
            return Ok("Activated");
        }
    }
}
