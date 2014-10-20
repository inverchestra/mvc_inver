using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Models;
using System;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

    }
    public class AnalyticsAttribute : ActionFilterAttribute
    {

        public string Medium { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            var request = filterContext.HttpContext.Request;

            var browser = "";
            var version = "";
            if (request.UserAgent.Contains("OPR"))
            {
                browser = "Opera";
                version = request.UserAgent.Substring(request.UserAgent.LastIndexOf("/") + 1, 4);
            }
            else if (request.UserAgent.Contains("Maxthon"))
            {
                browser = "Maxthon";
                version = request.UserAgent.Split(new string[] { "Maxthon/" }, StringSplitOptions.None)[1].Split(' ')[0].Trim().Substring(0, 9);
            }
            else
            {
                browser = request.Browser.Browser;
                version = request.Browser.Version;
            }
            Analytic analysis = new Analytic()
            {
                Ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                AreaAccessed = request.RawUrl.Contains("?") ? request.RawUrl.Split('?')[0] : request.RawUrl,
                TimeStamp = DateTime.UtcNow,
                UserName = ((request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous") == "Anonymous" ? filterContext.RequestContext.HttpContext.Session["TUN"] as string : filterContext.HttpContext.User.Identity.Name,
                Media = Medium ?? "",
                Referrer = (request.UrlReferrer == null) ? request.Url.AbsoluteUri.Split('?')[0] : request.UrlReferrer.AbsoluteUri.Split('?')[0],
                Browser = browser,
                Version = version,
                UserAgent = request.UserAgent
            };

            var code = AddData(analysis);

            base.OnActionExecuted(filterContext);
        }

        private string AddData(Analytic data)
        {
            try
            {

                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Headers["Accept"] = "application/json";
                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Analytic));

                serializer.WriteObject(stream, data);
                string url = DomainServerPath.Service+"Analytics/add/";

                byte[] rdata = client.UploadData(url, "POST", stream.ToArray());

                stream = new MemoryStream(rdata);

                serializer = new DataContractJsonSerializer(typeof(string));

                var code = serializer.ReadObject(stream) as string;

                return code;
            }
            catch (Exception)
            {
                return "0";
            }
        }

    }
}