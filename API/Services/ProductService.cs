using API.Data;
using API.Dtos;
using API.IServices;
using API.Models;
using API.ServiceResponses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        public ProductService(ApplicationDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task AddProduct(string Name, string Type, int Price, string img)
        {
            var prod = new Product
            {
                Name = Name,
                TypeProduct = Type,
                Price = Price,
                ImageUrl = img
            };

            await db.Products.AddAsync(prod);
            await db.SaveChangesAsync();
        }

        public async Task<ServiceResponse<string>> AddProductToCart(int userId, int productId)
        {
            var service = new ServiceResponse<string>();

            try
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
                var product = await db.Products.FirstOrDefaultAsync(x => x.Id == productId);
                
                await db.Entry(user).Reference(x => x.Cart).LoadAsync();

                var cartproduct = new CartProduct()
                {
                    Cart = user.Cart,
                    Product = product
                };

                await db.CartProducts.AddAsync(cartproduct);
                await db.SaveChangesAsync();

                service.Description = "Продукт добавлен в корзину";
                service.StatusCode = true;


            }
            catch(Exception ex)
            {
                service.Description = ex.Message;
                service.StatusCode = false;
            }
            return service;
        }

        public async Task<ServiceResponse<string>> DeleteProductByCart(int userId, int productId)
        {
            var service = new ServiceResponse<string>();
            try
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
                await db.Entry(user).Reference(x => x.Cart).LoadAsync();

                var cartproduct = await db.CartProducts.FirstOrDefaultAsync(x => x.Cart == user.Cart && x.ProductId == productId);

                db.CartProducts.Remove(cartproduct);
                await db.SaveChangesAsync();

                service.Description = "Товар удален";

            }
            catch(Exception ex)
            {
                service.Description = ex.Message;
                service.StatusCode = false;
            }
            return service;
        }

        public async Task<ServiceResponse<List<ProductDto>>> GetAllProduct(string? searchitem)
        {
           var service = new ServiceResponse<List<ProductDto>>();

            try
            {
                var products = new List<Product>();
                if(searchitem == null)
                {
                    products = await db.Products.ToListAsync();
                    service.Data = mapper.Map<List<ProductDto>>(products);
                    service.Description = "Все товары возвращены";
                    service.StatusCode = true;

                }
                else
                {
                    products = await  db.Products.Where(x => x.Name.Contains(searchitem)).ToListAsync();
                    service.Data = mapper.Map<List<ProductDto>>(products);
                    service.Description = $"Товары с именем {searchitem} возвращены";
                    service.StatusCode = true;
                }

            }
            catch (Exception ex)
            {
                service.Description = ex.Message;
                service.StatusCode = false;

            }

            return service;
        }

        public async Task<ServiceResponseSum<List<ProductDto>>> GetAllProductByCart(int userId)
        {
            var service = new ServiceResponseSum<List<ProductDto>>();
            try
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
                await db.Entry(user).Reference(x => x.Cart).LoadAsync();

                var products = await db.CartProducts.Where(x => x.Cart == user.Cart).Select(x => x.Product).ToListAsync();

                if(products.Count == 0)
                {
                    service.Description = "Ваша корзина пуста";
                    service.StatusCode = true;
                }
                else
                {
                    
                    service.Data = mapper.Map<List<ProductDto>>(products);
                    service.Description = "Продукты в коризне выведены";
                    service.StatusCode = true;
                    service.TotalSum = products.Select(x=>x.Price).Sum();
                }

            }
            catch(Exception ex)
            {
                service.Description= ex.Message;
                service.StatusCode = false;
            }

            return service;
        }
    }
}
