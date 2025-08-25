using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
