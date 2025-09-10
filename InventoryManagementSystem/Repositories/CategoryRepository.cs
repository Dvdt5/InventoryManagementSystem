using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
