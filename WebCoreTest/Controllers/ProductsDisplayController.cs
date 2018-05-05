using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCoreTest.Domain.Entities;
using WebCoreTest.Domain.DB.Services;

namespace WebCoreTest.Controllers
{
    public class ProductsDisplayController : Controller
    {
        public IProductService ProductService { get; set; }

        public IActionResult Index()
        {
            var products = ProductService.GetNwmProducts();
            return View(products);
        }
    }
}