using ClothingBazaar.Entities;
using ClothingBazaar.web.ViewModels;
using ClothingBazaar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ClothingBazaar.web.Controllers
{
    public class CategoryController : Controller 
    {
        //CategoriesService categoryService = new CategoriesService(); //Creating an object of the serivce.

        public ActionResult CategoryTable(String search, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            model.SearchTerm = search;
            int pageSize = 5; //int.Parse(ConfigurationService.Instance.GetConfig("ListingPageSize").Value);

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = CategoriesService.Instance.GetCategoriesCount(search);
            model.Categories = CategoriesService.Instance.GetCategories(search, pageNo.Value);

            if(model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, pageSize);

                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Category
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);
        }

        //POST: Category
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.ImageURL = model.ImageURL;
                newCategory.isFeatured = model.isFeatured;

                CategoriesService.Instance.SaveCategory(newCategory); 

                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
            
        }

        // GET: Edit
        public ActionResult Edit(int id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoriesService.Instance.GetCategory(id);
            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.isFeatured = category.isFeatured;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = CategoriesService.Instance.GetCategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.isFeatured = model.isFeatured;

            CategoriesService.Instance.UpdateCategory(existingCategory);

            return RedirectToAction("CategoryTable");
        }

        public ActionResult Delete(int id)
        {
            var category = CategoriesService.Instance.GetCategory(id);

            return PartialView(category);
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            CategoriesService.Instance.DeleteCategory(category.ID);

            return RedirectToAction("CategoryTable");
        }
    }
}