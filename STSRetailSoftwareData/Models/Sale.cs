using System;
using System.Collections.Generic;

namespace STSRetailSoftwareData.Models;

public partial class Sale
{
    public decimal SaleId { get; set; }

    public decimal? EmpId { get; set; }

    public string ItemList { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public virtual EmployeeProfile? Emp { get; set; }
}
