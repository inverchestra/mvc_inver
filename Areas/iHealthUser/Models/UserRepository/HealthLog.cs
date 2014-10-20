using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class HealthLog
    {
        public string logId { get; set; }
        public string symptoms { get; set; }
        public string domainId { get; set; }
        public string logName { get; set; }
        public string logDescription { get; set; }
        public DateTime? createdOn { get; set; }
        public DateTime? modifiedOn { get; set; }
        public string ihealthuserID { get; set; }
        public DateTime? FirstObserved { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string UserID { get; set; }
        public string OwnerId { get; set; }
        public string ModifiedBy { get; set; }
        public string MType { get; set; }

    }
}