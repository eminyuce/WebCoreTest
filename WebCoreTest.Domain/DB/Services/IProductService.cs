using System.Collections.Generic;
using WebCoreTest.Domain.Entities;
using WebCoreTest.Domain.DB.Repositories;

namespace WebCoreTest.Domain.DB.Services
{
    public interface IProductService
    {
        void DeleteNwmProduct(int id);
        NwmProduct GetNwmProduct(int id);
        List<NwmProduct> GetNwmProducts();
        int SaveOrUpdateNwmProduct(NwmProduct item);
    }
}