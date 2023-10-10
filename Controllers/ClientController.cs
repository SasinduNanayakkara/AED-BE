using AED_BE.Models;
using AED_BE.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AED_BE.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/client
        [HttpGet]
        public async Task<List<Client>> Get()
        {
            return await _clientService.GetClientsAsync();
        }

        // GET api/client/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetOneClient(string id)
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
        public async Task<ActionResult<Client>> Post(Client newClient)
        {
            await _clientService.CreateAsync(newClient);
            return CreatedAtAction(nameof(Get), new { id = newClient.Id }, newClient);
        }

        // PUT api/client/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Client updatedClient)
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
        public async Task<ActionResult> Delete(string id)
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
