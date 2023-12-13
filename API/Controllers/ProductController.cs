using API.Dtos;
using API.IServices;
using API.ServiceResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productservice;
        public ProductController(IProductService _productservice)
        {
            this._productservice = _productservice;
        }

        [HttpPost("AddProductToCart")]
        public async Task<ActionResult<ServiceResponse<string>>> AddProductToCart(int userId, int productId)
        {
            var request = await _productservice.AddProductToCart(userId, productId);

            return Ok(request);
        }

        [HttpPut("DeleteProductToCart")]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteProductByCart(int userId, int productId)
        {
            var request = await _productservice.DeleteProductByCart(userId, productId);

            return Ok(request);
        }

        [HttpGet("GetProduct")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetAllProduct(string? searchitem)
        {
            var request = await _productservice.GetAllProduct(searchitem);

            return Ok(request);
        }

        [HttpGet("GetProductByCart")]
        public async Task<ActionResult<ServiceResponseSum<List<ProductDto>>>> GetAllProductByCart(int userId)
        {
            var request = await _productservice.GetAllProductByCart(userId);

            return Ok(request);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddProduct(string Name, string Type, int Price, string img)
        {
           await _productservice.AddProduct(Name, Type, Price, img);

            return Ok();
        }
    }
}
