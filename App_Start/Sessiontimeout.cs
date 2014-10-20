using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Routing;

namespace InnDocs.iHealth.Web.UI
{

    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;

            // check if session is supported
            if (ctx.Session != null)
            {
                if (ctx.Session.IsNewSession)
                {
                    string sessionCookie = ctx.Request.Headers["Cookie"];
                    if ((null != sessionCookie) && (sessionCookie.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            filterContext.Result = new JsonResult { Data = "_LogOn_", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                        else
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Home" }, { "Action", "Home" }, { "Area", "" } });  
                        }
                        //ctx.Response.Redirect("~/Home");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }

}
