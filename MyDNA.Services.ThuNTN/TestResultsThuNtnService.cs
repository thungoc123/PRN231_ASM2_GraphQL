using MyDNA.Repositories.ThuNTN;
using MyDNA.Repositories.ThuNTN.ModelExtensions;
using MyDNA.Repositories.ThuNTN.Models;


namespace MyDNA.Services.ThuNTN
{
    public class TestResultsThuNtnService : ITestResultsThuNtnService
    {
        private readonly TestResultsThuNtnRepository _testestResultsThuNtnRepository;
        public TestResultsThuNtnService() => _testestResultsThuNtnRepository = new TestResultsThuNtnRepository();
        public TestResultsThuNtnService(TestResultsThuNtnRepository testResultsThuNtnRepository)
        {
            _testestResultsThuNtnRepository = testResultsThuNtnRepository;
        }

        public async Task<int> CreateAsync(TestResultsThuNtn testResultsThuNtn)
        {
            return await _testestResultsThuNtnRepository.CreateAsync(testResultsThuNtn);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var testResult = await _testestResultsThuNtnRepository.GetByIdAsync(id);
            return await _testestResultsThuNtnRepository.RemoveAsync(testResult);
        }

        public async Task<List<TestResultsThuNtn>> GetAllAsync()
        {
            return await _testestResultsThuNtnRepository.GetAllAsync();
        }

        public async Task<PaginationResult<List<TestResultsThuNtn>>> GetAllWithPagingAsync(int currentPage, int pageSize)
        {
            return await _testestResultsThuNtnRepository.GetAllWithPagingAsync(currentPage, pageSize);
        }

        public async Task<TestResultsThuNtn> GetByIdAsync(int id)
        {
            return await _testestResultsThuNtnRepository.GetByIdAsync(id);
        }

        public async Task<List<TestResultsThuNtn>> SearchAsync(string resultStatus, string sampleMethod, string FullName)
        {
           return await _testestResultsThuNtnRepository.SearchAsync(resultStatus, sampleMethod, FullName);
        }

        public async Task<PaginationResult<List<TestResultsThuNtn>>> SearchWithPagingAsync(SearcTestResultThuNTN searchRequest)
        {
            return await _testestResultsThuNtnRepository.SearchWithPagingAsync(searchRequest);
        }

        public async Task<int> UpdateAsync(TestResultsThuNtn testResultsThuNtn)
        {
            return await _testestResultsThuNtnRepository.UpdateAsync(testResultsThuNtn);
        }
    }
}
