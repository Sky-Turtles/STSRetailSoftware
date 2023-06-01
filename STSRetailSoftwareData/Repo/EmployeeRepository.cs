using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using STSRetailSoftwareData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STSRetailSoftwareData.Repo
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly StsRetailErpContext _context;
        private readonly string ConnectionString;

        public EmployeeRepository(IOptions<Configuration.Configuration> options)
        {
            this.ConnectionString = options.Value.ConnectionString;
            this._context = new(this.ConnectionString);
        }

        public async Task SaveEmployee(EmployeeCredential employeeCredential, EmployeeProfile employeeProfile)
        {
            using (var dbTransaction =  await _context.Database.BeginTransactionAsync())
            {
                await InsertEmployeeCredentials(employeeCredential);

                var employeeIdGenerated = _context.EmployeeCredentials.Where(e => e.Password == employeeCredential.Password).FirstOrDefault().EmpCredId;

                await UpdatingEmployeeIdForCreatingProfile(employeeProfile, employeeIdGenerated);

                await dbTransaction.CommitAsync();
            }
        }

        public async Task UpdateEmployeeInformation(EmployeeProfile employeeProfile)
        {
            await _context.EmployeeProfiles.AddAsync(employeeProfile);
            await _context.SaveChangesAsync();  
        }

        public async Task<EmployeeProfile> GetEmployeeProfile(decimal employeeId)
        {
            return _context.EmployeeProfiles.Where(e => e.EmpProfileId == employeeId).FirstOrDefaultAsync().Result;
        }

        private async Task InsertEmployeeCredentials(EmployeeCredential employeeCredential)
        {
            await _context.EmployeeCredentials.AddAsync(employeeCredential);
            await _context.SaveChangesAsync();
        }

        private async Task InsertEmployeeProfile(EmployeeProfile employeeProfile)
        {
            await _context.EmployeeProfiles.AddAsync(employeeProfile);
            await _context.SaveChangesAsync();
        }

        private async Task UpdatingEmployeeIdForCreatingProfile(EmployeeProfile employeeProfile, decimal employeeProfileId)
        {
            employeeProfile.EmpProfileId = employeeProfileId;
            await InsertEmployeeProfile(employeeProfile);
        }

    }
}
