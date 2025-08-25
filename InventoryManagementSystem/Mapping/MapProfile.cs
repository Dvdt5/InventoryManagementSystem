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
        }
    }
}
