using AutoMapper;
using Nongwe.Services.ProductAPI.Models;
using Nongwe.Services.ProductAPI.Models.Dto;

namespace Nongwe.Services.ProductAPI
{
    public class MappingCondig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
