using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using WebCoreTest.Data.Entities;

namespace WebCoreTest.Services.DB.Repositories
{
    public interface IProductRepository
    {
        void DeleteNwmProduct(int id);
        NwmProduct GetNwmProduct(int id);
        List<NwmProduct> GetNwmProducts();
        int SaveOrUpdateNwmProduct(NwmProduct item);
    }
}