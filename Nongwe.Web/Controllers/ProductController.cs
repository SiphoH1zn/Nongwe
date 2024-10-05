using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Nongwe.Models;
using Nongwe.Web.Models;
using Nongwe.Web.Service;
using Nongwe.Web.Service.IService;
using System.Collections.Generic;
using System.Reflection;

namespace Nongwe.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
                _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = new();

            ResponseDto? response = await _productService.GetAllProductsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.CreateProductAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product Created successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                List<ProductDto>? modelList = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
                {
                    if(modelList != null && modelList.Count > 0)
                    {
                        ProductDto? model = modelList[0];
                        return View("ProductDelete", model);
                    }
                }
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

       

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            if (productDto == null || productDto.ProductId <= 0)
            {
                ModelState.AddModelError("", "Invalid Coupon Data.");
                return View(productDto);
            }
            try
            {
                ResponseDto? response = await _productService.DeleteProductAsync(productDto.ProductId);
                if (response != null && response.IsSuccess)
                {
                    // Consider adding a success message to TempData which can be displayed on the CouponIndex view
                    TempData["SuccessMessage"] = "Product deleted successfully!";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    // Log the error or add an error message to the view if the response is not successful
                    ModelState.AddModelError("", "Failed to delete the product. Please try again.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (using your preferred logging framework)
                ModelState.AddModelError("", "An error occurred while deleting the product.");
            }
            return View(productDto);
        }


        public async Task<IActionResult> ProductEdit(int productId)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {

                // Deserialize the response into a list of CouponDto
                List<ProductDto>? product = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));

                // Use LINQ to find the specific coupon by couponId
                ProductDto? model = product.FirstOrDefault(product => product.ProductId == productId);
                //CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                // model = model?.First(coupon => coupon.Id == couponId);
                return View(model);

            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _productService.UpdateProductAsync(model);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product Updated successfully";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }
    }
}