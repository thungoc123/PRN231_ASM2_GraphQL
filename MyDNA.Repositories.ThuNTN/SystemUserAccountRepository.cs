using Microsoft.EntityFrameworkCore;
using MyDNA.Repositories.ThuNTN.Basic;
using MyDNA.Repositories.ThuNTN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDNA.Repositories.ThuNTN
{
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountRepository() => _context ??= new DBContext.SU25_PRN231_SE171992_G1_MyDNAContext();

        public SystemUserAccountRepository(DBContext.SU25_PRN231_SE171992_G1_MyDNAContext context)
        {
            _context = context;
        }

        public async Task<SystemUserAccount> GetUserAccountAsync(string userName, string password)
        {
            return await _context.SystemUserAccounts.FirstOrDefaultAsync(x => x.Email == userName && x.Password == password && x.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password && x.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(x => x.Phone == userName && x.Password == password && x.IsActive == true);
            //return await _context.SystemUserAccounts.FirstOrDefaultAsync(x => x.EmployeeCode == userName && x.Password == password && x.IsActive == true);
        }
    }
}
