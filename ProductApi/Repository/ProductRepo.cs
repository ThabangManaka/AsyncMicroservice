using ProductApi.Data;
using Shared.DTOs;
using Shared.Models;

namespace ProductApi.Repository
{
    public class ProductRepo(ProductDbContext context) : IProduct
    {

        public async Task<ServiceResponse> AddProductAsync(Product product)
        {

        }

        public async Task<List<Product>> GetAllProductsAsync()
        {

        }
    }
}
