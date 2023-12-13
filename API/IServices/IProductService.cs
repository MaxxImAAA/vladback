using API.Dtos;
using API.ServiceResponses;

namespace API.IServices
{
    public interface IProductService
    {
        Task<ServiceResponse<List<ProductDto>>> GetAllProduct(string? searchitem);

        Task<ServiceResponse<string>> AddProductToCart(int userId, int productId);

        Task<ServiceResponseSum<List<ProductDto>>> GetAllProductByCart(int userId);

        Task<ServiceResponse<string>> DeleteProductByCart(int userId, int productId);

        Task AddProduct(string Name, string Type, int Price, string img);
    }
}
