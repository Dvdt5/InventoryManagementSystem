using AutoMapper;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.ViewModels;

namespace InventoryManagementSystem.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<UnitOfMeasure, UnitOfMeasureModel>().ReverseMap();
            CreateMap<Warehouse, WarehouseModel>().ReverseMap();
            CreateMap<InventoryStock, InventoryStockModel>().ReverseMap();
            CreateMap<InventoryTransaction, InventoryTransactionModel>().ReverseMap();
        }
    }
}
