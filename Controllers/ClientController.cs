/**
 * @Author E.M.S.D. Ekanayake
 * @Created 10/1/2023
 * @Description Implement Client API Controllers
 **/

using AED_BE.DTO.RequestDto;
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
            String token = await _loginService.ClientLogin(loginRequest);
            if (token != null)
            {
                response = Ok(new { access_token = token });
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetOneClient(string id) //Get one client
        {
            Client client = await _clientService.GetClientAsync(id);
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
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Client updatedClient) //update Client
        {
            Client client = await _clientService.GetClientAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            updatedClient.Id = client.Id;
            await _clientService.UpdateAsync(id, updatedClient);

            return Ok(updatedClient);
        }

        // DELETE api/client/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id) // Delete Client
        {
            Client client = await _clientService.GetClientAsync(id);
            if (client == null)
            {
                return NotFound("There is no client with this id: " + id);
            }

            await _clientService.DeleteAsync(id);
            return Ok("Deleted successfully");
        }
    }
}
