using ClothingBazaar.Services;
using ClothingBazaar.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothingBazaar.web.Controllers
{
    public class ShopController : Controller
    {
        //ProductsService productsService = new ProductsService();

        // GET: Shop
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];

            if (CartProductsCookie != null)
            {
                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.ProductsInCart = ProductsService.Instance.GetProducts(model.CartProductIDs); 
            }


            return View(model);
        }
    }
}