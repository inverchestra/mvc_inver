using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    public class HealthTrackerController : Controller
    {
        //
        // GET: /iHealthUser/HealthTracker/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
