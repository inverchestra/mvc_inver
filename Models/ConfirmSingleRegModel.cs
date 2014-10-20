using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.IO;
using System.Configuration;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class ConfirmSingleRegModel
    {
        public string RUserId { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string RUserName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required(ErrorMessage = "Pincode is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid pincode")]        
        [RegularExpression(@"(\d{6})|(\d{5})", ErrorMessage = "Invalid Pincode (Please enter valid pincode)")]
        public int Pincode { get; set; }

        public DateTime? CreatedOn { get; set; }


        [Required]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Enter a valid Date")]
        public DateTime? DOB { get; set; }

        public string Address { get; set; }

        [Required]
        public bool IsGroupUser { get; set; }

        public string PCodeUserGroup { get; set; }

        public string UserType { get; set; }
        public string FirstName { get; set; }

        public DateTime? ExpireOn { get; set; }
        public bool IsVerified { get; set; }
        public bool IsFbVerified { get; set; } //dileep
        public string PromoCodeValue { get; set; }

        public bool SelectPromotionalCode { get; set; }     // added by prashanth
        public string CustomerId { get; set; }      // aded by prashanth


        //public bool TryIt { get; set; }

        public string FreeUser { get; set; }

        public string UserPlan { get; set; }

        public string SubscriptionType { get; set; }
        /* Hospital(venkateswari) */
        private string groupType;

        public string Notificationtime { get; set; }
        public string Timezone { get; set; }

        public string GroupType
        {
            get { return groupType; }
            set { groupType = value; }
        }
        /* Hospital(venkateswari) */

        //ck added
        [Required(ErrorMessage = "Hospital name is required")]
        public string Hospitalname { get; set; }
        [Required(ErrorMessage = "Mainbranch name is required")]
        public string MainBranch { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string HospAddress { get; set; }
        public string HospitalID { get; set; }
        //ck ended

        public bool Gestination { get; set; }
        public bool Hypertension { get; set; }
        public bool Latepregnancy { get; set; }
        public bool Earlypregnancy { get; set; }
        public bool Thyroid { get; set; }
        public bool Diabetes { get; set; }
        public bool Twins { get; set; }
        public bool Tripplets { get; set; }
        public bool Asthma { get; set; }

        public bool Outdoorsports { get; set; }

        public bool Swimming { get; set; }
        public bool Yoga { get; set; }
        public bool Adventuresports { get; set; }
        public bool Gardening { get; set; }
        public bool Animalcare { get; set; }
        public bool Pets { get; set; }
        public bool Arts { get; set; }
        public bool Modeling { get; set; }
        public bool Interiordesigning { get; set; }
        public bool Travelling { get; set; }

        public string interests { get; set; }
        public string filtertext { get; set; }
        public bool Privacy { get; set; }

      //  [Required(ErrorMessage="Method is required")]
        public string duemethod { get; set; }
        [Required(ErrorMessage = "Date  is required")]
        [DataType(DataType.Date, ErrorMessage = "Enter a valid Date")]
        public DateTime? duedate { get; set; } 

        public List<SelectListItem> CountryList { get; set; }

        public List<SelectListItem> Countries()
        {
            CountryList = new List<SelectListItem>();
            /* static path*/
              string path = ConfigurationManager.AppSettings["CountryList"];
          //  string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Countrylist/countrylist.txt");
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


      //  [Required(ErrorMessage="please select one option")]
        public List<SelectListItem> duedateList { get; set; }
        public List<SelectListItem> duedatetypes()
        {
            duedateList = new List<SelectListItem>();
           // duedateList.Add(new SelectListItem() { Text = "Please Select", Value = "Please Select", Selected = false });
           
            duedateList.Add(new SelectListItem() { Text = "First day of last menstrual period", Value = "First day of last menstrual period", Selected = false });
            duedateList.Add(new SelectListItem() { Text = "Date of conception", Value = "Date of conception", Selected = false });
            duedateList.Add(new SelectListItem() { Text = "I know my due date", Value = "I know my due date", Selected = false });

            return duedateList;
        }
    }
}