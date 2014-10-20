using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class SerumTest
    {
        public string SerumId { get; set; }
        public string PatientName { get; set; }
        public DateTime? TestOnDate { get; set; }
        public string ReffDoctor { get; set; }
        public string Thyroidstimulatinghormone { get; set; }
        public string Pathologist { get; set; }
        public string UserId { get; set; }
        public string CreatedOn { get; set; }
        // sandeep added start here on 3-4-2014

        public string TestOnDate1
        {
            get;
            set;
        }

        // sandeep added end here on 3-4-2014


    }
}