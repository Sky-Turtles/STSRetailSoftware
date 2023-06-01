using System;
using System.Collections.Generic;

namespace STSRetailSoftwareData.Models;

public partial class EmployeeCredential
{
    public decimal EmpCredId { get; set; }

    public string Password { get; set; } = null!;

    public virtual EmployeeProfile? EmployeeProfile { get; set; }
}
