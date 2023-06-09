using STSRetailSoftwareData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STSRetailSoftwareData.Repo
{
    public interface IInventoryRepository
    {
        Task<ICollection<Inventory>> GetInventoryAsync();
    }
}
