using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Charts
    {
        public string ChartId { get; set; }
        public string ChartName { get; set; }
        public string ChartType { get; set; }
        public DateTime? ChartDate { get; set; }
        public string ChartDetails { get; set; }
        public string Comments { get; set; }
        public bool IsIndexed { get; set; }
        public string CaseId { get; set; }
        public string DomainId { get; set; }
        public string iHealthUserId { get; set; }
        public string Date { get; set; }

        public IList<Documents> lstDocuments { get; set; }
        public string DateOfChart1 { get { return ChartDate.Value.ToShortDateString(); } set { } }

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