using ClothingBazaar.Entities;
using ClothingBazaar.web.ViewModels;
using ClothingBazaar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothingBazaar.web.Controllers
{
    //Its just a normal simple class and the only difference is that its inherited from controller class which comes from MVC
    public class CategoryController : Controller 
    {
        //CategoriesService categoryService = new CategoriesService(); //Creating an object of the serivce.

        public ActionResult CategoryTable(String search)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();

            model.Categories = CategoriesService.Instance.GetCategories();

            if (string.IsNullOrEmpty(search) == false)
            {
                model.SearchTerm = search;
                model.Categories = model.Categories.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();   
            }

            return PartialView(model);
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Category
        //When server has to give something to the user, the user will get through this. so it will be received in get method.
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);
        }

        //POST: Category
        //If its post then we're expecting to receive a data so we have to set something as a parameter.
        //Now if we're creating a category so we should receive a category as well.
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model) 
        {
            var newCategory = new Category();
            newCategory.Name = model.Name;
            newCategory.Description = model.Description;
            newCategory.ImageURL = model.ImageURL;
            newCategory.isFeatured = model.isFeatured;

            CategoriesService.Instance.SaveCategory(newCategory); //This method comes from the Category Service class

            return RedirectToAction("CategoryTable");
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
            CategoriesService.Instance.DeleteCategory(category.ID); //This method comes from the Category Service class

            return RedirectToAction("CategoryTable");
        }
    }
}