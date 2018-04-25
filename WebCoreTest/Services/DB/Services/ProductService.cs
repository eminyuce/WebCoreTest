﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreTest.Data.Entities;
using WebCoreTest.Services.DB.Repositories;

namespace WebCoreTest.Services.DB.Services
{
    public class ProductService : IProductService
    {
        private static string CacheKeyAllItems = "NwmProductCache";

        public IProductRepository ProductRepository { get; set; }
        private readonly ILogger _logger;

        public ProductService(IProductRepository _ProductRepository, ILogger<ProductService> logger)
        {
            ProductRepository = _ProductRepository;
            _logger = logger;
        }


        public  List<NwmProduct> GetNwmProducts()
        {
            var nwmproductResult = new List<NwmProduct>();
            try
            {
                nwmproductResult = ProductRepository.GetNwmProducts();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
            }
            return nwmproductResult;
        }
        public  int SaveOrUpdateNwmProduct(NwmProduct item)
        {
            try
            {
                return ProductRepository.SaveOrUpdateNwmProduct(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return -1;
        }
        public  NwmProduct GetNwmProduct(int id)
        {
            var item = new NwmProduct();
            try
            {
                item = ProductRepository.GetNwmProduct(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return item;
        }
        public  void DeleteNwmProduct(int id)
        {
            try
            {
                ProductRepository.DeleteNwmProduct(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
       
    }
}
