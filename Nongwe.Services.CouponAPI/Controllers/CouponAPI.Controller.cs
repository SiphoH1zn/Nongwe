using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nongwe.Services.CouponAPI.Data;
using Nongwe.Services.CouponAPI.Models;
using Nongwe.Services.CouponAPI.Models.Dto;

namespace Nongwe.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPI : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponAPI(ApplicationDbContext db, ResponseDto response, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto GetCoupons()
        {
            try
            {
                IEnumerable<Coupon> obj = _db.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(obj);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto GetCouponById(int id)
        {
            try
            {
                Coupon objList = _db.Coupons.First(c => c.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(objList);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("GetCouponByCode/{code}")]
        public ResponseDto GetCouponByCode(string code)
        {
            try
            {
                Coupon couponCode = _db.Coupons.FirstOrDefault(u => u.CouponCode.ToLower() == code.ToLower());
                if (couponCode == null)
                {
                    _response.IsSuccess = false;
                }
                _response.Result = _mapper.Map<CouponDto>(couponCode);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto CreateCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(coupon);
                _db.SaveChanges();
               
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto UpdateCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(coupon);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(coupon);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto DeleteCoupon(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);
                _db.Coupons.Remove(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
