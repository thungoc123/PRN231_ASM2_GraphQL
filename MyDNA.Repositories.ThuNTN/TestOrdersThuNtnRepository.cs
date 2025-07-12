using MyDNA.Repositories.ThuNTN.Basic;
using MyDNA.Repositories.ThuNTN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDNA.Repositories.ThuNTN
{
    public class TestOrdersThuNtnRepository : GenericRepository<TestOrdersThuNtn>
    {
        public TestOrdersThuNtnRepository() => _context ??= new DBContext.SU25_PRN231_SE171992_G1_MyDNAContext();
        public TestOrdersThuNtnRepository(DBContext.SU25_PRN231_SE171992_G1_MyDNAContext context) 
        {
            _context = context;
        }
    }
}
