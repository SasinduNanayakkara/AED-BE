/**
 * @Author S.P. Rupasinghe
 * @Created 10/6/2023
 * @Description Implement Login service
 **/
using System.Text;
using AED_BE.Data;
using AED_BE.DTO;
using AED_BE.DTO.RequestDto;
using AED_BE.Models;
using AED_BE.Utils;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AED_BE.Services
{
    public class LoginService
    {


        private readonly IConfiguration _config;
        private readonly IMongoCollection<Employee> _employeeCollection;
        private readonly IMongoCollection<Client> _clientCollection;

        public LoginService(IOptions<DatabaseSettings> settings, IConfiguration config) //contructor
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _clientCollection = mongoDb.GetCollection<Client>("Client");
            _employeeCollection = mongoDb.GetCollection<Employee>("Employee");
            _config = config;

        }


        public async Task<String> ClientLogin(ClientLoginRequest loginRequest) //login service
        {
            String token = null;
            UserDto user = await AuthenticateClient(loginRequest);

            if (user != null)
            {
                token = GenerateJSONWebToken(user);
              
            }
            return token;
        }

        public async Task<String> EmployeeLogin(EmployeeLoginRequest loginRequest) //employee logice service
        {
            String token = null;
            UserDto user = await AuthenticateEmployee(loginRequest);

            if (user != null)
            {
                token = GenerateJSONWebToken(user);

            }
            return token;
        }

        private string GenerateJSONWebToken(UserDto user) //generate access token
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims: GetTokenClaims(user),
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static IEnumerable<Claim> GetTokenClaims(UserDto user)
        {
            return new List<Claim>
                {
                    new Claim("email", user.email),
                    new Claim("nic", user.nic),
                    new Claim("role", user.role),
                    //Add more custom claims
                };
        }

        private async Task<UserDto> AuthenticateClient(ClientLoginRequest loginRequest) //client authenticate
        {

            UserDto user = null;
            Client isClient = await _clientCollection.Find(x => x.NIC == loginRequest.nic).FirstOrDefaultAsync();

            if (isClient != null)
            {
                if (CheckPassword(isClient.Password, loginRequest.password))
                {
                    user = new UserDto(
                        id: isClient.Id,
                        nic: isClient.NIC,
                        email: isClient.Email,
                        role: "cleint");
                    return user;
                }
                else
                {
                    return user;
                }
            }
            else
            {
                return user;
            }

        }

        private async Task<UserDto> AuthenticateEmployee(EmployeeLoginRequest loginRequest) //employee authenticate
        {

            UserDto user = null;
            Employee isEmployee = await _employeeCollection.Find(x => x.Email == loginRequest.email).FirstOrDefaultAsync();

            if (isEmployee != null)
            {
                if (CheckPassword(isEmployee.Password, loginRequest.password))
                {
                    user = new UserDto(
                        id: isEmployee.Id,
                        nic: "",
                        email: isEmployee.Email,
                        role: isEmployee.Role);
                    return user;
                }
                else
                {
                    return user;
                }
            }
            else
            {
                return user;
            }

        }

        private Boolean CheckPassword(String hash, String password) //password matching
        {
            string hashedPassword = GenericHasher.ComputeHash(password);
            if (hash == hashedPassword) { return true; } else { return false; }
        }
    }
}
