using ProductCatalog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.BLL.UnitOfWork
{
    public interface IUnitOfWork :IDisposable
    {
        Task SaveChangesAsync();
        Task<IGenericRepo<T>> Repository<T>() where T : class;

    }
}
