using AED_BE.Data;
using AED_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace AED_BE.Services
{
    public class LoginService
    {
        private readonly IMongoCollection<Client> _clientCollection;
        private readonly IMongoCollection<Employee> _employeeCollection;
        private readonly ClientService _clientService;
        private readonly EmployeeService _employeeService;

        public LoginService(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _clientCollection = mongoDb.GetCollection<Client>("Client");
            _employeeCollection = mongoDb.GetCollection<Employee>("Employee");
        }

        public ClientService Get_clientService()
        {
            return _clientService;
        }

        //public async Task LoginService(string email, string password, ClientService _clientService)
        //{
        //    Client isClient = await _clientService.GetClientByEmail(email);
        //    Employee isEmployee = await  _employeeService.GetEmployeeByEmailAsync(email);
        //    if (isClient == null && isEmployee == null)
        //    {
        //        return;
        //    }
        //    if (isClient != null && isEmployee == null)
        //    {
        //        if (isClient.password == password)
        //        {

        //        }
        //    }
        //}
    }
}
