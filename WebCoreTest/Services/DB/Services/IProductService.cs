using System.Collections.Generic;
using WebCoreTest.Data.Entities;
using WebCoreTest.Services.DB.Repositories;

namespace WebCoreTest.Services.DB.Services
{
    public interface IProductService
    {
        void DeleteNwmProduct(int id);
        NwmProduct GetNwmProduct(int id);
        List<NwmProduct> GetNwmProducts();
        int SaveOrUpdateNwmProduct(NwmProduct item);
    }
}