using AutoMapper;
using MMTShop.Data.Entities.Products;
using MMTShop.Data.Models;

namespace MMTShop.Data.Mappings
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
        }
    }
}
