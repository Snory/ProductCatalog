using AutoMapper;
using ProductCatalog.ApiModel.ProductManagement.ReadModels;
using ProductCatalog.Structures;

namespace ProductCatalog.API.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {

            CreateMap<CategoryView, CategoryViewOut>();
        }
    }
}
