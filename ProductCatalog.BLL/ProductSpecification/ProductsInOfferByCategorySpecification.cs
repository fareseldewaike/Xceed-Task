using ProductCatalog.BLL.Specification;
using ProductCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.ProductSpecification
{
    public class ProductsInOfferByCategorySpecification : BaseSpecification<Product>
    {
        public ProductsInOfferByCategorySpecification(int categoryId, DateTime currentTime)
            : base(p =>
                p.CategoryId == categoryId &&
                p.StartDate <= currentTime &&
                p.StartDate.AddDays(p.DurationInDays) >= currentTime)
        {
            AddInclude(p => p.Category);
        }
    }
}
