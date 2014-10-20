using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Generator;
using Ionic.Zip;
using System.Runtime.Serialization.Json;
using PagedList;
using Aspose.Pdf.Facades;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    [SessionExpireFilter]
    public class CasesController : Controller
    {
        //[Authorize]
        public ActionResult Cases(int? page)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            UserInformation LoginUser = null;
            RegistrationServiceCalls reguser = new RegistrationServiceCalls();
            if (Session["CurrentUser"] != null)
                LoginUser = Session["CurrentUser"] as UserInformation;
            else
                LoginUser = Session["LoginUser"] as UserInformation;

            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }

            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);
            DateTime CurrentDate = DateTime.Now.Date;
            ViewBag.ImageName = m.PInfo1.ImageName;
            DateTime startDate = (DateTime)LoginUser.StartDate;
            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);
            SingleRegisterModel domainUser = reguser.GetSingleRegUserbyID(LoginUser.DomainId.ToString());//added by ck for FreeUser modification
            if (domainUser.UserPlan == "FreeUser")
            {
                DateTime expire = Convert.ToDateTime(domainUser.ExpireOn);
                DateTime presenttime = DateTime.Now;
                //var ms = expire.Subtract(presenttime).TotalDays;
                domainUser.Result = (expire.Date - presenttime.Date).Days;
                return View("SubscribeNow", domainUser);
            }

          

            return View(GetUserCases.GetAllCases(LoginUser.UserId,LoginUser.GroupType).ToPagedList(PageNumber, PageSize));
        }

        public ActionResult GetSearchedCases(int? page, string searchtext1)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            SearchFields sFileds = new SearchFields();
            sFileds.Cases = searchtext1;

            sFileds.UserId = LoginUser.UserId;
            ViewBag.FromSearch = searchtext1;
            return View("Cases", GetUserCases.GetAllSearchedCases(sFileds).ToPagedList(PageNumber, PageSize));

        }

        public ActionResult EditCase(string CaseId)
        {
            string currentpage = "HealthRecord";
            Session["CurrentPage"] = currentpage;
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            GetUserCases _getUserCase;
            _getUserCase = new GetUserCases();
            Getdocuments _objdocuments = new Getdocuments();
            Services3 s3 = new Services3();
            MedicalInformation medinfo = new MedicalInformation();

            medinfo = s3.GetMedicalInfo(LoginUser.UserId);
            Cases _cases = _getUserCase.GetCaseByID(CaseId);
            _cases.lstVisits = GetVisits.GetAllVisitsByCaseId(CaseId);
            _cases.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(CaseId);
            foreach (MedicalTests medTest in _cases.lstMedicalTest)
            {
                medTest.lstDocuments = _objdocuments.GetDocumentsbyMedicalTestId(medTest.MedicalTestId);
            }

            _cases.lstProcedure = GetProcedures.GetAllProceduresByCaseId(CaseId);
            foreach (Procedure procedure in _cases.lstProcedure)
            {
                procedure.lstDocuments = _objdocuments.GetDocumentsbyProcId(procedure.ProcedureId);
            }

            _cases.lstMedicalSchedule = GetMedicalSchedules.GetAllMeicalschdulesbyCaseId(CaseId);
            foreach (MedicalSchedule medSchedule in _cases.lstMedicalSchedule)
            {
                medSchedule.lstDocuments = _objdocuments.GetDocumentsbyMedicalScheduleId(medSchedule.MedicalScheduleId);

            }

            _cases.lstChart = GetCharts.GetAllChartsByCaseId(CaseId);
            foreach (Charts chrt in _cases.lstChart)
            {
                chrt.lstDocuments = _objdocuments.GetDocumentsbyChartId(chrt.ChartId);
            }


            _cases.lstcasetocase = GetCaseTocases.GetAllCaseToCasesByCaseId(CaseId);
            _cases.lstcasetolog = GetCaseToLogs.GetAllCaseTologsByCaseId(CaseId);
            _cases.lstCases = GetUserCases.GetAllCases(LoginUser.UserId,LoginUser.GroupType);
            _cases.lstCases.Where(a => a.CaseId == CaseId).ToList().ForEach(a => _cases.lstCases.Remove(a));


            _cases.lstHealthlog = GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType);
            _cases.lstresultCaselist = ResultList.GetResulCaseList(_cases.lstCases, _cases.lstcasetocase);
            _cases.lstresultLoglist = ResultList.GetResultLogList(_cases.lstHealthlog, _cases.lstcasetolog);
            _cases.BMIHeight = medinfo.BMIHeight;
            _cases.BMIWeight = medinfo.BMIWeight;

            //for rightside pannel
            MedicalAndPersonal m = new MedicalAndPersonal();
            // Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);
            DateTime CurrentDate = DateTime.Now.Date;
            ViewBag.ImageName = m.PInfo1.ImageName;
            DateTime startDate = (DateTime)LoginUser.StartDate;
            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);
            //ends here



            return View("EditCase", _cases);
        }

        public ActionResult UpdateCase(Cases cs)
        {

            string currentpage = "HealthRecord";
            Session["CurrentPage"] = currentpage;
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            Cases csInfo = new Cases();
            MedicalInformation _medInfo = new MedicalInformation(); // sandeep added

            string a = "cf";
            string strDate = DateTime.Now.ToString("ddMMyyyyhhmm");
            string retCfName = a + strDate;
            csInfo.CaseId = cs.CaseId;
            csInfo.CaseName = cs.CaseName;
            csInfo.CaseDescription = cs.CaseDescription;
            csInfo.CfId = retCfName;

            csInfo.HospitalInfo = XmlStringListSerializer.ToXmlString<IList<Hospital>>(cs.HospitalInfos);
            csInfo.PatientType = cs.PatientType;

            csInfo.CustHealthlogDesc = cs.CustHealthlogDesc;

            csInfo.TypeOfProblem = cs.TypeOfProblem;

            csInfo.DomainId = LoginUser.DomainId;
            csInfo.ModifiedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                csInfo.ModifiedBy = CreatedByUser.UserId;
                csInfo.MType = CreatedByUser.GroupType;
            }
            
            csInfo.CreatedBy = LoginUser.UserId;
            csInfo.CreatedOn = DateTime.Now;

            string successcode = EditUserCases.UpdateCase(csInfo, LoginUser.GroupType);
            if (successcode == "1020")
            {
                if (cs.s1 != null)
                {
                    CaseToCase ctc = null;
                    ctc = new CaseToCase();
                    foreach (var v in cs.s1)
                    {
                        ctc.CaseId = cs.CaseId;
                        ctc.RelatedCaseId = v;
                        string InsertCasetoCase = CreateCaseToCase.InsertCaseToCaseInfo(ctc);
                    }
                }
                if (cs.s2 != null)
                {
                    CaseToLog ctl = null;
                    ctl = new CaseToLog();
                    foreach (var v in cs.s2)
                    {
                        ctl.CaseId = cs.CaseId;
                        ctl.LogId = v;
                        string InsertCasetoLog = CreateCaseToLog.InsertCaseToLogInfo(ctl.LogId.ToString(), ctl.CaseId.ToString());

                    }
                }

                //Services3 _updateMedInfo = new Services3();
                //_updateMedInfo.UpdateMedicalInfo(_medInfo, csInfo.CreatedBy.ToString(), csInfo.DomainId.ToString(), "PInfo3"); // sandeep added

            }
            cs.CaseId = successcode;
            JsonResult r = new JsonResult();
            if (successcode == "1020")
            {
                r.Data = successcode;
                return r;
            }
            else
            {
                r.Data = "Failed";
                return r;
            }
        }

        public ActionResult UpdateProcView(string procId)
        {
            Cases cases = new Cases();
            cases.Procedures = GetProcedures.GetProcedureById(procId);
            return PartialView("UpdateProcedure", cases);
        }

        public ActionResult UpdateMScheduleView(string mscId)
        {
            Cases cases = new Cases();
            cases.MedicalSchdule = GetMedicalSchedules.GetMedicationScheduleById(mscId);

            return PartialView("UpdateMedicalSchedule", cases);
        }

        public ActionResult UpdateMTView(string mtId)
        {
            Cases cases = new Cases();
            cases.MedicalTests = GetMedicalTests.GetMedicalTestById(mtId);
            return PartialView("UpdateMedicalTest", cases);
        }

        public ActionResult UpdateCTView(string ctId)
        {
            Cases cases = new Cases();
            cases.Charts = GetCharts.GetChartById(ctId);
            return PartialView("UpdateCharts", cases);
        }

        public ActionResult UpdateVisitView(string vstId)
        {
            Cases cases = new Cases();
            cases.Visits = GetVisits.GetVisitsById(vstId);
            return PartialView("UpdateVisit", cases);
        }

        [HttpPost]
        public ActionResult UpdateProcedure(Cases cs)
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
            EditProcedure _objEditProc = new EditProcedure();

            //dileep
            cs.Procedures.ModifiedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                cs.Procedures.ModifiedBy = CreatedByUser.UserId;
                cs.Procedures.MType = CreatedByUser.GroupType;
            }
            //dileep

            string ProcedureId = _objEditProc.UpdateProcedure(cs.Procedures);

            string success = InsertFiles("proc", cs.Procedures.ProcedureId, cs.Procedures.CaseId, cs.procfiles);

            cs.lstProcedure = GetProcedures.GetAllProceduresByCaseId(cs.Procedures.CaseId);


            return PartialView("EditProcedures", cs);
        }

        [HttpPost]
        public ActionResult UpdateMedicalTest(Cases cs)
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
            EditMedicalTest _objEditProc = new EditMedicalTest();
            //dileep
            cs.MedicalTests.ModifiedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                cs.MedicalTests.ModifiedBy = CreatedByUser.UserId;
                cs.MedicalTests.MType = CreatedByUser.GroupType;
            }
            //dileep
            string successCode = _objEditProc.UpdateMedicalTest(cs.MedicalTests);

            string success = InsertFiles("medt", cs.MedicalTests.MedicalTestId, cs.MedicalTests.CaseId, cs.MTfiles);

            cs.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(cs.MedicalTests.CaseId);


            return PartialView("EditMTests", cs);
        }

        [HttpPost]
        public ActionResult UpdateCharts(Cases cs)
        {

            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["Currentuser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            EditChart _objEditChart = new EditChart();
            //dileep
            cs.Charts.ModifiedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                cs.Charts.ModifiedBy = CreatedByUser.UserId;
                cs.Charts.MType = CreatedByUser.GroupType;
            }
            //dileep
            string sucessCode = _objEditChart.UpdateChart(cs.Charts);

            string sucess = InsertFiles("chrt", cs.Charts.ChartId, cs.Charts.CaseId, cs.MTfiles);

            cs.lstChart = GetCharts.GetAllChartsByCaseId(cs.Charts.CaseId);

            return PartialView("EditCharts", cs);

        }

        [HttpPost]
        public ActionResult UpdateVisits(Cases cs)
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
            EditVisits _objEditProc = new EditVisits();
            //dileep
            cs.Visits.ModifiedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                cs.Visits.ModifiedBy = CreatedByUser.UserId;
                cs.Visits.MType = CreatedByUser.GroupType;
            }
            //dileep
            cs.Visits.NurseReadings = XmlStringListSerializer.ToXmlString<NurseReadings>(cs.Visits.NurseReadingsInfo);
            Services3 s3 = new Services3();
            MedicalInformation MedicInfo = new MedicalInformation();
            MedicInfo = s3.GetMedicalInfo(LoginUser.UserId);
            if (cs.Visits.NurseReadingsInfo != null)
            {

                if (cs.Visits.NurseReadingsInfo.Height >= 0)
                {
                    MedicInfo.BMIHeight = cs.Visits.NurseReadingsInfo.Height;
                }
                if (cs.Visits.NurseReadingsInfo.Weight >= 0)
                {
                    MedicInfo.BMIWeight = cs.Visits.NurseReadingsInfo.Weight;
                }
                if (cs.Visits.NurseReadingsInfo.Sistole != null || cs.Visits.NurseReadingsInfo.Diastole != null)
                {
                    string bp = cs.Visits.NurseReadingsInfo.Sistole + "/" + cs.Visits.NurseReadingsInfo.Diastole;
                    MedicInfo.BloodPressure = bp;
                }
                MedicInfo.UpdatedBy = LoginUser.UserId;
                MedicInfo.UpdatedOn = DateTime.Now;

                int result;
                if (cs.Visits.NurseReadingsInfo.Height >= 0 || cs.Visits.NurseReadingsInfo.Weight >= 0)
                {
                    float BMIValue = (Convert.ToSingle(MedicInfo.BMIWeight)) / ((Convert.ToSingle(MedicInfo.BMIHeight) / 100) * (Convert.ToSingle(MedicInfo.BMIHeight) / 100));
                    MedicInfo.BMIValue = Math.Round(BMIValue, 2);
                    result = s3.UpdateMedicalInfo(MedicInfo, LoginUser.UserId.ToString(), LoginUser.DomainId.ToString(), "PInfo3", LoginUser.GroupType);

                }


            }
            //cs.Visits.DoctorsInfo = XmlStringListSerializer.ToXmlString<Doctor>(cs.Visits.DoctorInfos);
            string successCode = _objEditProc.UpdateVisit(cs.Visits);

            cs.lstVisits = GetVisits.GetAllVisitsByCaseId(cs.Visits.CaseId);


            return PartialView("EditVisits", cs);
        }

        [HttpPost]
        public ActionResult UpdateMedicalSchedule(Cases cs)
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
            EditMedicalSchedule _objEditMs = new EditMedicalSchedule();
            cs.MedicalSchdule.Schedule = XmlStringListSerializer.ToXmlString<IList<Schedule>>(cs.ScheduleInfo);
            //dileep
            cs.MedicalSchdule.ModifiedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                cs.MedicalSchdule.ModifiedBy = CreatedByUser.UserId;
                cs.MedicalSchdule.MType = CreatedByUser.GroupType;
            }
            //dileep
            string successCode = _objEditMs.UpdateMedicalSchedule(cs.MedicalSchdule);

            string success = InsertFiles("medsc", cs.MedicalSchdule.MedicalScheduleId, cs.MedicalSchdule.CaseId, cs.msfiles);

            cs.lstMedicalSchedule = GetMedicalSchedules.GetAllMeicalschdulesbyCaseId(cs.MedicalSchdule.CaseId);

            return PartialView("EditMedicalSchedule", cs);
        }

        public ActionResult CreateCase()
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

            MedicalAndPersonal m = new MedicalAndPersonal();
            Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);
            DateTime CurrentDate = DateTime.Now.Date;
            ViewBag.ImageName = m.PInfo1.ImageName;
            DateTime startDate = (DateTime)LoginUser.StartDate;
            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);

            return View(new Cases(LoginUser.UserId,LoginUser.GroupType));
        }

        [HttpPost]
        public ActionResult SaveCase(Cases cases)
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


            Cases csInfo = new Cases();
            Services3 s3 = new Services3();
            MedicalInformation MedicInfo = new MedicalInformation();
            MedicInfo = s3.GetMedicalInfo(LoginUser.UserId);

            string a = "cf";
            string strDate = DateTime.Now.ToString("ddMMyyyyhhmm");
            string retCfName = a + strDate;

            csInfo.CaseName = cases.CaseName;
            csInfo.CaseDescription = cases.CaseDescription;
            csInfo.CfId = retCfName;

            csInfo.HospitalInfo = XmlStringListSerializer.ToXmlString<IList<Hospital>>(cases.HospitalInfos);
            csInfo.PatientType = cases.PatientType;

            csInfo.CustHealthlogDesc = cases.CustHealthlogDesc;

            csInfo.TypeOfProblem = cases.TypeOfProblem;

            csInfo.DomainId = LoginUser.DomainId;
            csInfo.CreatedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                csInfo.CreatedBy = CreatedByUser.UserId;
            }
            csInfo.OwnerId = LoginUser.UserId;
            csInfo.Type = LoginUser.GroupType;          
           
            MedicInfo.BMIHeight = cases.BMIHeight;
            MedicInfo.BMIWeight = cases.BMIWeight;
            MedicInfo.UpdatedBy = LoginUser.UserId;
            MedicInfo.UpdatedOn = DateTime.Now;

            string caseId = CreateUserCase.CreateCase(csInfo, LoginUser.GroupType);
            //int result;
            //if (!String.IsNullOrEmpty(cases.BMIHeight.ToString()) && !String.IsNullOrEmpty(cases.BMIWeight.ToString()))
            //    result = s3.UpdateMedicalInfo(MedicInfo, LoginUser.UserId.ToString(), LoginUser.DomainId.ToString(), "PInfo3");

            if (cases.s1 != null)
            {
                CaseToCase ctc = null;
                ctc = new CaseToCase();
                foreach (var v in cases.s1)
                {
                    ctc.CaseId = caseId;
                    ctc.RelatedCaseId = v;
                    string InsertCasetoCase = CreateCaseToCase.InsertCaseToCaseInfo(ctc);
                }
            }
            if (cases.s2 != null)
            {
                CaseToLog ctl = null;
                ctl = new CaseToLog();
                foreach (var v in cases.s2)
                {
                    string lId = v;
                    string csId = caseId;
                    string InsertCasetoLog = CreateCaseToLog.InsertCaseToLogInfo(lId.ToString(), csId.ToString());

                }
            }

            cases.CaseId = caseId;
            JsonResult js = new JsonResult();
            js.Data = caseId;
            return js;
        }

        public ActionResult CreateProcedures(string caseId)
        {
            return PartialView(new Cases { CaseId = caseId });
        }

        [HttpPost]
        public ActionResult SaveProcedure(Cases cases)
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
            cases.Procedures.CaseId = cases.CaseId;
            // dileep
            cases.Procedures.CreatedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                cases.Procedures.UserID = CreatedByUser.UserId;
            }
            cases.Procedures.OwnerId = LoginUser.UserId;
            cases.Procedures.Type = LoginUser.GroupType;


            //dileep
            string ProcedureId = CreateProcedure.InsertProcedureInfo(cases.Procedures);

            string success = InsertFiles("proc", ProcedureId, cases.CaseId, cases.procfiles);

            cases.lstProcedure = GetProcedures.GetAllProceduresByCaseId(cases.CaseId);

            return PartialView("ProcedureList", cases);

        }

        public ActionResult CreateMedicalTests(string caseId)
        {
            return PartialView(new Cases { CaseId = caseId });
        }

        [HttpPost]
        public ActionResult SaveMedicalTest(Cases cases)
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
            cases.MedicalTests.CaseId = cases.CaseId;

            // dileep
            cases.MedicalTests.CreatedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                cases.MedicalTests.UserID = CreatedByUser.UserId;
            }
            cases.MedicalTests.OwnerId = LoginUser.UserId;
            cases.MedicalTests.Type = LoginUser.GroupType;
            //dileep
            string MedicalTestId = CreateMedicalTest.InsertMeidcalTestInfo(cases.MedicalTests);

            string success = InsertFiles("medt", MedicalTestId, cases.CaseId, cases.MTfiles);

            cases.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(cases.CaseId);

            return PartialView("MedicalTestList", cases);
        }

        public ActionResult CreateCharts(string caseId)
        {
            return PartialView(new Cases { CaseId = caseId });
        }
        [HttpPost]
        public ActionResult SaveChart(Cases cases)
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
            cases.Charts.CaseId = cases.CaseId;

            // dileep
            cases.Charts.CreatedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                cases.Charts.UserID = CreatedByUser.UserId;
            }
            cases.Charts.OwnerId = LoginUser.UserId;
            cases.Charts.Type = LoginUser.GroupType;


            //dileep

            string ChartId = CreateChart.InsertChartInfo(cases.Charts);

            string success = InsertFiles("chrt", ChartId, cases.CaseId, cases.MTfiles);

            cases.lstChart = GetCharts.GetAllChartsByCaseId(cases.CaseId);
            return PartialView("ChartList", cases);
        }

        public ActionResult CreateVisits(string caseId)
        {
            return PartialView(new Cases { CaseId = caseId });
        }

        [HttpPost]
        public ActionResult SaveVisit(Cases cases)
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
            cases.Visits.CaseId = cases.CaseId;

            // dileep
            cases.Visits.CreatedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                cases.Visits.UserID = CreatedByUser.UserId;
            }
            cases.Visits.OwnerId = LoginUser.UserId;
            cases.Visits.Type = LoginUser.GroupType;


            //dileep

            Services3 s3 = new Services3();
            MedicalInformation MedicInfo = new MedicalInformation();
            MedicInfo = s3.GetMedicalInfo(LoginUser.UserId);
            if (cases.Visits.NurseReadingsInfo != null)
            {

                if (cases.Visits.NurseReadingsInfo.Height >= 0)
                {
                    MedicInfo.BMIHeight = cases.Visits.NurseReadingsInfo.Height;
                }
                if (cases.Visits.NurseReadingsInfo.Weight >= 0)
                {
                    MedicInfo.BMIWeight = cases.Visits.NurseReadingsInfo.Weight;
                }
                if (cases.Visits.NurseReadingsInfo.Sistole != null || cases.Visits.NurseReadingsInfo.Diastole != null)
                {
                    string bp = cases.Visits.NurseReadingsInfo.Sistole + "/" + cases.Visits.NurseReadingsInfo.Diastole;
                    MedicInfo.BloodPressure = bp;
                }
                MedicInfo.UpdatedBy = LoginUser.UserId;
                MedicInfo.UpdatedOn = DateTime.Now;

                int result;
                if (cases.Visits.NurseReadingsInfo.Height >= 0 || cases.Visits.NurseReadingsInfo.Weight >= 0)
                {
                    float BMIValue = (Convert.ToSingle(MedicInfo.BMIWeight)) / ((Convert.ToSingle(MedicInfo.BMIHeight) / 100) * (Convert.ToSingle(MedicInfo.BMIHeight) / 100));
                    MedicInfo.BMIValue = Math.Round(BMIValue, 2);
                    result = s3.UpdateMedicalInfo(MedicInfo, LoginUser.UserId.ToString(), LoginUser.DomainId.ToString(), "PInfo3", LoginUser.GroupType);

                }
                cases.Visits.NurseReadings = XmlStringListSerializer.ToXmlString<NurseReadings>(cases.Visits.NurseReadingsInfo);

            }



            string VisitId = CreateVisit.CreateVisitInfo(cases.Visits);

            cases.lstVisits = GetVisits.GetAllVisitsByCaseId(cases.CaseId);

            return PartialView("VisitList", cases);
        }



        public ActionResult CreateMedicalSchedules(string caseId)
        {
            return PartialView("Prescription", new Cases { CaseId = caseId });
        }

        [HttpPost]
        public ActionResult SaveMedicalSchedule(Cases cases)
        {
            //if (ModelState.IsValid)
            //{
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            string a = "prno";
            string strDate = DateTime.Now.ToString("ddMMyyyyhhmmss");
            string retCfName = a + strDate;
            cases.MedicalSchdule.PrescriptionNo = retCfName;
            cases.MedicalSchdule.CaseId = cases.CaseId;
            cases.MedicalSchdule.Schedule = XmlStringListSerializer.ToXmlString<IList<Schedule>>(cases.ScheduleInfo);

            // dileep
            cases.MedicalSchdule.CreatedOn = DateTime.Now;
            if (Session["LoginUser"] != null)
            {
                UserInformation CreatedByUser = null;
                CreatedByUser = Session["LoginUser"] as UserInformation;
                cases.MedicalSchdule.UserID = CreatedByUser.UserId;
            }
            cases.MedicalSchdule.OwnerId = LoginUser.UserId;
            cases.MedicalSchdule.Type = LoginUser.GroupType;


            //dileep
            string MedicalScId = CreateMedicalSchedule.InsertMeidcalScheduleInfo(cases.MedicalSchdule);

            string success = InsertFiles("medsc", MedicalScId, cases.CaseId, cases.msfiles);

            cases.lstMedicalSchedule = GetMedicalSchedules.GetAllMeicalschdulesbyCaseId(cases.CaseId);

            return PartialView("MedicalScheduleList", cases);
            //}
            //return PartialView("Prescription", cases);
        }

        private string InsertFiles(string op, string operationId, string caseId, IEnumerable<HttpPostedFileBase> files)
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
            Documents doc = new Documents();
            if (files != null)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        Stream inputStream = file.InputStream;
                        doc.Name = Path.GetFileName(file.FileName);
                        byte[] arraybyt = new byte[file.ContentLength];
                        inputStream.Read(arraybyt, 0, file.ContentLength);
                        doc.yData = arraybyt;

                        doc.UserEmail = LoginUser.EmailId;

                        doc.DocSource = "1";
                        doc.OwnerId = LoginUser.UserId;
                        doc.Type = LoginUser.GroupType;
                        if (Session["LoginUser"] != null)
                        {
                            UserInformation CreatedByUser = null;
                            CreatedByUser = Session["LoginUser"] as UserInformation;
                            doc.CreatedBy = CreatedByUser.UserId;
                        }
                        doc.CaseId = caseId;
                        doc.CreatedOn = DateTime.Now;

                        if (op == "proc")
                        {
                            doc.ProcedureId = operationId;
                        }
                        else if (op == "medt")
                        {
                            doc.MedicalLabTestId = operationId;
                        }
                        else if (op == "medsc")
                        {
                            doc.MedicalScheduleTestId = operationId;
                        }
                        else if (op == "chrt")
                        {
                            doc.ChartId = operationId;
                        }
                        doc.UserID = LoginUser.UserId;
                        doc.Type = LoginUser.GroupType;
                        doc.DomainID = LoginUser.DomainId;
                        string s = UploadFiles.UploadFile(doc);
                    }
                }
            }

            return "1010";
        }

        #region Delete Cases Procedures,MedicalTests,Medications,Visits

        [HttpPost]
        public ActionResult DeleteCase(string id)
        {
            GetUserCases _getCase;
            _getCase = new GetUserCases();
            string Sucesscode = _getCase.deleteCaseByID(id);
            string currentpage = "HealthRecord";
            Session["CurrentPage"] = currentpage;
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }


            JsonResult r = new JsonResult();
            if (Sucesscode == "1020")
            {
                r.Data = Sucesscode;
                return r;
            }
            else
            {
                r.Data = "Failed";
                return r;
            }
        }

        //public ActionResult DeleteProcedure(int id)
        //{
        //    GetProcedures _getProc;
        //    _getProc = new GetProcedures();
        //    string Sucesscode = _getProc.DeleteProcedure(id);

        //    JsonResult r = new JsonResult();

        //    if (Sucesscode == "1020")
        //    {
        //        r.Data = Sucesscode;
        //        return r;
        //    }
        //    else
        //    {
        //        r.Data = "Failed";
        //        return r;
        //    }
        //}

        //public ActionResult DeleteMedicalTest(int id)
        //{
        //    GetMedicalTests _getMtest;
        //    _getMtest = new GetMedicalTests();
        //    string Sucesscode = _getMtest.DeleteMTestbyMtestId(id);
        //    JsonResult r = new JsonResult();
        //    if (Sucesscode == "1020")
        //    {
        //        r.Data = Sucesscode;
        //        return r;
        //    }
        //    else
        //    {
        //        r.Data = "Failed";
        //        return r;
        //    }
        //}
        public ActionResult DeleteProcedure(Cases cs)
        {
            GetProcedures _getProc;
            _getProc = new GetProcedures();
            string Sucesscode = _getProc.DeleteProcedure(cs.Procedures.ProcedureId);
            cs.lstProcedure = GetProcedures.GetAllProceduresByCaseId(cs.Procedures.CaseId);
            cs.CaseId = cs.Procedures.CaseId;
            return PartialView("EditProcedures", cs);
        }

        public ActionResult DeleteMedicalTest(Cases cs)
        {
            //GetMedicalTests _getMtest;
            //_getMtest = new GetMedicalTests();
            //string Sucesscode = _getMtest.DeleteMTestbyMtestId(id);
            //JsonResult r = new JsonResult();
            //if (Sucesscode == "1020")
            //{
            //    r.Data = Sucesscode;
            //    return r;
            //}
            //else
            //{
            //    r.Data = "Failed";
            //    return r;
            //}
            // GetProcedures _getProc;
            GetMedicalTests _getMtest;
            _getMtest = new GetMedicalTests();
            string Sucesscode = _getMtest.DeleteMTestbyMtestId(cs.MedicalTests.MedicalTestId);
            cs.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(cs.MedicalTests.CaseId);
            //cs.lstProcedure = GetProcedures.GetAllProceduresByCaseId(Convert.ToInt32(cs.Procedures.CaseId));
            cs.CaseId = cs.MedicalTests.CaseId;
            return PartialView("EditMTests", cs);
        }

        public ActionResult DeleteMedication(string id)
        {
            GetMedicalSchedules _getMedSch;
            _getMedSch = new GetMedicalSchedules();
            string Sucesscode = _getMedSch.DeleteMedSchById(id);

            JsonResult r = new JsonResult();

            if (Sucesscode == "1020")
            {
                r.Data = Sucesscode;
                return r;
            }
            else
            {
                r.Data = "Failed";
                return r;
            }
        }

        public ActionResult DeleteVisit(Cases cs)
        {
            //GetMedicalTests _getMtest;
            GetVisits _getVisit;
            _getVisit = new GetVisits();
            //_getMtest = new GetMedicalTests();
            string Sucesscode = _getVisit.DeleteVisitsById(cs.Visits.visitId);
            cs.lstVisits = GetVisits.GetAllVisitsByCaseId(cs.Visits.CaseId);
            //cs.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(Convert.ToInt32(cs.MedicalTests.CaseId));
            //cs.lstProcedure = GetProcedures.GetAllProceduresByCaseId(Convert.ToInt32(cs.Procedures.CaseId));
            cs.CaseId = cs.Visits.CaseId;
            return PartialView("EditVisits", cs);
        }

        public ActionResult DeleteChart(Cases cs)
        {
            //GetCharts _getChart;
            //_getChart = new GetCharts();
            //string Sucesscode = _getChart.DeleteChartByChartId(id);
            //JsonResult r = new JsonResult();
            //if (Sucesscode == "1020")
            //{
            //    r.Data = Sucesscode;
            //    return r;

            //}
            //else
            //{
            //    r.Data = "Failed";
            //    return r;
            //}
            //GetVisits _getVisit;
            GetCharts _getchart;
            _getchart = new GetCharts();
            //_getMtest = new GetMedicalTests();
            string Sucesscode = _getchart.DeleteChartByChartId(cs.Charts.ChartId);
            cs.lstChart = GetCharts.GetAllChartsByCaseId(cs.Charts.CaseId);
            //cs.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(Convert.ToInt32(cs.MedicalTests.CaseId));
            //cs.lstProcedure = GetProcedures.GetAllProceduresByCaseId(Convert.ToInt32(cs.Procedures.CaseId));
            cs.CaseId = cs.Charts.CaseId;
            return PartialView("EditCharts", cs);
        }

        #endregion

        public ActionResult CaseView(string CaseId)
        {
            string currentpage = "Cases";
            Session["CurrentPage"] = currentpage;
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            GetUserCases _getUserCase;
            _getUserCase = new GetUserCases();
            Getdocuments _objdocuments = new Getdocuments();
            Services3 s3 = new Services3();
            MedicalInformation medinfo = new MedicalInformation();

            medinfo = s3.GetMedicalInfo(LoginUser.UserId);
            Cases _cases = _getUserCase.GetCaseByID(CaseId);
            _cases.lstVisits = GetVisits.GetAllVisitsByCaseId(CaseId);
            _cases.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(CaseId);
            foreach (MedicalTests medTest in _cases.lstMedicalTest)
            {
                medTest.lstDocuments = _objdocuments.GetDocumentsbyMedicalTestId(medTest.MedicalTestId);
            }

            _cases.lstProcedure = GetProcedures.GetAllProceduresByCaseId(CaseId);
            foreach (Procedure procedure in _cases.lstProcedure)
            {
                procedure.lstDocuments = _objdocuments.GetDocumentsbyProcId(procedure.ProcedureId);
            }

            _cases.lstMedicalSchedule = GetMedicalSchedules.GetAllMeicalschdulesbyCaseId(CaseId);
            foreach (MedicalSchedule medSchedule in _cases.lstMedicalSchedule)
            {
                medSchedule.lstDocuments = _objdocuments.GetDocumentsbyMedicalScheduleId(medSchedule.MedicalScheduleId);

            }

            _cases.lstChart = GetCharts.GetAllChartsByCaseId(CaseId);
            foreach (Charts chrts in _cases.lstChart)
            {
                chrts.lstDocuments = _objdocuments.GetDocumentsbyChartId(chrts.ChartId);
            }

            _cases.lstcasetocase = GetCaseTocases.GetAllCaseToCasesByCaseId(CaseId);
            _cases.lstcasetolog = GetCaseToLogs.GetAllCaseTologsByCaseId(CaseId);
            _cases.lstCases = GetUserCases.GetAllCases(LoginUser.UserId,LoginUser.GroupType);
            _cases.lstHealthlog = GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType);
            _cases.lstresultCaselist = ResultList.GetResulCaseList(_cases.lstCases, _cases.lstcasetocase);
            _cases.lstCases.Where(a => a.CaseId == CaseId).ToList().ForEach(a => _cases.lstCases.Remove(a));
            _cases.lstresultLoglist = ResultList.GetResultLogList(_cases.lstHealthlog, _cases.lstcasetolog);
            _cases.BMIHeight = medinfo.BMIHeight;
            _cases.BMIWeight = medinfo.BMIWeight;


            //for rightside pannel
            MedicalAndPersonal m = new MedicalAndPersonal();
            // Services3 s3 = new Services3();
            m.PInfo1 = s3.GetPersonalInfo(LoginUser.UserId);
            DateTime CurrentDate = DateTime.Now.Date;
            ViewBag.ImageName = m.PInfo1.ImageName;
            DateTime startDate = (DateTime)LoginUser.StartDate;
            int currentDay = Convert.ToInt32(CurrentDate.Subtract(startDate).TotalDays) + 1;
            ViewBag.currentday = currentDay;
            DateTime d = Convert.ToDateTime(LoginUser.StartDate);
            ViewBag.EDDdate = d.AddDays(280);
            //ends here


            return View("CaseView", _cases);
        }

        public ActionResult HandDinfo(string CaseId)
        {
            string currentpage = "HealthRecord";
            Session["CurrentPage"] = currentpage;
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
            GetUserCases _getUserCase;
            _getUserCase = new GetUserCases();
            Getdocuments _objdocuments = new Getdocuments();
            Services3 s3 = new Services3();
            MedicalInformation medinfo = new MedicalInformation();

            medinfo = s3.GetMedicalInfo(LoginUser.UserId);
            Cases _cases = _getUserCase.GetCaseByID(CaseId);
            _cases.lstVisits = GetVisits.GetAllVisitsByCaseId(CaseId);
            _cases.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(CaseId);
            return PartialView("HandDinfo", _cases);
        }

        public ActionResult AddProcedures(string caseId)
        {
            return PartialView("CreateProcedures", new Cases { CaseId = caseId });
        }

        [HttpPost]
        public ActionResult SaveEditAddProcedure(Cases cases)
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
            cases.Procedures.CaseId = cases.CaseId;
            string ProcedureId = CreateProcedure.InsertProcedureInfo(cases.Procedures);

            string success = InsertFiles("proc", ProcedureId, cases.CaseId, cases.procfiles);

            cases.lstProcedure = GetProcedures.GetAllProceduresByCaseId(cases.CaseId);

            return PartialView("EditProcedures", cases);
        }

        public ActionResult AddMedicalTests(string caseId)
        {
            return PartialView("CreateMedicalTests", new Cases { CaseId = caseId });
        }

        [HttpPost]
        public ActionResult SaveEditAddMedicalTest(Cases cases)
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
            cases.MedicalTests.CaseId = cases.CaseId;
            string MedicalTestId = CreateMedicalTest.InsertMeidcalTestInfo(cases.MedicalTests);

            string success = InsertFiles("medt", MedicalTestId, cases.CaseId, cases.MTfiles);

            cases.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(cases.CaseId);

            return PartialView("EditMTests", cases);

        }

        public ActionResult AddCharts(string caseId)
        {
            return PartialView("CreateCharts", new Cases { CaseId = caseId });
        }

        [HttpPost]
        public ActionResult SaveEditAddChart(Cases cases)
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
            cases.Charts.CaseId = cases.CaseId;
            string ChartId = CreateChart.InsertChartInfo(cases.Charts);

            string sucess = InsertFiles("chrt", ChartId, cases.CaseId, cases.MTfiles);

            cases.lstChart = GetCharts.GetAllChartsByCaseId(cases.CaseId);

            return PartialView("EditCharts", cases);

        }

        public ActionResult AddMedicalSchedules(string caseId)
        {
            return PartialView("CreateMedicalSchedules", new Cases { CaseId = caseId });
        }

        [HttpPost]
        public ActionResult SaveEditAddMedicalSchedule(Cases cases)
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
            string a = "prno";
            string strDate = DateTime.Now.ToString("ddMMyyyyhhmmss");
            string retCfName = a + strDate;
            cases.MedicalSchdule.PrescriptionNo = retCfName;
            cases.MedicalSchdule.CaseId = cases.CaseId;
            cases.MedicalSchdule.Schedule = XmlStringListSerializer.ToXmlString<IList<Schedule>>(cases.ScheduleInfo);
            string MedicalScId = CreateMedicalSchedule.InsertMeidcalScheduleInfo(cases.MedicalSchdule);
            if (MedicalScId == null)
            {
                return new JsonResult { Data = 0, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            string success = InsertFiles("medsc", MedicalScId, cases.CaseId, cases.msfiles);

            cases.lstMedicalSchedule = GetMedicalSchedules.GetAllMeicalschdulesbyCaseId(cases.CaseId);

            return PartialView("EditMedicalSchedule", cases);
        }

        public ActionResult AddVisits(string caseId)
        {
            return PartialView("CreateVisits", new Cases { CaseId = caseId });
        }

        [HttpPost]
        public ActionResult SaveEditAddVisit(Cases cases)
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
            cases.Visits.CaseId = cases.CaseId;
            cases.Visits.NurseReadings = XmlStringListSerializer.ToXmlString<NurseReadings>(cases.Visits.NurseReadingsInfo);
            Services3 s3 = new Services3();
            MedicalInformation MedicInfo = new MedicalInformation();
            MedicInfo = s3.GetMedicalInfo(LoginUser.UserId);
            if (cases.Visits.NurseReadingsInfo != null)
            {

                if (cases.Visits.NurseReadingsInfo.Height >= 0)
                {
                    MedicInfo.BMIHeight = cases.Visits.NurseReadingsInfo.Height;
                }
                if (cases.Visits.NurseReadingsInfo.Weight >= 0)
                {
                    MedicInfo.BMIWeight = cases.Visits.NurseReadingsInfo.Weight;
                }
                if (cases.Visits.NurseReadingsInfo.Sistole != null || cases.Visits.NurseReadingsInfo.Diastole != null)
                {
                    string bp = cases.Visits.NurseReadingsInfo.Sistole + "/" + cases.Visits.NurseReadingsInfo.Diastole;
                    MedicInfo.BloodPressure = bp;
                }
                MedicInfo.UpdatedBy = LoginUser.UserId;
                MedicInfo.UpdatedOn = DateTime.Now;

                int result;
                if (cases.Visits.NurseReadingsInfo.Height >= 0 || cases.Visits.NurseReadingsInfo.Weight >= 0)
                {
                    float BMIValue = (Convert.ToSingle(MedicInfo.BMIWeight)) / ((Convert.ToSingle(MedicInfo.BMIHeight) / 100) * (Convert.ToSingle(MedicInfo.BMIHeight) / 100));
                    MedicInfo.BMIValue = Math.Round(BMIValue, 2);
                    result = s3.UpdateMedicalInfo(MedicInfo, LoginUser.UserId.ToString(), LoginUser.DomainId.ToString(), "PInfo3", LoginUser.GroupType);

                }
                cases.Visits.NurseReadings = XmlStringListSerializer.ToXmlString<NurseReadings>(cases.Visits.NurseReadingsInfo);

            }
            string VisitId = CreateVisit.CreateVisitInfo(cases.Visits);

            cases.lstVisits = GetVisits.GetAllVisitsByCaseId(cases.CaseId);

            return PartialView("EditVisits", cases);
        }

        public ActionResult ViewDocument(string DocumentId)
        {
            return PartialView("ViewDocument", DocumentId);
        }

        public ActionResult GetPdf(string Id)
        {
            Getdocuments _objdocuments = new Getdocuments();
            Documents doc = _objdocuments.GetDocument(Id);
            return File(doc.Path, "application/pdf");
        }

        public FileResult DownloadDoc(string Id)
        {
            UserInformation LoginUser = new UserInformation();

            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }


            Getdocuments _objdocuments = new Getdocuments();
            Documents newdoc = null;
            newdoc = new Documents();
            Documents doc = _objdocuments.GetDocumentDownload(Id);


            newdoc.yData = doc.yData;


            string s = Convert.ToDateTime(doc.CreatedOn).ToShortDateString();
            string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
            string retFileName = doc.OriginalName + LoginUser.UserId + strDate + ".pdf";


            string tempDir = "C:\\InndocsiHealth\\PDFFiles\\" + LoginUser.UserId;
            string DestPdfFilePath = tempDir + "\\" + retFileName;

            Directory.CreateDirectory(tempDir);



            return File(newdoc.yData, "application/pdf", retFileName);


        }

        public ActionResult DownloadFile(string CaseId, string strRelatedCases)
        {
            UserInformation LoginUser = new UserInformation();
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            GetUserCases _getCaseById = new GetUserCases();
            Cases objCase = _getCaseById.GetCaseByID(CaseId);
            Services3 s3 = new Services3();
            MedicalInformation medinfo = new MedicalInformation();

            medinfo = s3.GetMedicalInfo(LoginUser.UserId);
            objCase.lstcasetocase = GetCaseTocases.GetAllCaseToCasesByCaseId(CaseId);
            objCase.lstcasetolog = GetCaseToLogs.GetAllCaseTologsByCaseId(CaseId);
            objCase.lstCases = GetUserCases.GetAllCases(LoginUser.UserId,LoginUser.GroupType);
            objCase.lstHealthlog = GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType);
            objCase.lstresultCaselist = ResultList.GetResulCaseList(objCase.lstCases, objCase.lstcasetocase);
            objCase.lstCases.Where(a => a.CaseId == CaseId).ToList().ForEach(a => objCase.lstCases.Remove(a));
            objCase.lstresultLoglist = ResultList.GetResultLogList(objCase.lstHealthlog, objCase.lstcasetolog);
            //_cases.lstCasetext = ResultList.TextBoxText(_cases.lstresultCaselist);
            //_cases.lstLogtext = ResultList.TextBoxText(_cases.lstresultLoglist);
            objCase.BMIHeight = medinfo.BMIHeight;
            objCase.BMIWeight = medinfo.BMIWeight;


            string s = Convert.ToDateTime(objCase.CreatedOn).ToShortDateString();
            string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
            string retFileName = objCase.CaseName + LoginUser.UserId + strDate + ".pdf";
            string zipFileName = objCase.CaseName + LoginUser.UserId + strDate + ".zip";

            string tempDir = "C:\\InndocsiHealth\\PDFFiles\\temp\\" + LoginUser.EmailId.Substring(0, LoginUser.EmailId.IndexOf('@')) + LoginUser.UserId;
            string SendToDocInfo = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
            string DestPdfFilePath = tempDir + "\\" + objCase.CaseName + "\\" + retFileName;
            string SourceDirectoryPath = tempDir;//@"C:\InndocsiHealth\PDFFiles\74";
            string TargetZipFilePath = tempDir + objCase.CaseName + LoginUser.EmailId.Substring(0, LoginUser.EmailId.IndexOf('@')) + strDate + ".zip";//@"C:\InndocsiHealth\PDFFiles\" + objCase.CaseName + LoginUser.LoginName + strDate + ".zip";

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            if (!Directory.Exists(DestPdfFilePath))
            {
                Directory.CreateDirectory(SendToDocInfo);
            }


            // AD Code for Zip files and folders

            //End here
            License lic = new License();
            lic.SetLicense("C:\\InndocsiHealth\\Aspose.Total.lic");

            Pdf pdf1 = new Pdf();
            Section sec1 = pdf1.Sections.Add();


            sec1.PageInfo.PageWidth = Aspose.Pdf.Generator.PageSize.A3Width;
            sec1.PageInfo.PageHeight = Aspose.Pdf.Generator.PageSize.A3Height;

            MarginInfo marginInfo = new MarginInfo();
            marginInfo.Top = 72;
            marginInfo.Bottom = 72;
            marginInfo.Left = 90;
            marginInfo.Right = 90;

            sec1.PageInfo.Margin = marginInfo;


            Text pdfHeadText = new Text("Health Record Information");

            pdfHeadText.TextInfo.FontName = "Arial";
            pdfHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfHeadText.TextInfo.FontSize = 16;
            pdfHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText);



            Table tab1 = new Table();
            sec1.Paragraphs.Add(tab1);
            tab1.ColumnWidths = "122 400";

            tab1.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            tab1.Border = new BorderInfo((int)BorderSide.All, 1F);

            MarginInfo margin = new MarginInfo();
            margin.Top = 10;
            margin.Left = 10;
            margin.Right = 10;
            margin.Bottom = 10;


            tab1.DefaultCellPadding = margin;


            Aspose.Pdf.Generator.Row row1 = tab1.Rows.Add();
            row1.Cells.Add("Case Name:");
            row1.Cells.Add(objCase.CaseName);

            Aspose.Pdf.Generator.Row row2 = tab1.Rows.Add();
            row2.Cells.Add("Case Description:");
            row2.Cells.Add(objCase.CaseDescription);

            Aspose.Pdf.Generator.Row row4 = tab1.Rows.Add();
            row4.Cells.Add("Type Of Problem:");
            row4.Cells.Add(objCase.TypeOfProblem);

            Aspose.Pdf.Generator.Row row5 = tab1.Rows.Add();
            row5.Cells.Add("Hospital Info:");
            string HInfo = string.Empty;

            if (objCase.HospitalInfos != null)
            {
                foreach (var lstHos in objCase.HospitalInfos)
                {
                    HInfo = HInfo + "Hospital Name: " + lstHos.HospitalName + "," + "Address: " + lstHos.HospAddress + "," + "Contact No.: " + lstHos.HospPhno + "," + "Email: " + lstHos.HospEmail + Environment.NewLine;
                }
            }

            row5.Cells.Add(HInfo);

            Aspose.Pdf.Generator.Row row6 = tab1.Rows.Add();
            row6.Cells.Add("Patient Type:");
            string PatientType = objCase.PatientType ? "In Patient" : "Out Patient";
            row6.Cells.Add(PatientType);

            //Aspose.Pdf.Generator.Row row7 = tab1.Rows.Add();
            //row7.Cells.Add("Physical Readings:");
            //string PhysicalReadings = "Height: " + objCase.BMIHeight + "," + "Weight: " + objCase.BMIWeight;
            //row7.Cells.Add(PhysicalReadings);

            Aspose.Pdf.Generator.Row row18 = tab1.Rows.Add();
            row18.Cells.Add("Related Cases:");
            string CaseToCases = string.Empty;
            if (objCase.lstresultCaselist != null)
            {
                foreach (var lstRelatedCases in objCase.lstresultCaselist)
                {
                    if (lstRelatedCases.IsChecked)
                    {
                        CaseToCases = CaseToCases + lstRelatedCases.name + ", ";
                        if (strRelatedCases == "YesRelatedCases")
                        {
                            RelatedCases(lstRelatedCases.Id);
                        }

                    }

                    // row8.Cells.Add(objCase.);
                }
            }
            row18.Cells.Add(CaseToCases);

            Aspose.Pdf.Generator.Row row8 = tab1.Rows.Add();

            row8.Cells.Add("Related Logs:");
            string CaseToLogs = string.Empty;
            if (objCase.lstresultLoglist.Count != null)
            {
                foreach (var lstRelatedLogs in objCase.lstresultLoglist)
                {
                    if (lstRelatedLogs.IsChecked)
                    {
                        CaseToLogs = CaseToLogs + lstRelatedLogs.name + ", ";

                    }

                    // row8.Cells.Add(objCase.);
                }
            }
            row8.Cells.Add(CaseToLogs);
            Aspose.Pdf.Generator.Row row9 = tab1.Rows.Add();
            row9.Cells.Add("Custom Description:");
            row9.Cells.Add(objCase.CustHealthlogDesc);

            //Aspose.Pdf.Generator.Row row10 = tab1.Rows.Add();
            //row10.Cells.Add("Doctors Log:");
            //row10.Cells.Add(objCase.DoctorsLog);

            //Aspose.Pdf.Generator.Row row11 = tab1.Rows.Add();
            //row11.Cells.Add("Precautions:");
            //row11.Cells.Add(objCase.Precautions);



            //Procedures
            #region

            Text pdfProcHeadText = new Text("Procedures");

            pdfProcHeadText.TextInfo.FontName = "Arial";
            pdfProcHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfProcHeadText.TextInfo.FontSize = 16;
            pdfProcHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfProcHeadText);


            Table tab39 = new Table();
            sec1.Paragraphs.Add(tab39);
            tab39.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo margin2 = new MarginInfo();
            margin2.Top = 5;
            margin2.Left = 5;
            margin2.Right = 5;
            margin2.Bottom = 5;


            tab39.DefaultCellPadding = margin2;

            int i = 1;
            Getdocuments _objdocuments = new Getdocuments();

            objCase.lstProcedure = GetProcedures.GetAllProceduresByCaseId(CaseId);
            if (objCase.lstProcedure.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;


                Row srow5 = tab39.Rows.Add();
                srow5.Cells.Add("No Procedures ", t);
            }
            else
            {
                foreach (Procedure procedure in objCase.lstProcedure)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;


                    Row srow5 = tab39.Rows.Add();
                    srow5.Cells.Add("Procedure" + i, t);



                    Row srow1 = tab39.Rows.Add();
                    srow1.Cells.Add("Procedure Name:");
                    srow1.Cells.Add(procedure.ProcedureName);


                    Row srow2 = tab39.Rows.Add();
                    srow2.Cells.Add("Date of Service:");
                    string main = procedure.DateOfService.ToString();
                    string[] tmp;
                    string date;//, time;

                    tmp = main.Split(' ');

                    date = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow2.Cells.Add(date);


                    Row srow3 = tab39.Rows.Add();
                    srow3.Cells.Add("Provider or Facility:");
                    srow3.Cells.Add(procedure.ProviderOrFacility);

                    Row srow4 = tab39.Rows.Add();
                    srow4.Cells.Add("Surgeon:");
                    srow4.Cells.Add(procedure.Surgeon);

                    procedure.lstDocuments = _objdocuments.GetDocumentsbyProcId(procedure.ProcedureId);
                    int j = 0;
                    foreach (var docInfo in procedure.lstDocuments)
                    {
                        j++;
                        Row srow6 = tab39.Rows.Add();
                        srow6.Cells.Add("Document" + j + ":");
                        srow6.Cells.Add(docInfo.OriginalName);
                        // docInfo.Name = SendToDocInfo;//Case Name for folder Creation Name
                        docInfo.Name = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
                        //doc = new Documents();
                        string FilePath = docInfo.Path;//"C:\\InndocsiHealth\\PDFFiles\\74\\5be431c3-7664-45a0-ae4a-a2764eec11b8.pdf";//Path.GetFileName(docInfo.Path);
                        docInfo.CreatedBy = LoginUser.UserId;
                        CreateDoc(docInfo);
                        //if (result == "1020")
                        //{
                        //    result = string.Empty;
                        //}              


                    }
                    i++;
                }
            }
            #endregion
            //Medical Tests
            #region

            Text pdfMTHeadText = new Text("Medical Tests");

            pdfMTHeadText.TextInfo.FontName = "Arial";
            pdfMTHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfMTHeadText.TextInfo.FontSize = 16;
            pdfMTHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfMTHeadText);


            Table MTtab = new Table();
            sec1.Paragraphs.Add(MTtab);
            MTtab.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo MTmargin = new MarginInfo();
            MTmargin.Top = 5;
            MTmargin.Left = 5;
            MTmargin.Right = 5;
            MTmargin.Bottom = 5;


            MTtab.DefaultCellPadding = margin2;

            int k = 1;
            Getdocuments _objMTdocuments = new Getdocuments();

            objCase.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(CaseId);
            if (objCase.lstMedicalTest.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;


                Row srow5 = MTtab.Rows.Add();
                srow5.Cells.Add("No Medical Tests", t);

            }
            else
                foreach (MedicalTests objMTests in objCase.lstMedicalTest)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;


                    Row srow5 = MTtab.Rows.Add();
                    srow5.Cells.Add("Medical Test" + k, t);



                    Row srow1 = MTtab.Rows.Add();
                    srow1.Cells.Add("Test Name:");
                    srow1.Cells.Add(objMTests.TestName);


                    Row srow2 = MTtab.Rows.Add();
                    srow2.Cells.Add("Date of Service:");
                    string main = objMTests.DateOfTest.ToString();
                    string[] tmp;
                    string date;//, time;

                    tmp = main.Split(' ');

                    date = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow2.Cells.Add(date);


                    objMTests.lstDocuments = _objMTdocuments.GetDocumentsbyMedicalTestId(objMTests.MedicalTestId);
                    int l = 1;
                    foreach (var docInfo in objMTests.lstDocuments)
                    {

                        Row srow6 = MTtab.Rows.Add();
                        srow6.Cells.Add("Document" + l + ":");
                        srow6.Cells.Add(docInfo.OriginalName);

                        //doc = new Documents();
                        string FilePath = docInfo.Path;//"C:\\InndocsiHealth\\PDFFiles\\74\\5be431c3-7664-45a0-ae4a-a2764eec11b8.pdf";//Path.GetFileName(docInfo.Path);
                        docInfo.Name = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
                        docInfo.CreatedBy = LoginUser.UserId;
                        CreateDoc(docInfo);
                        l++;

                    }
                    k++;
                }
            #endregion
            //Medications
            #region

            Text pdfMHeadText = new Text("Medications");

            pdfMHeadText.TextInfo.FontName = "Arial";
            pdfMHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfMHeadText.TextInfo.FontSize = 16;
            pdfMHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfMHeadText);


            Table Mtab = new Table();
            sec1.Paragraphs.Add(Mtab);
            Mtab.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo Mmargin = new MarginInfo();
            Mmargin.Top = 5;
            Mmargin.Left = 5;
            Mmargin.Right = 5;
            Mmargin.Bottom = 5;


            Mtab.DefaultCellPadding = Mmargin;

            int x = 1;
            Getdocuments _objMdocuments = new Getdocuments();

            objCase.lstMedicalSchedule = GetMedicalSchedules.GetAllMeicalschdulesbyCaseId(CaseId);
            if (objCase.lstMedicalSchedule.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;


                Row srow1 = Mtab.Rows.Add();
                srow1.Cells.Add("No Medications", t);

            }
            else
                foreach (MedicalSchedule objMSinfo in objCase.lstMedicalSchedule)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;


                    Row srow1 = Mtab.Rows.Add();
                    srow1.Cells.Add("Medication" + x, t);



                    //Row srow1 = tab3.Rows.Add();
                    //srow1.Cells.Add("Date Prescribed:");
                    //srow1.Cells.Add(objMSinfo.DatePrescribed.);


                    Row srow2 = Mtab.Rows.Add();
                    srow2.Cells.Add("Date Prescribed:");
                    string main = objMSinfo.StartDate.ToString();
                    string[] tmp;
                    string date;//, time;

                    tmp = main.Split(' ');

                    date = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow2.Cells.Add(date);

                    Row srow3 = Mtab.Rows.Add();
                    srow3.Cells.Add("Date Dispensed:");
                    string main1 = objMSinfo.EndDate.ToString();
                    string[] tmp1;
                    string date1;//, time;

                    tmp1 = main.Split(' ');

                    date1 = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow3.Cells.Add(date1);


                    //Row srow4 = Mtab.Rows.Add();
                    //srow4.Cells.Add("Doctor Name:");
                    //srow4.Cells.Add(objMSinfo.DoctorName);
                    //Row srow5 = Mtab.Rows.Add();
                    //srow5.Cells.Add("Reason for Taken:");
                    //srow5.Cells.Add(objMSinfo.Reason);
                    Row srow6 = Mtab.Rows.Add();
                    srow6.Cells.Add("Pharmacy Name:");
                    srow6.Cells.Add(objMSinfo.PharmacyName);
                    Row srow7 = Mtab.Rows.Add();
                    srow7.Cells.Add("Send Notifications:");
                    string SendNotification = objMSinfo.Notify ? "Yes" : "No";
                    srow7.Cells.Add(SendNotification);

                    // objMSinfo.lstSchedule = 
                    // string g = objMSinfo.lstSchedule.Count().ToString();

                    // AD

                    Aspose.Pdf.Generator.TextInfo t2 = new Aspose.Pdf.Generator.TextInfo();
                    t2.FontSize = 14;
                    t2.IsTrueTypeFontBold = true;


                    Row srow12 = Mtab.Rows.Add();
                    srow12.Cells.Add("Schedules", t2);

                    //End
                    if (objMSinfo.lstSchedule == null)
                    {
                        //Row srow88 = Mtab.Rows.Add();
                        //srow88.Cells.Add("No Schedules");
                        Aspose.Pdf.Generator.TextInfo t1 = new Aspose.Pdf.Generator.TextInfo();
                        t1.FontSize = 12;
                        t1.IsTrueTypeFontBold = false;


                        Row srow51 = Mtab.Rows.Add();
                        srow51.Cells.Add("No Schedules ", t1);
                    }
                    else
                    {
                        int m = 1;
                        foreach (var lstSchedules in objMSinfo.lstSchedule)
                        {
                            Aspose.Pdf.Generator.TextInfo t1 = new Aspose.Pdf.Generator.TextInfo();
                            t1.FontSize = 12;
                            t1.IsTrueTypeFontBold = true;


                            Row srow51 = Mtab.Rows.Add();
                            srow51.Cells.Add("Schedule " + m + ":", t1);
                            //Row srow88 = Mtab.Rows.Add();
                            //srow88.Cells.Add("Schedule" + m + ":",t2);
                            Row srow87 = Mtab.Rows.Add();
                            srow87.Cells.Add("Medicine Name:");
                            srow87.Cells.Add(objMSinfo.lstSchedule[m - 1].MedicineName);
                            Row srow86 = Mtab.Rows.Add();
                            srow86.Cells.Add("Medicine Type:");
                            srow86.Cells.Add(objMSinfo.lstSchedule[m - 1].TypeOfMedicine);
                            Row srow85 = Mtab.Rows.Add();
                            srow85.Cells.Add("Dosage Taken:");
                            srow85.Cells.Add(objMSinfo.lstSchedule[m - 1].DosageTaken);
                            //Row srow84 = Mtab.Rows.Add();
                            //srow84.Cells.Add("Frequency Taken:");
                            //srow84.Cells.Add(objMSinfo.lstSchedule[m - 1].FrequencyTaken);
                            Row srow83 = Mtab.Rows.Add();
                            srow83.Cells.Add("Quantity:");
                            srow83.Cells.Add(objMSinfo.lstSchedule[m - 1].TotalQuantity);
                            //Row srow82 = Mtab.Rows.Add();
                            //srow82.Cells.Add("Days Supplied:");
                            //srow82.Cells.Add(objMSinfo.lstSchedule[m - 1]..DaysSupply.ToString());
                            //Row srow81 = Mtab.Rows.Add();
                            //srow81.Cells.Add("Notes:");
                            //srow81.Cells.Add(objMSinfo.lstSchedule[m - 1].Notes);
                            m++;

                        }
                    }
                    objMSinfo.lstDocuments = _objMdocuments.GetDocumentsbyMedicalScheduleId(objMSinfo.MedicalScheduleId);
                    int y = 0;
                    foreach (var docInfo in objMSinfo.lstDocuments)
                    {
                        y++;
                        Row srow8 = Mtab.Rows.Add();
                        srow8.Cells.Add("Document" + y + ":");
                        srow8.Cells.Add(docInfo.OriginalName);
                        string FilePath = docInfo.Path;
                        docInfo.Name = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
                        docInfo.CreatedBy = LoginUser.UserId;
                        CreateDoc(docInfo);
                    }
                    x++;
                }
            #endregion
            //Visits
            #region

            Text pdfVHeadText = new Text("Visits");

            pdfVHeadText.TextInfo.FontName = "Arial";
            pdfVHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfVHeadText.TextInfo.FontSize = 16;
            pdfVHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfVHeadText);


            Table Vtab = new Table();
            sec1.Paragraphs.Add(Vtab);
            Vtab.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo Vmargin = new MarginInfo();
            Vmargin.Top = 5;
            Vmargin.Left = 5;
            Vmargin.Right = 5;
            Vmargin.Bottom = 5;


            Vtab.DefaultCellPadding = Vmargin;

            int p = 1;
            Getdocuments _objVdocuments = new Getdocuments();

            objCase.lstVisits = GetVisits.GetAllVisitsByCaseId(CaseId);
            if (objCase.lstVisits.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;


                Row srow51 = Vtab.Rows.Add();
                srow51.Cells.Add("No Visits", t);

            }
            else
                foreach (Visits visitsInfo in objCase.lstVisits)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;


                    Row srow51 = Vtab.Rows.Add();
                    srow51.Cells.Add("Visit" + p, t);



                    Row srow11 = Vtab.Rows.Add();
                    srow11.Cells.Add(" Doctor's Log:");
                    srow11.Cells.Add(visitsInfo.DoctorsLog);

                    Row srow10 = Vtab.Rows.Add();
                    srow10.Cells.Add(" Precautions:");
                    srow10.Cells.Add(visitsInfo.Precautions);
                    Row srow12 = Vtab.Rows.Add();
                    srow12.Cells.Add(" Diet Prescription:");
                    srow12.Cells.Add(visitsInfo.DietPresc);

                    Row srow13 = Vtab.Rows.Add();
                    srow13.Cells.Add(" Nurse Readings:");
                    string NurseReadings = string.Empty;

                    if (visitsInfo.NurseReadingsInfo != null)
                    {
                        //foreach (var NReadings in visitsInfo.NurseReadingsInfo)
                        //{
                        NurseReadings = NurseReadings + "Height: " + visitsInfo.NurseReadingsInfo.Height + "," + "Weight: " + visitsInfo.NurseReadingsInfo.Weight + "," + "Temperature: " + visitsInfo.NurseReadingsInfo.Temparature + "," + "Sistole(High B.P): " + visitsInfo.NurseReadingsInfo.Sistole + "," + "Diastole(Low B.P): " + visitsInfo.NurseReadingsInfo.Diastole + "," + "Respiratoryrate: " + visitsInfo.NurseReadingsInfo.Respiratoryrate + "," + "PulseRate: " + visitsInfo.NurseReadingsInfo.PulseRate + Environment.NewLine;
                        //}
                    }

                    row5.Cells.Add(NurseReadings);
                    //string NurseReadings = "Height: " + visitsInfo.NurseReadingsInfo..BMIHeight + "," + "Weight: " + visitsInfo.BMIWeight;
                    //row7.Cells.Add(NurseReadings);
                    //srow13.Cells.Add(visitsInfo.Precautions);
                    //Row srow24 = Vtab.Rows.Add();
                    //srow24.Cells.Add("Visit Date:");
                    //string main = visitsInfo.VisitDate.ToString();
                    //string[] tmp2;
                    //string date2;//, time;

                    //tmp2 = main.Split(' ');

                    //date2 = tmp2[0].ToString();
                    //// time = tmp[1].ToString() + " " + tmp[2].ToString();
                    //srow24.Cells.Add(date2);


                    Aspose.Pdf.Generator.Row row41 = tab1.Rows.Add();
                    row41.Cells.Add("Doctor Info:");
                    string DInfo = string.Empty;

                    if (visitsInfo.DoctorInfos != null)
                    {
                        //foreach (var DoctorInfos in visitsInfo.DoctorInfos)
                        //{
                        DInfo = DInfo + "Doctor Name: " + visitsInfo.DoctorInfos.DoctorName + "," + "Contact No.: " + visitsInfo.DoctorInfos.DoctorPhno + "," + "Email: " + visitsInfo.DoctorInfos.DoctorEmail + Environment.NewLine;
                        //}
                    }

                    row5.Cells.Add(DInfo);

                    //Row srow32 = Vtab.Rows.Add();
                    //srow32.Cells.Add("Provider or Facility:");
                    //srow32.Cells.Add(visitsInfo.ProviderFacility);
                    Row srow24 = Vtab.Rows.Add();
                    srow24.Cells.Add("Next Visit Date:");
                    string main3 = visitsInfo.NextVisitDate.ToString();
                    string[] tmp3;
                    string date3;//, time;

                    tmp3 = main3.Split(' ');

                    date3 = tmp3[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow24.Cells.Add(date3);

                    Row srow30 = Vtab.Rows.Add();
                    srow30.Cells.Add("Send Notification:");
                    srow30.Cells.Add(visitsInfo.IsNotify.ToString());

                    //visitsInfo.lstDocuments = _objdocuments.GetDocumentsbyProcId(procedure.ProcedureId);
                    //int j = 0;
                    //foreach (var docInfo in procedure.lstDocuments)
                    //{
                    //    j++;
                    //    Row srow6 = tab3.Rows.Add();
                    //    srow6.Cells.Add("Document" + j + ":");
                    //    srow6.Cells.Add(docInfo.OriginalName);

                    //    //doc = new Documents();
                    //    string FilePath = docInfo.Path;//"C:\\InndocsiHealth\\PDFFiles\\74\\5be431c3-7664-45a0-ae4a-a2764eec11b8.pdf";//Path.GetFileName(docInfo.Path);

                    //    CreateDoc(docInfo);
                    //    //if (result == "1020")
                    //    //{
                    //    //    result = string.Empty;
                    //    //}              


                    //}
                    p++;
                }
            #endregion
            pdf1.Save(DestPdfFilePath);
            try
            {

                ZipFile zip = null;
                using (zip = new ZipFile())
                {
                    zip.AddDirectory(SourceDirectoryPath, System.IO.Path.GetFileName(SourceDirectoryPath));
                    zip.Save(TargetZipFilePath);
                    zip.Dispose();
                }
            }
            catch (System.Exception ex1)
            {
                throw ex1;
            }
            var bytes = System.IO.File.ReadAllBytes(TargetZipFilePath);
            System.IO.File.Delete(TargetZipFilePath);
            Directory.Delete(tempDir, true);

            return File(bytes, "application/x-zip-compressed", zipFileName);// DestPdfFilePath

        }

        private string RelatedCases(string CaseId)
        {

            UserInformation LoginUser = new UserInformation();
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }

            GetUserCases _getCaseById = new GetUserCases();
            Cases objCase = _getCaseById.GetCaseByID(CaseId);
            Services3 s3 = new Services3();
            MedicalInformation medinfo = new MedicalInformation();

            medinfo = s3.GetMedicalInfo(LoginUser.UserId);
            objCase.lstcasetocase = GetCaseTocases.GetAllCaseToCasesByCaseId(CaseId);
            objCase.lstcasetolog = GetCaseToLogs.GetAllCaseTologsByCaseId(CaseId);
            objCase.lstCases = GetUserCases.GetAllCases(LoginUser.UserId,LoginUser.GroupType);
            objCase.lstHealthlog = GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType);
            objCase.lstresultCaselist = ResultList.GetResulCaseList(objCase.lstCases, objCase.lstcasetocase);
            objCase.lstCases.Where(a => a.CaseId == CaseId).ToList().ForEach(a => objCase.lstCases.Remove(a));
            objCase.lstresultLoglist = ResultList.GetResultLogList(objCase.lstHealthlog, objCase.lstcasetolog);
            //_cases.lstCasetext = ResultList.TextBoxText(_cases.lstresultCaselist);
            //_cases.lstLogtext = ResultList.TextBoxText(_cases.lstresultLoglist);
            objCase.BMIHeight = medinfo.BMIHeight;
            objCase.BMIWeight = medinfo.BMIWeight;


            string s = Convert.ToDateTime(objCase.CreatedOn).ToShortDateString();
            string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
            string retFileName = objCase.CaseName + LoginUser.UserId + strDate + ".pdf";
            string zipFileName = objCase.CaseName + LoginUser.UserId + strDate + ".zip";

            string tempDir = "C:\\InndocsiHealth\\PDFFiles\\temp\\" + LoginUser.EmailId.Substring(0, LoginUser.EmailId.IndexOf('@')) + LoginUser.UserId;
            string SendToDocInfo = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
            string DestPdfFilePath = tempDir + "\\" + objCase.CaseName + "\\" + retFileName;
            string SourceDirectoryPath = tempDir;//@"C:\InndocsiHealth\PDFFiles\74";
            string TargetZipFilePath = tempDir + objCase.CaseName + LoginUser.EmailId.Substring(0, LoginUser.EmailId.IndexOf('@')) + strDate + ".zip";//@"C:\InndocsiHealth\PDFFiles\" + objCase.CaseName + LoginUser.LoginName + strDate + ".zip";

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            if (!Directory.Exists(DestPdfFilePath))
            {
                Directory.CreateDirectory(SendToDocInfo);
            }


            // AD Code for Zip files and folders

            //End here
            License lic = new License();
            lic.SetLicense("C:\\InndocsiHealth\\Aspose.Total.lic");

            Pdf pdf1 = new Pdf();
            Section sec1 = pdf1.Sections.Add();


            sec1.PageInfo.PageWidth = Aspose.Pdf.Generator.PageSize.A3Width;
            sec1.PageInfo.PageHeight = Aspose.Pdf.Generator.PageSize.A3Height;

            MarginInfo marginInfo = new MarginInfo();
            marginInfo.Top = 72;
            marginInfo.Bottom = 72;
            marginInfo.Left = 90;
            marginInfo.Right = 90;

            sec1.PageInfo.Margin = marginInfo;


            Text pdfHeadText = new Text("Health Record Information");

            pdfHeadText.TextInfo.FontName = "Arial";
            pdfHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfHeadText.TextInfo.FontSize = 16;
            pdfHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText);



            Table tab1 = new Table();
            sec1.Paragraphs.Add(tab1);
            tab1.ColumnWidths = "122 400";

            tab1.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            tab1.Border = new BorderInfo((int)BorderSide.All, 1F);

            MarginInfo margin = new MarginInfo();
            margin.Top = 10;
            margin.Left = 10;
            margin.Right = 10;
            margin.Bottom = 10;


            tab1.DefaultCellPadding = margin;


            Aspose.Pdf.Generator.Row row1 = tab1.Rows.Add();
            row1.Cells.Add("Case Name:");
            row1.Cells.Add(objCase.CaseName);

            Aspose.Pdf.Generator.Row row2 = tab1.Rows.Add();
            row2.Cells.Add("Case Description:");
            row2.Cells.Add(objCase.CaseDescription);

            Aspose.Pdf.Generator.Row row4 = tab1.Rows.Add();
            row4.Cells.Add("Type Of Problem:");
            row4.Cells.Add(objCase.TypeOfProblem);

            Aspose.Pdf.Generator.Row row5 = tab1.Rows.Add();
            row5.Cells.Add("Hospital Info:");
            string HInfo = string.Empty;

            if (objCase.HospitalInfos != null)
            {
                foreach (var lstHos in objCase.HospitalInfos)
                {
                    HInfo = HInfo + "Hospital Name: " + lstHos.HospitalName + "," + "Address: " + lstHos.HospAddress + "," + "Contact No.: " + lstHos.HospPhno + "," + "Email: " + lstHos.HospEmail + Environment.NewLine;
                }
            }

            row5.Cells.Add(HInfo);

            Aspose.Pdf.Generator.Row row6 = tab1.Rows.Add();
            row6.Cells.Add("Patient Type:");
            string PatientType = objCase.PatientType ? "In Patient" : "Out Patient";
            row6.Cells.Add(PatientType);

            //Aspose.Pdf.Generator.Row row7 = tab1.Rows.Add();
            //row7.Cells.Add("Physical Readings:");
            //string PhysicalReadings = "Height: " + objCase.BMIHeight + "," + "Weight: " + objCase.BMIWeight;
            //row7.Cells.Add(PhysicalReadings);

            Aspose.Pdf.Generator.Row row18 = tab1.Rows.Add();
            row18.Cells.Add("Related Cases:");
            string CaseToCases = string.Empty;
            if (objCase.lstresultCaselist != null)
            {
                foreach (var lstRelatedCases in objCase.lstresultCaselist)
                {
                    if (lstRelatedCases.IsChecked)
                    {
                        CaseToCases = CaseToCases + lstRelatedCases.name + ", ";
                        //if (strRelatedCases == "YesRelatedCases")
                        //{
                        //    RelatedCases(lstRelatedCases.Id);
                        //}

                    }

                    // row8.Cells.Add(objCase.);
                }
            }
            row18.Cells.Add(CaseToCases);

            Aspose.Pdf.Generator.Row row8 = tab1.Rows.Add();

            row8.Cells.Add("Related Logs:");
            string CaseToLogs = string.Empty;
            if (objCase.lstresultLoglist.Count != null)
            {
                foreach (var lstRelatedLogs in objCase.lstresultLoglist)
                {
                    if (lstRelatedLogs.IsChecked)
                    {
                        CaseToLogs = CaseToLogs + lstRelatedLogs.name + ", ";

                    }

                    // row8.Cells.Add(objCase.);
                }
            }
            row8.Cells.Add(CaseToLogs);
            Aspose.Pdf.Generator.Row row9 = tab1.Rows.Add();
            row9.Cells.Add("Custom Description:");
            row9.Cells.Add(objCase.CustHealthlogDesc);

            //Aspose.Pdf.Generator.Row row10 = tab1.Rows.Add();
            //row10.Cells.Add("Doctors Log:");
            //row10.Cells.Add(objCase.DoctorsLog);

            //Aspose.Pdf.Generator.Row row11 = tab1.Rows.Add();
            //row11.Cells.Add("Precautions:");
            //row11.Cells.Add(objCase.Precautions);



            //Procedures
            #region

            Text pdfProcHeadText = new Text("Procedures");

            pdfProcHeadText.TextInfo.FontName = "Arial";
            pdfProcHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfProcHeadText.TextInfo.FontSize = 16;
            pdfProcHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfProcHeadText);


            Table tab39 = new Table();
            sec1.Paragraphs.Add(tab39);
            tab39.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo margin2 = new MarginInfo();
            margin2.Top = 5;
            margin2.Left = 5;
            margin2.Right = 5;
            margin2.Bottom = 5;


            tab39.DefaultCellPadding = margin2;

            int i = 1;
            Getdocuments _objdocuments = new Getdocuments();

            objCase.lstProcedure = GetProcedures.GetAllProceduresByCaseId(CaseId);
            if (objCase.lstProcedure.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;


                Row srow5 = tab39.Rows.Add();
                srow5.Cells.Add("No Procedures ", t);
            }
            else
            {
                foreach (Procedure procedure in objCase.lstProcedure)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;


                    Row srow5 = tab39.Rows.Add();
                    srow5.Cells.Add("Procedure" + i, t);



                    Row srow1 = tab39.Rows.Add();
                    srow1.Cells.Add("Procedure Name:");
                    srow1.Cells.Add(procedure.ProcedureName);


                    Row srow2 = tab39.Rows.Add();
                    srow2.Cells.Add("Date of Service:");
                    string main = procedure.DateOfService.ToString();
                    string[] tmp;
                    string date;//, time;

                    tmp = main.Split(' ');

                    date = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow2.Cells.Add(date);


                    Row srow3 = tab39.Rows.Add();
                    srow3.Cells.Add("Provider or Facility:");
                    srow3.Cells.Add(procedure.ProviderOrFacility);

                    Row srow4 = tab39.Rows.Add();
                    srow4.Cells.Add("Surgeon:");
                    srow4.Cells.Add(procedure.Surgeon);

                    procedure.lstDocuments = _objdocuments.GetDocumentsbyProcId(procedure.ProcedureId);
                    int j = 0;
                    foreach (var docInfo in procedure.lstDocuments)
                    {
                        j++;
                        Row srow6 = tab39.Rows.Add();
                        srow6.Cells.Add("Document" + j + ":");
                        srow6.Cells.Add(docInfo.OriginalName);
                        // docInfo.Name = SendToDocInfo;//Case Name for folder Creation Name
                        docInfo.Name = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
                        //doc = new Documents();
                        string FilePath = docInfo.Path;//"C:\\InndocsiHealth\\PDFFiles\\74\\5be431c3-7664-45a0-ae4a-a2764eec11b8.pdf";//Path.GetFileName(docInfo.Path);
                        docInfo.CreatedBy = LoginUser.UserId;
                        CreateDoc(docInfo);
                        //if (result == "1020")
                        //{
                        //    result = string.Empty;
                        //}              


                    }
                    i++;
                }
            }
            #endregion
            //Medical Tests
            #region

            Text pdfMTHeadText = new Text("Medical Tests");

            pdfMTHeadText.TextInfo.FontName = "Arial";
            pdfMTHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfMTHeadText.TextInfo.FontSize = 16;
            pdfMTHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfMTHeadText);


            Table MTtab = new Table();
            sec1.Paragraphs.Add(MTtab);
            MTtab.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo MTmargin = new MarginInfo();
            MTmargin.Top = 5;
            MTmargin.Left = 5;
            MTmargin.Right = 5;
            MTmargin.Bottom = 5;


            MTtab.DefaultCellPadding = margin2;

            int k = 1;
            Getdocuments _objMTdocuments = new Getdocuments();

            objCase.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(CaseId);
            if (objCase.lstMedicalTest.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;


                Row srow5 = MTtab.Rows.Add();
                srow5.Cells.Add("No Medical Tests", t);

            }
            else
                foreach (MedicalTests objMTests in objCase.lstMedicalTest)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;


                    Row srow5 = MTtab.Rows.Add();
                    srow5.Cells.Add("Medical Test" + k, t);



                    Row srow1 = MTtab.Rows.Add();
                    srow1.Cells.Add("Test Name:");
                    srow1.Cells.Add(objMTests.TestName);


                    Row srow2 = MTtab.Rows.Add();
                    srow2.Cells.Add("Date of Service:");
                    string main = objMTests.DateOfTest.ToString();
                    string[] tmp;
                    string date;//, time;

                    tmp = main.Split(' ');

                    date = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow2.Cells.Add(date);


                    objMTests.lstDocuments = _objMTdocuments.GetDocumentsbyMedicalTestId(objMTests.MedicalTestId);
                    int l = 1;
                    foreach (var docInfo in objMTests.lstDocuments)
                    {

                        Row srow6 = MTtab.Rows.Add();
                        srow6.Cells.Add("Document" + l + ":");
                        srow6.Cells.Add(docInfo.OriginalName);

                        //doc = new Documents();
                        string FilePath = docInfo.Path;//"C:\\InndocsiHealth\\PDFFiles\\74\\5be431c3-7664-45a0-ae4a-a2764eec11b8.pdf";//Path.GetFileName(docInfo.Path);
                        docInfo.Name = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
                        docInfo.CreatedBy = LoginUser.UserId;
                        CreateDoc(docInfo);
                        l++;

                    }
                    k++;
                }
            #endregion
            //Medications
            #region

            Text pdfMHeadText = new Text("Medications");

            pdfMHeadText.TextInfo.FontName = "Arial";
            pdfMHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfMHeadText.TextInfo.FontSize = 16;
            pdfMHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfMHeadText);


            Table Mtab = new Table();
            sec1.Paragraphs.Add(Mtab);
            Mtab.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo Mmargin = new MarginInfo();
            Mmargin.Top = 5;
            Mmargin.Left = 5;
            Mmargin.Right = 5;
            Mmargin.Bottom = 5;


            Mtab.DefaultCellPadding = Mmargin;

            int x = 1;
            Getdocuments _objMdocuments = new Getdocuments();

            objCase.lstMedicalSchedule = GetMedicalSchedules.GetAllMeicalschdulesbyCaseId(CaseId);
            if (objCase.lstMedicalSchedule.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;


                Row srow1 = Mtab.Rows.Add();
                srow1.Cells.Add("No Medications", t);

            }
            else
                foreach (MedicalSchedule objMSinfo in objCase.lstMedicalSchedule)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;


                    Row srow1 = Mtab.Rows.Add();
                    srow1.Cells.Add("Medication" + x, t);



                    //Row srow1 = tab3.Rows.Add();
                    //srow1.Cells.Add("Date Prescribed:");
                    //srow1.Cells.Add(objMSinfo.DatePrescribed.);


                    Row srow2 = Mtab.Rows.Add();
                    srow2.Cells.Add("Date Prescribed:");
                    string main = objMSinfo.StartDate.ToString(); // sandeep modified
                    string[] tmp;
                    string date;//, time;

                    tmp = main.Split(' ');

                    date = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow2.Cells.Add(date);

                    Row srow3 = Mtab.Rows.Add();
                    srow3.Cells.Add("Date Dispensed:"); // sandeep modified
                    string main1 = objMSinfo.EndDate.ToString();
                    string[] tmp1;
                    string date1;//, time;

                    tmp1 = main.Split(' ');

                    date1 = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow3.Cells.Add(date1);


                    //Row srow4 = Mtab.Rows.Add();
                    //srow4.Cells.Add("Doctor Name:");
                    //srow4.Cells.Add(objMSinfo.DoctorName);
                    //Row srow5 = Mtab.Rows.Add();
                    //srow5.Cells.Add("Reason for Taken:");
                    //srow5.Cells.Add(objMSinfo.Reason);
                    Row srow6 = Mtab.Rows.Add();
                    srow6.Cells.Add("Pharmacy Name:");
                    srow6.Cells.Add(objMSinfo.PharmacyName); // sandeep modified
                    Row srow7 = Mtab.Rows.Add();
                    srow7.Cells.Add("Send Notifications:");
                    string SendNotification = objMSinfo.Notify ? "Yes" : "No";
                    srow7.Cells.Add(SendNotification);

                    Aspose.Pdf.Generator.TextInfo t2 = new Aspose.Pdf.Generator.TextInfo();
                    t2.FontSize = 14;
                    t2.IsTrueTypeFontBold = true;

                    Row srow12 = Mtab.Rows.Add();
                    srow12.Cells.Add("Schedules", t2);

                    //End
                    if (objMSinfo.lstSchedule == null)
                    {
                        //Row srow88 = Mtab.Rows.Add();
                        //srow88.Cells.Add("No Schedules");
                        Aspose.Pdf.Generator.TextInfo t1 = new Aspose.Pdf.Generator.TextInfo();
                        t1.FontSize = 12;
                        t1.IsTrueTypeFontBold = false;


                        Row srow51 = Mtab.Rows.Add();
                        srow51.Cells.Add("No Schedules ", t1);
                    }
                    else
                    {
                        int m = 1;
                        foreach (var lstSchedules in objMSinfo.lstSchedule)
                        {
                            Aspose.Pdf.Generator.TextInfo t1 = new Aspose.Pdf.Generator.TextInfo();
                            t1.FontSize = 12;
                            t1.IsTrueTypeFontBold = true;


                            Row srow51 = Mtab.Rows.Add();
                            srow51.Cells.Add("Schedule " + m + ":", t1);

                            Row srow87 = Mtab.Rows.Add();
                            srow87.Cells.Add("Medicine Name:");
                            srow87.Cells.Add(objMSinfo.lstSchedule[m - 1].MedicineName);
                            Row srow86 = Mtab.Rows.Add();
                            srow86.Cells.Add("Medicine Type:");
                            srow86.Cells.Add(objMSinfo.lstSchedule[m - 1].TypeOfMedicine);
                            Row srow85 = Mtab.Rows.Add();
                            srow85.Cells.Add("Dosage Taken:");
                            srow85.Cells.Add(objMSinfo.lstSchedule[m - 1].DosageTaken);

                            Row srow83 = Mtab.Rows.Add();
                            srow83.Cells.Add("Quantity:");
                            srow83.Cells.Add(objMSinfo.lstSchedule[m - 1].TotalQuantity);

                            m++;

                        }
                    }
                    objMSinfo.lstDocuments = _objMdocuments.GetDocumentsbyMedicalScheduleId(objMSinfo.MedicalScheduleId);
                    int y = 0;
                    foreach (var docInfo in objMSinfo.lstDocuments)
                    {
                        y++;
                        Row srow8 = Mtab.Rows.Add();
                        srow8.Cells.Add("Document" + y + ":");
                        srow8.Cells.Add(docInfo.OriginalName);
                        string FilePath = docInfo.Path;
                        docInfo.Name = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
                        docInfo.CreatedBy = LoginUser.UserId;
                        CreateDoc(docInfo);
                    }
                    x++;
                }
            #endregion
            //Visits
            #region

            Text pdfVHeadText = new Text("Visits");

            pdfVHeadText.TextInfo.FontName = "Arial";
            pdfVHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfVHeadText.TextInfo.FontSize = 16;
            pdfVHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfVHeadText);


            Table Vtab = new Table();
            sec1.Paragraphs.Add(Vtab);
            Vtab.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo Vmargin = new MarginInfo();
            Vmargin.Top = 5;
            Vmargin.Left = 5;
            Vmargin.Right = 5;
            Vmargin.Bottom = 5;


            Vtab.DefaultCellPadding = Vmargin;

            int p = 1;
            Getdocuments _objVdocuments = new Getdocuments();

            objCase.lstVisits = GetVisits.GetAllVisitsByCaseId(CaseId);
            if (objCase.lstVisits.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;


                Row srow51 = Vtab.Rows.Add();
                srow51.Cells.Add("No Visits", t);

            }
            else
                foreach (Visits visitsInfo in objCase.lstVisits)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;


                    Row srow51 = Vtab.Rows.Add();
                    srow51.Cells.Add("Visit" + p, t);



                    Row srow11 = Vtab.Rows.Add();
                    srow11.Cells.Add(" Doctor's Log:");
                    srow11.Cells.Add(visitsInfo.DoctorsLog);

                    Row srow10 = Vtab.Rows.Add();
                    srow10.Cells.Add(" Precautions:");
                    srow10.Cells.Add(visitsInfo.Precautions);
                    Row srow12 = Vtab.Rows.Add();
                    srow12.Cells.Add(" Diet Prescription:");
                    srow12.Cells.Add(visitsInfo.DietPresc);

                    Row srow13 = Vtab.Rows.Add();
                    srow13.Cells.Add(" Nurse Readings:");
                    string NurseReadings = string.Empty;

                    if (visitsInfo.NurseReadingsInfo != null)
                    {
                        //foreach (var NReadings in visitsInfo.NurseReadingsInfo)
                        //{
                        NurseReadings = NurseReadings + "Height: " + visitsInfo.NurseReadingsInfo.Height + "," + "Weight: " + visitsInfo.NurseReadingsInfo.Weight + "," + "Temperature: " + visitsInfo.NurseReadingsInfo.Temparature + "," + "Sistole(High B.P): " + visitsInfo.NurseReadingsInfo.Sistole + "," + "Diastole(Low B.P): " + visitsInfo.NurseReadingsInfo.Diastole + "," + "Respiratoryrate: " + visitsInfo.NurseReadingsInfo.Respiratoryrate + "," + "PulseRate: " + visitsInfo.NurseReadingsInfo.PulseRate + Environment.NewLine;
                        //}
                    }

                    row5.Cells.Add(NurseReadings);
                    //string NurseReadings = "Height: " + visitsInfo.NurseReadingsInfo..BMIHeight + "," + "Weight: " + visitsInfo.BMIWeight;
                    //row7.Cells.Add(NurseReadings);
                    //srow13.Cells.Add(visitsInfo.Precautions);
                    //Row srow24 = Vtab.Rows.Add();
                    //srow24.Cells.Add("Visit Date:");
                    //string main = visitsInfo.VisitDate.ToString();
                    //string[] tmp2;
                    //string date2;//, time;

                    //tmp2 = main.Split(' ');

                    //date2 = tmp2[0].ToString();
                    //// time = tmp[1].ToString() + " " + tmp[2].ToString();
                    //srow24.Cells.Add(date2);


                    Aspose.Pdf.Generator.Row row41 = tab1.Rows.Add();
                    row41.Cells.Add("Doctor Info:");
                    string DInfo = string.Empty;

                    if (visitsInfo.DoctorInfos != null)
                    {
                        //foreach (var DoctorInfos in visitsInfo.DoctorInfos)
                        //{
                        DInfo = DInfo + "Doctor Name: " + visitsInfo.DoctorInfos.DoctorName + "," + "Contact No.: " + visitsInfo.DoctorInfos.DoctorPhno + "," + "Email: " + visitsInfo.DoctorInfos.DoctorEmail + Environment.NewLine;
                        //}
                    }

                    row5.Cells.Add(DInfo);

                    //Row srow32 = Vtab.Rows.Add();
                    //srow32.Cells.Add("Provider or Facility:");
                    //srow32.Cells.Add(visitsInfo.ProviderFacility);
                    Row srow24 = Vtab.Rows.Add();
                    srow24.Cells.Add("Next Visit Date:");
                    string main3 = visitsInfo.NextVisitDate.ToString();
                    string[] tmp3;
                    string date3;//, time;

                    tmp3 = main3.Split(' ');

                    date3 = tmp3[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow24.Cells.Add(date3);

                    Row srow30 = Vtab.Rows.Add();
                    srow30.Cells.Add("Send Notification:");
                    srow30.Cells.Add(visitsInfo.IsNotify.ToString());


                    p++;
                }
            #endregion
            pdf1.Save(DestPdfFilePath);

            return DestPdfFilePath;
        }

        //Added by jagadeesh
        private string Relatedlogs(string logId)
        {
            UserInformation LoginUser = new UserInformation();
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            UserHealthLog hl = GetHealthLogs.GetHealthLog(logId);

            string s = Convert.ToDateTime(hl.date).ToShortDateString();
            string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
            string retFileName = hl.logName + LoginUser.UserId + strDate + ".pdf";

            string tempDir = "C:\\InndocsiHealth\\PDFFiles\\" + LoginUser.UserId;
            string DestPdfFilePath = tempDir + "\\" + retFileName;

            //string[] files = Directory.GetFiles(tempDir);

            //foreach (string file in files)
            //{
            //    if (System.IO.File.Exists(file))
            //    {
            //        System.IO.File.Delete(file);
            //    }

            //}
            //Directory.Delete(tempDir, true);

            Directory.CreateDirectory(tempDir);


            License lic = new License();
            lic.SetLicense("C:\\InndocsiHealth\\Aspose.Total.lic");

            Pdf pdf1 = new Pdf();
            Section sec1 = pdf1.Sections.Add();


            sec1.PageInfo.PageWidth = Aspose.Pdf.Generator.PageSize.A3Width;
            sec1.PageInfo.PageHeight = Aspose.Pdf.Generator.PageSize.A3Height;

            MarginInfo marginInfo = new MarginInfo();
            marginInfo.Top = 72;
            marginInfo.Bottom = 72;
            marginInfo.Left = 90;
            marginInfo.Right = 90;

            sec1.PageInfo.Margin = marginInfo;


            Text pdfHeadText = new Text("Log");

            pdfHeadText.TextInfo.FontName = "Arial";
            pdfHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfHeadText.TextInfo.FontSize = 16;
            pdfHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText);



            Table tab1 = new Table();
            sec1.Paragraphs.Add(tab1);
            tab1.ColumnWidths = "120 400";

            tab1.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            tab1.Border = new BorderInfo((int)BorderSide.All, 1F);

            MarginInfo margin = new MarginInfo();
            margin.Top = 10;
            margin.Left = 10;
            margin.Right = 10;
            margin.Bottom = 10;


            tab1.DefaultCellPadding = margin;


            Aspose.Pdf.Generator.Row row1 = tab1.Rows.Add();
            row1.Cells.Add("Log name:");
            row1.Cells.Add(hl.logName);

            Aspose.Pdf.Generator.Row row2 = tab1.Rows.Add();
            row2.Cells.Add("Log description:");
            row2.Cells.Add(hl.logDescription);


            Aspose.Pdf.Generator.Row row3 = tab1.Rows.Add();
            row3.Cells.Add("First observed:");
            row3.Cells.Add(hl.date);

            Text pdfHeadText1 = new Text("Symptoms");

            pdfHeadText1.TextInfo.FontName = "Arial";
            pdfHeadText1.TextInfo.IsTrueTypeFontBold = true;

            pdfHeadText1.TextInfo.FontSize = 16;
            pdfHeadText1.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText1);


            Table tab2 = new Table();
            sec1.Paragraphs.Add(tab2);
            tab2.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo margin1 = new MarginInfo();
            margin1.Top = 5;
            margin1.Left = 5;
            margin1.Right = 5;
            margin1.Bottom = 5;


            tab2.DefaultCellPadding = margin1;

            int i = 1;

            foreach (var symptom in hl.symptoms)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = true;


                Row srow5 = tab2.Rows.Add();
                srow5.Cells.Add("Symptom" + i, t);



                Row srow1 = tab2.Rows.Add();
                srow1.Cells.Add("Created on:");
                srow1.Cells.Add(symptom.DateTime.ToString());


                Row srow2 = tab2.Rows.Add();
                srow2.Cells.Add("Symptom name:");
                srow2.Cells.Add(symptom.Name);


                Row srow3 = tab2.Rows.Add();
                srow3.Cells.Add("Symptom description:");
                srow3.Cells.Add(symptom.Description);

                Row srow4 = tab2.Rows.Add();
                srow4.Cells.Add("Reason:");
                srow4.Cells.Add(symptom.Reasons);

                Row srow6 = tab2.Rows.Add();
                srow6.Cells.Add("Medicine:");
                srow6.Cells.Add(symptom.Medicines);
                i++;
            }



            pdf1.Save(DestPdfFilePath);
            return DestPdfFilePath;


        }
        //end

        //Added by jagadeesh
        FileStream[] input, mt, ms, rec, rel, c, final;
        public ActionResult PrintDownload(string Id, string strRelatedCases)
        {
            UserInformation LoginUser = new UserInformation();
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }


            GetUserCases _getCaseById = new GetUserCases();
            Cases objCase = _getCaseById.GetCaseByID(Id);
            Services3 s3 = new Services3();
            MedicalInformation medinfo = new MedicalInformation();

            string relatedvalues = strRelatedCases;

            string[] related = relatedvalues.Split(',');
            string relatedcases = related[0];
            string relatedlogs = related[1];
            string relateddocs;
            if (related.Length > 2)
            {
                relateddocs = related[2];
            }
            else
            {
                relateddocs = null;
            }

            medinfo = s3.GetMedicalInfo(LoginUser.UserId);
            objCase.lstcasetocase = GetCaseTocases.GetAllCaseToCasesByCaseId(Id);
            objCase.lstcasetolog = GetCaseToLogs.GetAllCaseTologsByCaseId(Id);
            objCase.lstCases = GetUserCases.GetAllCases(LoginUser.UserId,LoginUser.GroupType);
            objCase.lstHealthlog = GetHealthLogs.GetAllHealthLogs(LoginUser.UserId,LoginUser.GroupType);
            objCase.lstresultCaselist = ResultList.GetResulCaseList(objCase.lstCases, objCase.lstcasetocase);
            objCase.lstCases.Where(a => a.CaseId == Id).ToList().ForEach(a => objCase.lstCases.Remove(a));
            objCase.lstresultLoglist = ResultList.GetResultLogList(objCase.lstHealthlog, objCase.lstcasetolog);
            objCase.BMIHeight = medinfo.BMIHeight;
            objCase.BMIWeight = medinfo.BMIWeight;

            //FileStream[] rec;
            //FileStream[] rel;
            PdfFileEditor pdfEditor = new PdfFileEditor();
            int ab = 0;
            string s = Convert.ToDateTime(objCase.CreatedOn).ToShortDateString();
            string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
            string retFileName = objCase.CaseName + LoginUser.UserId + strDate + ".pdf";
            string zipFileName = objCase.CaseName + LoginUser.UserId + strDate + ".zip";
            string concatFile = objCase.CaseName + LoginUser.UserId + strDate + "1.pdf";
            string tempDir = "C:\\InndocsiHealth\\" + LoginUser.UserId;// +LoginUser.UserId;
            string SendToDocInfo = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
            string DestPdfFilePath = tempDir + "\\" + retFileName;
            string SourceDirectoryPath = tempDir;//@"C:\InndocsiHealth\PDFFiles\74";
            string TargetZipFilePath = tempDir + objCase.CaseName + LoginUser.UserId + strDate + ".zip";//@"C:\InndocsiHealth\PDFFiles\" + objCase.CaseName + LoginUser.LoginName + strDate + ".zip";
            string concatDir = tempDir + "\\" + concatFile;

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }

            License lic = new License();
            lic.SetLicense("C:\\InndocsiHealth\\Aspose.Total.lic");

            Pdf pdf1 = new Pdf();
            Section sec1 = pdf1.Sections.Add();

            sec1.PageInfo.PageWidth = Aspose.Pdf.Generator.PageSize.A3Width;
            sec1.PageInfo.PageHeight = Aspose.Pdf.Generator.PageSize.A3Height;

            MarginInfo marginInfo = new MarginInfo();
            marginInfo.Top = 72;
            marginInfo.Bottom = 72;
            marginInfo.Left = 90;
            marginInfo.Right = 90;

            sec1.PageInfo.Margin = marginInfo;

            Text pdfHeadText = new Text("Health Record Information");

            pdfHeadText.TextInfo.FontName = "Arial";
            pdfHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfHeadText.TextInfo.FontSize = 16;
            pdfHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfHeadText);

            Table tab1 = new Table();
            sec1.Paragraphs.Add(tab1);
            tab1.ColumnWidths = "122 400";

            tab1.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);

            tab1.Border = new BorderInfo((int)BorderSide.All, 1F);

            MarginInfo margin = new MarginInfo();
            margin.Top = 10;
            margin.Left = 10;
            margin.Right = 10;
            margin.Bottom = 10;

            tab1.DefaultCellPadding = margin;

            Aspose.Pdf.Generator.Row row1 = tab1.Rows.Add();
            row1.Cells.Add("Case Name:");
            row1.Cells.Add(objCase.CaseName);

            Aspose.Pdf.Generator.Row row2 = tab1.Rows.Add();
            row2.Cells.Add("Case Description:");
            row2.Cells.Add(objCase.CaseDescription);

            Aspose.Pdf.Generator.Row row4 = tab1.Rows.Add();
            row4.Cells.Add("Type Of Problem:");
            row4.Cells.Add(objCase.TypeOfProblem);

            Aspose.Pdf.Generator.Row row5 = tab1.Rows.Add();
            row5.Cells.Add("Hospital Info:");
            string HInfo = string.Empty;

            if (objCase.HospitalInfos != null)
            {
                foreach (var lstHos in objCase.HospitalInfos)
                {
                    HInfo = HInfo + "Hospital Name: " + lstHos.HospitalName + "," + "Address: " + lstHos.HospAddress + "," + "Contact No.: " + lstHos.HospPhno + "," + "Email: " + lstHos.HospEmail + Environment.NewLine;
                }
            }

            row5.Cells.Add(HInfo);

            Aspose.Pdf.Generator.Row row6 = tab1.Rows.Add();
            row6.Cells.Add("Patient Type:");
            string PatientType = objCase.PatientType ? "In Patient" : "Out Patient";
            row6.Cells.Add(PatientType);

            Aspose.Pdf.Generator.Row row18 = tab1.Rows.Add();
            row18.Cells.Add("Related Cases:");
            string CaseToCases = string.Empty;
            if (objCase.lstresultCaselist != null)
            {
                var size = (from e in objCase.lstresultCaselist
                            where e.IsChecked
                            select e).ToArray().Length;
                rec = new FileStream[size];
                int gh = 0;
                foreach (var lstRelatedCases in objCase.lstresultCaselist)
                {
                    if (lstRelatedCases.IsChecked)
                    {
                        CaseToCases = CaseToCases + lstRelatedCases.name + ", ";
                        if (relatedcases == "Relatedcases")
                        {

                            string path = RelatedCases(lstRelatedCases.Id);

                            rec[gh] = new FileStream(path, FileMode.Open, FileAccess.Read);
                            gh++;


                        }
                    }

                }
            }
            row18.Cells.Add(CaseToCases);

            Aspose.Pdf.Generator.Row row8 = tab1.Rows.Add();

            row8.Cells.Add("Related Logs:");

            string casetolog = string.Empty;
            if (objCase.lstresultLoglist != null)
            {
                var size = (from e in objCase.lstresultLoglist
                            where e.IsChecked
                            select e).ToArray().Length;
                rel = new FileStream[size];
                int ij = 0;
                foreach (var lstRelatedlogs in objCase.lstresultLoglist)
                {
                    if (lstRelatedlogs.IsChecked)
                    {
                        casetolog = casetolog + lstRelatedlogs.name + ", ";
                        if (relatedlogs == "Relatedlogs")
                        {

                            string path = Relatedlogs(lstRelatedlogs.Id);

                            rel[ij] = new FileStream(path, FileMode.Open, FileAccess.Read);
                            ij++;


                        }
                    }

                }
            }

            #region

            Text pdfProcHeadText = new Text("Procedures");

            pdfProcHeadText.TextInfo.FontName = "Arial";
            pdfProcHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfProcHeadText.TextInfo.FontSize = 16;
            pdfProcHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfProcHeadText);

            Table tab39 = new Table();
            sec1.Paragraphs.Add(tab39);
            tab39.ColumnWidths = "120 400 ";

            MarginInfo margin2 = new MarginInfo();
            margin2.Top = 5;
            margin2.Left = 5;
            margin2.Right = 5;
            margin2.Bottom = 5;

            tab39.DefaultCellPadding = margin2;

            int i = 1;
            Getdocuments _objdocuments = new Getdocuments();

            objCase.lstProcedure = GetProcedures.GetAllProceduresByCaseId(Id);
            if (objCase.lstProcedure.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;
                input = new FileStream[1];
                Row srow5 = tab39.Rows.Add();
                srow5.Cells.Add("No Procedures ", t);
            }
            else
            {
                foreach (Procedure procedure in objCase.lstProcedure)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;

                    Row srow5 = tab39.Rows.Add();
                    srow5.Cells.Add("Procedure" + i, t);

                    Row srow1 = tab39.Rows.Add();
                    srow1.Cells.Add("Procedure Name:");
                    srow1.Cells.Add(procedure.ProcedureName);

                    Row srow2 = tab39.Rows.Add();
                    srow2.Cells.Add("Date of Service:");
                    string main = procedure.DateOfService.ToString();
                    string[] tmp;
                    string date;//, time;

                    tmp = main.Split(' ');

                    date = tmp[0].ToString();

                    srow2.Cells.Add(date);

                    Row srow3 = tab39.Rows.Add();
                    srow3.Cells.Add("Provider or Facility:");
                    srow3.Cells.Add(procedure.ProviderOrFacility);

                    Row srow4 = tab39.Rows.Add();
                    srow4.Cells.Add("Surgeon:");
                    srow4.Cells.Add(procedure.Surgeon);

                    procedure.lstDocuments = _objdocuments.GetDocumentsbyProcId(procedure.ProcedureId);
                    int j = 0;

                    input = new FileStream[procedure.lstDocuments.Count + 1];


                    if (relateddocs == null || relateddocs == "")
                    {
                        foreach (var docInfo in procedure.lstDocuments)
                        {
                            Documents newdoc = new Documents();
                            // Documents doc = _objdocuments.GetDocumentDownload(Convert.ToInt32(docInfo.Id));

                            //newdoc.yData = doc.yData;
                            j++;
                            Row srow6 = tab39.Rows.Add();
                            srow6.Cells.Add("Document" + j + ":");
                            srow6.Cells.Add(docInfo.OriginalName);
                            // docInfo.Name = SendToDocInfo;//Case Name for folder Creation Name
                            docInfo.Name = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
                            //doc = new Documents();
                            string FilePath = docInfo.Path;//"C:\\InndocsiHealth\\PDFFiles\\74\\5be431c3-7664-45a0-ae4a-a2764eec11b8.pdf";//Path.GetFileName(docInfo.Path);
                            docInfo.CreatedBy = LoginUser.UserId;
                            CreateDoc(docInfo);
                            ab++;
                            input[ab] = new FileStream(docInfo.Path, FileMode.Open, FileAccess.Read);

                        }
                    }

                    i++;
                }
            }
            #endregion
            //Medical Tests
            #region

            Text pdfMTHeadText = new Text("Medical Tests");

            pdfMTHeadText.TextInfo.FontName = "Arial";
            pdfMTHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfMTHeadText.TextInfo.FontSize = 16;
            pdfMTHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfMTHeadText);


            Table MTtab = new Table();
            sec1.Paragraphs.Add(MTtab);
            MTtab.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);

            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo MTmargin = new MarginInfo();
            MTmargin.Top = 5;
            MTmargin.Left = 5;
            MTmargin.Right = 5;
            MTmargin.Bottom = 5;


            MTtab.DefaultCellPadding = margin2;

            int k = 1;
            Getdocuments _objMTdocuments = new Getdocuments();

            objCase.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(Id);
            if (objCase.lstMedicalTest.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;


                Row srow5 = MTtab.Rows.Add();
                srow5.Cells.Add("No Medical Tests", t);

            }
            else
            {
                int cd = 0;
                foreach (MedicalTests objMTests in objCase.lstMedicalTest)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;

                    Row srow5 = MTtab.Rows.Add();
                    srow5.Cells.Add("Medical Test" + k, t);

                    Row srow1 = MTtab.Rows.Add();
                    srow1.Cells.Add("Test Name:");
                    srow1.Cells.Add(objMTests.TestName);

                    Row srow2 = MTtab.Rows.Add();
                    srow2.Cells.Add("Date of Service:");
                    string main = objMTests.DateOfTest.ToString();
                    string[] tmp;
                    string date;//, time;

                    tmp = main.Split(' ');

                    date = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow2.Cells.Add(date);

                    objMTests.lstDocuments = _objMTdocuments.GetDocumentsbyMedicalTestId(objMTests.MedicalTestId);
                    mt = new FileStream[objMTests.lstDocuments.Count];
                    int l = 1;
                    if (relateddocs == null || relateddocs == "")
                    {
                        foreach (var docInfo in objMTests.lstDocuments)
                        {

                            Row srow6 = MTtab.Rows.Add();
                            srow6.Cells.Add("Document" + l + ":");
                            srow6.Cells.Add(docInfo.OriginalName);
                            //doc = new Documents();
                            string FilePath = docInfo.Path;//"C:\\InndocsiHealth\\PDFFiles\\74\\5be431c3-7664-45a0-ae4a-a2764eec11b8.pdf";//Path.GetFileName(docInfo.Path);
                            docInfo.Name = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
                            docInfo.CreatedBy = LoginUser.UserId;
                            CreateDoc(docInfo);
                            mt[cd] = new FileStream(docInfo.Path, FileMode.Open, FileAccess.Read);
                            l++;
                            cd++;
                        }

                    }
                    k++;
                }
            }
            #endregion
            //Medications
            #region

            Text pdfMHeadText = new Text("Medications");

            pdfMHeadText.TextInfo.FontName = "Arial";
            pdfMHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfMHeadText.TextInfo.FontSize = 16;
            pdfMHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfMHeadText);


            Table Mtab = new Table();
            sec1.Paragraphs.Add(Mtab);
            Mtab.ColumnWidths = "120 400 ";

            // tab2.DefaultCellBorder = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 0.1F);


            // tab2.Border = new BorderInfo((int)Aspose.Pdf.Generator.BorderSide.All, 1F);

            MarginInfo Mmargin = new MarginInfo();
            Mmargin.Top = 5;
            Mmargin.Left = 5;
            Mmargin.Right = 5;
            Mmargin.Bottom = 5;


            Mtab.DefaultCellPadding = Mmargin;

            int x = 1;
            Getdocuments _objMdocuments = new Getdocuments();

            objCase.lstMedicalSchedule = GetMedicalSchedules.GetAllMeicalschdulesbyCaseId(Id);
            if (objCase.lstMedicalSchedule.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;


                Row srow1 = Mtab.Rows.Add();
                srow1.Cells.Add("No Medications", t);

            }
            else
                foreach (MedicalSchedule objMSinfo in objCase.lstMedicalSchedule)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;


                    Row srow1 = Mtab.Rows.Add();
                    srow1.Cells.Add("Medication" + x, t);



                    //Row srow1 = tab3.Rows.Add();
                    //srow1.Cells.Add("Date Prescribed:");
                    //srow1.Cells.Add(objMSinfo.DatePrescribed.);


                    Row srow2 = Mtab.Rows.Add();
                    srow2.Cells.Add("Date Prescribed:");
                    string main = objMSinfo.StartDate.ToString();
                    string[] tmp;
                    string date;//, time;

                    tmp = main.Split(' ');

                    date = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow2.Cells.Add(date);

                    Row srow3 = Mtab.Rows.Add();
                    srow3.Cells.Add("Date Dispensed:");
                    string main1 = objMSinfo.EndDate.ToString();
                    string[] tmp1;
                    string date1;//, time;

                    tmp1 = main.Split(' ');

                    date1 = tmp[0].ToString();
                    // time = tmp[1].ToString() + " " + tmp[2].ToString();
                    srow3.Cells.Add(date1);


                    //Row srow4 = Mtab.Rows.Add();
                    //srow4.Cells.Add("Doctor Name:");
                    //srow4.Cells.Add(objMSinfo.DoctorName);
                    //Row srow5 = Mtab.Rows.Add();
                    //srow5.Cells.Add("Reason for Taken:");
                    //srow5.Cells.Add(objMSinfo.Reason);
                    Row srow6 = Mtab.Rows.Add();
                    srow6.Cells.Add("Pharmacy Name:");
                    srow6.Cells.Add(objMSinfo.PharmacyName);
                    Row srow7 = Mtab.Rows.Add();
                    srow7.Cells.Add("Send Notifications:");
                    string SendNotification = objMSinfo.Notify ? "Yes" : "No";
                    srow7.Cells.Add(SendNotification);

                    // objMSinfo.lstSchedule = 
                    // string g = objMSinfo.lstSchedule.Count().ToString();

                    // AD

                    Aspose.Pdf.Generator.TextInfo t2 = new Aspose.Pdf.Generator.TextInfo();
                    t2.FontSize = 14;
                    t2.IsTrueTypeFontBold = true;


                    Row srow12 = Mtab.Rows.Add();
                    srow12.Cells.Add("Schedules", t2);

                    //End
                    if (objMSinfo.lstSchedule == null)
                    {
                        //Row srow88 = Mtab.Rows.Add();
                        //srow88.Cells.Add("No Schedules");
                        Aspose.Pdf.Generator.TextInfo t1 = new Aspose.Pdf.Generator.TextInfo();
                        t1.FontSize = 12;
                        t1.IsTrueTypeFontBold = false;


                        Row srow51 = Mtab.Rows.Add();
                        srow51.Cells.Add("No Schedules ", t1);
                    }
                    else
                    {

                        int m = 1;
                        foreach (var lstSchedules in objMSinfo.lstSchedule)
                        {
                            Aspose.Pdf.Generator.TextInfo t1 = new Aspose.Pdf.Generator.TextInfo();
                            t1.FontSize = 12;
                            t1.IsTrueTypeFontBold = true;


                            Row srow51 = Mtab.Rows.Add();
                            srow51.Cells.Add("Schedule " + m + ":", t1);
                            //Row srow88 = Mtab.Rows.Add();
                            //srow88.Cells.Add("Schedule" + m + ":",t2);
                            Row srow87 = Mtab.Rows.Add();
                            srow87.Cells.Add("Medicine Name:");
                            srow87.Cells.Add(objMSinfo.lstSchedule[m - 1].MedicineName);
                            Row srow86 = Mtab.Rows.Add();
                            srow86.Cells.Add("Medicine Type:");
                            srow86.Cells.Add(objMSinfo.lstSchedule[m - 1].TypeOfMedicine);
                            Row srow85 = Mtab.Rows.Add();
                            srow85.Cells.Add("Dosage Taken:");
                            srow85.Cells.Add(objMSinfo.lstSchedule[m - 1].DosageTaken);
                            //Row srow84 = Mtab.Rows.Add();
                            //srow84.Cells.Add("Frequency Taken:");
                            //srow84.Cells.Add(objMSinfo.lstSchedule[m - 1].FrequencyTaken);
                            Row srow83 = Mtab.Rows.Add();
                            srow83.Cells.Add("Quantity:");
                            srow83.Cells.Add(objMSinfo.lstSchedule[m - 1].TotalQuantity);
                            //Row srow82 = Mtab.Rows.Add();
                            //srow82.Cells.Add("Days Supplied:");
                            //srow82.Cells.Add(objMSinfo.lstSchedule[m - 1]..DaysSupply.ToString());
                            //Row srow81 = Mtab.Rows.Add();
                            //srow81.Cells.Add("Notes:");
                            //srow81.Cells.Add(objMSinfo.lstSchedule[m - 1].Notes);
                            m++;

                        }
                    }
                    objMSinfo.lstDocuments = _objMdocuments.GetDocumentsbyMedicalScheduleId(objMSinfo.MedicalScheduleId);
                    int y = 0, ef = 0;
                    ms = new FileStream[objMSinfo.lstDocuments.Count];
                    if (relateddocs == null || relateddocs == "")
                    {
                        foreach (var docInfo in objMSinfo.lstDocuments)
                        {
                            y++;
                            Row srow8 = Mtab.Rows.Add();
                            srow8.Cells.Add("Document" + y + ":");
                            srow8.Cells.Add(docInfo.OriginalName);
                            string FilePath = docInfo.Path;
                            docInfo.Name = tempDir + "\\" + objCase.CaseName;//Sending this string to docinfo obj in docinfo.Name
                            docInfo.CreatedBy = LoginUser.UserId;
                            CreateDoc(docInfo);
                            ms[ef] = new FileStream(docInfo.Path, FileMode.Open, FileAccess.Read);
                            ef++;
                        }
                    }
                    x++;
                }
            #endregion
            //Visits
            #region

            Text pdfVHeadText = new Text("Visits");

            pdfVHeadText.TextInfo.FontName = "Arial";
            pdfVHeadText.TextInfo.IsTrueTypeFontBold = true;
            pdfVHeadText.TextInfo.FontSize = 16;
            pdfVHeadText.TextInfo.LineSpacing = 5f;

            sec1.Paragraphs.Add(pdfVHeadText);


            Table Vtab = new Table();
            sec1.Paragraphs.Add(Vtab);
            Vtab.ColumnWidths = "120 400 ";

            MarginInfo Vmargin = new MarginInfo();
            Vmargin.Top = 5;
            Vmargin.Left = 5;
            Vmargin.Right = 5;
            Vmargin.Bottom = 5;

            Vtab.DefaultCellPadding = Vmargin;

            int p = 1;
            Getdocuments _objVdocuments = new Getdocuments();

            objCase.lstVisits = GetVisits.GetAllVisitsByCaseId(Id);
            if (objCase.lstVisits.Count == 0)
            {
                Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                t.FontSize = 14;
                t.IsTrueTypeFontBold = false;

                Row srow51 = Vtab.Rows.Add();
                srow51.Cells.Add("No Visits", t);

            }
            else
                foreach (Visits visitsInfo in objCase.lstVisits)
                {
                    Aspose.Pdf.Generator.TextInfo t = new Aspose.Pdf.Generator.TextInfo();
                    t.FontSize = 14;
                    t.IsTrueTypeFontBold = true;

                    Row srow51 = Vtab.Rows.Add();
                    srow51.Cells.Add("Visit" + p, t);

                    Row srow11 = Vtab.Rows.Add();
                    srow11.Cells.Add(" Doctor's Log:");
                    srow11.Cells.Add(visitsInfo.DoctorsLog);

                    Row srow10 = Vtab.Rows.Add();
                    srow10.Cells.Add(" Precautions:");
                    srow10.Cells.Add(visitsInfo.Precautions);
                    Row srow12 = Vtab.Rows.Add();
                    srow12.Cells.Add(" Diet Prescription:");
                    srow12.Cells.Add(visitsInfo.DietPresc);

                    Row srow13 = Vtab.Rows.Add();
                    srow13.Cells.Add(" Nurse Readings:");
                    string NurseReadings = string.Empty;

                    if (visitsInfo.NurseReadingsInfo != null)
                    {

                        NurseReadings = NurseReadings + "Height: " + visitsInfo.NurseReadingsInfo.Height + "," + "Weight: " + visitsInfo.NurseReadingsInfo.Weight + "," + "Temperature: " + visitsInfo.NurseReadingsInfo.Temparature + "," + "Sistole(High B.P): " + visitsInfo.NurseReadingsInfo.Sistole + "," + "Diastole(Low B.P): " + visitsInfo.NurseReadingsInfo.Diastole + "," + "Respiratoryrate: " + visitsInfo.NurseReadingsInfo.Respiratoryrate + "," + "PulseRate: " + visitsInfo.NurseReadingsInfo.PulseRate + Environment.NewLine;

                    }

                    row5.Cells.Add(NurseReadings);

                    Aspose.Pdf.Generator.Row row41 = tab1.Rows.Add();
                    row41.Cells.Add("Doctor Info:");
                    string DInfo = string.Empty;

                    if (visitsInfo.DoctorInfos != null)
                    {

                        DInfo = DInfo + "Doctor Name: " + visitsInfo.DoctorInfos.DoctorName + "," + "Contact No.: " + visitsInfo.DoctorInfos.DoctorPhno + "," + "Email: " + visitsInfo.DoctorInfos.DoctorEmail + Environment.NewLine;

                    }

                    row5.Cells.Add(DInfo);

                    Row srow24 = Vtab.Rows.Add();
                    srow24.Cells.Add("Next Visit Date:");
                    string main3 = visitsInfo.NextVisitDate.ToString();
                    string[] tmp3;
                    string date3;//, time;

                    tmp3 = main3.Split(' ');

                    date3 = tmp3[0].ToString();

                    srow24.Cells.Add(date3);

                    Row srow30 = Vtab.Rows.Add();
                    srow30.Cells.Add("Send Notification:");
                    srow30.Cells.Add(visitsInfo.IsNotify.ToString());

                    p++;
                }
            #endregion

            pdf1.Save(DestPdfFilePath);

            FileStream outputStream = new FileStream(concatDir, FileMode.OpenOrCreate);//Replace @"\path" with DestPdfFilePath while hosting and change FileMode.Create to FileMode.Open
            input.SetValue(new FileStream(DestPdfFilePath, FileMode.Open), 0);
            if (mt != null) input = input.Concat(mt).ToArray();
            if (ms != null) input = input.Concat(ms).ToArray();
            if (rec != null) input = input.Concat(rec).ToArray();
            if (rel != null) input = input.Concat(rel).ToArray();
            pdfEditor.Concatenate(input, outputStream);
            foreach (var item in input)
            {
                item.Close();
            }
            //foreach (var item in mt)
            //{
            //    item.Close();
            //}

            outputStream.Close();

            //JsonResult r = new JsonResult();
            return Json(concatDir, JsonRequestBehavior.AllowGet);



            // return File(concatDir, "application/pdf");// DestPdfFilePath
        }
        //end
        private void CreateDoc(Documents docInfo)
        {

            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(Documents));
            serializerToUplaod.WriteObject(ms, docInfo);
            string ServiceURl = DomainServerPath.Service+"DocumentLoader/CreateDoc";
            Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());

        }


        //Added by jagadeesh
        public ActionResult ViewPrintDown(string path)
        {
            //ViewData["caseid"] = caseid;
            return PartialView("ViewPrintDown", path);//, Convert.ToInt32(caseid));

        }
        //end
        //Added by jagadeesh
        public ActionResult PrintSelection(string CaseId)
        {
            return PartialView(new Cases { CaseId = CaseId });
        }
        //end
        //Added by jagadeesh
        public ActionResult GetPrintDocument(string Path)
        {
            string documentpath = Path;
            return File(documentpath, "application/pdf");
        }
        //end
    }
}