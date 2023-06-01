using System;
using System.Collections.Generic;

namespace STSRetailSoftwareData.Models;

public partial class Inventory
{
    public decimal InventoryId { get; set; }

    public string ItemName { get; set; } = null!;

    public decimal Cost { get; set; }

    public string Catagory { get; set; } = null!;

    public decimal Quantity { get; set; }
}
