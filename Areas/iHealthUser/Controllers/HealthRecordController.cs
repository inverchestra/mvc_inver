using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    [SessionExpireFilter]
    public class HealthRecordController : Controller
    {
        //
        // GET: /iHealthUser/HealthRecord/

        public ActionResult Cases()
        {
            return View();
        }

        [Authorize]
        public ActionResult MyAllCases()
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
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            // return View(GetHealthLogs.GetAllHealthLogs(LoginUser.UserId).ToList());
            return View(GetUserCases.GetAllCases(LoginUser.UserId,LoginUser.GroupType).ToList());
        }

        public ActionResult ViewCase(string CaseId)
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
            foreach (Charts chrts in _cases.lstChart)
            {
                chrts.lstDocuments = _objdocuments.GetDocumentsbyChartId(chrts.ChartId);
            }

            _cases.lstcasetocase = GetCaseTocases.GetAllCaseToCasesByCaseId(CaseId);
            _cases.lstcasetolog = GetCaseToLogs.GetAllCaseTologsByCaseId(CaseId);
            _cases.lstCases = GetUserCases.GetAllCases(LoginUser.UserId,LoginUser.GroupType);
            //_cases.lstHealthlog = GetHealthLogs.GetAllHealthLogs(LoginUser.UserId);
            //_cases.lstresultCaselist = ResultList.GetResulCaseList(_cases.lstCases, _cases.lstcasetocase); //dillep
            _cases.lstCases.Where(a => a.CaseId == CaseId).ToList().ForEach(a => _cases.lstCases.Remove(a));
            //_cases.lstresultLoglist = ResultList.GetResultLogList(_cases.lstHealthlog, _cases.lstcasetolog);//dileep
            _cases.BMIHeight = medinfo.BMIHeight;
            _cases.BMIWeight = medinfo.BMIWeight;

            return PartialView("ViewCase", _cases);

        }

        public ActionResult ViewDocument(string DocumentId)
        {

            return PartialView("ViewDocument", DocumentId);
        }
        public ActionResult GetPdf(string Id)
        {
            Getdocuments _objdocuments = new Getdocuments();
            Documents doc = _objdocuments.GetDocument(Id);
            //logic to get docment obj
            return File(doc.Path, "application/pdf");
        }




        public ActionResult CreateCase()
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
            return PartialView("CreateCase", new Cases(LoginUser.UserId,LoginUser.GroupType));

            //block of code


        }


        [HttpPost]
        public ActionResult CreateCase(Cases cases)
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
            //int procedureID = 0;
            //int medicalScheduleID = 0;
            //int medicalTestID = 0;


            Cases csInfo = new Cases();
            Services3 s3 = new Services3();
            MedicalInformation MedicInfo = new MedicalInformation();
            MedicInfo = s3.GetMedicalInfo(LoginUser.UserId);

            string a = "cf";
            //string s = Convert.ToDateTime(DateTime.Now).ToShortDateString();
            //string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
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
            csInfo.CreatedBy = LoginUser.UserId;
            //csInfo.ModifiedBy = LoginUser.UserId;

            MedicInfo.BMIHeight = cases.BMIHeight;
            MedicInfo.BMIWeight = cases.BMIWeight;
            MedicInfo.UpdatedBy = LoginUser.UserId;
            MedicInfo.UpdatedOn = DateTime.Now;

            string caseId = CreateUserCase.CreateCase(csInfo, LoginUser.GroupType);
            int result;
            if (!String.IsNullOrEmpty(cases.BMIHeight.ToString()) && !String.IsNullOrEmpty(cases.BMIWeight.ToString()))
                result = s3.UpdateMedicalInfo(MedicInfo, LoginUser.UserId.ToString(), LoginUser.DomainId.ToString(), "PInfo3", LoginUser.GroupType);

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
                    //ctl.CaseId = caseId;
                    //ctl.LogId = v;
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



        [HttpPost]
        public ActionResult CreateNewProcedure(Cases cs)
        {


            string ProcedureId = string.Empty;
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
            //foreach (Procedure proc in cs.lstProcedure)
            //{
            Procedure p1 = null;
            p1 = new Procedure();
            p1.CaseId = cs.CaseId;
            p1.DateOfService = cs.Procedures.DateOfService;
            p1.Notes = cs.Procedures.Notes;
            p1.ProcedureName = cs.Procedures.ProcedureName;
            p1.ProviderOrFacility = cs.Procedures.ProviderOrFacility;
            p1.Surgeon = cs.Procedures.Surgeon;

            ProcedureId = CreateProcedure.InsertProcedureInfo(p1);
            // }
            string[] str = new string[cs.procfiles.Count()];
            int i = 0, k;
            Documents doc = null;
            if (cs.procfiles != null)
            {
                foreach (HttpPostedFileBase file in cs.procfiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        doc = new Documents();
                        string srcFilePath = file.FileName;
                        Stream inputStream = file.InputStream;
                        doc.Name = Path.GetFileName(file.FileName);
                        int length = file.ContentLength;
                        byte[] arraybyt = new byte[length];
                        inputStream.Read(arraybyt, 0, length);
                        doc.yData = arraybyt;

                        doc.UserEmail = LoginUser.EmailId;

                        doc.DocSource = "1";
                        doc.CreatedBy = LoginUser.UserId;
                        doc.CaseId = cs.CaseId;
                        doc.CreatedOn = DateTime.Now;
                        //doc.MedicalScheduleTestId = 1;
                        //doc.MedicalLabTestId = 4;
                        doc.ProcedureId = ProcedureId;

                        doc.DomainID = LoginUser.DomainId;

                        str[i] = UploadFiles.UploadFile(doc);

                        //if (UploadFiles.UploadFile(doc) == "Upload Failed")
                        //    k = i;
                        i++;
                    }
                }
                i = 0;
            }

            JsonResult js = new JsonResult();
            js.Data = ProcedureId;
            return js;
        }


        [HttpPost]
        public ActionResult CreateNewMedicalTest(Cases cm)
        {
            //UserInformation LoginUser;
            //if (Session["CurrentUser"] != null)
            //{
            //    LoginUser = Session["CurrentUser"] as UserInformation;
            //}
            //else
            //{
            //    LoginUser = Session["LoginUser"] as UserInformation;
            //}
            //cases.MedicalTests.CaseId = 150;
            //cases.MedicalTests.DomainId = LoginUser.DomainId;
            //cases.MedicalTests.CaseId = LoginUser.UserId;
            //cases.MedicalTests.DateOfTest = DateTime.Now.ToUniversalTime();
            //int i = CreateMedicalTest.InsertMeidcalTestInfo(cases.MedicalTests);

            //return new JsonResult { Data = i };

            string medicaltestId = string.Empty;
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
            //foreach (MedicalTests mt in cm.MedicalTests)
            //{
            MedicalTests m1 = null;
            m1 = new MedicalTests();
            m1.CaseId = cm.CaseId;
            m1.TestName = cm.MedicalTests.TestName;
            m1.DateOfTest = cm.MedicalTests.DateOfTest;
            medicaltestId = CreateMedicalTest.InsertMeidcalTestInfo(m1);
            //}
            string[] str = new string[cm.MTfiles.Count()];
            int i = 0, k;
            Documents doc = null;
            if (cm.MTfiles != null)
            {
                foreach (HttpPostedFileBase file in cm.MTfiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        doc = new Documents();
                        string srcFilePath = file.FileName;
                        Stream inputStream = file.InputStream;
                        doc.Name = Path.GetFileName(file.FileName);
                        int length = file.ContentLength;
                        byte[] arraybyt = new byte[length];
                        inputStream.Read(arraybyt, 0, length);
                        doc.yData = arraybyt;

                        doc.UserEmail = LoginUser.EmailId;

                        doc.DocSource = "1";
                        doc.CreatedBy = LoginUser.UserId;
                        doc.CaseId = cm.CaseId;
                        doc.CreatedOn = DateTime.Now;

                        doc.MedicalLabTestId = medicaltestId;

                        doc.DomainID = LoginUser.DomainId;

                        str[i] = UploadFiles.UploadFile(doc);

                        //if (UploadFiles.UploadFile(doc) == "Upload Failed")
                        //    k = i;
                        i++;
                    }

                } i = 0;

            }

            JsonResult js = new JsonResult();
            js.Data = medicaltestId;
            return js;
        }

        [HttpPost]
        public ActionResult CreateNewMedicalSchedule(Cases cms)
        {
            string medicalScheduleID = string.Empty;
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

            MedicalSchedule ms = new MedicalSchedule();
            //foreach (MedicalSchedule ms1 in cms.lstMedicalSchedule)
            //{
            ms.CaseId = cms.CaseId;
            ms.StartDate = DateTime.Now;
            ms.EndDate = DateTime.Now;

            ms.IsIndexed = true;
            ms.Notify = cms.MedicalSchdule.Notify; //true;
            ms.Schedule = XmlStringListSerializer.ToXmlString<IList<Schedule>>(cms.ScheduleInfo);

            ms.DomainId = LoginUser.DomainId;

            medicalScheduleID = CreateMedicalSchedule.InsertMeidcalScheduleInfo(ms);
            //}
            string[] str = new string[cms.msfiles.Count()];
            int i = 0, k;

            Documents doc = null;
            if (cms.msfiles != null)
            {
                foreach (HttpPostedFileBase file in cms.msfiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        doc = new Documents();
                        string srcFilePath = file.FileName;
                        Stream inputStream = file.InputStream;
                        doc.Name = Path.GetFileName(file.FileName);
                        int length = file.ContentLength;
                        byte[] arraybyt = new byte[length];
                        inputStream.Read(arraybyt, 0, length);
                        doc.yData = arraybyt;

                        doc.UserEmail = LoginUser.EmailId;

                        doc.DocSource = "1";
                        doc.CreatedBy = LoginUser.UserId;
                        doc.CaseId = cms.CaseId;
                        doc.CreatedOn = DateTime.Now;

                        doc.MedicalScheduleTestId = medicalScheduleID;

                        doc.DomainID = LoginUser.DomainId;

                        str[i] = UploadFiles.UploadFile(doc);
                        i++;
                    }

                } i = 0;
            }

            JsonResult js = new JsonResult();
            js.Data = medicalScheduleID;
            return js;

        }


        [HttpPost]
        public ActionResult CreateNewVisit(Cases cv)
        {
            string vtinsertCode = string.Empty;
            //Visits
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

            Visits vt = new Visits();
            foreach (Visits vt1 in cv.lstVisits)
            {

                vt.Reason = vt1.Reason;

                vt.IsNotify = vt1.IsNotify;
                vt.DomainId = LoginUser.DomainId;
                vt.CaseId = cv.CaseId;
                vt.IsIndexed = true;
                vt.Ihealthuserid = LoginUser.UserId;
                vtinsertCode = CreateVisit.CreateVisitInfo(vt);
            }

            JsonResult js = new JsonResult();
            js.Data = vtinsertCode;
            return js;
        }
        // for EditCaseView
        public ActionResult EditViewCase(string CaseId)
        {
            Getdocuments _objdocuments = new Getdocuments();
            GetUserCases _getUserCase;
            _getUserCase = new GetUserCases();
            Cases _cases = _getUserCase.GetCaseByID(CaseId);
            _cases.lstVisits = GetVisits.GetAllVisitsByCaseId(CaseId);
            _cases.lstMedicalTest = GetMedicalTests.GetAllMedicalTestsByCaseId(CaseId);
            _cases.lstProcedure = GetProcedures.GetAllProceduresByCaseId(CaseId);
            _cases.lstMedicalSchedule = GetMedicalSchedules.GetAllMeicalschdulesbyCaseId(CaseId);
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
            return PartialView("EditViewCase", _cases);

        }

        //AD Code for Delete starts here
        public ActionResult DeleteMTest(string id)
        {
            GetMedicalTests _getMtest;
            _getMtest = new GetMedicalTests();
            string Sucesscode = _getMtest.DeleteMTestbyMtestId(id);
            JsonResult r = new JsonResult();
            if (Sucesscode == "1030")
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
        public ActionResult DeleteProcedure(string id)
        {
            GetProcedures _getProc;
            _getProc = new GetProcedures();
            string Sucesscode = _getProc.DeleteProcedure(id);
            // _objMtest = _getMtest.GetMedicalTestsbyMtestId(id);
            JsonResult r = new JsonResult();

            if (Sucesscode == "1030")
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
        public ActionResult DeleteVisits(string id)
        {
            GetVisits _getVisits;
            _getVisits = new GetVisits();
            string Sucesscode = _getVisits.DeleteVisitsById(id);
            // _objMtest = _getMtest.GetMedicalTestsbyMtestId(id);
            JsonResult r = new JsonResult();

            if (Sucesscode == "1030")
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
        public ActionResult DeleteMedSchedule(string id)
        {
            GetMedicalSchedules _getMedSch;
            _getMedSch = new GetMedicalSchedules();
            string Sucesscode = _getMedSch.DeleteMedSchById(id);
            // _objMtest = _getMtest.GetMedicalTestsbyMtestId(id);
            JsonResult r = new JsonResult();

            if (Sucesscode == "1030")
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
        //AD Code for Delete ends here


        //Added By V
        [HttpGet]
        public ActionResult GetProcedure(string procId)
        {
            Procedure p = GetProcedures.GetProcedureById(procId);
            return new JsonResult { Data = p, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        [HttpGet]
        public ActionResult GetMedicalTest(string mtId)
        {
            MedicalTests m = GetMedicalTests.GetMedicalTestById(mtId);

            return new JsonResult { Data = m, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //End

        [HttpPost]
        public ActionResult UpdateProcedure(Cases cs)
        {
            int result = 0;
            string procedureId = string.Empty;
            string caseId = string.Empty;
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
            foreach (Procedure proc in cs.lstProcedure)
            {
                Procedure p1 = null;
                p1 = new Procedure();
                p1.ProcedureId = proc.ProcedureId;
                procedureId = proc.ProcedureId;
                caseId = proc.CaseId;
                p1.CaseId = cs.CaseId;
                p1.CaseId = proc.CaseId;
                p1.DateOfService = proc.DateOfService;

                p1.Notes = proc.Notes;
                p1.ProcedureName = proc.ProcedureName;
                p1.ProviderOrFacility = proc.ProviderOrFacility;
                p1.Surgeon = proc.Surgeon;
                EditProcedure _getUserCase = null;
                _getUserCase = new EditProcedure();
                // result = _getUserCase.UpdateProcedure(p1);
            }
            string[] str = new string[cs.procfiles.Count()];
            int i = 0, k;
            Documents doc = null;
            if (cs.procfiles != null)
            {
                foreach (HttpPostedFileBase file in cs.procfiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        doc = new Documents();
                        string srcFilePath = file.FileName;
                        Stream inputStream = file.InputStream;
                        doc.Name = Path.GetFileName(file.FileName);
                        int length = file.ContentLength;
                        byte[] arraybyt = new byte[length];
                        inputStream.Read(arraybyt, 0, length);
                        doc.yData = arraybyt;

                        doc.UserEmail = LoginUser.EmailId;

                        doc.DocSource = "1";
                        doc.CreatedBy = LoginUser.UserId;
                        doc.CaseId = caseId;
                        doc.CreatedOn = DateTime.Now;
                        //doc.MedicalScheduleTestId = 1;
                        //doc.MedicalLabTestId = 4;
                        doc.ProcedureId = procedureId;

                        doc.DomainID = LoginUser.DomainId;

                        str[i] = UploadFiles.UploadFile(doc);

                        //if (UploadFiles.UploadFile(doc) == "Upload Failed")
                        // k = i;
                        i++;
                    }
                }
                i = 0;
            }
            JsonResult js = new JsonResult();
            js.Data = result;
            return js;

        }

        [HttpPost]
        public ActionResult UpdateMedicalTest(Cases cs)
        {
            int result = 0;
            string medicalTestId = string.Empty;
            string caseId = string.Empty;
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
            foreach (MedicalTests mt in cs.lstMedicalTest)
            {
                MedicalTests p1 = null;
                p1 = new MedicalTests();
                p1.MedicalTestId = mt.MedicalTestId;
                p1.CaseId = cs.CaseId;
                p1.CaseId = mt.CaseId;
                p1.DateOfTest = mt.DateOfTest;
                p1.TestName = mt.TestName;

                EditMedicalTest _getUserCase = null;
                _getUserCase = new EditMedicalTest();
                //result = _getUserCase.UpdateMedicalTest(p1);
            }
            string[] str = new string[cs.MTfiles.Count()];
            int i = 0, k;
            Documents doc = null;
            if (cs.MTfiles != null)
            {
                foreach (HttpPostedFileBase file in cs.MTfiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        doc = new Documents();
                        string srcFilePath = file.FileName;
                        Stream inputStream = file.InputStream;
                        doc.Name = Path.GetFileName(file.FileName);
                        int length = file.ContentLength;
                        byte[] arraybyt = new byte[length];
                        inputStream.Read(arraybyt, 0, length);
                        doc.yData = arraybyt;

                        doc.UserEmail = LoginUser.EmailId;

                        doc.DocSource = "1";
                        doc.CreatedBy = LoginUser.UserId;
                        doc.CaseId = cs.CaseId;
                        doc.CreatedOn = DateTime.Now;
                        //doc.MedicalScheduleTestId = 1;
                        //doc.MedicalLabTestId = 4;
                        doc.ProcedureId = medicalTestId;

                        doc.DomainID = LoginUser.DomainId;

                        str[i] = UploadFiles.UploadFile(doc);

                        //if (UploadFiles.UploadFile(doc) == "Upload Failed")
                        // k = i;
                        i++;
                    }
                }
                i = 0;
            }
            JsonResult js = new JsonResult();
            js.Data = result;
            return js;

        }




        //by kumar
        [HttpPost]
        public ActionResult UpDateCaseInfo(Cases cs)
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
            string a = "cf";
            //string s = Convert.ToDateTime(DateTime.Now).ToShortDateString();
            //string strDate = Convert.ToDateTime(s).ToString("ddMMyyyy");
            string strDate = DateTime.Now.ToString("ddMMyyyyhhmm");
            string retCfName = a + strDate;
            csInfo.CaseId = cs.CaseId;
            csInfo.CaseName = cs.CaseName;
            csInfo.CaseDescription = cs.CaseDescription;
            csInfo.CfId = retCfName;

            csInfo.HospitalInfo = XmlStringListSerializer.ToXmlString<IList<Hospital>>(cs.HospitalInfos);
            csInfo.PatientType = cs.PatientType;

            csInfo.CustHealthlogDesc = cs.CustHealthlogDesc;

            csInfo.TypeOfProblem = "SomeProblem";

            csInfo.DomainId = LoginUser.DomainId;
            csInfo.ModifiedOn = DateTime.Now;
            csInfo.ModifiedBy = LoginUser.UserId;
            csInfo.CreatedBy = LoginUser.UserId;
            csInfo.CreatedOn = DateTime.Now;


            //int caseId = Convert.ToInt32(CreateUserCase.CreateCase(csInfo));
            // int caseId=Convert.ToInt32(EditUserCases.UpdateCase(csInfo));
            string caseId = EditUserCases.UpdateCase(csInfo, LoginUser.GroupType);
            //if (cs.s1 != null)
            //{
            //    CaseToCase ctc = null;
            //    ctc = new CaseToCase();
            //    foreach (var v in cs.s1)
            //    {
            //        ctc.CaseId = cs.CaseId;
            //        ctc.RelatedCaseId = v;
            //        int InsertCasetoCase = CreateCaseToCase.InsertCaseToCaseInfo(ctc);
            //    }
            //}
            cs.CaseId = caseId;
            JsonResult r = new JsonResult();
            if (caseId == "1020")
            {
                r.Data = caseId;
                return r;
            }
            else
            {
                r.Data = "Failed";
                return r;
            }
            //JsonResult js = new JsonResult();
            //js.Data = caseId;
            //return js;
            //return RedirectToAction("MyCase");
        }
        //kumar

        //Ad COde
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
                // LoginUser = new UserInformation();
                LoginUser = Session["LoginUser"] as UserInformation;
            }
            //return RedirectToAction("MyCase");// View("MyCase", GetUserCases.GetAllCases(LoginUser.UserId).ToList());

            JsonResult r = new JsonResult();
            if (Sucesscode == "1030")
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

        //End


        //By Sandeep

        [HttpPost]
        public ActionResult AddVisits(Cases cv)
        {

            //Visits
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

            //cv.Visits.CaseId=cv.CaseId;
            string result = CreateVisit.CreateVisitInfo(cv.Visits);

            JsonResult js = new JsonResult();
            js.Data = result;
            return js;
        }

        [HttpPost]
        public ActionResult AddProcedure(Cases cv)
        {

            //Visits
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

            //cv.Visits.CaseId=cv.CaseId;
            string result = CreateProcedure.InsertProcedureInfo(cv.Procedures);

            JsonResult js = new JsonResult();
            js.Data = result;
            return js;
        }

        [HttpPost]
        public ActionResult AddMedicalTest(Cases cv)
        {
            //MedicalTEst
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

            //cv.Visits.CaseId=cv.CaseId;
            string result = CreateMedicalTest.InsertMeidcalTestInfo(cv.MedicalTests);

            JsonResult js = new JsonResult();
            js.Data = result;
            return js;
        }

        //[HttpPost]
        public ActionResult AddMedicalSchduleTest(Cases cms)
        {
            string medicalScheduleID = string.Empty;
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

            //MedicalSchedule ms = new MedicalSchedule();
            //foreach (MedicalSchedule ms1 in cms.lstMedicalSchedule)
            //{
            //    ms.CaseId = cms.CaseId;
            //    ms.DatePrescribed = DateTime.Now;
            //    ms.Dispensed = DateTime.Now;
            //    ms.DoctorName = ms1.DoctorName; //"Dileep";
            //    ms.IsIndexed = true;
            //    ms.SendNotification = ms1.SendNotification; //true;
            cms.MedicalSchdule.Schedule = XmlStringListSerializer.ToXmlString<IList<Schedule>>(cms.ScheduleInfo);

            //    ms.Reason = ms1.Reason;//"badHealth";
            //    ms.DomainId = LoginUser.DomainId;

            //    medicalScheduleID = CreateUserCase.InsertMeidcalScheduleInfo(ms);
            //}

            //Documents doc = null;
            //if (cms.msfiles != null)
            //{
            //    foreach (HttpPostedFileBase file in cms.msfiles)
            //    {
            //        doc = new Documents();
            //        string srcFilePath = file.FileName;
            //        Stream inputStream = file.InputStream;
            //        doc.Name = Path.GetFileName(file.FileName);
            //        int length = file.ContentLength;
            //        byte[] arraybyt = new byte[length];
            //        inputStream.Read(arraybyt, 0, length);
            //        doc.yData = arraybyt;

            //        doc.UserEmail = LoginUser.EmailId;

            //        doc.DocSource = "1";
            //        doc.CreatedBy = LoginUser.UserId;
            //        doc.CaseId = cms.CaseId;
            //        doc.CreatedOn = DateTime.Now;

            //        doc.MedicalScheduleTestId = medicalScheduleID;

            //        doc.DomainID = LoginUser.DomainId;

            //        string str = UploadFiles.UploadFile(doc);

            //    }
            //}
            medicalScheduleID = CreateMedicalSchedule.InsertMeidcalScheduleInfo(cms.MedicalSchdule);
            JsonResult js = new JsonResult();
            js.Data = medicalScheduleID;
            return js;


        }

        //End

        //By CHP

        [HttpPost]
        public ActionResult UpdateMedicalSchedule(Cases cs)
        {
            int result = 0;
            string mScheduleId = string.Empty;
            string CaseId = string.Empty;
            //int MedicalScheduleId = 0;
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
            foreach (MedicalSchedule ms in cs.lstMedicalSchedule)
            {
                MedicalSchedule MS = null;
                MS = new MedicalSchedule();
                mScheduleId = ms.MedicalScheduleId;
                CaseId = ms.MedicalScheduleId;

                MS.CaseId = ms.CaseId;
                //p1.DateOfService = proc.DateOfService;
                //p1.Notes = proc.Notes;
                //p1.ProcedureName = proc.ProcedureName;
                //p1.ProviderOrFacility = proc.ProviderOrFacility;
                //p1.Surgeon = proc.Surgeon;
                MS.MedicalScheduleId = ms.MedicalScheduleId;
                MS.StartDate = ms.StartDate;
                MS.EndDate = ms.EndDate;

                MS.PharmacyName = ms.PharmacyName;
                EditMedicalSchedule _getMedicalSchedule = null;
                _getMedicalSchedule = new EditMedicalSchedule();
                // result = _getMedicalSchedule.UpdateMedicalSchedule(MS);
            }
            string[] str = new string[cs.msfiles.Count()];
            int i = 0, k;
            Documents doc = null;
            if (cs.procfiles != null)
            {
                foreach (HttpPostedFileBase file in cs.procfiles)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        doc = new Documents();
                        string srcFilePath = file.FileName;
                        Stream inputStream = file.InputStream;
                        doc.Name = Path.GetFileName(file.FileName);
                        int length = file.ContentLength;
                        byte[] arraybyt = new byte[length];
                        inputStream.Read(arraybyt, 0, length);
                        doc.yData = arraybyt;

                        doc.UserEmail = LoginUser.EmailId;

                        doc.DocSource = "1";
                        doc.CreatedBy = LoginUser.UserId;
                        doc.CaseId = CaseId;
                        doc.CreatedOn = DateTime.Now;
                        //doc.MedicalScheduleTestId = 1;
                        //doc.MedicalLabTestId = 4;
                        doc.MedicalScheduleTestId = mScheduleId;

                        doc.DomainID = LoginUser.DomainId;

                        str[i] = UploadFiles.UploadFile(doc);

                        //if (UploadFiles.UploadFile(doc) == "Upload Failed")
                        // k = i;
                        i++;
                    }
                }
                i = 0;
            }
            JsonResult js = new JsonResult();
            js.Data = result;
            return js;

        }

        [HttpPost]
        public ActionResult UpdateVisit(Cases cs)
        {
            // int VisitId = 0;
            int result = 0;
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
            foreach (Visits visits in cs.lstVisits)
            {
                Visits v1 = null;
                v1 = new Visits();
                //p1.CaseId = cs.CaseId;
                //p1.DateOfService = proc.DateOfService;
                //p1.Notes = proc.Notes;
                //p1.ProcedureName = proc.ProcedureName;
                //p1.ProviderOrFacility = proc.ProviderOrFacility;
                //p1.Surgeon = proc.Surgeon;
                v1.CaseId = cs.CaseId;
                //v1.VisitDate = visits.VisitDate;
                v1.visitId = visits.visitId;
                v1.Reason = visits.Reason;
                //v1.ProviderFacility = visits.ProviderFacility;
                v1.IsNotify = visits.IsNotify;

                EditVisits _getEditVisit = null;
                _getEditVisit = new EditVisits();
                //result = _getEditVisit.UpdateVisit(v1);
            }
            //string[] str = new string[cs.procfiles.Count()];
            //int i = 0, k;
            //Documents doc = null;
            //if (cs.procfiles != null)
            //{
            // foreach (HttpPostedFileBase file in cs.procfiles)
            // {
            // if (file != null && file.ContentLength > 0)
            // {
            // doc = new Documents();
            // string srcFilePath = file.FileName;
            // Stream inputStream = file.InputStream;
            // doc.Name = Path.GetFileName(file.FileName);
            // int length = file.ContentLength;
            // byte[] arraybyt = new byte[length];
            // inputStream.Read(arraybyt, 0, length);
            // doc.yData = arraybyt;

            // doc.UserEmail = LoginUser.EmailId;

            // doc.DocSource = "1";
            // doc.CreatedBy = LoginUser.UserId;
            // doc.CaseId = cs.CaseId;
            // doc.CreatedOn = DateTime.Now;
            // //doc.MedicalScheduleTestId = 1;
            // //doc.MedicalLabTestId = 4;
            // doc.ProcedureId = ProcedureId;

            // doc.DomainID = LoginUser.DomainId;

            // str[i] = UploadFiles.UploadFile(doc);

            // //if (UploadFiles.UploadFile(doc) == "Upload Failed")
            // // k = i;
            // i++;
            // }
            // }
            // i = 0;
            //}
            JsonResult js = new JsonResult();
            js.Data = result;
            return js;

        }

        [HttpGet]
        public ActionResult GetMedicationSchedule(string mSheduleId)
        {
            MedicalSchedule ms = GetMedicalSchedules.GetMedicationScheduleById(mSheduleId);
            return new JsonResult { Data = ms, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpGet]
        public ActionResult GetMyVisits(string visitsId)
        {
            Visits v = GetVisits.GetVisitsById(visitsId);
            return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }




        //


    }
}
