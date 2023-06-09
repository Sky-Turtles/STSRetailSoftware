using Microsoft.Extensions.Hosting;
using STSRetailApi.Business;
using STSRetailSoftwareData.Models;
using STSRetailSoftwareData.Repo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STSRetailTests
{
    public class InventoryManagerTests
    {
        private Mock<IInventoryRepository> _inventoryRepository = new();
        private IInventoryManager _inventoryManager;

        [Fact]
        public async Task GetInventoryAsync_IsSuccessful_ShouldSuccess()
        {
            Inventory inventory = new()
            {
                InventoryId = 1,
                ItemName = "Milk",
                Cost = 3.55M,
                Catagory = "Dairy",
                Quantity = 55

            };
            Inventory inventory1 = new()
            {
                InventoryId = 2,
                ItemName = "Bread",
                Cost = 1.50M,
                Catagory = "Bakery",
                Quantity = 100
            };
            _inventoryRepository.Setup(e => e.GetInventoryAsync());

            _inventoryManager = new InventoryManager(_inventoryRepository.Object);


            _inventoryManager.GetInventory();

            _inventoryRepository.Verify(repo => repo.GetInventoryAsync(), Times.Once);
        }
    }
}
