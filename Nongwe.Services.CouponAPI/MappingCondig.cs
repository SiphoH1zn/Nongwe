using AutoMapper;
using Nongwe.Services.CouponAPI.Models;
using Nongwe.Services.CouponAPI.Models.Dto;

namespace Nongwe.Services.CouponAPI
{
    public class MappingCondig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
