using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductRegistrationForm form);
        Task DeleteProductAsync(int id);
        Task<Product?> GetProductAsync(int id);
        Task<Product?> GetProductAsync(string productName);
        Task<IEnumerable<Product?>> GetProductsAsync();
        Task<bool> ProductExistsAsync(string ProductName);
        Task UpdateProductAsync(ProductUpdateForm product);
    }
}