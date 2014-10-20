using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using Microsoft.AspNet.SignalR;

namespace InnDocs.iHealth.Web.UI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.MapHubs(new HubConfiguration() { EnableCrossDomain = true, EnableDetailedErrors = true });
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {

            Context.Response.AppendHeader("Access-Control-Allow-Credentials", "true");

            string fullOrigionalpath = Request.Url.ToString();
            //if (fullOrigionalpath.Contains("http://localhost:12869/"))
            //{

            //}
            //else if (fullOrigionalpath.Contains("http://localhost:12869/ihealthuser"))
            //{
            //    Context.RewritePath("http://localhost:12869/iheadsadsadslthuserjdhdgsgshgdsd");
            //}

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
    }



}