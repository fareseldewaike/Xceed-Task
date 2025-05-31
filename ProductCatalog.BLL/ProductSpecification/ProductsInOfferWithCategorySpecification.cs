using ProductCatalog.BLL.Specification;
using ProductCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.ProductSpecification
{
    public class ProductsInOfferWithCategorySpecification :BaseSpecification<Product>
    {

        public ProductsInOfferWithCategorySpecification(DateTime currentTime)
               : base(p => p.StartDate <= currentTime && p.StartDate.AddDays(p.DurationInDays) >= currentTime)
        {
            AddInclude(p => p.Category);
        }

    }
}
