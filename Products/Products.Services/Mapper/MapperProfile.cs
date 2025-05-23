using AutoMapper;
using Products.Domain.Entities;
using Products.Services.DTOs.Products;

namespace Products.Services.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
