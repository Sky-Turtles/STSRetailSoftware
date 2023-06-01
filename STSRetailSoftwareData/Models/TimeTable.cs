using System;
using System.Collections.Generic;

namespace STSRetailSoftwareData.Models;

public partial class TimeTable
{
    public decimal TimeTableId { get; set; }

    public decimal? EmpProfileId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? Hours { get; set; }

    public virtual EmployeeProfile? EmpProfile { get; set; }
}
