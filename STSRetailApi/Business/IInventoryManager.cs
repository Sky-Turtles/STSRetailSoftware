using STSRetailSoftwareData.Models;

namespace STSRetailApi.Business
{
    public interface IInventoryManager
    {
        Task<ICollection<Inventory>> GetInventory();
    }
}
