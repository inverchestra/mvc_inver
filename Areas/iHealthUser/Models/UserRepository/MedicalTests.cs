using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class MedicalTests
    {
        public string MedicalTestId { get; set; }
        public string TestName { get; set; }
        public DateTime? DateOfTest { get; set; }
        public bool IsIndexed { get; set; }
        public string CaseId { get; set; }
        public string DomainId { get; set; }
        public string iHealthUserId { get; set; }
        public string Date { get; set; }
        public bool IsNotify { get; set; } // sandeep added
        public DateTime? NotifyDate { get; set; } // sandeep added

        public IList<Documents> lstDocuments { get; set; }//usha
        public string DateOfTest1 { get { return DateOfTest.Value.ToShortDateString(); } set { } }

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