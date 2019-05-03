using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Book", action = "List", category = (string)null, page = 1 }
            );

            routes.MapRoute(
                name: null,
                url: "BookListPage{page}",
                defaults: new { controller = "Book", action = "List", category = (string)null }
            );

            routes.MapRoute(
                name: null,
                url: "{category}/page{page}",
                defaults: new { controller = "Book", action = "List" },
                constraints: new { page = @"\d+" }
            );

            routes.MapRoute(
                name: "Book List",
                url: "BookListPage{page}",
                defaults: new { controller = "Book", action = "List"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
            );
        }
    }
}
