using MyDNA.Repositories.ThuNTN;
using MyDNA.Repositories.ThuNTN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDNA.Services.ThuNTN
{
    public class TestOrdersThuNtnService
    {
        private readonly TestOrdersThuNtnRepository _testOrdersThuNtnRepository;

        public TestOrdersThuNtnService() => 
       
            _testOrdersThuNtnRepository = new TestOrdersThuNtnRepository();

        public TestOrdersThuNtnService(TestOrdersThuNtnRepository testOrdersThuNtnRepository)
        {
            _testOrdersThuNtnRepository = testOrdersThuNtnRepository;
        }

        public async Task<List<TestOrdersThuNtn>> GetAllAsync()
        {
            return await _testOrdersThuNtnRepository.GetAllAsync();
        }
    }
}
