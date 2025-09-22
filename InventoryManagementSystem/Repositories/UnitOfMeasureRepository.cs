using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public class UnitOfMeasureRepository : GenericRepository<UnitOfMeasure>
    {
        public UnitOfMeasureRepository(AppDbContext context) : base(context)
        {
        }
    }
}
