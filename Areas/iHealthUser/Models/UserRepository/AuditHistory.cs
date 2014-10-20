using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class AuditHistory
    {
        public string AuditId { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public string AuditEvent { get; set; }
        public string CaptureMode { get; set; }
        public string UserName { get; set; }
        public DateTime? AuditDate { get; set; }
        public string AuditUserName { get; set; }

        public DateTime? frmDate { get; set; }
        public DateTime? toDate { get; set; }
    }
}