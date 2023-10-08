using AED_BE.Data;
using AED_BE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace AED_BE.Services
{
    public class ClientService
    {
        private readonly IMongoCollection<Client> _clientCollection;

        public ClientService(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _clientCollection = mongoDb.GetCollection<Client>(settings.Value.CollectionName);
        }


        public async Task<List<Client>> GetClientsAsync() => await _clientCollection.Find(_ => true).ToListAsync();
        
        public async Task<Client> GetClientAsync(String id) => await _clientCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Client newClient) => await _clientCollection.InsertOneAsync(newClient);

        public async Task UpdateAsync(string id, Client updatedClient) => await _clientCollection.ReplaceOneAsync(x => x.Id == id, updatedClient);

        public async Task DeleteAsync(string id) => await _clientCollection.DeleteOneAsync(x => x.Id == id);
    }
}
