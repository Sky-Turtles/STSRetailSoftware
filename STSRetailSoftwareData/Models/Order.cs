using System;
using System.Collections.Generic;

namespace STSRetailSoftwareData.Models;

public partial class Order
{
    public decimal OrderId { get; set; }

    public string ItemList { get; set; } = null!;

    public string Quantity { get; set; } = null!;
}
