using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Controllers
{
    public class CampaignController : Controller
    {

        public ActionResult Index()
        {
            return PartialView(new InnDocs.iHealth.Web.UI.Models.SingleRegisterModel());
        }


    }
}
