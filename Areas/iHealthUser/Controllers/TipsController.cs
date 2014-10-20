using System;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers {
    [Authorize]
    public class TipsController : Controller {

        public ActionResult WeekTips(int week, int day) {
            week = week > 42 ? 42 : week; week = week < 1 ? 1 : week;
            day = day > 7 ? 7 : day; day = day < 1 ? 1 : day;
            var tip = TipService.WeekTip(week, day);
            return Json(tip, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrevWeekTip(int week) {
            var tip = TipService.WeekTip(week, 7);
            return Json(tip, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Tip(int week = 1, int day = 1) {
            week = week > 42 ? 42 : week; week = week < 1 ? 1 : week;
            day = day > 7 ? 7 : day; day = day < 1 ? 1 : day;
            var tip = TipService.WeekTip(week, day);
            return View(tip);
        }

        public string UnreadTips(int week) {
            var user = HttpContext.User.Identity.Name;
            var cnt = TipService.UnreadTips(week, user);
            return cnt == "0" ? "" : cnt;
        }

        public string UpdateTip(int week) {
            return TipService.UpdateTip(HttpContext.User.Identity.Name, week.ToString());
        }

    }
}
