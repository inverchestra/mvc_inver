using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.Web.Optimization;
using System.ComponentModel.DataAnnotations;
using InnDocs.iHealth.Web.UI.Controllers;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;

namespace InnDocs.iHealth.Web.UI.Models
{
    public class RegisteredUser
    {
        public string RUserId { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(15, ErrorMessage = "The {0} must be between {2} to {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Re-enter password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Should match 'PASSWORD'")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Country { get; set; }

        [Required(ErrorMessage = "Pincode number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Pincode number")]
        [RegularExpression(@"\d{6}", ErrorMessage = "Invalid Pin Number (should give 6 digits) ")]
        public int Pincode { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid email address")]
        [RegularExpression(@"\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Enter a valid email address")]
        public string EmailId { get; set; }

        public DateTime? CreatedOn { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Invalid Phone Number (should give 10 digits) ")]
        public long? PhoneNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Enter a valid Date")]
        public DateTime? DOB { get; set; }

        public string Address { get; set; }

        [Required]
        public bool IsGroupUser { get; set; }

        public bool IsVerified { get; set; }

        public string PCodeUserGroup { get; set; }

        public string UserType { get; set; }

        public DateTime? ExpireOn { get; set; }

        [Required(ErrorMessage = "Promocode is required ")]
        public string PromoCodeValue { get; set; }

        public bool TryIt { get; set; }

        public bool FreeUser { get; set; }

        public string UserPlan { get; set; }

        

        public string SubscriptionType { get; set; }

        public string GroupType { get; set; }   // added by chp
    }
    public class PromoCode
    {
        private string _promoCodeId;

        public string PromoCodeId
        {
            get { return _promoCodeId; }
            set { _promoCodeId = value; }
        }
        // private string _promoCode;
        [Required(ErrorMessage = "Enter Promo code")]
        public string PromoCodes { get; set; }
        //{
        //    get { return _promoCode; }
        //    set { _promoCode = value; }
        //}
        //private string _codeUser;
        [Required(ErrorMessage = "Enter Group code")]
        public string CodeUser { get; set; }
        //{
        //    get { return _codeUser; }
        //    set { _codeUser = value; }
        //}
        // private DateTime _expireOn;
        [Required(ErrorMessage = "Enter Expire Date")]
        [DataType(DataType.DateTime)]
        public DateTime ExpireOn//Added by AD
        {
            get;
            set;
        }
        //{
        //    get { return _expireOn; }
        //    set { _expireOn = value; }
        //}
        private DateTime _createdOn;

        public DateTime CreatedOn//Added by AD
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }
        private bool _isDeleted;

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
       

    }
    //dileep
    public class SendMail
    {

        public string To
        {

            get;
            set;
        }


        public string Body
        {

            get;
            set;
        }


        public string Subject
        {

            get;
            set;
        }


        //public string DomainId
        //{

        //    get;
        //    set;
        //}
        //dileep
    }
    public class SendMessage
    {

        public string TophNumber
        {

            get;
            set;
        }


        public string Message
        {

            get;
            set;
        }



    }
    public class RegistrationServiceCalls
    {
        public string InsertRegisterUserInfo(RegisteredUser RegUser)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(RegisteredUser));
            serializerToUplaod.WriteObject(ms, RegUser);
            string ServiceURl = DomainServerPath.Service+"Registration/InsertRegisterUser";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }
        /* Hospital(venkateswari ) */
        public string InsertRegisterUserInfo(SingleRegisterModel RegUser)
        {

            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(SingleRegisterModel));
            serializerToUplaod.WriteObject(ms, RegUser);
            string ServiceURl = DomainServerPath.Service+"Registration/InsertRegisterUser";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }

        public string InsertRegisterUserInfo(HospitalRegisterModel RegUser)
        {

            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(HospitalRegisterModel));
            serializerToUplaod.WriteObject(ms, RegUser);
            string ServiceURl = DomainServerPath.Service+"Registration/InsertRegisterUser";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }
        public SingleRegisterModel GetSingleRegUserbyID(string RegUserID)
        {
            SingleRegisterModel RegUserInfo = null;
            RegUserInfo = new SingleRegisterModel();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Registration/GetRegisterUserbyID/" + RegUserID + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(SingleRegisterModel));

            RegUserInfo = OutPut.ReadObject(stream) as SingleRegisterModel;
            return RegUserInfo;
        }
        public int UpdateRUserDetails(SingleRegisterModel RegUser, string UserId)
        {
            string result = "false";
            string UserID = Convert.ToString(UserId);
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            Proxy1.Headers["Accept"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(SingleRegisterModel));

            serializerToUplaod.WriteObject(ms, RegUser);
            string ServiceURl = DomainServerPath.Service+"Registration/UpdateRegisterUser/" + UserID + "";

            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
            result = serializerToDownLoad.ReadObject(stream) as string;
            return Convert.ToInt32(result);
        }

        /* Hospital(venkateswari ) */
        public string UpdateRUserDetails(RegisteredUser RegUser, string UserId)
        {
            string result = "false";
            string UserID = Convert.ToString(UserId);
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            Proxy1.Headers["Accept"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(RegisteredUser));

            serializerToUplaod.WriteObject(ms, RegUser);
            string ServiceURl = DomainServerPath.Service+"Registration/UpdateRegisterUser/" + UserID + "";

            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(string));
            result = serializerToDownLoad.ReadObject(stream) as string;
            return result;
        }



        public RegisteredUser GetRegUserbyEmail(string EmailId)
        {
            RegisteredUser RegUserInfo = null;
            RegUserInfo = new RegisteredUser();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Registration/GetRegisterUserbyEmail/" + EmailId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(RegisteredUser));

            RegUserInfo = OutPut.ReadObject(stream) as RegisteredUser;
            return RegUserInfo;
        }

        public RegisteredUser GetRegUserbyID(string RegUserID)
        {
            RegisteredUser RegUserInfo = null;
            RegUserInfo = new RegisteredUser();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Registration/GetRegisterUserbyID/" + RegUserID + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(RegisteredUser));

            RegUserInfo = OutPut.ReadObject(stream) as RegisteredUser;
            return RegUserInfo;
        }
        //Added by Ashwin Starts here


        //public SingleRegisterModel GetRegUserbyMobile(string MobileNumber)
        //{
        //    SingleRegisterModel RegUserInfo = null;
        //    RegUserInfo = new SingleRegisterModel();
        //    WebClient PerInfoServiceProxy = new WebClient();
        //    string ServiceUrl = DomainServerPath.Service+"Registration/GetRegisterUserbyMobile/" + MobileNumber + "";
        //    byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
        //    Stream stream = new MemoryStream(data);
        //    DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(SingleRegisterModel));

        //    RegUserInfo = OutPut.ReadObject(stream) as SingleRegisterModel;
        //    return RegUserInfo;
        //}
        //ends here

        public SingleRegisterModel GetRegUserbyMobile(string MobileNumber)
        {
            SingleRegisterModel RegUserInfo = null;
            RegUserInfo = new SingleRegisterModel();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Registration/GetRegisterUserbyMobile/" + MobileNumber + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(SingleRegisterModel));

            RegUserInfo = OutPut.ReadObject(stream) as SingleRegisterModel;
            return RegUserInfo;
        }
        //ends here
        public string UpdateUserOTACode(SingleRegisterModel reginfo)
        {
            try
            {
                string result = "";
                //string UserID = ID;
                WebClient Proxy1 = new WebClient();
                Proxy1.Headers["Content-type"] = "application/json";
                Proxy1.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(SingleRegisterModel));

                serializerToUplaod.WriteObject(ms, reginfo);
                string ServiceURl = DomainServerPath.Service+"Registration/UpdatOTACode";

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
        //ends here
        public List<PromoCode> GetAllPromoCodes()
        {
            List<PromoCode> PromoCodesInfo = null;
            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"Registration/GetAllPromoCodesList";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<PromoCode>));

            PromoCodesInfo = OutPut.ReadObject(stream) as List<PromoCode>;
            return PromoCodesInfo;
        }

        public string InsertPCodeInfo(PromoCode PCodeInfo)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(PromoCode));
            serializerToUplaod.WriteObject(ms, PCodeInfo);
            string ServiceURl = DomainServerPath.Service+"Registration/InsertPromoCodeInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }

        public PromoCode GetPCodeInfoById(string pCodeId)
        {
            PromoCode pCodeInfo = null;
            pCodeInfo = new PromoCode();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Registration/GetPromoCodeById/" + pCodeId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(PromoCode));

            pCodeInfo = OutPut.ReadObject(stream) as PromoCode;
            return pCodeInfo;
        }
        public List<SingleRegisterModel> GetAllRegUsers()
        {
            List<SingleRegisterModel> RegUsers = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"Registration/GetAllRegUsers";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<SingleRegisterModel>));
            RegUsers = OutPut.ReadObject(stream) as List<SingleRegisterModel>;
            return RegUsers;
        }

        public int UpdatePromoCodeInfo(PromoCode PCodeInfo)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(PCodeInfo.GetType());
                serializer.WriteObject(ms, PCodeInfo);
                string url = DomainServerPath.Service+"Registration/UpdatePromoCodeInfo/" + PCodeInfo.PromoCodeId.ToString() + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return Convert.ToInt32(code);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public string DeletePromoCode(string id)
        {

            string Successcode;
            WebClient ServProxy = new WebClient();
            string ServiceURL = DomainServerPath.Service+"Registration/DeletePromoCode/" + id.ToString().Trim();
            byte[] data = ServProxy.DownloadData(ServiceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            Successcode = OutPut.ReadObject(stream) as string;
            return Successcode;
        }

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

        public string InsertPaymentInfo(Payments paymentInfo)
        {
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(Payments));
            serializerToUplaod.WriteObject(ms, paymentInfo);
            string ServiceURl = DomainServerPath.Service+"Registration/InsertPaymentsInfo";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            string InsertedID = serializerToResult.ReadObject(stream) as string;

            return InsertedID;
        }
        public List<PromoCode> GetAllPromocodesByIsDeleted(string IsDeleted)
        {
            List<PromoCode> _lstPromoCodes = null;
            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"Registration/GetAllPromocodesByIsDeleted/" + IsDeleted + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<PromoCode>));

            _lstPromoCodes = OutPut.ReadObject(stream) as List<PromoCode>;
            return _lstPromoCodes;
        }

        //Added by Ashwin Ends here

        //Dileep
        public bool SendMail(SendMail _sendmail)
        {
            bool IsSendMail = false;


            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(SendMail));
            serializerToUplaod.WriteObject(ms, _sendmail);
            string ServiceURl = DomainServerPath.Service+"Registration/SendEmail";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            IsSendMail = Convert.ToBoolean(serializerToResult.ReadObject(stream));

            return IsSendMail;
        }


        public bool SendMessage(SendMessage _sendMessage)
        {
            bool IsSendMessage = false;


            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(SendMessage));
            serializerToUplaod.WriteObject(ms, _sendMessage);
            string ServiceURl = DomainServerPath.Service+"Registration/SendMessage";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            IsSendMessage = Convert.ToBoolean(serializerToResult.ReadObject(stream));

            return IsSendMessage;
        }
        //Dileep

        public SingleRegisterModel GetRegUserbyEmailId(string EmailId) // sandeep added new method on 18-09-2013
        {
            SingleRegisterModel RegUserInfo = null;
            RegUserInfo = new SingleRegisterModel();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Registration/GetRegisterUserbyEmail/" + EmailId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(SingleRegisterModel));

            RegUserInfo = OutPut.ReadObject(stream) as SingleRegisterModel;
            return RegUserInfo;
        }

        public SingleRegisterModel GetSingleRegUserbyCustomerId(string custid)  // added by Prashanth 
        {
            SingleRegisterModel RegUserInfo = null;
            RegUserInfo = new SingleRegisterModel();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"Registration/GetRegUserbyCustomerId/" + custid + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(SingleRegisterModel));

            RegUserInfo = OutPut.ReadObject(stream) as SingleRegisterModel;
            return RegUserInfo;
        }


        //dileep for Single Service Calls

        public SingleRegisterModel ConfirmRegUserbyId(ConfirmSingleRegModel cnfirmSingle, string RegUserID)
        {
            SingleRegisterModel RegUserInfo = null;
            RegUserInfo = new SingleRegisterModel();
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(ConfirmSingleRegModel));

            serializerToUplaod.WriteObject(ms, cnfirmSingle);
            string ServiceUrl = DomainServerPath.Service+"Registration/ConfirmRegUserbyId/" + RegUserID + "";

            byte[] data = PerInfoServiceProxy.UploadData(ServiceUrl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer serializerToDownLoad = new DataContractJsonSerializer(typeof(SingleRegisterModel));
            RegUserInfo = serializerToDownLoad.ReadObject(stream) as SingleRegisterModel;
            return RegUserInfo;
        }
        public string ResendOTA(string UserId)
        {
            try
            {
                string successcode = string.Empty; ;

                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"Registration/ResendOTA/" + UserId + "";
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
        //dileep for Single Service Calss

        public string EmailVerification(string Emailid)
        {

            string Successcode;
            WebClient ServProxy = new WebClient();
            string ServiceURL = DomainServerPath.Service+"Registration/EmailVerification/" + Emailid.ToString();
            byte[] data = ServProxy.DownloadData(ServiceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            Successcode = OutPut.ReadObject(stream) as string;
            return Successcode;
        }

    }
}