using AutoMapper;
using LusCore.User;
using Microsoft.IdentityModel.Tokens;
using Neo4jClient;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace LusService.UserService
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly GraphClient _client;
        private readonly IMapper _mapper;

        public UserService(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
            _client = new GraphClient(new Uri(_config["DatabaseServer"]), _config["DatabaseUser"], _config["DatabasePassword"]);
            _client.Connect();
        }
        public string GenerateToken(UserModel user)
        {
            if (user != null)
            {
                return GenerateJSONWebToken(user);
            }
            else return null;
        }

        public void Register(UserModel user)
        {
            var userDto = _mapper.Map<UserDto>(user);

            if (userDto == null)
            {
                return;
            }
            userDto.CustomerId = Guid.NewGuid().ToString();
            userDto.HashCode = HashPassword(user.Password);
            if (userDto.Type == null)
                userDto.Type = "customer";

            _client.Cypher
           .Create("(n:User {userDto})")
           .WithParams(new { userDto })
           .ExecuteWithoutResults();

        }

        public UserModel GetUser(int Id)
        {
            return new UserModel { Username = "hihud", HashCode = "123" };
        }


        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim("username", userInfo.Username),
                //new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("type",userInfo.Type)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserModel Login(UserModel user)
        {
            if (user == null)
                return null;
            user.HashCode = HashPassword(user.Password);
            var results = _client.Cypher
             .Match("(n: User)")
             .Where((UserDto n) => n.Username == user.Username && n.HashCode == user.HashCode)
             .Return(n => n.As<UserDto>())
             .Results;
            if (results.Count() > 0)
                return _mapper.Map<UserModel>(results.FirstOrDefault());
            else return null;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
