using AutoMapper;
using Nongwe.Services.ShoppingCartAPI.Models;
using Nongwe.Services.ShoppingCartAPI.Models.Dto;

namespace Nongwe.Services.ShoppingCartAPI
{
    public class MappingCondig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
