using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class InventoryStockModel : BaseModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public int QuantityOnHand { get; set; }
        public int QuantityReserved { get; set; }
        public string LastUpdated { get; set; }
    }
}
