using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.Specification
{
    public class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> specification) where T : class
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }
            foreach (var include in specification.Includes)
            {
                query = query.Include(include);
            }
            if (specification.OrderBy != null)
            {
                query = specification.OrderBy(query);
            }
            else if (specification.OrderByDescending != null)
            {
                query = specification.OrderByDescending(query);
            }
            if (specification.Skip > 0)
            {
                query = query.Skip(specification.Skip);
            }
            if (specification.Take > 0)
            {
                query = query.Take(specification.Take);
            }
            return query;
        }
    }
}
