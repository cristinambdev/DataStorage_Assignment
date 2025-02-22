using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    private readonly IProductRepository _productRepository = productRepository;

    //CREATE
    public async Task<bool> CreateProductAsync(ProductRegistrationForm form)
    {
        if (await _productRepository.AlreadyExistsAsync(x => x.ProductName == form.ProductName))
            return false;

        await _productRepository.BeginTransactionAsync();

        try
        {
            await _productRepository.CreateAsync(ProductFactory.Create(form)!);
            await _productRepository.SaveAsync();

            await _productRepository.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _productRepository.RollbackTransactionAsync();
            return false;
        }
    }
    //READ
    public async Task<IEnumerable<Product?>> GetProductsAsync()
    {
        var productEntities = await _productRepository.GetAllAsync();
        return productEntities.Select(entity => ProductFactory.Create(entity));
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        var productEntity = await _productRepository.GetAsync(x => x.Id == id);
        return ProductFactory.Create(productEntity!);
    }

    public async Task<Product?> GetProductAsync(string productName)
    {
        var productEntity = await _productRepository.GetAsync(x => x.ProductName == productName);
        return ProductFactory.Create(productEntity!);
    }

    //Update
    public async Task UpdateProductAsync(ProductUpdateForm product)
    {
        await _productRepository.BeginTransactionAsync();

        try
        {
            var existingProduct = await _productRepository.GetAsync(x => x.ProductName == product.ProductName);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            existingProduct = ProductFactory.Update(product);
            await _productRepository.UpdateAsync(existingProduct!);
            await _productRepository.CommitTransactionAsync();
        }
        catch
        {
            await _productRepository.RollbackTransactionAsync();
        }
    }

    //Delete

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.BeginTransactionAsync();

        try
        {
            var existingProduct = await _productRepository.GetAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }
            await _productRepository.DeleteAsync(existingProduct);
            await _productRepository.CommitTransactionAsync();

        }
        catch
        {
            await _productRepository.RollbackTransactionAsync();
        }

    }

    public async Task<bool> ProductExistsAsync(string ProductName)
    {
        var result = await _productRepository.AlreadyExistsAsync(x => x.ProductName == ProductName);
        return result;
    }
}
