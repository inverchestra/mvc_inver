using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class Analytic
    {
        public string Ip { get; set; }
        public string AreaAccessed { get; set; }
        public string UserName { get; set; }
        public string Media { get; set; }
        public string Referrer { get; set; }
        public string Browser { get; set; }
        public string Version { get; set; }
        public string UserAgent { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}