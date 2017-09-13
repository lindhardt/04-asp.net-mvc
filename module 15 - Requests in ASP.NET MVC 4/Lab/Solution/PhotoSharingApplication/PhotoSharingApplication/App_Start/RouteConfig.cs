using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoSharingApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //This route means we can access photos like this: /photo/3
            //Otherwise we'd have to use /photo/details/3
            //Note the use of a constraint with a regex that matches integers of any length
            //We need the constraint to ensure this route doesn't match /photo/create for example
            routes.MapRoute(
                name: "PhotoRoute",
                url: "photo/{id}",
                defaults: new { controller = "Photo", action = "Display" },
                constraints: new { id = "[0-9]+" }
            );

            //This route means we can access photos like this: /photo/title/my%20photo%20title
            routes.MapRoute(
                name: "PhotoTitleRoute",
                url: "photo/title/{title}",
                defaults: new { controller = "Photo", action = "DisplayByTitle" }
            );


            //The default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}