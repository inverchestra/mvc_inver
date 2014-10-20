using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Models;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    public class DoctorController : Controller
    {
        //
        // GET: /iHealthUser/Doctor/

        public ActionResult DoctorInfo()
        {

            InnDocs.iHealth.Web.UI.Models.SingleRegisterModel reguser = null;
            if (Session["RegUser"] != null)
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;
            else
                reguser = Session["RegUser"] as InnDocs.iHealth.Web.UI.Models.SingleRegisterModel;



            IList<UserInformation> userInfo = Services1.GetAllUsersbyDomainIDNew(reguser.RUserId,reguser.GroupType);


            return View(userInfo);
        }

    }
}
