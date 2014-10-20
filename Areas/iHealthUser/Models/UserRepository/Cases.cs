using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository;
using System.ComponentModel;
using System.Web.Mvc;
using System.IO;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Cases
    {
        public string CaseId { get; set; }
        public string CaseName { get; set; }
        public string CaseDescription { get; set; }
        public string CfId { get; set; }
        public string HospitalInfo { get; set; }
        public bool PatientType { get; set; }
        public string CustHealthlogDesc { get; set; }
        public string TypeOfProblem { get; set; }

        public string DomainId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string OwnerId { get; set; }
        public string Type { get; set; }
        public string MType { get; set; }

        public MedicalTests MedicalTests { get; set; }
        public Procedure Procedures { get; set; }
        public MedicalSchedule MedicalSchdule { get; set; }
        public Charts Charts { get; set; }

        public IList<Hospital> HospitalInfos { get; set; }
        public IEnumerable<HttpPostedFileBase> procfiles { get; set; }
        public IEnumerable<HttpPostedFileBase> MTfiles { get; set; }
        public IEnumerable<HttpPostedFileBase> msfiles { get; set; }
        public IList<Procedure> lstProcedure { get; set; }
        public IList<MedicalTests> lstMedicalTest { get; set; }
        public IList<MedicalSchedule> lstMedicalSchedule { get; set; }
        public IList<Schedule> ScheduleInfo { get; set; }
        public IList<Visits> lstVisits { get; set; }
        public IList<Charts> lstChart { get; set; }
        public IList<Cases> lstCases { get; set; }
        public IList<UserHealthLog> lstHealthlog { get; set; }
        public IList<CaseToCase> lstcasetocase { get; set; }
        public IList<CaseToLog> lstcasetolog { get; set; }
        public IList<ResultList> lstresultCaselist { get; set; }
        public IList<ResultList> lstresultLoglist { get; set; }
        public string lstCasetext { get; set; }
        public string lstLogtext { get; set; }

        public List<string> s2 { get; set; }
        public List<string> s1 { get; set; }

        public double? BMIHeight { get; set; }
        public double? BMIWeight { get; set; }

        public Visits Visits { get; set; }

        public IList<mSchedule> MSchedule { get; set; }

        public List<SelectListItem> MedType { get; set; }
        public List<SelectListItem> FreqType { get; set; }

        public Cases()
        {
            this.MedType = new List<SelectListItem>();
            this.FreqType = new List<SelectListItem>();
            string path = @"C:\InndocsiHealth\SelectTypes\MedType.txt";
            string path1 = @"C:\InndocsiHealth\SelectTypes\FreqType.txt";
            string value;
            using (StreamReader r = new StreamReader(path))
            {
                while ((value = r.ReadLine()) != null)
                {
                    this.MedType.Add(new SelectListItem() { Text = value, Value = value });
                }
            }

            using (StreamReader r = new StreamReader(path1))
            {
                while ((value = r.ReadLine()) != null)
                {
                    this.FreqType.Add(new SelectListItem() { Text = value, Value = value });
                }
            }
        }
        public Cases(string ID,string GroupType)
        {

            lstCases = GetUserCases.GetAllCases(ID,GroupType);
            lstHealthlog = GetHealthLogs.GetAllHealthLogs(ID,GroupType);//dileep
        }

    }
}