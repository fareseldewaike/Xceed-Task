using ProductCatalog.BLL.Specification;
using ProductCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.ProductSpecification
{
    public class AllProductsWithCategorySpecification : BaseSpecification<Product>
    {
        public AllProductsWithCategorySpecification()  
        {
            AddInclude(p => p.Category);
        }
    }
}
