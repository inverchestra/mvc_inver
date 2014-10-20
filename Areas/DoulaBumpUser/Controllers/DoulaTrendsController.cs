using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.Vitals.GetVitals;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;


namespace InnDocs.iHealth.Web.UI.Areas.DoulaBumpUser.Controllers
{
    [SessionExpireFilter]
    public class DoulaTrendsController : Controller
    {
        public ActionResult Trends()
        {
            return PartialView();
        }

        public ActionResult WeightTrend()
        {
            var user = User();
            Vitals _vitals = new Vitals();
            USTServiceCalls ustSerCalls = new USTServiceCalls();
            HeightandWeightServiceCalls HnWServiceCalls = new HeightandWeightServiceCalls();
            _vitals.LstHnW = HnWServiceCalls.GetHnWbyUserId(user.UserId);
            Session["hwb"] = _vitals.LstHnW;
            return Json(_vitals.LstHnW.OrderBy(e => e.CreatedOn), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BPTrend()
        {
            var user = User();
            var list = GetTests.GetAllBloodPicturesByUserId(user.UserId).ToList();
            Session["blood"] = list;
            list = list.Where(x => x.hemo != 0).OrderBy(x => x.TestOnDate).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BSTrend()
        {
            var list = BloodList();
            list = list.Where(x => x.sugar != 0).OrderBy(x => x.TestOnDate).ToList();
            Session["blood"] = null;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FHRTrend()
        {
            var user = User();
            Vitals _vc = new Vitals();
            _vc.lstFhr = GetAllVitals.GetRecentFHRByUserId(user.UserId).OrderBy(x => x.CreatedOn).ToList();
            return Json(_vc.lstFhr, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UrineTestTrend()
        {
            var user = User();
            UrineTestServiceCalls _UTServiceCalls = new UrineTestServiceCalls();
            Vitals _vitals = new Vitals();

            _vitals.lstUrineTest = GetAllVitals.GetRecentUrineTestByUserId(user.UserId).OrderByDescending(x => x.CreatedOn).ToList();

            return Json(_vitals.lstUrineTest.OrderBy(e => e.CreatedOn), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlPtrend()
        {
            var list = WeightList();
            list = list.Where(x => x.systole > 0 && x.diastole > 0).OrderBy(x => x.CreatedOn).ToList();

            Session["hwb"] = null;
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        private UserInformation User()
        {
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            return LoginUser;
        }

        private List<BloodPicture> BloodList()
        {
            var list = Session["blood"] as List<BloodPicture>;
            return (List<BloodPicture>)(list != null ? list : null);
        }

        private List<HeightandWeight> WeightList()
        {
            var list = Session["hwb"] as List<HeightandWeight>;
            return (List<HeightandWeight>)(list != null ? list : null);
        }


    }
}
