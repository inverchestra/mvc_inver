using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using InnDocs.iHealth.Web.UI.Models;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using System.Configuration;
namespace InnDocs.iHealth.Web.UI.Models
{
    public class LoginAndSignUp
    {
        private RegisteredUser _RegUser;
        public RegisteredUser RegUser
        {
            get { return _RegUser; }
            set { _RegUser = value; }
        }

        private UserInformation _IHealthUser;
        public UserInformation IHealthUser
        {
            get { return _IHealthUser; }
            set { _IHealthUser = value; }
        }
        private SingleRegisterModel _singleRegUser; // sandeep added new code on 18-09-2013
        public SingleRegisterModel SingleRegUser
        {
            get { return _singleRegUser; }
            set { _singleRegUser = value; }
        }

        private IList<UserInformation> _users;

        public IList<UserInformation> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        private PromoCode _promocode;
        public PromoCode PromoCodesInfo
        {
            get { return _promocode; }
            set { _promocode = value; }
        }
        private IList<PromoCode> _lstpromoCodes;

        public IList<PromoCode> ListPromoCodes
        {
            get { return _lstpromoCodes; }
            set { _lstpromoCodes = value; }
        }
        //usha
        private PersonalInformation _pInfo;

        public PersonalInformation PInfo
        {
            get { return _pInfo; }
            set { _pInfo = value; }
        }

        private IList<PersonalInformation> _lstpInfo;

        public IList<PersonalInformation> LstPInfo
        {
            get { return _lstpInfo; }
            set { _lstpInfo = value; }
        }


        //end usha

        // start chp

        private OnlinePayments _op;

        public OnlinePayments OP
        {
            get { return _op; }
            set { _op = value; }
        }

        private IList<OnlinePayments> _lstop;

        public IList<OnlinePayments> LstOP
        {
            get { return _lstop; }
            set { _lstop = value; }
        }

        // end chp



        //By Dileep
        private IList<AuditHistory> _audits;
        public IList<AuditHistory> Audits
        {
            get { return _audits; }
            set { _audits = value; }
        }

        private AuditHistory _aHistory;

        public AuditHistory AHistory
        {
            get { return _aHistory; }
            set { _aHistory = value; }
        }
        //Payment
        private string _x_login;
        public string x_login
        {
            get { return _x_login; }
            set { _x_login = value; }
        }

        private string transactionKey;
        public string Transactionkey
        {
            get { return transactionKey; }
            set { transactionKey = value; }
        }

        private int _x_amount;
        public int x_amount
        {
            get { return _x_amount; }
            set { _x_amount = value; }
        }


        private string _x_description;
        public string x_description
        {
            get { return _x_description; }
            set { _x_description = value; }
        }


        private string _x_invoice_num;
        public string x_invoice_num
        {
            get { return _x_invoice_num; }
            set { _x_invoice_num = value; }
        }


        private string _x_fp_sequence;
        public string x_fp_sequence
        {
            get { return _x_fp_sequence; }
            set { _x_fp_sequence = value; }
        }


        private string _x_fp_timestamp;
        public string x_fp_timestamp
        {
            get { return _x_fp_timestamp; }

            set { _x_fp_timestamp = value; }
        }


        private string _x_fp_hash;
        public string x_fp_hash
        {
            get { return _x_fp_hash; }
            set { _x_fp_hash = value; }
        }


        private string _x_test_request;
        public string x_test_request
        {
            get { return _x_test_request; }
            set { _x_test_request = value; }
        }
        //Payment
        //Dileep End

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

        public string Interest { get; set; }
        public string filtertext { get; set; }
        public bool Privacy { get; set; }

        public string Timezone { get; set; }
        public string Notificationtime { get; set; }

        //Added by AD starts here

        public int GetUsersCount(string DomainId)
        {
            int usersCount = 0;
            string DId = DomainId;



            InnDocs.iHealth.Web.UI.Models.Services1 objSrv = new Services1();
            List<UserInformation> Users = null;
            Users = new List<UserInformation>();
            Users = objSrv.GetAllUsersbyDomainID(DId);
            usersCount = Users.Count;
            return usersCount;
        }
        //Added by AD Ends here
        public List<SelectListItem> CountryList { get; set; }

        public List<SelectListItem> Countries()
        {
            CountryList = new List<SelectListItem>();
            /* static path*/
              string path = ConfigurationManager.AppSettings["CountryList"];
            //string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Countrylist/countrylist.txt");
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

        //Dileep

        public List<SelectListItem> StypeList { get; set; }
        public List<SelectListItem> Stypes()
        {
            StypeList = new List<SelectListItem>();
            StypeList.Add(new SelectListItem() { Text = "MedLogx Pro", Value = "MedLogxPro", Selected = false });
            //StypeList.Add(new SelectListItem() { Text = "TrialUser", Value = "TrialUser", Selected = false });
            //StypeList.Add(new SelectListItem() { Text = "PaidUser", Value = "PaidUser", Selected = false });
            return StypeList;
        }

        public List<SelectListItem> StypeListfrAdmin { get; set; }
        public List<SelectListItem> StypesfrAdmin()
        {
            StypeListfrAdmin = new List<SelectListItem>();
            StypeListfrAdmin.Add(new SelectListItem() { Text = "FreeUser", Value = "FreeUser", Selected = false });
            StypeListfrAdmin.Add(new SelectListItem() { Text = "MedLogx Pro", Value = "MedLogxPro", Selected = false });
            //StypeListfrAdmin.Add(new SelectListItem() { Text = "PaidUser", Value = "PaidUser", Selected = false });
            return StypeListfrAdmin;
        }

        //Dileep

        // sandeep added new property start here on 3-3-2014

        private IList<DoulaUser> _doulaUser;

        public IList<DoulaUser> DoulaUser
        {
            get { return _doulaUser; }
            set { _doulaUser = value; }
        }

        // sandeep added new property end here on 3-3-2014


    }
}