using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InnDocs.iHealth.Web.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Campaign",
            //    url: "campaign",
            //    defaults: new { controller = "Home", action = "Home" }
            //);
            routes.MapRoute(
                name: "Campaign",
                url: "campaign",
                defaults: new { controller = "Home", action = "Home" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
            
        }
    }
}