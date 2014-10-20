using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class BloodPicture
    {
        public string BloodPictureId { get; set; }
        public string PatientName { get; set; }
        public DateTime? TestOnDate { get; set; }
        public string ReffDoctor { get; set; }
        public string Cbp { get; set; }
        public string Antihiv { get; set; }
        public string Vdrl { get; set; }
        public string Hepatitiesb { get; set; }
        public string Rbs { get; set; }
        public string UserId { get; set; }
        public string CreatedOn { get; set; }
        public Cbp cbp1 { get; set; }
        public Antihiv Antihiv1 { get; set; }
        public Vdrl Vdrl1 { get; set; }
        public Hepatitisb Hepatitisb { get; set; }
        public Rbs rbs1 { get; set; }
        public float hemo
        {
            get
            {
                string i = String.IsNullOrEmpty(Cbp) ? "" : XmlStringListSerializer.Deserialize<Cbp>(Cbp).Hemoglobin;
                return String.IsNullOrEmpty(i) ? 0 : float.Parse(i);
            }
        }
        public float sugar
        {
            get
            {
                string i = String.IsNullOrEmpty(Rbs) ? "" : XmlStringListSerializer.Deserialize<Rbs>(Rbs).Rbsresult;
                return String.IsNullOrEmpty(i) ? 0 : float.Parse(i);
            }
        }
        public string TestDate
        {
            get
            {
                return TestOnDate.Value.ToShortDateString();
            }
        }
        // sandeep added start here on 3-4-2014
        public string TestOnDate1
        {
            get;
            set;
        }
        // sandeep added end here on 3-4-2014

    }
}