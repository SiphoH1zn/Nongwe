using Microsoft.AspNetCore.Mvc;
using Nongwe.Models;
using Nongwe.Web.Models;
using Nongwe.Web.Service.IService;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Nongwe.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
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

        [Authorize]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess)
            {
                try
                {
                    var modelList = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
                    ProductDto model = modelList.FirstOrDefault(); // Takes the first product, or null if the list is empty
                    if (model != null)
                    {
                        return View(model);
                    }
                    else
                    {
                        return NotFound(); // or any other appropriate action
                    }
                }
                catch (JsonSerializationException ex)
                {
                    // Log the exception or handle it as needed
                    return View("Error"); // Assuming you have an Error view to display the error
                }
            }
            else
            {
                // If the response is null or unsuccessful, handle accordingly
                return View("Error"); // Show an error view or another appropriate response
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
