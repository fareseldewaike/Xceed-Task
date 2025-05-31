using ProductCatalog.BLL.ProductSpecification;
using ProductCatalog.BLL.UnitOfWork;
using ProductCatalog.DAL.Entities;
using ProductCatalog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        private async Task<IGenericRepo<Product>> GetProductRepositoryAsync()
        {
            return await _unitOfWork.Repository<Product>();
        }
        public async Task AddProductAsync(Product product)
        {
            var productRepo = await GetProductRepositoryAsync();
            await productRepo.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productRepo = await GetProductRepositoryAsync();
            var product = await productRepo.GetByIdAsync(id);
            if (product == null) throw new NotFoundException($"Product with ID {id} not found");
            return product;
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            var productRepo = await GetProductRepositoryAsync();
            var spec = new AllProductsWithCategorySpecification();
            var products = await productRepo.GetAllWithSpecAsync(spec);
            return products;
        }
        public async Task UpdateProductAsync(Product product)
        {
            var productRepo = await GetProductRepositoryAsync();
            await productRepo.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int id)
        {
            var productRepo = await GetProductRepositoryAsync();
            await productRepo.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsWithStillInOfferAsync()
        {
            var now = DateTime.UtcNow;
            var productRepo = await GetProductRepositoryAsync();
            var spec = new ProductsInOfferWithCategorySpecification(now);
            var products = await productRepo.GetAllWithSpecAsync(spec);
            return products;
        }
        public async Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            var productRepo = await GetProductRepositoryAsync();
            var spec = new ProductsByCategorySpecification(categoryId);
            var products = await productRepo.GetAllWithSpecAsync(spec);
            return products;
        }
        public async Task<IReadOnlyList<Category>> GetAllCategoriesAsync()
        {
            var categoryRepo = await _unitOfWork.Repository<Category>();
            return await categoryRepo.GetAllAsync();
        }
        public async Task<IReadOnlyList<Product>> GetProductsByCategoryStillInOfferAsync(int categoryId)
        {
            var now = DateTime.UtcNow;
            var productRepo = await GetProductRepositoryAsync();
            var spec = new ProductsInOfferByCategorySpecification(categoryId, now);
            var products = await productRepo.GetAllWithSpecAsync(spec);
            return products;
        }
    }






    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
