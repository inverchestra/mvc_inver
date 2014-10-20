using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class MedicalSchedule
    {
        //public int MedicalScheduleId { get; set; }
        //public DateTime? DatePrescribed { get; set; }
        //public DateTime? Dispensed { get; set; }
        //public string Pharmacy { get; set; }
        //public bool Notify { get; set; }
        //public string Schedule { get; set; }
        //public bool IsIndexed { get; set; }
        //public int CaseId { get; set; }
        //public int DomainId { get; set; }
        //public IList<Documents> lstDocuments { get; set; }
        //public IList<Schedule> lstSchedule { get; set; }
        //public string DateDispensed1 { get { return Dispensed.Value.ToShortDateString(); } set { } }
        //public string DatePrescribed1 { get { return DatePrescribed.Value.ToShortDateString(); } set { } }
        public string MedicalScheduleId { get; set; }
        [Required(ErrorMessage = "Please Enter Start Date")]
        [DataType(DataType.DateTime, ErrorMessage = "Please Enter Valid Date")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "Please Enter End Date")]
        [DataType(DataType.DateTime, ErrorMessage = "Please Enter Valid Date")]
        public DateTime? EndDate { get; set; }
        public string PharmacyName { get; set; }
        public string PrescriptionNo { get; set; } // sandeep added
        public string PrescriptionDescription { get; set; } // sandeep added
        public bool Notify { get; set; }
        public string Schedule { get; set; }
        public bool IsIndexed { get; set; }
        public string CaseId { get; set; }
        public string DomainId { get; set; }
        public IList<Documents> lstDocuments { get; set; }
        public IList<Schedule> lstSchedule { get; set; }
        public string DateDispensed1 { get { return EndDate.Value.ToShortDateString(); } set { } }
        public string DatePrescribed1 { get { return StartDate.Value.ToShortDateString(); } set { } }

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