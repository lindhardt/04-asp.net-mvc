using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoSharingApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //This is the default route for the site's Web API
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //This route means we can access photos like this: /photo/3
            //Otherwise we'd have to use /photo/details/3
            //Note the use of a constraint with a regex that matches integers of any length
            //We need the constraint to ensure this route doesn't match /photo/create for example
            routes.MapRoute(
                name: "PhotoRoute",
                url: "photo/{id}",
                defaults: new { controller = "Photo", action = "Details" },
                constraints: new { id = "[0-9]+" }
            );

            //This route means we can access comments like this: /comment/3
            routes.MapRoute(
                name: "CommentRoute",
                url: "comment/{id}",
                defaults: new { controller = "Comment", action = "Details" },
                constraints: new { id = "[0-9]+" }
            );

            //This is the default route
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}