using AutoMapper;
using ProductsAPI.Models;

namespace ProductsAPI.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<ProductDTO, Product>();

            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name)); // mapping so categoryname gets the name of the category! thats awesome!
        }
    }
}
