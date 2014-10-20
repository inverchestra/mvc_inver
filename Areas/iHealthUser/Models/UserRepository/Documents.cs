using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Documents
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OriginalName { get; set; }

        public string Path { get; set; }

        public string DocSource { get; set; }

        public int PageCount { get; set; }

        public int FileSize { get; set; }

        public string FileType { get; set; }

        public bool IsMetadataIndexed { get; set; }

        public string Metadata { get; set; }

        public bool IsIndexed { get; set; }

        public string Extension { get; set; }

        public bool IsDeleted { get; set; }

        public string CaseId { get; set; }

        public string MedicalLabTestId { get; set; }

        public string ChartId { get; set; }

        public string MedicalScheduleTestId { get; set; }

        public string ProcedureId { get; set; }

        public string CreatedBy { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string DomainID { get; set; }

        public byte[] yData { get; set; }

        public string UserEmail { get; set; }

        public IList<Documents> lstdocprocedures { get; set; }// sandeep added
        public IList<Documents> lstdocmedicaltests { get; set; }
        public IList<Documents> lstdocCharts { get; set; }
        public IList<Documents> lstdocMedications { get; set; }

        public string CaseName { get; set; }
        public string CfId { get; set; }
        public string ProcedureName { get; set; }
        public string MedicalTestName { get; set; }
        public string ChartName { get; set; }
        public string UserID { get; set; }
        public string Type { get; set; }
        public string OwnerId { get; set; }
    }
}