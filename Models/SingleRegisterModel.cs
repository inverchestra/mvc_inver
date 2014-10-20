using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

namespace InnDocs.iHealth.Web.UI.Models {
    public class SingleRegisterModel {
        public string RUserId { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "User name required.")]
        public string RUserName { get; set; }

        [Required(ErrorMessage = "Password required")]
        [StringLength(15, ErrorMessage = "Between {2} and {1} characters.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Should match the password.")]
        public string ConfirmPassword { get; set; }

        [Compare("Password", ErrorMessage = "Should match the password.")]
        public string password_again { get; set; }

        public string Country { get; set; }

        public int Pincode { get; set; }

        public string IpAddress { get; set; }

        [RegularExpression(@"(\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)|(\d{10})", ErrorMessage = "Valid email or phone no.")]
        [Required(ErrorMessage = "Email or phone required")]
        public string EmailId { get; set; }

        public DateTime? CreatedOn { get; set; }

        private int _result;

        public int Result {
            get { return _result; }
            set { _result = value; }
        }

        public long? PhoneNumber { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of birth required.")]
        [DataType(DataType.Date, ErrorMessage = "Valid Date of birth.")]
        public DateTime? DOB { get; set; }

        public string Address { get; set; }

        [Required]
        public bool IsGroupUser { get; set; }

        public string PCodeUserGroup { get; set; }

        public string UserType { get; set; }
        public string FirstName { get; set; }

        public DateTime? ExpireOn { get; set; }
        public string ExpireOn1 { get; set; }
        public bool IsVerified { get; set; }

        public bool IsPhnVerified { get; set; }

        public string PromoCodeValue { get; set; }

        public string FreeUser { get; set; }

        public string UserPlan { get; set; }

        private int _rCode;
        public int ResultCode {
            get { return _rCode; }
            set { _rCode = value; }
        }
        private string _fbuid;
        public string fbuserid {
            get { return _fbuid; }
            set { _fbuid = value; }
        }

        public string CustomerId { get; set; }
        public bool IsFbVerified { get; set; }

        public string SubscriptionType { get; set; }

        private string groupType;

        public string GroupType {
            get { return groupType; }
            set { groupType = value; }
        }

        public string created { get; set; }
        public List<SelectListItem> CountryList { get; set; }

        public List<SelectListItem> Countries() {
            CountryList = new List<SelectListItem>();
            /* static path*/
             string path = ConfigurationManager.AppSettings["CountryList"];
           //string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Countrylist/countrylist.txt");
            // string path = @"C:\InndocsiHealth\Countrylist\countrylist.txt";
            /* static path*/
            List<string> aList = new List<string>();
            using (StreamReader r = new StreamReader(path)) {
                string line;
                while ((line = r.ReadLine()) != null) {
                    aList.Add(line);
                }
                foreach (string a in aList) {
                    if (a == "India") {
                        CountryList.Add(new SelectListItem() { Text = a, Value = a, Selected = true });

                    } else {

                        CountryList.Add(new SelectListItem() { Text = a, Value = a, Selected = false });
                    }

                }
            }
            return CountryList;
        }


        public List<SelectListItem> StypeList { get; set; }
        public List<SelectListItem> Stypes() {
            StypeList = new List<SelectListItem>();
            StypeList.Add(new SelectListItem() { Text = "MedLogx Pro", Value = "MedLogxPro", Selected = false });
            return StypeList;
        }

        private string _otaCode;
        public string OTACode {
            get { return _otaCode; }
            set { _otaCode = value; }
        }

        private DateTime? _otaExpire;
        public DateTime? OTAExpire {
            get { return _otaExpire; }
            set { _otaExpire = value; }
        }

    }
}