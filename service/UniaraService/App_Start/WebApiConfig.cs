using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace UniaraService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "ApiById",
                routeTemplate: "rest/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"^[0-9]+$" }
            );

            config.Routes.MapHttpRoute(
                name: "ApiByAction",
                routeTemplate: "rest/{controller}/{action}",
                defaults: new { action = "Get"}
            );

            config.Routes.MapHttpRoute(
                name: "ApiByName",
                routeTemplate: "rest/{controller}/{action}/{name}",
                defaults: null,
                constraints: new { name = @"^[a-z]+$" }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.Indent = true;
        }
    }
}
