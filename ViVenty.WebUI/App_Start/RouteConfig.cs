using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ViVenty.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "", new {controller = "Home", action = "Index"});

            routes.MapRoute(
                name: null,
                url: "hsuit/Page{page}",
                defaults: new { controller = "Hsuit", action = "List", category = (string)null },
                constraints: new {page = @"\d+"}
                );

            routes.MapRoute(null,
                "hsuit/{category}",
                new {controller = "Hsuit", action = "List", page = 1}
                );

            routes.MapRoute(null,
                "hsuit/{category}/Page{page}",
                new { controller = "Hsuit", action = "List"},
                new {page = @"\d+"}
                );

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
