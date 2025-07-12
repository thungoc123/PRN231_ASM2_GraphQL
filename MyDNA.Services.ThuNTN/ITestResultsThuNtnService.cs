using MyDNA.Repositories.ThuNTN.ModelExtensions;
using MyDNA.Repositories.ThuNTN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDNA.Services.ThuNTN
{
    public interface ITestResultsThuNtnService
    {
        Task<List<TestResultsThuNtn>> GetAllAsync();
        Task<TestResultsThuNtn> GetByIdAsync(int id);
        Task<List<TestResultsThuNtn>> SearchAsync(string resultStatus, string sampleMethod, string FullName);
        Task<int> CreateAsync(TestResultsThuNtn testResultsThuNtn);
        Task<int> UpdateAsync(TestResultsThuNtn testResultsThuNtn);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResult<List<TestResultsThuNtn>>> GetAllWithPagingAsync(int currentPage, int pageSize);
        Task<PaginationResult<List<TestResultsThuNtn>>> SearchWithPagingAsync(SearcTestResultThuNTN searchRequest);

    }
}
