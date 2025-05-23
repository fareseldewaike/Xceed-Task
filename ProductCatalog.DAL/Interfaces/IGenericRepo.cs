using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.DAL.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> GetByIdAsync(int id) ;
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity) ;
        Task UpdateAsync(T entity) ;
        Task DeleteAsync(int id)  ;
    }
}
