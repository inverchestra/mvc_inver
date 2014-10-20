using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    [Serializable]
    public class NurseReadings
    {

        public float Height { get; set; }
        public float Weight { get; set; }
        public int Temparature { get; set; }
        public string Sistole { get; set; }
        public string Diastole { get; set; }
        public string Respiratoryrate { get; set; }
        public string PulseRate { get; set; }

    }
}