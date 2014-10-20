using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class UserHealthLog
    {
        public string logId { get; set; }
        public IList<UserSymptom> symptoms { get; set; }
        public string domainId { get; set; }
        [Required(ErrorMessage = "Logname is required")]
        public string logName { get; set; }
        public string logDescription { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string ihealthuserId { get; set; }
        public DateTime? CreatedOn { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Date  is required")]
        public DateTime? FirstObserved { get; set; } // sandeep added
        public IList<UserHealthLog> lstLogs { get; set; }
        public IList<LogToLog> lstlogtolog { get; set; }
        public IList<ResultList> lstresultLogToLoglist { get; set; }
        public IList<UserHealthLog> lstHealthlog { get; set; }
        public List<string> s3 { get; set; }

    }
}