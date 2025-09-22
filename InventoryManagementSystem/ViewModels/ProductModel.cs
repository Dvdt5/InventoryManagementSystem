using InventoryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.ViewModels
{
    public class ProductModel : BaseModel
    {
        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SKU is required")]
        public string SKU { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Reorder Level is required")]
        public int ReorderLevel { get; set; }

        [Required(ErrorMessage = "Unit of Measure is required")]
        public int? UnitOfMeasureId { get; set; }

        public string? UnitOfMeasureName { get; set; }

        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

    }
}
