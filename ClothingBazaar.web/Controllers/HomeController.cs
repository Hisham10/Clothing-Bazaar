using ClothingBazaar.Services;
using ClothingBazaar.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ClothingBazaar.web.Controllers
{
    public class HomeController : Controller
    {
        //CategoriesService categoryService = new CategoriesService();
        //ProductsService productService = new ProductsService();

        public ActionResult Index()
        {
            HomeViewModels model = new HomeViewModels();
            model.FeaturedCategories = CategoriesService.Instance.GetFeaturedCategories();
            model.FeaturedProducts = ProductsService.Instance.GetLatestProducts();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}