using Nongwe.Services.ShoppingCartAPI.Models.Dto;

namespace Nongwe.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
