using AutoMapper;
using MMTShop.Data.Entities.Products;
using MMTShop.Data.Models;

namespace MMTShop.Data.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
        }
    }
}
