using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser
{
    public class HospitalUserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HospitalUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HospitalUser_default",
                "HospitalUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
