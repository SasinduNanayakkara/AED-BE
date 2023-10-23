/**
 * @Author E.M.S.D. Ekanayake
 * @Created 10/7/2023
 * @Description Implement Client Services
 **/
using AED_BE.Data;
using AED_BE.Models;
using AED_BE.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace AED_BE.Services
{
    public class ClientService
    {
        private readonly IMongoCollection<Client> _clientCollection;

        public ClientService(IOptions<DatabaseSettings> settings) //constructor
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _clientCollection = mongoDb.GetCollection<Client>("Client");
        }


        public async Task<List<Client>> GetClientsAsync() => await _clientCollection.Find(_ => true).ToListAsync(); //Get all clients service

        public async Task<Client> GetClientAsync(String nic) => await _clientCollection.Find(x => x.NIC == nic).FirstOrDefaultAsync(); //get one client service

        public async Task CreateAsync(Client newClient) //create client service
        {

            string hashedPassword = GenericHasher.ComputeHash(newClient.Password);
            newClient.Password = hashedPassword;
            await _clientCollection.InsertOneAsync(newClient);

        }

        public async Task<Client> GetClientByEmail(string email) => await _clientCollection.Find(x => x.Email == email).FirstOrDefaultAsync(); //get cleints using email

        public async Task UpdateAsync(string nic, Client updatedClient) => await _clientCollection.ReplaceOneAsync(x => x.NIC == nic, updatedClient); //update client service

        public async Task DeleteAsync(string nic) => await _clientCollection.DeleteOneAsync(x => x.NIC == nic); //delete client ervice

        public async Task DeactivateClient(string nic)
        {
            var user = Builders<Client>.Filter.Eq("nic", nic);
            var update = Builders<Client>.Update.Set("isActive", false);

            await _clientCollection.UpdateOneAsync(user, update); //deactivate client account
        }

        public async Task ActivateClient(string nic)
        {
            var user = Builders<Client>.Filter.Eq("nic", nic);
            var update = Builders<Client>.Update.Set("isActive", true);
            await _clientCollection.UpdateOneAsync(user, update); //activate client account

        }
    }
}
