using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Visits
    {
        public string visitId { get; set; }
        public DateTime? CurrentVisitDate { get; set; }
        public DateTime? NextVisitDate { get; set; }
        public string Reason { get; set; }
        public string DoctorName { get; set; }
        public string DoctorEmail { get; set; }
        public long? DoctorPhoneNo { get; set; }
        public string DoctorsLog { get; set; }
        public string Precautions { get; set; }
        public string DietPresc { get; set; }
        public string NurseReadings { get; set; }
        public bool IsNotify { get; set; }
        public bool IsIndexed { get; set; }
        public string CaseId { get; set; }
        public string DomainId { get; set; }
        public string Ihealthuserid { get; set; }
        public string CVisitDate1
        {
            get
            {
                if (CurrentVisitDate != null)
                    return CurrentVisitDate.Value.ToShortDateString();
                return null;
            }
            set { }
        }
        public string NVisitDate1
        {
            get
            {
                if (NextVisitDate != null)
                    return NextVisitDate.Value.ToShortDateString();
                return null;
            }
            set { }
        }
        public Doctor DoctorInfos { get; set; }
        public NurseReadings NurseReadingsInfo { get; set; }

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