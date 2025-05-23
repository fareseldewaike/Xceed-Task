using ProductCatalog.BLL.UnitOfWork;
using ProductCatalog.DAL.Entities;
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
        public async Task AddProductAsync(Product product)
        {
            var productRepo = await _unitOfWork.Repository<Product>();
            await productRepo.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productRepo = await _unitOfWork.Repository<Product>();
            return await productRepo.GetByIdAsync(id);
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            var productRepo = await _unitOfWork.Repository<Product>();
            return await productRepo.GetAllAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            var productRepo = await _unitOfWork.Repository<Product>();
            await productRepo.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int id)
        {
            var productRepo = await _unitOfWork.Repository<Product>();
            await productRepo.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsWithStillInOfferAsync()
        {
            var now = DateTime.UtcNow;
            var productRepo = await _unitOfWork.Repository<Product>();
            var products = await productRepo.GetAllWithSpecAsync(p => p.StartDate <= now && p.StartDate.AddDays(p.DurationInDays) >= now);
            return products;
        }
        public async Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            var productRepo = await _unitOfWork.Repository<Product>();
            var products = await productRepo.GetAllWithSpecAsync(p => p.CategoryId == categoryId);

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
            var productRepo = await _unitOfWork.Repository<Product>();
            var products = await productRepo.GetAllWithSpecAsync(p => p.CategoryId == categoryId && p.StartDate <= now && p.StartDate.AddDays(p.DurationInDays) >= now);
            return products;
        }
    }
}
