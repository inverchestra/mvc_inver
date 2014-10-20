using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    [Serializable]
    public class Cbp
    {
        public string Hemoglobin { get; set; }
        public string RedBloodCells { get; set; }
        public string PCV { get; set; }
        public string MCV { get; set; }
        public string MCH { get; set; }
        public string MCHC { get; set; }
        public string PlateletCount { get; set; }
        public string WbcCount { get; set; }
        public string Bands { get; set; }
        public string Neutrophils { get; set; }
        public string Lymphocytes { get; set; }
        public string Monocytes { get; set; }
        public string Eosinophils { get; set; }
        public string RDW { get; set; }
        public string BloodGroup { get; set; }
        public string Rhtype { get; set; }
        public string Patholgist { get; set; }
    }
}