using STSRetailSoftwareData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STSRetailSoftwareData.Repo
{
    public interface IEmployeeRepository
    {
        Task SaveEmployee(EmployeeCredential employeeCredential, EmployeeProfile employeeProfile);
        Task UpdateEmployeeInformation(EmployeeProfile employeeProfile);
        Task<EmployeeProfile> GetEmployeeProfile(decimal employeeId);
    }
}
