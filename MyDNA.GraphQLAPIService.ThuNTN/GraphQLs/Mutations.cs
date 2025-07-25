using Microsoft.IdentityModel.Tokens;
using MyDNA.GraphQLAPIService.ThuNTN.Models;
using MyDNA.Repositories.ThuNTN.Models;
using MyDNA.Services.ThuNTN;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;


namespace MyDNA.GraphQLAPIService.ThuNTN.GraphQLs
{
    public class Mutations
    {
        //https://localhost:7070/graphql/
        private readonly IServiceProviders _serviceProviders;
        private readonly IConfiguration _config;
       
        public Mutations(IConfiguration config, IServiceProviders serviceProviders)
        {
            _config = config;

            _serviceProviders = serviceProviders;
        }

        public async Task<int> CreateTestResultAsync(Repositories.ThuNTN.Models.TestResultsThuNtn testResultsThuNtn)
        {
            // Tạo một object mới, không gán TestResultThuNtnid
            var newTestResult = new Repositories.ThuNTN.Models.TestResultsThuNtn
            {
                OrderId = testResultsThuNtn.OrderId,
                ResultVersion = testResultsThuNtn.ResultVersion,
                ResultSummary = testResultsThuNtn.ResultSummary,
                ResultDetail = testResultsThuNtn.ResultDetail,
                ResultFileUrl = testResultsThuNtn.ResultFileUrl,
                IssuedBy = testResultsThuNtn.IssuedBy,
                CompletedAt = testResultsThuNtn.CompletedAt,
                CreatedAt = testResultsThuNtn.CreatedAt,
                ResultStatus = testResultsThuNtn.ResultStatus,
                Order = null // Hoặc testResultsThuNtn.Order nếu cần liên kết navigation
            };

            var created = await _serviceProviders.testResultsThuNtnService.CreateAsync(newTestResult);
            return created;
        }


        public async Task<int> UpdateTestResultAsync(Repositories.ThuNTN.Models.TestResultsThuNtn testResultsThuNtn)
        {
            return await _serviceProviders.testResultsThuNtnService.UpdateAsync(testResultsThuNtn);
        }

        public async Task<bool> DeleteTestResultAsync(int id)
        {
            return await _serviceProviders.testResultsThuNtnService.DeleteAsync(id);
        }


        public async Task<LoginResponse> Login(string username, string password)
        {
            var user = await _serviceProviders.SystemUserAccountService.GetUserAccountAsync(username, password);
            if (user == null) return null;

            var token = GenerateJSONWebToken(user);

            return new LoginResponse
            {
                Token = token,
                FullName = user.FullName,
                Role = user.RoleId
            };

        }

        // helper 
        private string GenerateJSONWebToken(Repositories.ThuNTN.Models.SystemUserAccount systemUserAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                new(ClaimTypes.Name, systemUserAccount.UserName),
                //new(ClaimTypes.Email, systemUserAccount.Email),
                new(ClaimTypes.Role, systemUserAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
