using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public class InventoryTransactionRepository : GenericRepository<InventoryTransaction>
    {
        public InventoryTransactionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
