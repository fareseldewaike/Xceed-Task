using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get;   set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderByDescending { get;   set; }
        public int Take { get;   set; }
        public int Skip { get;   set; }
        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        public void AddOrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public void AddOrderByDescending(Func<IQueryable<T>, IOrderedQueryable<T>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
        public void SetPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }

}
