using ClothingBazaar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothingBazaar.web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> ProductsInCart { get; set; }

        public List<int> CartProductIDs { get; set; }
    }
}