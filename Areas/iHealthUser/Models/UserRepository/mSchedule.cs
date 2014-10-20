using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    [Serializable()]
    public class mSchedule
    {
        public string MedicineName { get; set; }
        public string TypeOfMedicine { get; set; }
        public string DosageQuantity { get; set; }
        public string FrequencyTaken { get; set; }
        public string Quantity { get; set; }
        public int DaysSupply { get; set; }
        public string Notes { get; set; }
    }
}