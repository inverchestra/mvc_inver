using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class HospAudit
    {
        public string AuditId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime? AuditDate { get; set; }
        public string AuditUserName { get; set; }
        public string Email { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string DomainId { get; set; }
        public string PatientType { get; set; }
    }
}