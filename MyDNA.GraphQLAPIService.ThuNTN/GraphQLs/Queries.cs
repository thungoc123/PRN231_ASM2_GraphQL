using MyDNA.Repositories.ThuNTN.Models;
using MyDNA.Services.ThuNTN;

namespace MyDNA.GraphQLAPIService.ThuNTN.GraphQLs
{
    public class Queries
    {
        //https://localhost:7070/graphql/
        private IServiceProviders _serviceProviders;
        public Queries(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }
        public async Task<List<TestResultsThuNtn>> GetAllTestResultsAsync()
        {
            return await _serviceProviders.testResultsThuNtnService.GetAllAsync();
        }
        public async Task<TestResultsThuNtn> GetTestResultByIdAsync(int id)
        {
            return await _serviceProviders.testResultsThuNtnService.GetByIdAsync(id);
        }
        public async Task<List<TestResultsThuNtn>> SearchTestResultsAsync(string resultStatus, string sampleMethod, string fullName)
        {
            return await _serviceProviders.testResultsThuNtnService.SearchAsync(resultStatus, sampleMethod, fullName);
        }
    }
}
