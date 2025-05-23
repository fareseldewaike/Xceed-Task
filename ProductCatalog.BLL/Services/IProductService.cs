using ProductCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.Services
{
    public interface IProductService
    {
       Task<IReadOnlyList<Product>> GetAllProductsWithStillInOfferAsync();
        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(Product product);
        Task<IReadOnlyList<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task<IReadOnlyList<Product>> GetProductsByCategoryAsync(int categoryId);
        Task<IReadOnlyList<Product>> GetProductsByCategoryStillInOfferAsync(int categoryId);
        Task<IReadOnlyList<Category>> GetAllCategoriesAsync();

    }
}
