namespace InventoryManagementSystem.Models
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string ManagerName { get; set; }
    }
}
