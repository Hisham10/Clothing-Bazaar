﻿using ClothingBazaar.Entities;
using ClothingBazaar.Services;
using ClothingBazaar.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothingBazaar.web.Controllers
{
    public class ProductController : Controller
    {
        //ProductsService productsService = new ProductsService(); //using Singleton so disabling this.
        //CategoriesService categoriesService = new CategoriesService();

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search)
        {
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.Products = ProductsService.Instance.GetProducts();

            if (string.IsNullOrEmpty(search) == false)
            {
                model.SearchTerm = search;
                model.Products = model.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }

            return PartialView(model);
        }

        #region Creation

        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();

            model.categories = CategoriesService.Instance.GetAllCategories();
            return PartialView(model);
        }
        
        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Price = model.Price;
            newProduct.Description = model.Description;
            newProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID); 
            newProduct.ImageUrl = model.ImageUrl;
            newProduct.AddedOn = model.AddedOn;
            

            ProductsService.Instance.SaveProduct(newProduct); 

            return RedirectToAction("ProductTable");
        }
        #endregion

        #region Modification
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = ProductsService.Instance.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.ImageUrl = product.ImageUrl;
            model.AddedOn = product.AddedOn;
            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductsService.Instance.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.ImageUrl = model.ImageUrl;
            existingProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);
            //existingProduct.CategoryID = model.CategoryID;

            ProductsService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
            
        }
        #endregion

        #region Delete
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsService.Instance.DeleteProduct(ID);

            return RedirectToAction("ProductTable");
        }
        #endregion
    }
}