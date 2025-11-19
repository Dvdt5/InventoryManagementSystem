namespace InventoryManagementSystem.Models
{
    public class InventoryStock : BaseEntity
    {
        public int ProductId {get; set; }
        public Product Product { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int QueantityOnHand { get; set; }
        public int QuantityReserved { get; set; }
        public string LastUpdated { get; set; }
    }
}
