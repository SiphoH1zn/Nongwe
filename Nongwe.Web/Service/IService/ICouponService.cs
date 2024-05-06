using Nongwe.Web.Models;

namespace Nongwe.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponByCodeAsync(string couponCode);
        Task<ResponseDto> GetAllCouponsAsync();
        Task<ResponseDto> GetCouponByIdAsync(int id);
        Task<ResponseDto> CreateCouponAsync(CouponDto couponCode);
        Task<ResponseDto> UpdateCouponAsync(CouponDto couponCode);
        Task<ResponseDto> DeleteCouponAsync(int id);
    }
}
