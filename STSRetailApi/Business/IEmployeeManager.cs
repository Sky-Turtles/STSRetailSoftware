using STSRetailApi.DTO;

namespace STSRetailApi.Business
{
    public interface IEmployeeManager
    {
        Task CreateEmployee(PostNewEmployeeRequest request);
        Task UpdateEmployeePay(decimal? empId, decimal? payDifference);
        Task UpdateEmployeeRole(decimal? empId, string? roleTitle);
    }
}
