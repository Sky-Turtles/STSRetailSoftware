using STSRetailApi.Business;
using STSRetailApi.DTO;
using STSRetailSoftwareData.Models;
using STSRetailSoftwareData.Repo;

namespace STSRetailTests
{
    public class EmployeeManagerTests
    {
        private Mock<IEmployeeRepository> _employeeRepository = new();
        private IEmployeeManager _employeeManager;
        public EmployeeManagerTests() { }

        [Fact]
        public async Task CreateEmployee_IsSuccessful_ShouldCreate()
        {
            PostNewEmployeeRequest request = new()
            { 
                Password = "Password123!",
                Name = "Test User",
                Role = "Employee",
                PayRate = 12.45M
            };

            _employeeRepository.Setup(repo => repo.SaveEmployee(It.IsAny<EmployeeCredential>(), It.IsAny<EmployeeProfile>()));

            _employeeManager = new EmployeeManager(_employeeRepository.Object);

            await _employeeManager.CreateEmployee(request);

            _employeeRepository.Verify(repo => repo.SaveEmployee(It.IsAny<EmployeeCredential>(), It.IsAny<EmployeeProfile>()), Times.Once());
            _employeeRepository.Invocations.Clear();
        }

        [Theory]
        [InlineData("", "Test User", "Employee", 12.45)]
        [InlineData("Password123!", "", "Employee", 12.45)]
        [InlineData("Password123!", "Test User", "", 12.45)]
        public async Task CreateEmployee_NullOrWhiteInFields_ShouldThrowException(string password, string name, string role, decimal payRate)
        {
            PostNewEmployeeRequest request = new()
            {
                Password = password,
                Name = name,
                Role = role,
                PayRate = payRate
            };
            _employeeManager = new EmployeeManager(_employeeRepository.Object);
            Assert.Throws<AggregateException>(() => _employeeManager.CreateEmployee(request).Wait());
            _employeeRepository.Invocations.Clear();
        }

        [Fact]
        public async Task CreateEmployee_IfPayRateUnderMin_ShouldThrowException()
        {
            PostNewEmployeeRequest request = new()
            {
                Password = "Password123!",
                Name = "Test User",
                Role = "Employee",
                PayRate = 9.55M
            };
            _employeeManager = new EmployeeManager(_employeeRepository.Object);
            Assert.Throws<AggregateException>(() => _employeeManager.CreateEmployee(request).Wait());
            _employeeRepository.Invocations.Clear();
        }

        [Fact]
        public async Task UpdateEmployeePay_IsSuccessful_ShouldUpdate()
        {
            decimal empId = 1000000;
            decimal payIncrease = 0.25m;

            _employeeRepository.Setup(repo => repo.GetEmployeeProfile(empId)).Returns(Task.FromResult(new EmployeeProfile()
            {
                EmpProfileId = empId,
                Name = "Test User",
                Role = "Employee",
                PayRate = 11.50M
            }));
            _employeeRepository.Setup(repo => repo.UpdateEmployeeInformation(It.IsAny<EmployeeProfile>()));

            _employeeManager = new EmployeeManager(_employeeRepository.Object);

            await _employeeManager.UpdateEmployeePay(empId, payIncrease);

            _employeeRepository.Verify(repo => repo.UpdateEmployeeInformation(It.IsAny<EmployeeProfile>()), Times.Once);
            _employeeRepository.Invocations.Clear();
        }

        [Theory]
        [InlineData(null, "0.25")]
        [InlineData("1000000", null)]
        public async Task UpdateEmployeePay_InvalidField_ShouldThrowException(string empId, string payDif)
        {
            decimal correctEmpId = Convert.ToDecimal(empId);
            decimal correctPayDif = Convert.ToDecimal(payDif);
            _employeeManager = new EmployeeManager(_employeeRepository.Object);
            Assert.Throws<AggregateException>(() => _employeeManager.UpdateEmployeePay(correctEmpId, correctPayDif).Wait());
        }

        [Fact]
        public async Task UpdateEmployeeRole_IsSuccessful_ShouldUpdate()
        {
            decimal empId = 1000000;
            string roleTitle = "Manager";
            _employeeRepository.Setup(repo => repo.GetEmployeeProfile(empId)).Returns(Task.FromResult(new EmployeeProfile()
            {
                EmpProfileId = empId,
                Name = "Test User",
                Role = "Employee",
                PayRate = 11.50M
            }));
            _employeeRepository.Setup(repo => repo.UpdateEmployeeInformation(It.IsAny<EmployeeProfile>()));
            _employeeManager = new EmployeeManager(_employeeRepository.Object);

            await _employeeManager.UpdateEmployeeRole(empId, roleTitle);

            _employeeRepository.Verify(repo => repo.UpdateEmployeeInformation(It.IsAny<EmployeeProfile>()), Times.Once);
            _employeeRepository.Invocations.Clear();
        }

        [Theory]
        [InlineData(null, "Manager")]
        [InlineData("1000000", "")]
        [InlineData("1000000", "     ")]
        [InlineData("1000000", null)] //TODO Even though the method is nullable and double checking if a null gets passed down, it's not Recongizing the fix.
        public async Task UpdateEmployeeRole_InvalidField_ShouldThrowException(string empId, string roleTitle)
        {
            decimal correctEmpId = Convert.ToDecimal(empId);
            _employeeManager = new EmployeeManager(_employeeRepository.Object);
            await Assert.ThrowsAsync<AggregateException>( async () => await _employeeManager.UpdateEmployeeRole(correctEmpId, roleTitle));
        }
    }
}
