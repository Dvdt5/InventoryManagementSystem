using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public class InventoryStockRepository : GenericRepository<InventoryStock>
    {
        public InventoryStockRepository(AppDbContext context) : base(context)
        {
        }
    }
}
