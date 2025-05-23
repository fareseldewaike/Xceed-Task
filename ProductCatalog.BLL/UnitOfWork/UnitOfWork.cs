using ProductCatalog.DAL;
using ProductCatalog.DAL.Interfaces;
using ProductCatalog.DAL.Repos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductCatalogDbContext _productCatalogDbContext;
        private readonly Hashtable repositories = new();

        public UnitOfWork(ProductCatalogDbContext productCatalogDbContext)
        {
            _productCatalogDbContext = productCatalogDbContext;
        }

        public void Dispose()
        {
            _productCatalogDbContext.Dispose();
        }

        public Task<IGenericRepo<T>> Repository<T>() where T : class
        {
            var type = typeof(T).Name;
            if (!repositories.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepo<T>(_productCatalogDbContext);
                repositories.Add(type, repositoryInstance);
            }

            return Task.FromResult((IGenericRepo<T>)repositories[type]);
        }

        public async Task SaveChangesAsync()
        {
            await _productCatalogDbContext.SaveChangesAsync();
        }
    }
}
