/**
 * @Author S.P. Rupasinghe
 * @Created 10/7/2023
 * @Description Implement Employee Services
 **/
using AED_BE.Data;
using AED_BE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AED_BE.Utils;

namespace AED_BE.Services
{
    public class EmployeeService
    {
        private readonly IMongoCollection<Employee> _employeeCollection;

        public EmployeeService(IOptions<DatabaseSettings> settings) //constructor
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _employeeCollection = mongoDb.GetCollection<Employee>("Employee");
        }

        public async Task CreateAsync(Employee newEmployee) //create employee service
        {
             string hashedPassword = GenericHasher.ComputeHash(newEmployee.Password);
             newEmployee.Password = hashedPassword;
             await _employeeCollection.InsertOneAsync(newEmployee);
        }
        //public async Task CreateAsync (Employee newEmployee) => await _employeeCollection.InsertOneAsync(newEmployee);

        public async Task<Employee> GetEmployeeAsync(string id) => await _employeeCollection.Find(x => x.Id == id).FirstOrDefaultAsync(); //get one employee service

        public async Task<List<Employee>> GetEmployeesAsync() => await _employeeCollection.Find(_ => true).ToListAsync(); //get all employees service

        public async Task<Employee> GetEmployeeByEmailAsync(string email) => await _employeeCollection.Find(x => x.Email == email).FirstOrDefaultAsync(); //get employee by email service

        public async Task<Employee> GetEmployeeByRole(string role) => await _employeeCollection.Find(x => x.Role == role).FirstOrDefaultAsync(); //get employee by role

    }
}
