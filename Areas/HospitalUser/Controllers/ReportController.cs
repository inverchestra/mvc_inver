using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult AllUsersReports()
        {
            IList<UserInformation> lstUser = new List<UserInformation>();
            var list = new SelectList(new[]
                                          {
                                              new{ID="table",Name="Table"},
                                              new{ID="graph",Name="Graph"},
                                              
                                          },
                          "ID", "Name");
            ViewData["SelectType"] = list;

            return View("UserReports");
        }
        [HttpPost]
        public ActionResult GetAllHospitaluserByDateToDate(string FromDate, string ToDate, string Type)
        {
            UserInformation LoginUser = null;
            if (Session["LoginUser"] != null)
            {
                LoginUser = (UserInformation)Session["LoginUser"];
                LoginUser.IsLoggedIn = false;
                Services1 s1 = new Services1();
            }
            UserInformation userInfo = new UserInformation();
            userInfo.FromDate = Convert.ToDateTime(FromDate);
            userInfo.ToDate = Convert.ToDateTime(ToDate);
            userInfo.DomainId = LoginUser.DomainId;
            if (string.IsNullOrWhiteSpace(Type))
            {
                IList<UserInformation> newuser = GetHospitalUserInfo.GetAllHospitaluserByObj(userInfo);

                return PartialView("HospitalReg", newuser);

            }
            else if (Type == "1")
            {

                IList<Cases> newcase = GetHospitalUserInfo.GetAllIOpatients(new UserInformation() { ToDate = Convert.ToDateTime(ToDate), FromDate = Convert.ToDateTime(FromDate), Type = Type, DomainId = LoginUser.DomainId });
                IList<HospitalUserInfo> lstusers = null;
                var userids = newcase.Select(i => i.CreatedBy).Distinct();
                if (userids.Count() != 0)
                {
                    lstusers = new List<HospitalUserInfo>();
                    foreach (string id in userids)
                    {
                        HospitalUserInfo _user = new HospitalUserInfo();
                        _user = GetHospitalUserInfo.GetUserbyId(id);
                        lstusers.Add(_user);
                    }
                }

              return PartialView("HospitalIo", lstusers);

            }
            else
            {
                IList<Cases> newcase = GetHospitalUserInfo.GetAllIOpatients(new UserInformation() { ToDate = Convert.ToDateTime(ToDate), FromDate = Convert.ToDateTime(FromDate), Type = Type, DomainId = LoginUser.DomainId });
                IList<HospitalUserInfo> lstusers = null;
                var userids = newcase.Select(i => i.CreatedBy).Distinct();
                if (userids.Count() != 0)
                {
                    lstusers = new List<HospitalUserInfo>();
                    foreach (string id in userids)
                    {
                        HospitalUserInfo _user = new HospitalUserInfo();
                        _user = GetHospitalUserInfo.GetUserbyId(id);
                        lstusers.Add(_user);
                    }
                }

                //return PartialView("HospitalIo", newcase);IEnumerable<int> ids = list.Select(x=>x.ID).Distinct();


                return PartialView("HospitalIo", lstusers);
            }

        }

        [HttpPost]
        public ActionResult GetIOpatients(string FromDate, string ToDate, string Type)
        {
            UserInformation LoginUser = null;
            if (Session["LoginUser"] != null)
            {
                LoginUser = (UserInformation)Session["LoginUser"];
                LoginUser.IsLoggedIn = false;
                Services1 s1 = new Services1();
            }

            IList<Cases> newcase = GetHospitalUserInfo.GetAllIOpatients(new UserInformation() { ToDate = Convert.ToDateTime(ToDate), FromDate = Convert.ToDateTime(FromDate), Type = Type, DomainId = LoginUser.DomainId });

            return View(VirtualPathUtility.ToAbsolute("~/Areas/HospitalUser/Views/Report/UserReports.cshtml"), newcase);
        }
       
        public ActionResult Getdata(string FromDate, string ToDate, string Type)
        {
            UserInformation LoginUser = null;
            if (Session["LoginUser"] != null)
            {
                LoginUser = (UserInformation)Session["LoginUser"];
                LoginUser.IsLoggedIn = false;
                Services1 s1 = new Services1();
            }
            UserInformation userInfo = new UserInformation();
            userInfo.FromDate = Convert.ToDateTime(FromDate);
            userInfo.ToDate = Convert.ToDateTime(ToDate);
            userInfo.DomainId = LoginUser.DomainId;
            List<Analysis> obj = new List<Analysis>();
            Analysis newobj ;

            for (DateTime date = Convert.ToDateTime(FromDate).Date; date.Date <= Convert.ToDateTime(ToDate).Date; date = date.AddDays(1))
            {
                newobj = new Analysis();
                if (string.IsNullOrWhiteSpace(Type))
                {
                    newobj.date = date.Date.ToShortDateString();
                    IList<UserInformation> newuser = GetHospitalUserInfo.GetAllHospitaluserByObj(userInfo);
                    newobj.count = newuser.Where(x => x.CreatedOn.Value.Date == date).ToList().Count;
                    obj.Add(newobj);
                }
                else if ((Type == "1"))
                {

                    newobj.date = date.Date.ToShortDateString();
                    IList<Cases> newcase = GetHospitalUserInfo.GetAllIOpatients(new UserInformation() { ToDate = Convert.ToDateTime(ToDate), FromDate = Convert.ToDateTime(FromDate), Type = Type, DomainId = LoginUser.DomainId });
                    newobj.count = newcase.Where(x => x.CreatedOn.Value.Date == date.Date).GroupBy(x=>x.CreatedBy).ToList().Count;
                    obj.Add(newobj);
                   
                  
                }
                else if ((Type == "0"))
                {
                    newobj.date = date.Date.ToShortDateString();
                    IList<Cases> newcase = GetHospitalUserInfo.GetAllIOpatients(new UserInformation() { ToDate = Convert.ToDateTime(ToDate), FromDate = Convert.ToDateTime(FromDate), Type = Type, DomainId = LoginUser.DomainId });
                    newobj.count = newcase.Where(x => x.CreatedOn.Value.Date == date.Date).GroupBy(x => x.CreatedBy).ToList().Count;
                    obj.Add(newobj);
                }


            }

            ViewBag.chartdata = obj;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

    }
}
