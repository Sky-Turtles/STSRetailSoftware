using Microsoft.Extensions.Options;
using STSRetailSoftwareData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STSRetailSoftwareData.Repo
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly StsRetailErpContext _context;
        private readonly string ConnectionString;

        public InventoryRepository(IOptions<Configuration.Configuration> options)
        {
            this.ConnectionString = options.Value.ConnectionString;
            this._context = new(this.ConnectionString);
        }

        public async Task<ICollection<Inventory>> GetInventoryAsync()
        {
            return _context.Inventories.ToList();
        }
    }
}
