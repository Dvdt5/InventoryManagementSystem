using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.ViewModels
{
    public class InventoryTransactionModel : Base
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        public string TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
    }
}
