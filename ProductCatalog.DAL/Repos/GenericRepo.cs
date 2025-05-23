using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductCatalog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DAL.Repos
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ProductCatalogDbContext _productCatalogDbContext;
        private DbSet<T> _DbSet;
         
        public GenericRepo(ProductCatalogDbContext productCatalogDbContext)
        {
            _productCatalogDbContext = productCatalogDbContext;
            _DbSet = _productCatalogDbContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
           await _DbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _DbSet.FindAsync(id);
            if (entity != null)
            {
                _DbSet.Remove(entity);
            }
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _DbSet.FindAsync(id);
          
        }

        public async Task UpdateAsync(T entity)
        {
            _DbSet.Update(entity);
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(Expression<Func<T, bool>> predicate)
        {
            return await _DbSet.Where(predicate).ToListAsync() ;
        }

    }
}
