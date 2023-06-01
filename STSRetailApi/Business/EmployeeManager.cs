using STSRetailApi.DTO;
using STSRetailSoftwareData.Models;
using STSRetailSoftwareData.Repo;

namespace STSRetailApi.Business
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public async Task CreateEmployee(PostNewEmployeeRequest request)
        {
            if(request == null || string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Role))
            {
                throw new AggregateException("Missing Fields");
            }
            if(request.PayRate <= 10.00m)
            {
                throw new AggregateException("Payrate is too low");
            }
            EmployeeCredential credential = new()
            {
                Password = request.Password,
            };
            EmployeeProfile profile = new()
            {
                Name = request.Name,
                Role = request.Role,
                PayRate = request.PayRate
            };
            await this._employeeRepository.SaveEmployee(credential, profile);
        }

        public async Task UpdateEmployeePay(decimal? empId, decimal? payDifference)
        {
            if (empId == null || payDifference == null)
            {
                throw new AggregateException("Invalid Fields");
            }
            var profile = _employeeRepository.GetEmployeeProfile((decimal)empId).Result;
            profile.PayRate += (decimal)payDifference;
            await _employeeRepository.UpdateEmployeeInformation(profile);
        }

        public async Task UpdateEmployeeRole(decimal? empId, string? roleTitle)
        {
            if (empId == null || string.IsNullOrEmpty(roleTitle) || string.IsNullOrWhiteSpace(roleTitle) || roleTitle == null)
            {
                throw new AggregateException("Invalid Fields");
            }
            var profile = _employeeRepository.GetEmployeeProfile((decimal)empId).Result;
            profile.Role = roleTitle;
            await _employeeRepository.UpdateEmployeeInformation(profile);
        }
    }
}
