using Nongwe.Services.ShoppingCartAPI.Models.Dto;

namespace Nongwe.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();

    }
}
