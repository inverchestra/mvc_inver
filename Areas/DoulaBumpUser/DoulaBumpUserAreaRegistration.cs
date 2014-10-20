using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.DoulaBumpUser
{
    public class DoulaBumpUserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "DoulaBumpUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "DoulaBumpUser_default",
                "DoulaBumpUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
