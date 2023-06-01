using System;
using System.Collections.Generic;

namespace STSRetailSoftwareData.Models;

public partial class Payroll
{
    public decimal PayrollId { get; set; }

    public decimal? EmpId { get; set; }

    public decimal? ManagerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual EmployeeProfile? Emp { get; set; }

    public virtual EmployeeProfile? Manager { get; set; }
}
