using Microsoft.EntityFrameworkCore;
using MyDNA.Repositories.ThuNTN.Basic;
using MyDNA.Repositories.ThuNTN.ModelExtensions;
using MyDNA.Repositories.ThuNTN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDNA.Repositories.ThuNTN
{
    public class TestResultsThuNtnRepository : GenericRepository<TestResultsThuNtn>
    {
        public TestResultsThuNtnRepository() => _context ??= new DBContext.SU25_PRN231_SE171992_G1_MyDNAContext();
        // câu này có nghĩa là nếu nó null thì sẽ tạo nó. 

        public TestResultsThuNtnRepository(DBContext.SU25_PRN231_SE171992_G1_MyDNAContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<TestResultsThuNtn>> GetAllAsync()
        {
           var testResult =  await _context.TestResultsThuNtns.Include(d => d.Order).ToListAsync();
            return testResult ?? new List<TestResultsThuNtn>();
        }

        public async Task<TestResultsThuNtn> GetByIdAsync(int id)
        {
            var testResult = await _context.TestResultsThuNtns.Include(d => d.Order).FirstOrDefaultAsync(d => d.TestResultThuNtnid == id);
            return testResult ?? new TestResultsThuNtn();
        }

        public async Task<List<TestResultsThuNtn>> SearchAsync(string resultStatus, string sampleMethod, string FullName)
        {
            var testResult = await _context.TestResultsThuNtns.Include(d => d.Order).Where(d => d.ResultStatus.Contains(resultStatus) && d.Order.SampleMethod.Contains(sampleMethod) && d.Order.User.FullName.Contains(FullName)).ToListAsync();
            return testResult  ?? new List<TestResultsThuNtn>();
        }

        public async Task<PaginationResult<List<TestResultsThuNtn>>> GetAllWithPagingAsync(int currentPage, int pageSize)
        {
            var testResults = await this.GetAllAsync();

            var totalItems = testResults.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagedResults = testResults.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<TestResultsThuNtn>> 
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                Items = pagedResults,
            };
            return result;
        }

        public async Task<PaginationResult<List<TestResultsThuNtn>>> SearchWithPagingAsync (SearcTestResultThuNTN searchRequest)
        {
            var testResults = await this.SearchAsync(searchRequest.ResultStatus, searchRequest.SampleMethod, searchRequest.FullName);

            var totalItems = testResults.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / searchRequest.PageSize);

            testResults = testResults.Skip((searchRequest.CurrentPage - 1) * searchRequest.PageSize).Take(searchRequest.PageSize).ToList();
            var result = new PaginationResult<List<TestResultsThuNtn>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = searchRequest.CurrentPage,
                Items = testResults,
            };
            return result;
        }
    }
}
