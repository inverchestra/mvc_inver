using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
     [Serializable()]
    public class Hospital
    {
        public string HospitalName { get; set; }
        
        public string HospEmail { get; set; }
        public string HospPhno { get; set; }
        public string HospAddress { get; set; }
    }
}