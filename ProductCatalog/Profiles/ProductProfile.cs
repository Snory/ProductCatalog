using AutoMapper;
using ProductCatalog.ApiModel.ProductManagement.ReadModels;
using ProductCatalog.Structures;

namespace ProductCatalog.API.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductView, ProductViewOut>();
                //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));
        }
    }
}
