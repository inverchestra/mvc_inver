using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class MyDocsTree
    {
       public IList<int> lstYears;
       public IList<string> lstMonths { get; set; }
       public IList<Cases> lstCases { get; set; }
       public IList<Procedure> lstProcedure { get; set; }
       public IList<MedicalTests> lstMedTests { get; set; }
       public IList<MedicalSchedule> lstMedSchdules { get; set; }
       public IList<Visits> lstVisits { get; set; }
       public string caseId { get; set; }
       public int year { get; set; }
       public string month { get; set; }
       public Cases objCase { get; set; }
       public Procedure objProcedure { get; set; }
       public MedicalTests objMtest { get; set; }
       public MedicalSchedule objMSchedule { get; set; }
       public Visits objVisits { get; set; }
       public Charts objCharts { get; set; }
       public Documents objDocs { get; set; }  


    }
    //public class months
    //{ 
    //   public int MonthNumber;
    //   public string MonthName;
    //}
    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

}