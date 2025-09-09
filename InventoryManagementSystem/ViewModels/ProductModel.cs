using InventoryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class ProductModel : BaseModel
    {
        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock Quantity is required")]
        public int StockQuantity { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
