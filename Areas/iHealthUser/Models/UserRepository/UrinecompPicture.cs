using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class UrinecompPicture
    {
        public string UrieneId{get;set;}
        public string PatientName { get; set; }
        public string Reffdoctor { get; set; }
        public DateTime? TestOnDate { get; set; }
        public string UserId { get; set; }
        public string CreatedOn { get; set; }
        public string UrinePicture { get; set; }
        public string UrieneCulture { get; set; }
        public UrienePicture urienepicture1 { get; set; }
        public UrieneCulture urineculture1 { get; set; }
        // sandeep added start here on 3-4-2014
        public string TestOnDate1
        {
            get;
            set;
        }
        // sandeep added end here on 3-4-2014



    }
}