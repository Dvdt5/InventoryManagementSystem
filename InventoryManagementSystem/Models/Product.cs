namespace InventoryManagementSystem.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ReorderLevel { get; set; }
        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
