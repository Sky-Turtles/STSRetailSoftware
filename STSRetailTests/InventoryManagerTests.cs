using STSRetailApi.Business;
using STSRetailSoftwareData.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STSRetailTests
{
    public class InventoryManagerTests
    {
        private Mock<IInventoryRepository> _inventoryRepository = new();
        private IInventoryManager _inventoryManager;
    }
}
