using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    [Serializable]
    public class Schedule
    {
        public string MedicineName { get; set; }
        public string TypeOfMedicine { get; set; }
        public string DosageTaken { get; set; }
        public string FrequencyTaken { get; set; }
        public string TotalQuantity { get; set; }
        public string Starttime { get; set; }
        //public string Type { get; set; } // sandeep commented
        public bool IsNotify { get; set; }
        public string Reason { get; set; } // sandeep added
    }
}