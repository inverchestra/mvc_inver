using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser
{
    public class iHealthUserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "iHealthUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "iHealthUser_default",
                "iHealthUser/{controller}/{action}/{id}",
                new { action = "DashBoard", id = UrlParameter.Optional }
            );
        }
    }
}
