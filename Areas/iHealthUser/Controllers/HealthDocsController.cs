using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Web.WebPages.Deployment;
using System.Web.WebPages.Razor;
using System.Xml;
using System.Xml.Linq;
using InnDocs.iHealth.Web.UI.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;


namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers
{
    [SessionExpireFilter]
    public class HealthDocsController : Controller
    {
        //
        // GET: /iHealth1/iHealthUser/HealthDocs/

        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult MyDocs()
        {
            //return View();
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
            MyDocsTree _mydocstree = new MyDocsTree();
            string CreatedDate = LoginUser.CreatedOn.ToString();
            int year = DateTime.Parse(CreatedDate).Year;
            int Currentyear = DateTime.Now.Year;
            IList<int> Year = null;
            Year = new List<int>();
            for (int i = year; i <= Currentyear; i++)
            {
                Year.Add(i);
            }
            _mydocstree.lstYears = Year;
            string[] monthNames = Enum.GetNames(typeof(Month));
            IList<string> _lstmnths = null;
            _lstmnths = new List<string>();
            foreach (string _months in monthNames)
            {
                _lstmnths.Add(_months);
            }
            _mydocstree.lstMonths = _lstmnths;
            _mydocstree.lstCases=GetUserCases.GetAllCases(LoginUser.UserId,LoginUser.GroupType);
           // _mydocstree.lstProcedure = GetProcedures.GetAllProceduresByUserId(LoginUser.UserId);
            return View("MyDocs",_mydocstree);
        }
       
        public ActionResult GetAllValuesOfSingleCase(int SelectedYear,string SelectedMonth, string SelectedCaseId)
        {
            
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
            MyDocsTree _mydocstree = new MyDocsTree();
            string CreatedDate = LoginUser.CreatedOn.ToString();
            int year = DateTime.Parse(CreatedDate).Year;
            int Currentyear = DateTime.Now.Year;
            IList<int> Year=null;
            Year = new List<int>();
            for (int i=year; i <= Currentyear; i++)
            { 
                Year.Add(i);
            }
            _mydocstree.lstYears = Year;
            string[] monthNames = Enum.GetNames(typeof(Month));
             IList<string> _lstmnths=null;
             _lstmnths = new List<string>();
            foreach(string _months in monthNames)
            { 
               _lstmnths.Add(_months);
            }
            _mydocstree.lstMonths = _lstmnths;
            _mydocstree.lstMonths = _lstmnths;
            _mydocstree.year = SelectedYear;
            _mydocstree.month = SelectedMonth;
            Month _month = (Month)Enum.Parse(typeof(Month), SelectedMonth);
            int monthValue = (int)_month;
            _mydocstree.lstCases = GetUserCases.GetCasesByMonth(LoginUser.UserId, SelectedYear, monthValue); 
            _mydocstree.lstProcedure = GetProcedures.GetAllProceduresByCaseId(SelectedCaseId);
            _mydocstree.lstMedTests = GetMedicalTests.GetAllMedicalTestsByCaseId(SelectedCaseId);
            _mydocstree.lstMedSchdules = GetMedicalSchedules.GetAllMeicalschdulesbyCaseId(SelectedCaseId);
            _mydocstree.lstVisits = GetVisits.GetAllVisitsByCaseId(SelectedCaseId);
            _mydocstree.caseId = SelectedCaseId;
            return View("MyDocs", _mydocstree); 
        }

        public ActionResult GetAllCasesbyYearMonthCaseId(string monthv, int yearv )
        {

            string currentpage = "HealthDocs";
            Session["CurrentPage"] = currentpage;
           // Month monthval = monthv;
            UserInformation LoginUser = null;
            if (Session["CurrentUser"] != null)
            {
                LoginUser = Session["CurrentUser"] as UserInformation;
            }
            else
            {

                LoginUser = Session["LoginUser"] as UserInformation;
            }
           
            MyDocsTree _mydocstree = new MyDocsTree();
            string CreatedDate = LoginUser.CreatedOn.ToString();
            int year = DateTime.Parse(CreatedDate).Year;
            int Currentyear = DateTime.Now.Year;
            IList<int> Year = null;
            Year = new List<int>();
            for (int i = year; i <= Currentyear; i++)
            {
                Year.Add(i);
            }
            _mydocstree.lstYears = Year;
            string[] monthNames = Enum.GetNames(typeof(Month));
            IList<string> _lstmnths = null;
            _lstmnths = new List<string>();
            foreach (string _months in monthNames)
            {
                _lstmnths.Add(_months);
            }
            _mydocstree.lstMonths = _lstmnths;
            _mydocstree.year = yearv;
            _mydocstree.month = monthv; 
            Month _month=(Month) Enum.Parse(typeof(Month), monthv);
            int monthValue = (int)_month; 
            _mydocstree.lstCases = GetUserCases.GetCasesByMonth(LoginUser.UserId, yearv, monthValue); 
            return View("MyDocs", _mydocstree);
        }

        public ActionResult GetProcedureDocuments(string pId)
        {
            Procedure p = new Procedure();
            p = GetProcedures.GetProcedureById(pId);
            Getdocuments getDocuments = new Getdocuments();
            IList<Documents> lstdocuments = getDocuments.GetDocumentsbyProcId(pId);
            // return new JsonResult { Data = lstdocuments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return PartialView(lstdocuments);
            //return View("MyDocs", lstdocuments);
            // _cases.lstProcedure = GetProcedures.GetAllProceduresByCaseId(CaseId);
            //foreach (Procedure procedure in _cases.lstProcedure)
            //{
            //    procedure.lstDocuments = _objdocuments.GetDocumentsbyProcId(procedure.ProcedureId);
            //}

            //JsonResult js = new JsonResult();
            //js.Data = lstdocuments.na
            //return js;
        }

        public ActionResult GetSearchedDocs(string searchtext1)
        {

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
            sFileds.Documents = searchtext1;

            sFileds.UserId = LoginUser.UserId;
            IList<Documents> lstDocs = Getdocuments.GetAllSearchedDocs(sFileds);
            IList<MyDocsTree> lstmyDocTree = new List<MyDocsTree>();
            foreach (Documents doc in lstDocs)
            {
                MyDocsTree tree = new MyDocsTree();
                tree.objDocs = doc;
                if (doc.CaseId != null)
                {
                    GetUserCases _getUserCase = new GetUserCases();
                    tree.objCase = _getUserCase.GetCaseByID(doc.CaseId);
                }
                if (doc.ProcedureId !=null)
                {
                    tree.objProcedure = GetProcedures.GetProcedureById(doc.ProcedureId);

                }
                if (doc.MedicalLabTestId != null)
                {
                    tree.objMtest = GetMedicalTests.GetMedicalTestById(doc.MedicalLabTestId);
                }
                if (doc.MedicalScheduleTestId != null)
                {
                    tree.objMSchedule = GetMedicalSchedules.GetMedicationScheduleById(doc.MedicalScheduleTestId);
                }
                if (doc.ChartId != null)
                {
                    tree.objCharts = GetCharts.GetChartById(doc.ChartId);
                }
                lstmyDocTree.Add(tree);

            }
            ViewBag.FromSearch = searchtext1;
            return View("GetSearchedDocs", lstmyDocTree);
        }

        public ActionResult GetMedTestDocuments(string medTestId)
        {
            MyDocsTree myDocsTree = new MyDocsTree();
            Getdocuments getDocuments = new Getdocuments();
            IList<Documents> lstdocuments = getDocuments.GetDocumentsbyMedicalTestId(medTestId);
            // myDocsTree.lstDocuments = lstdocuments;
            return PartialView("GetProcedureDocuments", lstdocuments);
        }

        public ActionResult GetMedScheduleDocuments(string medScheduleId)
        {
            MyDocsTree myDocsTree = new MyDocsTree();
            Getdocuments getDocuments = new Getdocuments();
            IList<Documents> lstdocuments = getDocuments.GetDocumentsbyMedicalScheduleId(medScheduleId);
            // myDocsTree.lstDocuments = lstdocuments;
            return PartialView("GetProcedureDocuments", lstdocuments);
        }



    }

}
