using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class SearchFields
    {

        public string Log { get; set; }

        public string Symptom { get; set; }

        public string Documents { get; set; }

        public string Cases { get; set; }

        public string UserId { get; set; }

        //ck added
        public string iHealthUserId { get; set; }
        //ck ended

    }
}