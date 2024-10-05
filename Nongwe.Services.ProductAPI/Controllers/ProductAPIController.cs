using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nongwe.Services.ProductAPI.Data;
using Nongwe.Services.ProductAPI.Models;
using Nongwe.Services.ProductAPI.Models.Dto;

namespace Nongwe.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPI : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ResponseDto _response;
        private IMapper _mapper;
        public ProductAPI(ApplicationDbContext db, ResponseDto response, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto GetProducts()
        {
            try
            {
                IEnumerable<Product> obj = _db.products.ToList();
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(obj);
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
        public ResponseDto GetProductById(int id)
        {
            try
            {
                Product objList = _db.products.First(c => c.ProductId == id);
                _response.Result = _mapper.Map<ProductDto>(objList);
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("GetCouponByCode/{name}")]
        public ResponseDto GetProductByName(string name)
        {
            try
            {
                Product productName = _db.products.FirstOrDefault(u => u.ProductName.ToLower() == name.ToLower());
                if (productName == null)
                {
                    _response.IsSuccess = false;
                }
                _response.Result = _mapper.Map<ProductDto>(productName);
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
        public ResponseDto CreateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _db.products.Add(product);
                _db.SaveChanges();
               
                _response.Result = _mapper.Map<ProductDto>(product);
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
        public ResponseDto UpdateProduct([FromBody] ProductDto productDto)
        {
            try
            {
                Product product = _mapper.Map<Product>(productDto);
                _db.products.Update(product);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(product);
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
                Product obj = _db.products.First(u => u.ProductId == id);
                _db.products.Remove(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<ProductDto>(obj);
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
