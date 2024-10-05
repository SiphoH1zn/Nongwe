using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nongwe.Web.Models;
using Nongwe.Web.Service.IService;
using System.Collections.Generic;
using System.Reflection;

namespace Nongwe.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
                _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();

            ResponseDto? response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon Created successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
            ResponseDto? response = await _couponService.GetCouponByIdAsync(couponId);
            if (response != null && response.IsSuccess)
            {
               
                // Deserialize the response into a list of CouponDto
                List<CouponDto>? coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
                
                // Use LINQ to find the specific coupon by couponId
                CouponDto? model = coupons.FirstOrDefault(coupon => coupon.CouponId == couponId);
                //CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                // model = model?.First(coupon => coupon.Id == couponId);
                TempData["success"] = "Coupon deleted successfully";
                return View(model);
                
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

       

        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto)
        {
            if (couponDto == null || couponDto.CouponId <= 0)
            {
                ModelState.AddModelError("", "Invalid Coupon Data.");
                return View(couponDto);
            }
            
            try
            {
                ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);
                if (response != null && response.IsSuccess)
                {
                    // Consider adding a success message to TempData which can be displayed on the CouponIndex view
                    TempData["SuccessMessage"] = "Coupon deleted successfully!";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    // Log the error or add an error message to the view if the response is not successful
                    ModelState.AddModelError("", "Failed to delete the coupon. Please try again.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (using your preferred logging framework)
                ModelState.AddModelError("", "An error occurred while deleting the coupon.");
            }
            return View(couponDto);
        }
    }
}
