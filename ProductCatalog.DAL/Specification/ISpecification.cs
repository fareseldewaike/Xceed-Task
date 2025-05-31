using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.Specification
{
    public interface ISpecification<T> where T : class
    {
          Expression<Func<T,bool>> Criteria { get; set; }
          List<Expression<Func<T, object>>> Includes { get; set; }
          Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get;  set; }
          Func<IQueryable<T>, IOrderedQueryable<T>> OrderByDescending { get;  set; }
          int Take { get;  set; }
          int Skip { get;  set; }
    }
}
