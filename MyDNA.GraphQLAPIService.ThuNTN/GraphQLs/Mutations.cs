using MyDNA.Repositories.ThuNTN.Models;
using MyDNA.Services.ThuNTN;

namespace MyDNA.GraphQLAPIService.ThuNTN.GraphQLs
{
    public class Mutations
    {
        //https://localhost:7070/graphql/
        private readonly IServiceProviders _serviceProviders;

        public Mutations(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        public async Task<int> CreateTestResultAsync(TestResultsThuNtn testResultsThuNtn)
        {
            return await _serviceProviders.testResultsThuNtnService.CreateAsync(testResultsThuNtn);
        }

        public async Task<int> UpdateTestResultAsync(TestResultsThuNtn testResultsThuNtn)
        {
            return await _serviceProviders.testResultsThuNtnService.UpdateAsync(testResultsThuNtn);
        }

        public async Task<bool> DeleteTestResultAsync(int id)
        {
            return await _serviceProviders.testResultsThuNtnService.DeleteAsync(id);
        }
    }
}
