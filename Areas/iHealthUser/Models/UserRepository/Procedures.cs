using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Procedure
    {
        public string ProcedureId { get; set; }
        public string ProcedureName { get; set; }
        public DateTime? DateOfService { get; set; }
        public string ProviderOrFacility { get; set; }
        public string Surgeon { get; set; }
        public string Notes { get; set; }
        public string CaseId { get; set; }
        public string dateofservice1 { get { return DateOfService.Value.ToShortDateString(); } set { } }
        public bool IsNotify { get; set; } // sandeep added
        public DateTime? NotifyDate { get; set; } // sandeep added

        public IList<Documents> lstDocuments { get; set; }//usha
        public string ProcedureDescription { get; set; }//sandeep

        //dileep
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string Type { get; set; }
        public string MType { get; set; }
        public string OwnerId { get; set; }
        public string UserID { get; set; }


        //dileep

    }
}