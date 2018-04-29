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
    public class ProductsController : BaseController
    {
        private IProductService ProductService;
        public ProductsController(IProductService productService)
        {
            ProductService = productService;
        }

        // GET: Products
        public ActionResult Index()
        {
            var products = ProductService.GetNwmProducts();
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            var item = ProductService.GetNwmProduct(id);
            return View(item);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NwmProduct product)
        {
            try
            {
                // TODO: Add insert logic here
                ProductService.SaveOrUpdateNwmProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id)
        {
            var item = ProductService.GetNwmProduct(id);
            return View(item);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public ActionResult Edit(int id, IFormCollection collection)
        public ActionResult Edit(NwmProduct product)
        {
            try
            {
                ProductService.SaveOrUpdateNwmProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var item = ProductService.GetNwmProduct(id);
            return View(item);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                ProductService.DeleteNwmProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}