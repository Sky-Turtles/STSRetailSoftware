using STSRetailSoftwareData.Models;
using STSRetailSoftwareData.Repo;

namespace STSRetailApi.Business
{
    public class InventoryManager : IInventoryManager
    {
        private readonly IInventoryRepository _repository;
        public InventoryManager(IInventoryRepository inventoryRepository)
        {
            this._repository = inventoryRepository;
        }

        public async Task<ICollection<Inventory>> GetInventory()
        {
            return await this._repository.GetInventoryAsync();
        }
    }
}
