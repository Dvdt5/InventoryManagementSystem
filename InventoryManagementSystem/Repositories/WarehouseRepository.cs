using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public class WarehouseRepository : GenericRepository<Warehouse>
    {
        public WarehouseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
