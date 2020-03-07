using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClothingBazaar.web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //THis works line by line so the first map is tried first and if not then it goes to the other route.
            //USING A CUSTOMER URL ROUTE
            routes.MapRoute(
                name: "AllCategories",
                url: "categories/all",
                defaults: new { controller = "Category", action = "CategoryTable" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
