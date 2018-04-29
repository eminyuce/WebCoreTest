using System.Collections.Generic;
using WebCoreTest.Domain.Entities;

namespace WebCoreTest.Domain.DB.Repositories
{
    public interface IProductRepository
    {
        void DeleteNwmProduct(int id);
        NwmProduct GetNwmProduct(int id);
        List<NwmProduct> GetNwmProducts();
        int SaveOrUpdateNwmProduct(NwmProduct item);
    }
}