using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Models;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Controllers;
using System.ComponentModel.DataAnnotations;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using System.Configuration;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class UserInformation
    {

        private string _hospitalid;
        public string HospitalID
        {
            get { return _hospitalid; }
            set { _hospitalid = value; }
        }
        private string roleName;

        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
        /* Hospital(Venkateswari) */
        private string userid;

        public string UserId
        {
            get { return userid; }
            set { userid = value; }
        }
        private string lName;


        public List<SelectListItem> doctors { get; set; }
        public List<string> Patients { get; set; }


        public string Country { get; set; }
        public string Imagename { get; set; }
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string emailId;
        //Sangamitra Code Strats from here

        //public string EmailId
        //{
        //    get { return emailId; }
        //    set { emailId = value; }
        //}

        private string password;

        private bool isFbVerified;

        public bool IsFbVerified
        {
            get { return isFbVerified; }
            set { isFbVerified = value; }
        }
        public bool Privacy { get; set; }
        //public string Password
        //{
        //    get { return password; }
        //    set { password = value; }
        //}

        //[Required(ErrorMessage = "Enter Valid Email Address")]
        //[DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Email Address")]
        [RegularExpression(@"(\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)|(\d{10})", ErrorMessage = "Enter valid email or phone number ")]
        [Required(ErrorMessage = "Enter valid email or phone number")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password, ErrorMessage = "Enter Password")]
        public string Password { get; set; }

        // //Sangamitra Code Ends  here

        private DateTime? createdOn;

        public DateTime? CreatedOn
        {
            get { return createdOn; }
            set { createdOn = value; }
        }

        private DateTime? modifiedOn;

        public DateTime? ModifiedOn
        {
            get { return modifiedOn; }
            set { modifiedOn = value; }
        }
        private DateTime? startDate;

        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private bool isIndexed;

        public bool IsIndexed
        {
            get { return isIndexed; }
            set { isIndexed = value; }
        }

        private string domainId;

        public string DomainId
        {
            get { return domainId; }
            set { domainId = value; }
        }

        private bool isLoggedIn;

        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set { isLoggedIn = value; }
        }



        // private string userSessionId = string.Empty;
        private string userSessionId;
        public string UserSessionID
        {
            get { return userSessionId; }
            set { userSessionId = value; }
        }
        private bool ismoderator;

        public bool IsModerator
        {
            get { return ismoderator; }
            set { ismoderator = value; }
        }
        private bool isusingmoderatorcredentials;

        public bool IsUsingModeratorCredentials
        {
            get { return isusingmoderatorcredentials; }
            set { isusingmoderatorcredentials = value; }
        }
        private string relationship;

        public string Relationship
        {
            get { return relationship; }
            set { relationship = value; }
        }

        private HospitalInformation _hospitalinfo;
        public HospitalInformation HospitalInfoId
        {
            get { return _hospitalinfo; }
            set { _hospitalinfo = value; }
        }

        private string _patientId;
        public string PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        private bool isVerified;

        public bool IsVerified
        {
            get { return isVerified; }
            set { isVerified = value; }
        }

        private string _confirpassword;
        public string ConfirmPassword
        {
            get { return _confirpassword; }
            set { _confirpassword = value; }
        }
        private bool _isDeleted; //Added by AD
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        private string _modifiedBy; //Added by AD
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }
        private bool _isSuspend; //Added by AD
        public bool IsSuspend
        {
            get { return _isSuspend; }
            set { _isSuspend = value; }
        }

        private bool _isTransaction;
        public bool IsTransaction
        {
            get { return _isTransaction; }
            set { _isTransaction = value; }
        }

        private string _errorCode; //Added by Prashanth code for error code

        // sandeep added new code start here on 26-09-2013
        private int _preferredBy;
        public int PreferredBy
        {
            get { return _preferredBy; }
            set { _preferredBy = value; }
        }

        private bool _isEMail;
        public bool IsEmail
        {
            get { return _isEMail; }
            set { _isEMail = value; }
        }




        //dileep for single calls
        private string loginName;

        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        private string _isEmailFound;
        public string IsEmailFound
        {
            get { return _isEmailFound; }
            set { _isEmailFound = value; }
        }

        private int _resultCode;
        public int ResultCode
        {
            get { return _resultCode; }
            set { _resultCode = value; }
        }

        private string _userpaln;
        public string UserPlan
        {
            get { return _userpaln; }
            set { _userpaln = value; }
        }
        //dileep for single calls

        private bool _isPhone;
        public bool IsPhone
        {
            get { return _isPhone; }
            set { _isPhone = value; }
        }
        public int _isSendMailOrMsg;
        public int IsSendMail
        {
            get { return _isSendMailOrMsg; }
            set { _isSendMailOrMsg = value; }
        }

        // sandeep added new code end here

        //added by cs
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        //
        public string ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }
        /* Hospital Related */
        //[Required(ErrorMessage = "Please select one option")]
        public string GroupType { get; set; }
        public List<SelectListItem> GroupTypeList { get; set; }

        public List<SelectListItem> GroupTypes()
        {
            GroupTypeList = new List<SelectListItem>();
            GroupTypeList.Add(new SelectListItem() { Text = "PleaseSelect", Value = "" });
            GroupTypeList.Add(new SelectListItem() { Text = "Family", Value = "Family", Selected = false });
            GroupTypeList.Add(new SelectListItem() { Text = "Hospital", Value = "Hospital", Selected = false });
            GroupTypeList.Add(new SelectListItem() { Text = "School", Value = "School", Selected = false });
            GroupTypeList.Add(new SelectListItem() { Text = "CareCenter", Value = "CareCenter", Selected = false });
            return GroupTypeList;
        }
        public List<SelectListItem> LGroupTypeList { get; set; }

        public List<SelectListItem> loginGroupTypes()
        {
            LGroupTypeList = new List<SelectListItem>();
            //  LGroupTypeList.Add(new SelectListItem() { Text = "PleaseSelect", Value = "" });
            LGroupTypeList.Add(new SelectListItem() { Text = "Patient", Value = "Patient", Selected = false });

            LGroupTypeList.Add(new SelectListItem() { Text = "Hospital", Value = "Hospital", Selected = false });
            //LGroupTypeList.Add(new SelectListItem() { Text = "School", Value = "School", Selected = false });
            //LGroupTypeList.Add(new SelectListItem() { Text = "CareCenter", Value = "CareCenter", Selected = false });
            return LGroupTypeList;
        }
        /* Hospital Related */

        //Payment

        private int _x_response_code;
        public int x_response_code
        {
            get { return _x_response_code; }
            set { _x_response_code = value; }
        }

        private int _x_response_reason_code;
        public int x_response_reason_code
        {
            get { return _x_response_reason_code; }
            set { _x_response_reason_code = value; }
        }

        private int _x_response_reason_text;
        public int x_response_reason_text
        {
            get { return _x_response_reason_text; }
            set { _x_response_reason_text = value; }
        }

        private string _x_avs_code;
        public string x_avs_code
        {
            get { return _x_avs_code; }
            set { _x_avs_code = value; }
        }

        private int _x_auth_code;
        public int x_auth_code
        {
            get { return _x_auth_code; }
            set { _x_auth_code = value; }
        }

        private string _x_trans_id;
        public string x_trans_id
        {
            get { return _x_trans_id; }
            set { _x_trans_id = value; }
        }

        private int _x_method;
        public int x_method
        {
            get { return _x_method; }
            set { _x_method = value; }
        }

        private int _x_card_type;
        public int x_card_type
        {
            get { return _x_card_type; }
            set { _x_card_type = value; }
        }

        private int _x_account_number;
        public int x_account_number
        {
            get { return _x_account_number; }
            set { _x_account_number = value; }
        }

        private int _x_first_name;
        public int x_first_name
        {
            get { return _x_first_name; }
            set { _x_first_name = value; }
        }

        private int _x_last_name;
        public int x_last_name
        {
            get { return _x_last_name; }
            set { _x_last_name = value; }
        }

        private int _x_company;
        public int x_company
        {
            get { return _x_company; }
            set { _x_company = value; }
        }

        private int _x_address;
        public int x_address
        {
            get { return _x_address; }
            set { _x_address = value; }
        }

        private int _x_city;
        public int x_city
        {
            get { return _x_city; }
            set { _x_city = value; }
        }

        private int _x_state;
        public int x_state
        {
            get { return _x_state; }
            set { _x_state = value; }
        }

        private int _x_zip;
        public int x_zip
        {
            get { return _x_zip; }
            set { _x_zip = value; }
        }

        private int _x_country;
        public int x_country
        {
            get { return _x_country; }
            set { _x_country = value; }
        }


        private int _x_phone;
        public int x_phone
        {
            get { return _x_phone; }
            set { _x_phone = value; }
        }

        private int _x_fax;
        public int x_fax
        {
            get { return _x_fax; }
            set { _x_fax = value; }
        }

        private string _x_email;
        public string x_email
        {
            get { return _x_email; }
            set { _x_email = value; }
        }

        private string _x_invoice_num;
        public string x_invoice_num
        {
            get { return _x_invoice_num; }
            set { _x_invoice_num = value; }
        }

        private string _x_description;
        public string x_description
        {
            get { return _x_description; }
            set { _x_description = value; }
        }

        private int _x_type;
        public int x_type
        {
            get { return _x_type; }
            set { _x_type = value; }
        }

        private string _x_cust_id;
        public string x_cust_id
        {
            get { return _x_cust_id; }
            set { _x_cust_id = value; }
        }

        private string _x_ship_to_first_name;
        public string x_ship_to_first_name
        {
            get { return _x_ship_to_first_name; }
            set { _x_ship_to_first_name = value; }
        }

        private string _x_ship_to_last_name;
        public string x_ship_to_last_name
        {
            get { return _x_ship_to_last_name; }
            set { _x_ship_to_last_name = value; }
        }

        private string _x_ship_to_company;
        public string x_ship_to_company
        {
            get { return _x_ship_to_company; }
            set { _x_ship_to_company = value; }
        }

        private string _x_ship_to_address;
        public string x_ship_to_address
        {
            get { return _x_ship_to_address; }
            set { _x_ship_to_address = value; }
        }

        private string _x_ship_to_city;
        public string x_ship_to_city
        {
            get { return _x_ship_to_city; }
            set { _x_ship_to_city = value; }
        }

        private string _x_ship_to_state;
        public string x_ship_to_state
        {
            get { return _x_ship_to_state; }
            set { _x_ship_to_state = value; }
        }

        private string _x_ship_to_zip;
        public string x_ship_to_zip
        {
            get { return _x_ship_to_zip; }
            set { _x_ship_to_zip = value; }
        }

        private string _x_ship_to_country;
        public string x_ship_to_country
        {
            get { return _x_ship_to_country; }
            set { _x_ship_to_country = value; }
        }

        private int _x_amount;
        public int x_amount
        {
            get { return _x_amount; }
            set { _x_amount = value; }
        }

        private int _x_tax;
        public int x_tax
        {
            get { return _x_tax; }
            set { _x_tax = value; }
        }

        private int _x_duty;
        public int x_duty
        {
            get { return _x_duty; }
            set { _x_duty = value; }
        }

        private int _x_freight;
        public int x_freight
        {
            get { return _x_freight; }
            set { _x_freight = value; }
        }

        private int _x_tax_exempt;
        public int x_tax_exempt
        {
            get { return _x_tax_exempt; }
            set { _x_tax_exempt = value; }
        }

        private int _x_po_num;
        public int x_po_num
        {
            get { return _x_po_num; }
            set { _x_po_num = value; }
        }

        private int _x_MD5_Hash;
        public int x_MD5_Hash
        {
            get { return _x_MD5_Hash; }
            set { _x_MD5_Hash = value; }
        }

        private int _x_cvv2_resp_code;
        public int x_cvv2_resp_code
        {
            get { return _x_cvv2_resp_code; }
            set { _x_cvv2_resp_code = value; }
        }

        private int _x_cavv_response;
        public int x_cavv_response
        {
            get { return _x_cavv_response; }
            set { _x_cavv_response = value; }
        }

        private string _x_test_request;
        public string x_test_request
        {
            get { return _x_test_request; }
            set { _x_test_request = value; }
        }

        private int _x_method_available;
        public int x_method_available
        {
            get { return _x_method_available; }
            set { _x_method_available = value; }
        }

        // extra fields

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

        private int _x_split_tender_status;
        public int x_split_tender_status
        {
            get { return _x_split_tender_status; }
            set { _x_split_tender_status = value; }
        }

        private int _x_line_item;
        public int x_line_item
        {
            get { return _x_line_item; }
            set { _x_line_item = value; }
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

        // extra fields
        //Payment


        //added by kumar
        private string _otacode;
        public string OTACode
        {
            get { return _otacode; }
            set { _otacode = value; }
        }

        private DateTime? _otaExpire;
        public DateTime? OTAExpire
        {
            get { return _otaExpire; }
            set { _otaExpire = value; }
        }
        //ends here
        //added by kumar
        public List<SelectListItem> Roleslist { get; set; }

        public List<SelectListItem> Roles()
        {
            Roleslist = new List<SelectListItem>();
            Roleslist.Add(new SelectListItem() { Text = "PleaseSelect", Value = "" });
            Roleslist.Add(new SelectListItem() { Text = "Doctor", Value = "Doctor", Selected = false });
            Roleslist.Add(new SelectListItem() { Text = "Patient", Value = "Patient", Selected = false });
            //Roleslist.Add(new SelectListItem() { Text = "Nurse", Value = "Nurse", Selected = false });

            return Roleslist;
        }


        private PersonalInformation _pInfo;

        public PersonalInformation PInfo
        {
            get { return _pInfo; }
            set { _pInfo = value; }
        }

        private string password1;

        private IList<HospAudit> _haudits;
        public IList<HospAudit> HAudits
        {
            get { return _haudits; }
            set { _haudits = value; }
        }
        private DateTime? fromdate;
        public DateTime? FromDate
        {
            get { return fromdate; }
            set { fromdate = value; }
        }
        private DateTime? todate;
        public DateTime? ToDate
        {
            get { return todate; }
            set { todate = value; }
        }

        private long? phoneNo;
        public long? PhoneNo { get; set; }

        private bool isPhnVerified;
        //public long? PhoneNo;//ck added // sandeep commented on 28-09-2013

        public bool IsPhnVerified;//ck added
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


        private bool _tipsendingmail;
        public bool Tipsendingmail
        {
            get { return _tipsendingmail; }
            set { _tipsendingmail = value; }

        }


        private bool _dailysendingmail;
        public bool Dailysendingmail
        {
            get { return _dailysendingmail; }
            set
            {
                _dailysendingmail = value;
            }
        }

        private bool _dailysendingsms;
        public bool Dailysendingsms
        {
            get { return _dailysendingsms; }
            set { _dailysendingsms = value; }
        }

        private string _contactName;

        public string ContactName
        {
            get { return _contactName; }
            set { _contactName = value; }
        }
        private string _contactEmail;

        public string ContactEmail
        {
            get { return _contactEmail; }
            set { _contactEmail = value; }
        }
        private string _contactsubject;

        public string Contactsubject
        {
            get { return _contactsubject; }
            set { _contactsubject = value; }
        }
        private string _contactMessage;

        public string ContactMessage
        {
            get { return _contactMessage; }
            set { _contactMessage = value; }
        }
        public string Notificationtime { get; set; }
        public string Timezone { get; set; }


    }
    public class Services1
    {
        public UserInformation ValidateUser(string EmailId, string Password)
        {
            try
            {
                UserInformation userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/AuthenticateUser/" + EmailId + "/" + Password + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));

                userInfo = OutPut.ReadObject(stream) as UserInformation;
                return userInfo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public UserInformation ValidateUserNew(string EmailId, string Password, string GroupType, string HospitalID)
        {
            try
            {
                UserInformation userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/AuthenticateUserNew/" + EmailId + "/" + Password + "/" + GroupType + "/" + HospitalID + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));

                userInfo = OutPut.ReadObject(stream) as UserInformation;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        public string InsertiHealthUser(UserInformation UserInfo)
        {
            try
            {
                string InsertedID = string.Empty;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/InsertUserInfo";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                InsertedID = serializerToDownLoad.ReadObject(stream) as string;
                return InsertedID;
            }
            catch
            {
                throw;
            }

        }

        public string InsertiHealthUserNew(UserInformation UserInfo, string type)
        {
            try
            {
                string InsertedID = string.Empty;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/InsertUserInfoNew/" + type;

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                InsertedID = serializerToDownLoad.ReadObject(stream) as string;
                return InsertedID;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string UpdateLoginStatus(UserInformation UserInfo)
        {
            try
            {
                string result = "false";
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/UpdateLoginStatus";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string UpdateTimeZonestatus(UserInformation UserInfo)
        {
            try
            {
                string result = "false";
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service + "iHealthUser/UpdateTimeZoneandtime";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public UserInformation GetUserByEmail(string Email)
        {
            try
            {
                UserInformation userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetUserByEmailID/" + Email + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));

                userInfo = OutPut.ReadObject(stream) as UserInformation;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        public UserInformation GetUserbyId(string Id)
        {
            try
            {
                UserInformation userInfo = null;
                string UserID = Convert.ToString(Id);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetUserByID/" + UserID + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));

                userInfo = OutPut.ReadObject(stream) as UserInformation;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        public UserInformation GetUserbyIdNew(string Id, string GroupType)
        {
            try
            {
                UserInformation userInfo = null;
                string UserID = Convert.ToString(Id);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetUserByIDNew/" + UserID + "/" + GroupType;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));

                userInfo = OutPut.ReadObject(stream) as UserInformation;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        public List<UserInformation> GetAllUsersbyDomainID(string DomaiID)
        {
            try
            {
                List<UserInformation> userInfo = null;
                string UserID = Convert.ToString(DomaiID);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetAllUsers/" + UserID + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
                userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
                return userInfo;
            }
            catch
            {
                throw;
            }

        }

        public string UpdateUserDetails(UserInformation UserInfo, string ID)
        {
            try
            {

                string result = "false";
                string UserID = Convert.ToString(ID);
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/UpdateUserDetails/" + UserID + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string UpdateUserPasswordDetails(UserInformation UserInfo, string ID)
        {
            try
            {
                string result = "false";
                string UserID = Convert.ToString(ID);
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/UpdateforgotpasswordDetails/" + UserID + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string UpdateUserPasswordDetailsNew(UserInformation UserInfo, string ID, string GroupType)
        {
            try
            {
                string result = "false";
                //string UserID = Convert.ToString(ID);
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/UpdateforgotpasswordDetailsNew/" + ID + "/" + GroupType;

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public List<SelectListItem> Userslist(string DomainID, string UserID)
        {
            List<SelectListItem> _Userslist = null;
            _Userslist = new List<SelectListItem>();
            List<PersonalInformation> Users = null;
            Users = new List<PersonalInformation>();
            //List<UserInformation> Users = null;
            //Users = new List<UserInformation>();
            Services3 s3 = new Services3();
            Users = s3.GetAllUsersbyDomainID(DomainID);
            for (int i = 0; i < Users.Count; i++)
            {
                _Userslist.Add(new SelectListItem
                {
                    //changes cs
                    Text = Users[i].FirstName,
                    Value = Users[i].CreatedBy,
                    Selected = (Users[i].CreatedBy == UserID)

                });
            }
            return _Userslist;
            //Session["UsesrList"] = Userslist;
        }

        public List<UserInformation> GetAllActiveUsersVV()
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "iHealthUser/GetAllActiveUsersVV";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
    public List<UserInformation> GetAllActiveUsers()
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"loginservice/GetAllActiveUsers";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public UserInformation GetUserByNamenEmail(string UserName, string Email)
        {
            UserInformation userInfo = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"loginservice/GetUserByNamenEmail/" + UserName + "/" + Email;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));
            userInfo = OutPut.ReadObject(stream) as UserInformation;
            return userInfo;
        }

        public List<SelectListItem> GetAllUserslist()
        {
            List<SelectListItem> _Userslist = null;
            _Userslist = new List<SelectListItem>();
            List<UserInformation> Users = null;
            Users = new List<UserInformation>();
            Services3 s3 = new Services3();
            Users = GetAllActiveUsers(); //GetAlliHealthUsers();
            for (int i = 0; i < Users.Count; i++)
            {
                string uid = Users[i].UserId;
                PersonalInformation pinfo = new PersonalInformation();
                pinfo = s3.GetPersonalInfo(Users[i].UserId);
                _Userslist.Add(new SelectListItem
                {

                    Text = pinfo.FirstName + " " + pinfo.LastName != null ? pinfo.LastName : string.Empty,
                    Value = Users[i].UserId.ToString(),

                    Selected = (Users[i].UserId == uid)
                });
            }
            return _Userslist;
            //Session["UsesrList"] = Userslist;
        }
        //added by dora
        public int GetAllActiveUsersCount()
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();

            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginService/GetAllActiveUsersCount";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }

        public int GetUsersbyGestationCount(string UserId)
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginService/GetUsersbyGestationCount/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }

        public int GetUsersbyInterestsCount(string UserId)
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginService/GetUsersbyInterestsCount/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }

        public int GetUsersbyCountryCount(string UserId)
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginService/GetUsersbyCountryCount/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }

        public int GetUsersByPinCodeCount(string UserId)
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();

            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginService/GetUsersByPinCodeCount/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }
        //added by dora
        public string InsertContactInfo(ContactUsViewModel CInfo)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(ContactUsViewModel));
            serializerToUplaod.WriteObject(ms, CInfo);
            string ServiceURl = DomainServerPath.Service+"Registration/InsertContactInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }

        public string InsertFeedbackInfo(ContactUsViewModel FInfo)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(ContactUsViewModel));
            serializerToUplaod.WriteObject(ms, FInfo);
            string ServiceURl = DomainServerPath.Service+"Registration/InsertFeedbackInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }

        public static IList<UserInformation> GetAllSearchedUsers(SearchFields sFields)
        {
            IList<UserInformation> lstEmp = null;
            lstEmp = new List<UserInformation>();
            WebClient UserInfoServiceProxy = new WebClient();
            UserInfoServiceProxy.Headers["Content-Type"] = "application/json";
            UserInfoServiceProxy.Headers["Accept"] = "application/json";

            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer searialzeToInsert = new DataContractJsonSerializer((typeof(SearchFields)));
            searialzeToInsert.WriteObject(ms, sFields);
            string ServiceUrl = DomainServerPath.Service+"Search/GetiHealthuserSearchList";
            byte[] data = UserInfoServiceProxy.UploadData(ServiceUrl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            lstEmp = OutPut.ReadObject(stream) as List<UserInformation>;

            return lstEmp;
        }

        public string UpdateHospitalUserAccountDetails(UserInformation UserInfo)
        {
            try
            {
                string result = "false";

                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/UpdateHospitalUserAccountDetails";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string AddUser(AddUser UserInfo)
        {
            string InsertedID = string.Empty;
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            Proxy1.Headers["Accept"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(AddUser));

            serializerToUplaod.WriteObject(ms, UserInfo);
            string ServiceURl = DomainServerPath.Service+"loginservice/InsertUserDetails";

            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
            InsertedID = serializerToDownLoad.ReadObject(stream) as string;
            return InsertedID;

        }

        public AddUser GetUsrbyId(string Id)
        {
            AddUser userInfo = null;
            string UserID = Id;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"loginservice/GetUserByID/" + UserID + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(AddUser));

            userInfo = OutPut.ReadObject(stream) as AddUser;
            return userInfo;
        }

        public string UpdateUsrDetails(AddUser UserInfo, string ID)
        {
            try
            {
                string result = "false";
                string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(AddUser));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/UpdateUserDetails/" + UserID + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public static List<UserInformation> GetAllUsersbyDomainIDNew(string DomaiID, string type)
        {
            try
            {
                List<UserInformation> userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetAllUsersByDomainIdNew/" + DomaiID + "/" + type + "";

                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
                userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
                return userInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<UserInformation> GetAllPatientsbyDomainIDNew(string DomaiID, string type)
        {
            try
            {
                List<UserInformation> userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetAllPatientsbyDomainIDNew/" + DomaiID + "/" + type + "";

                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
                userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        public string AddUser(AddUser UserInfo, string type)
        {
            try
            {

                string InsertedID = "";
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(AddUser));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/InsertSubUserInfo/" + type + ""; // sandeep changed service name on 28-09-2013

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                InsertedID = serializerToDownLoad.ReadObject(stream) as string;
                return InsertedID;
            }
            catch
            {
                throw;
            }
        }

        public string UpdateUsrDetailsNew(UserInformation UserInfo, string ID, string type)
        {
            try
            {
                string result = "false";
                string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/UpdateUserDetailsNew/" + ID + "/" + type + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<UserInformation> GetAllDoctorsbyDomainIDNew(string DomaiID, string type)
        {
            try
            {
                List<UserInformation> userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetAllDoctorsbyDomainIDNew/" + DomaiID + "/" + type + "";

                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
                userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        public string UpdateUserOTACode(UserInformation userInfo)
        {
            try
            {
                string result = "";
                //string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, userInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/UpdateUserOTACode";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string UpdateUserAccountDetails(UserInformation UserInfo, string ID)
        {
            try
            {
                string result = "false";
                string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/UpdateUserAccountDetails/" + UserID + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public string InsertOnlinePayments(OnlinePayments singleRegisterModel)
        {
            try
            {
                string InsertedID = string.Empty;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(OnlinePayments));

                serializerToUplaod.WriteObject(ms, singleRegisterModel);
                string ServiceURl = DomainServerPath.Service+"loginservice/InsertOnlinePaymentsDetails/";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                InsertedID = serializerToDownLoad.ReadObject(stream) as string;
                return InsertedID;
            }
            catch
            {
                throw;
            }

        }

        public List<OnlinePayments> GetOnlinePaymentsByDomainId(string DomainId)
        {
            List<OnlinePayments> userInfo = null;
            //userInfo=new List<OnlinePayments>();
            try
            {
                // IList<OnlinePayments> userInfo = null;
                string UserID = Convert.ToString(DomainId);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetOnlinePaymentsByDomainId/" + DomainId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<OnlinePayments>));
                userInfo = OutPut.ReadObject(stream) as List<OnlinePayments>;

                for (int i = 0; i < userInfo.Count; i++)
                {
                    string xml = userInfo[i].TransactionDetails;
                    userInfo[i].TransactionDetailsInfo = XmlStringListSerializer.Deserialize<OnlinePayments>(xml);
                }

                return userInfo;
            }
            catch
            {
                throw;
            }
            //try
            //{
            //    List<OnlinePayments> userInfo = null;
            //    // string UserID = Convert.ToString(Id);
            //    WebClient PerInfoServiceProxy = new WebClient();
            //    string ServiceUrl = DomainServerPath.Service+"loginservice/GetOnlinePaymentsByDomainId/" + DomainId + "";
            //    byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            //    Stream stream = new MemoryStream(data);

            //    DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<OnlinePayments>));
            //    userInfo = OutPut.ReadObject(stream) as List<OnlinePayments>;

            //    for (int i = 0; i < userInfo.Count; i++)
            //    {
            //        string xml = userInfo[i].TransactionDetails;
            //        userInfo[i].TransactionDetailsInfo = XmlStringListSerializer.Deserialize<OnlinePayments>(xml);
            //    }

            //    //string xml = userInfo.TransactionDetails;
            //    //userInfo.TransactionDetailsInfo = XmlStringListSerializer.Deserialize<OnlinePayments>(xml);

            //    return userInfo;
            //}
            //catch
            //{
            //    throw;
            //}
        }

        public string Validateemail(string EmailId)
        {
            try
            {

                string userInfo = null;
                //string UserID = Convert.ToString(Id);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceURl = DomainServerPath.Service+"loginservice/ValidateEmail/" + EmailId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceURl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));

                userInfo = OutPut.ReadObject(stream) as string;
                return userInfo;


            }
            catch
            {
                throw;
            }
        }

        public UserInformation ChangeNewPassworaByUserId(string UserId, string NewPassword)
        {
            try
            {
                UserInformation userInfo = null;
                string UserID = Convert.ToString(UserId);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/ChangeNewPassworaByUserId/" + UserID + "/" + NewPassword;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));

                userInfo = OutPut.ReadObject(stream) as UserInformation;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        public UserInformation GetUserByEmailForgotPaswd(string Email)
        {
            try
            {
                UserInformation userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetUserByEmailForgotPaswd/" + Email + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));

                userInfo = OutPut.ReadObject(stream) as UserInformation;
                return userInfo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public UserInformation ChangeAcPaswdByUserId(string Id, string OldPassword, string NewPassword)
        {
            try
            {
                UserInformation userInfo = null;
                string UserID = Convert.ToString(Id);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/ChangeAcPaswdByUserId/" + UserID + "/" + OldPassword + "/" + NewPassword;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));

                userInfo = OutPut.ReadObject(stream) as UserInformation;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        public UserInformation GetUserAndValidateByEmail(string Email, string firstname)
        {
            try
            {
                UserInformation userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetUserAndValidateByEmail/" + Email + "/" + firstname + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));

                userInfo = OutPut.ReadObject(stream) as UserInformation;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }
        public UserInformation GetUserAndValidateByEmailNew(string Email, string firstname, string ip)
        {
            try
            {
                UserInformation userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service + "iHealthUser/GetUserAndValidateByEmailNew/" + Email + "/" + firstname + "/" + ip + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(UserInformation));

                userInfo = OutPut.ReadObject(stream) as UserInformation;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }
        public static string AddMobile(UserInformation UserInfo)
        {
            try
            {
                string result = "false";
                //string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/AddMobile" + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static string DeleteMobile(UserInformation UserInfo)
        {
            try
            {
                string result = "false";
                //string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service + "iHealthUser/DeleteMobile" + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string VerifyMobile(UserInformation UserInfo)
        {
           
            try
            {
                string result = "false";
                //string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/VerifyMobile" + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ResendConfirmationCode(string UserId)
        {
            try
            {
                string result = "false";
                //string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(string));

                serializerToUplaod.WriteObject(ms, UserId);
                string ServiceURl = DomainServerPath.Service+"loginservice/ResendConfirmationCode/" + UserId + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AddEmail(UserInformation userInfo)
        {
            try
            {
                string result = "false";
                //string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, userInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/AddEmail" + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string VerifyEmail(UserInformation UserInfo)
        {
            try
            {
                string result = "false";
                //string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(UserInformation));

                serializerToUplaod.WriteObject(ms, UserInfo);
                string ServiceURl = DomainServerPath.Service+"loginservice/VerifyEmail" + "";

                byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ChangePreferedby(string UserId, string cvalue, string type)
        {
            try
            {
                string result = null;
                string UserID = Convert.ToString(UserId);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/ChangePreferedby/" + UserID + "/" + cvalue + "/" + type;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));

                result = OutPut.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string TipSendinByGmail(string UserId, string cvalue)
        {
            try
            {
                string result = null;
                string UserID = Convert.ToString(UserId);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/ChangeTipMailSending/" + UserID + "/" + cvalue;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));

                result = OutPut.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DailySendinByGmail(string UserId, string cvalue)
        {
            try
            {
                string result = null;
                string UserID = Convert.ToString(UserId);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/ChangeDailyMailSending/" + UserID + "/" + cvalue;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));

                result = OutPut.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DailySendinBySms(string UserId, string cvalue)
        {
            try
            {
                string result = null;
                string UserID = Convert.ToString(UserId);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/ChangeDailySmsSending/" + UserID + "/" + cvalue;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));

                result = OutPut.ReadObject(stream) as string;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UpdatePassword(string userid, string password)
        {
            try
            {

                WebClient client = new WebClient();
                string url = DomainServerPath.Service+"loginservice/cup/" + userid.Trim() + "/" + password.Trim();
                byte[] data = client.DownloadData(url);
                Stream stream = new MemoryStream(data);
                return new DataContractJsonSerializer(typeof(string)).ReadObject(stream) as string;

            }
            catch (Exception ex)
            {
                return "1201";
            }
        }

        public string ResendOTAforForgotPaswd(string UserId)
        {
            try
            {
                string successcode = string.Empty; ;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/ResendOTAforForgotPaswd/" + UserId + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));

                successcode = OutPut.ReadObject(stream) as string;
                return successcode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserInformation> GetAllGestationUsers(string UserId)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"loginservice/GetUsersbyGestation/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public List<UserInformation> GetAllUsersByFilterText(string UserId, string filterText)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"loginservice/GetUsersbyFilters/" + UserId + "/" + filterText;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public List<UserInformation> GetUsersbyInterests(string UserId)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"loginservice/GetUsersbyInterests/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }

        public List<UserInformation> GetUsersbyCountry(string UserId)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"loginservice/GetUsersbyCountry/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }

        public List<UserInformation> GetUsersbyPincode(string UserId)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"loginservice/GetUsersbyPinCode/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        //public List<UserInformation> GetAlliHealthUsersPageWise(int start, int end)
        //{
        //    List<UserInformation> userInfo = null;

        //    WebClient PerInfoServiceProxy = new WebClient();
        //    //WebClient Proxy1 = new WebClient();
        //    PerInfoServiceProxy.Headers["Content-type"] = "application/json";
        //    PerInfoServiceProxy.Headers["Accept"] = "application/json";
        //    string ServiceUrl = DomainServerPath.Service + "LoginService/GetAlliHealthUsersPageWise/" + start + "/" + end;
        //    byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
        //    Stream stream = new MemoryStream(data);
        //    DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
        //    userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
        //    return userInfo;
        //}

        // sandeep added new methods start here on 3-3-2014

        public string AddDoulaUser(DoulaUser UserInfo)
        {
            string InsertedID = string.Empty;
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            Proxy1.Headers["Accept"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(DoulaUser));

            serializerToUplaod.WriteObject(ms, UserInfo);
            string ServiceURl = DomainServerPath.Service+"loginservice/InsertDoulaUserDetails";

            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
            InsertedID = serializerToDownLoad.ReadObject(stream) as string;
            return InsertedID;
        }

        public List<DoulaUser> GetDoulaUser(string userid)
        {
            List<DoulaUser> _lstDoulauser = null;
            string UserID = Convert.ToString(userid);

            try
            {
                WebClient Proxy1 = new WebClient();

                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(List<DoulaUser>));
                string ServiceURl = DomainServerPath.Service+"loginservice/GetDoulaUserDetails/" + UserID.Trim();

                byte[] data = Proxy1.DownloadData(ServiceURl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer output = new DataContractJsonSerializer(typeof(List<DoulaUser>));
                _lstDoulauser = output.ReadObject(stream) as List<DoulaUser>;
                return _lstDoulauser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoulaUser AuthenticateDoulaUser(string UserName, string Password)
        {
            DoulaUser _objDoulauser = null;

            try
            {
                WebClient Proxy1 = new WebClient();

                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(DoulaUser));
                string ServiceURl = DomainServerPath.Service+"loginservice/AuthenticateDoulaUser/" + UserName.Trim() + "/" + Password.Trim();

                byte[] data = Proxy1.DownloadData(ServiceURl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer output = new DataContractJsonSerializer(typeof(DoulaUser));
                _objDoulauser = output.ReadObject(stream) as DoulaUser;
                return _objDoulauser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoulaiHealthUser GetDoulaHealthUserbyDoulaId(string Id)
        {
            try
            {
                DoulaiHealthUser userInfo = null;
                string UserID = Convert.ToString(Id);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetDoulaiHealthUserDetails/" + UserID + "";
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(DoulaiHealthUser));

                userInfo = OutPut.ReadObject(stream) as DoulaiHealthUser;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }
        public List<DoulaiHealthUser> GetUsersByDoulaUser(string DoulaId)
        {
            List<DoulaiHealthUser> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();

            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"loginservice/GetUsersByDoulaUser/" + DoulaId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<DoulaiHealthUser>));
            userInfo = OutPut.ReadObject(stream) as List<DoulaiHealthUser>;
            return userInfo;
        }

        public List<SelectListItem> UserslistByDoula(string UserID,string cuid)
        {
            List<SelectListItem> _Userslist = null;
            _Userslist = new List<SelectListItem>();
            List<DoulaiHealthUser> Users = null;
            Users = new List<DoulaiHealthUser>();
            //List<UserInformation> Users = null;
            //Users = new List<UserInformation>();
            Services1 s1 = new Services1();
            Users = s1.GetUsersByDoulaUser(UserID).OrderBy(x => x.CreatedOn).ToList();
            for (int i = 0; i < Users.Count; i++)
            {
                UserInformation userinfo = s1.GetUserbyId(Users[i].iHealthUserId);
                _Userslist.Add(new SelectListItem
                {
                    //changes cs
                    Text = userinfo.LoginName,
                    Value = userinfo.UserId,
                    Selected = (userinfo.UserId == cuid)

                });
            }
            return _Userslist;
            //Session["UsesrList"] = Userslist;
        }
        public DoulaUser ChangePaswdByDoulaUserId(string DoulaUserId, string OldPassword, string NewPassword)
        {
            try
            {
                DoulaUser userInfo = null;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/ChangePaswdByDoulaUserId/" + DoulaUserId + "/" + OldPassword + "/" + NewPassword;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(DoulaUser));

                userInfo = OutPut.ReadObject(stream) as DoulaUser;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }
        public DoulaUser GetDoulaUserById(string userId)
        {
            try
            {
                DoulaUser userInfo = null;
                string UserID = Convert.ToString(userId);
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"loginservice/GetDoulaUserById/" + UserID;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);

                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(DoulaUser));

                userInfo = OutPut.ReadObject(stream) as DoulaUser;
                return userInfo;
            }
            catch
            {
                throw;
            }
        }

        public string DeleteDoulaByUserIdAndDoulaId(string userId, string ihealthuserid)
        {
            try
            {
                string result = null;

                string UserID = Convert.ToString(userId);
                string iHealthUserId = Convert.ToString(ihealthuserid);

                WebClient PerInfoServiceProxy = new WebClient();
                PerInfoServiceProxy.Headers["Content-type"] = "application/json";
                PerInfoServiceProxy.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();

                string ServiceUrl = DomainServerPath.Service+"loginservice/DeleteDoulaByUserIdAndDoulaId/" + UserID + "/" + iHealthUserId;

                byte[] data = PerInfoServiceProxy.UploadData(ServiceUrl, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
                result = serializerToDownLoad.ReadObject(stream) as string;
                return result;
            }
            catch
            {
                throw;
            }
        }

        // sandeep added new methods start here on 3-3-2014


        public string GetUserFirstName(string userId)
        {

            string firstName = string.Empty;
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "LoginService/GetUserFirstName/" + userId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            firstName = (string)OutPut.ReadObject(stream);
            return firstName;
        }


        /* sp related */
        public List<UserInformation> GetAlliHealthUsersPageWise(int start, int end)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetAlliHealthUsersPageWise/" + start + "/" + end;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public List<UserInformation> GetOnlineUsersPagewise(int start, int end)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetAlliHealthUsersPageWise/" + start + "/" + end;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public List<UserInformation> GetCountryUsersbyPagewise(string UserId, int start, int end)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetUsersbyCountryPageWise/" + UserId + "/" + start + "/" + end; ;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public List<UserInformation> GetOnlineUsersbyCountryPageWise(string UserId, int start, int end)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetOnlineUsersbyCountryPageWise/" + UserId + "/" + start + "/" + end; ;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }

        public List<UserInformation> GetAllUsersbyPincode(string UserId, int start, int end)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetAllUsersbyPincode/" + UserId + "/" + start + "/" + end; ;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public List<UserInformation> GetOnlineUsersbyPincode(string UserId, int start, int end)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetOnlineUsersbyPincode/" + UserId + "/" + start + "/" + end; ;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public List<UserInformation> GetAllUsersbyGestation(string UserId, int start, int end)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetAllUsersbyGestation/" + UserId + "/" + start + "/" + end; ;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public List<UserInformation> GetOnlineUsersbyGestation(string UserId, int start, int end)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetOnlineUsersbyGestation/" + UserId + "/" + start + "/" + end; ;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }

        public List<UserInformation> GetAllUsersyFilters(string Filtertext, int start, int end, string userid)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetAllUsersbyFilters/" + Filtertext + "/" + start + "/" + end + "/" + userid;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public List<UserInformation> GetonlineUsersyFilters(string Filtertext, int start, int end, string userid)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetonlineUsersbyFilters/" + Filtertext + "/" + start + "/" + end + "/" + userid;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }

        public List<UserInformation> GetAllUsersbyInterests(string Interests, int start, int end)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetAllUsersbyInterests/" + Interests + "/" + start + "/" + end; ;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        public List<UserInformation> GetonlineUsersbyInterests(string UserId, int start, int end)
        {
            List<UserInformation> userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetonlineUsersbyInterests/" + UserId + "/" + start + "/" + end; ;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<UserInformation>));
            userInfo = OutPut.ReadObject(stream) as List<UserInformation>;
            return userInfo;
        }
        /* sp related */
        public int GetOnlineGestationCount(string UserId)
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetOnlineGestationCount/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }

        public int GetOnlineInterestsCount(string UserId)
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetOnlineInterestsCount/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }

        public int GetOnlineCountryCount(string UserId)
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetOnlineCountryCount/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }

        public int GetOnlinePinCodeCount(string UserId)
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();

            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetOnlinePinCodeCount/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }
        public int GetOnlineActiveUsersCount()
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();

            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetOnlineActiveUsersCount";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }

        public int GetAllFiltersCount(string UserId)
        {
            int userInfo = 0;

            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service + "loginservice/GetAllFiltersCount/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(int));
            userInfo = (int)OutPut.ReadObject(stream);
            return userInfo;
        }
    }



}