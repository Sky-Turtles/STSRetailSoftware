using System;
using System.Collections.Generic;

namespace STSRetailSoftwareData.Models;

public partial class EmployeeProfile
{
    public decimal EmpProfileId { get; set; }

    public string Name { get; set; } = null!;

    public string Role { get; set; } = null!;

    public decimal PayRate { get; set; }

    public virtual EmployeeCredential EmpProfile { get; set; } = null!;

    public virtual ICollection<Payroll> PayrollEmps { get; set; } = new List<Payroll>();

    public virtual ICollection<Payroll> PayrollManagers { get; set; } = new List<Payroll>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<TimeTable> TimeTables { get; set; } = new List<TimeTable>();
}
