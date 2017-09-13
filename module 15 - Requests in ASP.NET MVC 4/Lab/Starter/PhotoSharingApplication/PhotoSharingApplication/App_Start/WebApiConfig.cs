using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PhotoSharingApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Configure API routes
            config.Routes.MapHttpRoute(
                name: "PhotoApi",
                routeTemplate: "api/photos/{id}",
                defaults: new { controller = "PhotoApi", action = "GetPhotoById" },
                constraints: new { id = "[0-9]+" }
            );

            config.Routes.MapHttpRoute(
                name: "PhotoTitleApi",
                routeTemplate: "api/photos/{title}",
                defaults: new { controller = "PhotoApi", action = "GetPhotoByTitle" }
            );

            config.Routes.MapHttpRoute(
                name: "PhotosApi",
                routeTemplate: "api/photos",
                defaults: new { controller = "PhotoApi", action = "GetAllPhotos", id = RouteParameter.Optional }
            );

            //Configure formatters.
            var json = config.Formatters.JsonFormatter; 
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects; 
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
