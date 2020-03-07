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
            //Now checking if the user has even bought anything or not. If the cx hasnt bought anything, the carProducts will be null

            if (CartProductsCookie != null)
            {
                //var productIDs = CartProductsCookie.Value;
                //var ids = productIDs.Split('-');
                //List<int> pIDs = ids.Select(s => int.Parse(s)).ToList();
                //The above method is being done in one line
                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.ProductsInCart = ProductsService.Instance.GetProducts(model.CartProductIDs); 
            }


            return View(model);
        }
    }
}