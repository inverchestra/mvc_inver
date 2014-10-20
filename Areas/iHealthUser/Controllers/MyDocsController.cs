using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository;
using PagedList;
using System.IO;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;


namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    [SessionExpireFilter]
    public class MyDocsController : Controller
    {
        //
        // GET: /iHealthUser/MyDocs/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyDocs(int? page, string TabId)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            string currentpage = "HealthDocs";
            Session["CurrentPage"] = currentpage;
            UserInformation LoginUser = null;
            RegistrationServiceCalls reguser = new RegistrationServiceCalls();
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            if (LoginUser == null)
            {
                return RedirectToActionPermanent("Home", "Home", new { area = "" });
            }
            //for rightside pannel
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
            //ends here

            SingleRegisterModel domainUser = reguser.GetSingleRegUserbyID(LoginUser.DomainId.ToString());//added by ck for FreeUser modification
            if (domainUser.UserPlan == "FreeUser")
            {
                DateTime expire = Convert.ToDateTime(domainUser.ExpireOn);
                DateTime presenttime = DateTime.Now;
                //var ms = expire.Subtract(presenttime).TotalDays;
                domainUser.Result = (expire.Date - presenttime.Date).Days;
                return View("SubscribeNow", domainUser);
            }

            Getdocuments getdoc = new Getdocuments();
            Documents _docs = new Documents();
            _docs.lstdocprocedures = getdoc.GetAllProcedureDocumentsbyUserId(LoginUser.UserId,LoginUser.GroupType);
            _docs.lstdocmedicaltests = getdoc.GetAllMedicalTestDocumentsbyUserId(LoginUser.UserId,LoginUser.GroupType);
            _docs.lstdocCharts = getdoc.GetAllChartDocumentsbyUserId(LoginUser.UserId,LoginUser.GroupType);
            if (TabId == null)
            {
                TabId = "tabs-1";
            }
            ViewBag.TabId = TabId;


            return View(_docs);

        }

        public ActionResult DeleteDocument(string DocumentId)
        {
            string Successcode = string.Empty;
            string[] Una = DocumentId.Split(',');

            //if (Una.ToString().EndsWith(","))
            //    Una.Remove(Una.Length - 1, 1);


            Documents docs = new Documents();
            Getdocuments getdoc = new Getdocuments();
            //docs = getdoc.GetDocument(Convert.ToInt32(DocumentId));


            foreach (string s in Una)
            {
                for (int i = 0; i < s.Length - 1; i++)
                {

                    docs = getdoc.GetDocument(s);
                    Successcode = Getdocuments.UpdateDocument(docs);
                }
            }


            JsonResult r = new JsonResult();

            if (Successcode == "1020")
            {
                r.Data = Successcode;
                return r;
            }
            else
            {
                r.Data = "Failed";
                return r;
            }

        }

        public JsonResult CaseCheckedChanged(string CaseId, string type)
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
            GetUserCases GetusrCases = new GetUserCases();
            GetProcedures getProcedure = new GetProcedures();
            GetCharts getCharts = new GetCharts();
            GetMedicalTests getMedTests = new GetMedicalTests();
            GetMedicalSchedules getMedsch = new GetMedicalSchedules();
            List<SelectListItem> lstcases = GetusrCases.UserCaseslist(LoginUser.UserId,LoginUser.GroupType);
            ViewBag.UserCaseslist = lstcases;
            if (type.Equals("proc"))
            {
                List<SelectListItem> lstProcedures = getProcedure.UserCaseProcList(CaseId);
                //ViewBag.UserCaseProcList = lstProcedures;

                return Json(lstProcedures, JsonRequestBehavior.AllowGet);
            }
            else if (type.Equals("charts"))
            {
                List<SelectListItem> lstCharts = getCharts.UserCaseChartsList(CaseId);
                //ViewBag.UserCaseChartsList = lstCharts;
                return Json(lstCharts, JsonRequestBehavior.AllowGet);
            }
            else if (type.Equals("medtest"))
            {
                List<SelectListItem> lstMedTests = getMedTests.UserCaseMedTestsList(CaseId);
                //ViewBag.UserCaseMedTestList = lstMedTests;
                return Json(lstMedTests, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<SelectListItem> lstMeschs = getMedsch.UserCaseMedicationsList((CaseId));
                return Json(lstMeschs, JsonRequestBehavior.AllowGet);
            }



        }

        public ActionResult ProcUploadDocument()
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
            GetUserCases GetusrCases = new GetUserCases();
            GetProcedures getProcedure = new GetProcedures();
            List<SelectListItem> lstcases = GetusrCases.UserCaseslist(LoginUser.UserId,LoginUser.GroupType);
            ViewBag.UserCaseslist = lstcases;
            if (lstcases.Count != 0)
            {
                List<SelectListItem> lstProcedures = getProcedure.UserCaseProcList(lstcases[0].Value);
                ViewBag.UserCaseProcList = lstProcedures;
                return PartialView("UploadDocument");
            }
            else
            {
                return Json("NoCases", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ChartsUploadDocument()
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
            GetUserCases GetusrCases = new GetUserCases();
            GetCharts getCharts = new GetCharts();
            List<SelectListItem> lstcases = GetusrCases.UserCaseslist(LoginUser.UserId,LoginUser.GroupType);
            ViewBag.UserCaseslist = lstcases;
            if (lstcases.Count != 0)
            {
                List<SelectListItem> lstCharts = getCharts.UserCaseChartsList(lstcases[0].Value);
                ViewBag.UserCaseChartsList = lstCharts;
                return PartialView("ChartsUploadDocument");
            }
            else
            {
                return Json("NoCases", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MedTestUploadDocument()
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
            GetUserCases GetusrCases = new GetUserCases();
            GetMedicalTests getMedTests = new GetMedicalTests();
            List<SelectListItem> lstcases = GetusrCases.UserCaseslist(LoginUser.UserId,LoginUser.GroupType);
            ViewBag.UserCaseslist = lstcases;
            if (lstcases.Count != 0)
            {
                List<SelectListItem> lstMedTests = getMedTests.UserCaseMedTestsList(lstcases[0].Value);
                ViewBag.UserCaseMedTestList = lstMedTests;
                return PartialView("MedTestUploadDocument");
            }
            else
            {
                return Json("NoCases", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MedicationUploadDocument()
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
            GetUserCases GetusrCases = new GetUserCases();
            GetMedicalSchedules getmedications = new GetMedicalSchedules();
            List<SelectListItem> lstcases = GetusrCases.UserCaseslist(LoginUser.UserId,LoginUser.GroupType);
            ViewBag.UserCaseslist = lstcases;
            if(lstcases.Count!=0)
            {
            List<SelectListItem> lstMedications = getmedications.UserCaseMedicationsList(lstcases[0].Value);
            ViewBag.UserCaseMedicationList = lstMedications;
            return PartialView("MedicationsUploadDocument");
            }
            else
            {
                return Json("NoCases", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult procUpload(Cases cases)
        {

            UserInformation LoginUser = null;
            string success;
            // string rectcode;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }

            // Getdocuments getdoc = new Getdocuments();
            // Documents _docs = new Documents();
            if (cases.Procedures.ProcedureId != null)
            {
                success = InsertFiles("proc", cases.Procedures.ProcedureId, cases.CaseId, cases.procfiles);
            }
            else
            {
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
                cases.Procedures.CaseId = cases.CaseId;
                string ProcedureId = CreateProcedure.InsertProcedureInfo(cases.Procedures);
                success = InsertFiles("proc", ProcedureId, cases.CaseId, cases.procfiles);
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MedTestUpload(Cases cases)
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
            string success;
            if (cases.MedicalTests.MedicalTestId != null)
            {
                success = InsertFiles("medt", cases.MedicalTests.MedicalTestId, cases.CaseId, cases.procfiles); //return View("MyDocs", _docs);
            }
            else
            {
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
                cases.MedicalTests.CaseId = cases.CaseId;
                string MedicalTestId = CreateMedicalTest.InsertMeidcalTestInfo(cases.MedicalTests);
                success = InsertFiles("medt", MedicalTestId, cases.CaseId, cases.procfiles);
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChartsUpload(Cases cases)
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
            string success;
            if (cases.Charts.ChartId != null)
            {
                success = InsertFiles("chrt", cases.Charts.ChartId, cases.CaseId, cases.procfiles);
            }
            else
            {
                cases.Charts.CreatedOn = DateTime.Now;
                if (Session["LoginUser"] != null)
                {
                    UserInformation CreatedByUser = null;
                    CreatedByUser = Session["LoginUser"] as UserInformation;
                    cases.Charts.UserID = CreatedByUser.UserId;
                }
                cases.Charts.OwnerId = LoginUser.UserId;
                cases.Charts.Type = LoginUser.GroupType;
                cases.Charts.CaseId = cases.CaseId;
                string ChartId = CreateChart.InsertChartInfo(cases.Charts);
                success = InsertFiles("chrt", ChartId, cases.CaseId, cases.procfiles);
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MedicationsUpload(Cases cases)
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
            string success;
            if (cases.MedicalSchdule.MedicalScheduleId != null)
            {
                success = InsertFiles("medsc", cases.MedicalSchdule.MedicalScheduleId, cases.CaseId, cases.procfiles);
            }
            else
            {
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
                string a = "prno";
                string strDate = DateTime.Now.ToString("ddMMyyyyhhmmss");
                string retCfName = a + strDate;
                cases.MedicalSchdule.PrescriptionNo = retCfName;

                //dileep
                cases.MedicalSchdule.CaseId = cases.CaseId;
                string MedicationId = CreateMedicalSchedule.InsertMeidcalScheduleInfo(cases.MedicalSchdule);
                success = InsertFiles("medsc", MedicationId, cases.CaseId, cases.procfiles);
            }
            return Json(success, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ProcView(string ProcId)
        {
            Procedure procedure = GetProcedures.GetProcedureById(ProcId);
            return Json(procedure, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MedTestView(string MedTestId)
        {
            MedicalTests medTest = GetMedicalTests.GetMedicalTestById(MedTestId);
            return Json(medTest, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChartsView(string ChartId)
        {
            Charts charts = GetCharts.GetChartById(ChartId);
            return Json(charts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MedicationView(string MedicationId)
        {
            MedicalSchedule medications = GetMedicalSchedules.GetMedicationScheduleById(MedicationId);
            return Json(medications, JsonRequestBehavior.AllowGet);
        }

        public string InsertFiles(string op, string operationId, string caseId, IEnumerable<HttpPostedFileBase> files)
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
                        doc.CreatedBy = LoginUser.UserId;
                        doc.CaseId = caseId;
                        doc.CreatedOn = DateTime.Now;
                        doc.OwnerId = LoginUser.UserId;
                        doc.Type = LoginUser.GroupType;
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
                        doc.DomainID = LoginUser.DomainId;
                        string s = UploadFiles.UploadFile(doc);
                    }
                }
            }

            return "1010";
        }

        //Dileep for paging
        public ActionResult ProcDocuments(int? page)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            string currentpage = "HealthDocs";
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
            Getdocuments getdoc = new Getdocuments();
            Documents _docs = new Documents();
            _docs.lstdocprocedures = getdoc.GetAllProcedureDocumentsbyUserId(LoginUser.UserId,LoginUser.GroupType);
            return PartialView("Procedures", _docs.lstdocprocedures.ToPagedList(PageNumber, PageSize));
        }

        public ActionResult MtestDocuments(int? page)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            string currentpage = "HealthDocs";
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
            Getdocuments getdoc = new Getdocuments();
            Documents _docs = new Documents();
            _docs.lstdocmedicaltests = getdoc.GetAllMedicalTestDocumentsbyUserId(LoginUser.UserId,LoginUser.GroupType);
            return PartialView("MedicalTests", _docs.lstdocmedicaltests.ToPagedList(PageNumber, PageSize));
        }

        public ActionResult ChartsDocuments(int? page)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            string currentpage = "HealthDocs";
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
            Getdocuments getdoc = new Getdocuments();
            Documents _docs = new Documents();
            _docs.lstdocCharts = getdoc.GetAllChartDocumentsbyUserId(LoginUser.UserId,LoginUser.GroupType);
            return PartialView("Charts", _docs.lstdocCharts.ToPagedList(PageNumber, PageSize));

        }

        public ActionResult MedicationDocuments(int? page)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            string currentpage = "HealthDocs";
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
            Getdocuments getdoc = new Getdocuments();
            Documents _docs = new Documents();
            _docs.lstdocMedications = getdoc.GetAllmedicationDocumentsbyUserId(LoginUser.UserId,LoginUser.GroupType);
            return PartialView("Medications", _docs.lstdocMedications.ToPagedList(PageNumber, PageSize));

        }

        public ActionResult GetSearchedProcDocuments(int? page, string searchtext)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            string currentpage = "HealthDocs";
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
            SearchFields sFileds = new SearchFields();
            sFileds.Documents = searchtext;

            sFileds.UserId = LoginUser.UserId;
            ViewBag.FromSearch = searchtext;
            Documents _docs = new Documents();
            _docs.lstdocprocedures = Getdocuments.GetAllSearchedProcDocs(sFileds);


            return PartialView("Procedures", _docs.lstdocprocedures.ToPagedList(PageNumber, PageSize));
        }

        public ActionResult GetSearchedMtestDocuments(int? page, string searchtext)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            string currentpage = "HealthDocs";
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
            SearchFields sFileds = new SearchFields();
            sFileds.Documents = searchtext;

            sFileds.UserId = LoginUser.UserId;
            ViewBag.FromSearch = searchtext;
            Documents _docs = new Documents();
            _docs.lstdocmedicaltests = Getdocuments.GetAllSearchedMtestDocs(sFileds);
            return PartialView("MedicalTests", _docs.lstdocmedicaltests.ToPagedList(PageNumber, PageSize));
        }
        public ActionResult GetSearchedChartsDocuments(int? page, string searchtext)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            string currentpage = "HealthDocs";
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

            SearchFields sFileds = new SearchFields();
            sFileds.Documents = searchtext;

            sFileds.UserId = LoginUser.UserId;
            ViewBag.FromSearch = searchtext;

            Documents _docs = new Documents();
            _docs.lstdocCharts = Getdocuments.GetAllSearchedChartDocs(sFileds);
            return PartialView("Charts", _docs.lstdocCharts.ToPagedList(PageNumber, PageSize));

        }

        public ActionResult GetSearchedMedicationDocuments(int? page, string searchtext)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            string currentpage = "HealthDocs";
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

            SearchFields sFileds = new SearchFields();
            sFileds.Documents = searchtext;

            sFileds.UserId = LoginUser.UserId;
            ViewBag.FromSearch = searchtext;

            Documents _docs = new Documents();
            _docs.lstdocMedications = Getdocuments.GetAllSearchedChartDocs(sFileds);
            return PartialView("Medications", _docs.lstdocMedications.ToPagedList(PageNumber, PageSize));

        }


        //For Paging

    }
}
