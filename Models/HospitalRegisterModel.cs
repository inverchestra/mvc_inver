using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.IO;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;
using System.Configuration;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class HospitalRegisterModel
    {
        public int RUserId { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string RUserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(15, ErrorMessage = "The {0} must be between {2} to {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-enter password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Should match 'PASSWORD'")]
        public string ConfirmPassword { get; set; }

        //[Required]
        public string Country { get; set; }

        //[Required(ErrorMessage = "Pincode number is required")]
        //[DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Pincode number")]
        //[RegularExpression(@"\d{6}", ErrorMessage = "Invalid Pincode number(should not exceed 6 digits)")]
        public int Pincode { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        public string EmailId { get; set; }

        public DateTime? CreatedOn { get; set; }

        //[Required(ErrorMessage = "Phone number is required")]
        //[DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        //[RegularExpression(@"\d{10}", ErrorMessage = "Invalid phone number(should not exceed 10 digits)")]
        public long? PhoneNumber { get; set; }

        //[Required]
        public string Gender { get; set; }

        //[Required(ErrorMessage = "Date of birth is required")]
        //[DataType(DataType.Date, ErrorMessage = "Enter a valid Date")]
        public DateTime? DOB { get; set; }

        public string Address { get; set; }

        [Required]
        public bool IsGroupUser { get; set; }

        public string PCodeUserGroup { get; set; }

        public string UserType { get; set; }

        public DateTime? ExpireOn { get; set; }


        public string PromoCodeValue { get; set; }

        public bool TryIt { get; set; }

        public bool FreeUser { get; set; }

        public string UserPlan { get; set; }

        public string SubscriptionType { get; set; }
        /* Hospital(venkateswari) */
        private string groupType;

        public string GroupType
        {
            get { return groupType; }
            set { groupType = value; }
        }



        //[Required(ErrorMessage = "Hospital name is required")]
        public string Hospitalname { get; set; }
        //[Required(ErrorMessage = "Mainbranch name is required")]
        public string MainBranch { get; set; }
        //[Required(ErrorMessage = "Address is required")]
        public string HospAddress { get; set; }
        public string HospitalID { get; set; }
        public string DomainId { get; set; }
        public IList<Branch> BranchInfo { get; set; }
        /* Hospital(venkateswari) */

        public List<SelectListItem> CountryList { get; set; }

        public List<SelectListItem> Countries()
        {
            CountryList = new List<SelectListItem>();
            /* static path*/
             string path = ConfigurationManager.AppSettings["CountryList"];
           // string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Countrylist/countrylist.txt");
            // string path = @"C:\InndocsiHealth\Countrylist\countrylist.txt";
            /* static path*/
            List<string> aList = new List<string>();
            using (StreamReader r = new StreamReader(path))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    aList.Add(line);
                }
                foreach (string a in aList)
                {
                    if (a == "India")
                    {
                        CountryList.Add(new SelectListItem() { Text = a, Value = a, Selected = true });

                    }
                    else
                    {

                        CountryList.Add(new SelectListItem() { Text = a, Value = a, Selected = false });
                    }

                }
            }
            return CountryList;
        }


        public List<SelectListItem> StypeList { get; set; }
        public List<SelectListItem> Stypes()
        {
            StypeList = new List<SelectListItem>();
            StypeList.Add(new SelectListItem() { Text = "MedLogx Pro", Value = "MedLogxPro", Selected = false });
            //StypeList.Add(new SelectListItem() { Text = "TrialUser", Value = "TrialUser", Selected = false });
            //StypeList.Add(new SelectListItem() { Text = "PaidUser", Value = "PaidUser", Selected = false });
            return StypeList;
        }


        private string _otaCode;
        public string OTACode
        {
            get { return _otaCode; }
            set { _otaCode = value; }
        }

        private DateTime? _otaExpire;
        public DateTime? OTAExpire
        {
            get { return _otaExpire; }
            set { _otaExpire = value; }
        }
    }
}