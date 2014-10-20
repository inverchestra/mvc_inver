using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models
{
    public class PersonalInformation
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private DateTime? dateOfBirth;
        public DateTime? DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }
        public string dateOfBirth1
        {
            get
            {
                if (DateOfBirth != null)
                    return DateOfBirth.Value.ToShortDateString();
                return null;
            }
            set { }
        }
        public string dateofbir { get; set; }
        private string gender;
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private string State;
        public string state
        {
            get { return State; }
            set { State = value; }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        //private double contact;
        //public double Contact
        //{
        //    get { return contact; }
        //    set { contact = value; }
        //}

        //private double emergency;
        //public double Emergency
        //{
        //    get { return emergency; }
        //    set { emergency = value; }
        //}
        // [Required(ErrorMessage = "emergency is required")]
        public long? Emergency { get; set; }

        private long? workTelNumber;
        public long? WorkTelNumber
        {
            get { return workTelNumber; }
            set { workTelNumber = value; }
        }

        private long? mobileNumber;
        public long? MobileNumber
        {
            get { return mobileNumber; }
            set { mobileNumber = value; }
        }

        private string religion;
        public string Religion
        {
            get { return religion; }
            set { religion = value; }
        }

        private string street;
        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        private string town;
        public string Town
        {
            get { return town; }
            set { town = value; }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private int zipCode;
        public int ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        private double wristSize;
        public double WristSize
        {
            get { return wristSize; }
            set { wristSize = value; }
        }

        private double cholesterolValues;
        public double CholesterolValues
        {
            get { return cholesterolValues; }
            set { cholesterolValues = value; }
        }

        private string priorSurgery;
        public string PriorSurgery
        {
            get { return priorSurgery; }
            set { priorSurgery = value; }
        }

        private string adverseDrugReaction;
        public string AdverseDrugReaction
        {
            get { return adverseDrugReaction; }
            set { adverseDrugReaction = value; }
        }

        private bool migraine;
        public bool Migraine
        {
            get { return migraine; }
            set { migraine = value; }
        }

        private bool circumcision;
        public bool Circumcision
        {
            get { return circumcision; }
            set { circumcision = value; }
        }

        private bool smoking;
        public bool Smoking
        {
            get { return smoking; }
            set { smoking = value; }
        }

        private bool srugs;
        public bool Srugs
        {
            get { return srugs; }
            set { srugs = value; }
        }

        private bool fertilityProblems;
        public bool FertilityProblems
        {
            get { return fertilityProblems; }
            set { fertilityProblems = value; }
        }

        private bool bloodTransfusions;
        public bool BloodTransfusions
        {
            get { return bloodTransfusions; }
            set { bloodTransfusions = value; }
        }

        private bool isIndexed;
        public bool IsIndexed
        {
            get { return isIndexed; }
            set { isIndexed = value; }
        }

        private DateTime? UpdatedOn;
        public DateTime? UpdatedOn1
        {
            get { return UpdatedOn; }
            set { UpdatedOn = value; }
        }

        private string updatedBy;
        public string UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        private string userId;
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private string domainID;
        public string DomainID
        {
            get { return domainID; }
            set { domainID = value; }
        }

        private string iD;
        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string originalName;
        public string OriginalName
        {
            get { return originalName; }
            set { originalName = value; }
        }

        private string imageName;
        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }
        // [Required(ErrorMessage = "contact is required")]
        public long? Contact { get; set; }
        private string cretaedBy;

        public string CreatedBy
        {
            get { return cretaedBy; }
            set { cretaedBy = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        // sandeep added on 28-09-2013
        private string _ownerId;
        public string OwnerId
        {
            get { return _ownerId; }
            set { _ownerId = value; }
        }


        public List<SelectListItem> duedateList { get; set; }
        public List<SelectListItem> duedatetypes()
        {
            duedateList = new List<SelectListItem>();
            // duedateList.Add(new SelectListItem() { Text = "Please Select", Value = "Please Select", Selected = false });

            duedateList.Add(new SelectListItem() { Text = "First day of last menstrual period", Value = "First day of last menstrual period", Selected = false });
            duedateList.Add(new SelectListItem() { Text = "Date of conception", Value = "Date of conception", Selected = false });
            duedateList.Add(new SelectListItem() { Text = "I know my due date", Value = "I know my due date", Selected = false });
            //StypeList.Add(new SelectListItem() { Text = "TrialUser", Value = "TrialUser", Selected = false });
            //StypeList.Add(new SelectListItem() { Text = "PaidUser", Value = "PaidUser", Selected = false });
            return duedateList;
        }

        private DateTime? eddDate;
        public DateTime? EDDDate
        {
            get { return eddDate; }
            set { eddDate = value; }
        }
        public string duemethod { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Enter a valid Date")]
        public DateTime? duedate { get; set; }
        public string duedat { get; set; }

    }

    public class Services3
    {
        public PersonalInformation GetPersonalInfo(string userId)
        {
            PersonalInformation userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"UserInfo/GetPersonalInfo/" + userId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(PersonalInformation));

            userInfo = OutPut.ReadObject(stream) as PersonalInformation;
            userInfo.dateOfBirth1 = userInfo.DateOfBirth.ToString();
            return userInfo;
        }

        //added by kumar
        public PersonalInformation GetPersonalInfoNew(string userId, string GroupType)
        {
            PersonalInformation userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"UserInfo/GetPersonalInfoNew/" + userId + "/" + GroupType;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(PersonalInformation));

            userInfo = OutPut.ReadObject(stream) as PersonalInformation;
            userInfo.dateOfBirth1 = userInfo.DateOfBirth.ToString();
            return userInfo;
        }
        //ends here
        public MedicalInformation GetMedicalInfo(string userId)
        {
            MedicalInformation userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"UserInfo/GetUserMedicalInfo/" + userId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(MedicalInformation));

            userInfo = OutPut.ReadObject(stream) as MedicalInformation;
            return userInfo;
        }
        public int InsertPersonalInfo(PersonalInformation p, string GroupType)
        {

            int errorCode = 0;
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(PersonalInformation));
            serializerToUplaod.WriteObject(ms, p);
            string ServiceURl = DomainServerPath.Service+"UserInfo/InsertPersonalInfo/" + GroupType + "";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            errorCode = Convert.ToInt32(serializerToResult.ReadObject(stream) as string);

            return errorCode;
        }
        //public int InsertPersonalInfo(PersonalInformation p)
        //{

        //    int errorCode = 0;
        //    WebClient Proxy1 = new WebClient();
        //    Proxy1.Headers["Content-type"] = "application/json";
        //    MemoryStream ms = new MemoryStream();
        //    DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(PersonalInformation));
        //    serializerToUplaod.WriteObject(ms, p);
        //    string ServiceURl = DomainServerPath.Service+"UserInfo/InsertPersonalInfoNew/"+"";
        //    byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
        //    Stream stream = new MemoryStream(data);

        //    DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
        //    errorCode = Convert.ToInt32(serializerToResult.ReadObject(stream) as string);

        //    return errorCode;
        //}
        public int UpdatePersonalInfo(PersonalInformation p, string userId)
        {
            int errorCode = 0;
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(PersonalInformation));
            serializerToUplaod.WriteObject(ms, p);
            string ServiceURl = DomainServerPath.Service+"UserInfo/UpdatePersonalInfo/" + userId + "";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            errorCode = Convert.ToInt32(serializerToResult.ReadObject(stream) as string);

            return errorCode;
        }
        /* Hospital(venkateswari) */
        //public int UpdatePersonalInfo(PersonalInformation p, string userId)
        //{
        //    int errorCode = 0;
        //    //RegisteredUser ruser = null;
        //    //ruser = new RegisteredUser();
        //    WebClient Proxy1 = new WebClient();
        //    Proxy1.Headers["Content-type"] = "application/json";
        //    MemoryStream ms = new MemoryStream();
        //    DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(PersonalInformation));
        //    serializerToUplaod.WriteObject(ms, p);
        //    string ServiceURl = DomainServerPath.Service+"UserInfo/UpdatePersonalInfo/" + userId +"";
        //    byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
        //    Stream stream = new MemoryStream(data);

        //    DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
        //    errorCode = Convert.ToInt32(serializerToResult.ReadObject(stream) as string);

        //    return errorCode;
        //}
        public int UpdateMedicalInfo(MedicalInformation m, string UserID, string domainID, string t, string GroupType)
        {
            int errorCode = 0;
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(MedicalInformation));
            serializerToUplaod.WriteObject(ms, m);
            // string s = Convert.ToString(userId);
            string ServiceURl = DomainServerPath.Service+"UserInfo/UpdateMedicalInfo/" + UserID + "/" + domainID + "/" + t + "/" + GroupType;
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            errorCode = Convert.ToInt32(serializerToResult.ReadObject(stream) as string);

            return errorCode;
        }
        public int UpdateMedicalInfoForDelete(MedicalInformation m, string UserID, string domainID)
        {
            int errorCode = 0;
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(MedicalInformation));
            serializerToUplaod.WriteObject(ms, m);
            // string s = Convert.ToString(userId);
            string ServiceURl = DomainServerPath.Service+"UserInfo/UpdateMedicalInfoForDelete/" + UserID + "/" + domainID + "";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            errorCode = Convert.ToInt32(serializerToResult.ReadObject(stream) as string);

            return errorCode;
        }
        /* Hospital(Venkateswari) */
        //public int UpdateMedicalInfo(MedicalInformation m, string UserID, string domainID, string t)
        //{
        //    int errorCode = 0;
        //    //RegisteredUser ruser = null;
        //    //ruser = new RegisteredUser();
        //    WebClient Proxy1 = new WebClient();
        //    Proxy1.Headers["Content-type"] = "application/json";
        //    MemoryStream ms = new MemoryStream();
        //    DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(MedicalInformation));
        //    serializerToUplaod.WriteObject(ms, m);
        //    // string s = Convert.ToString(userId);
        //    string ServiceURl = DomainServerPath.Service+"UserInfo/UpdateMedicalInfo/" + UserID + "/" + domainID + "/" + t;
        //    byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
        //    Stream stream = new MemoryStream(data);

        //    DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
        //    errorCode = Convert.ToInt32(serializerToResult.ReadObject(stream) as string);

        //    return errorCode;
        //}
        //public int UpdateMedicalInfoForDelete(MedicalInformation m, string UserID, string domainID)
        //{
        //    int errorCode = 0;
        //    //RegisteredUser ruser = null;
        //    //ruser = new RegisteredUser();
        //    WebClient Proxy1 = new WebClient();
        //    Proxy1.Headers["Content-type"] = "application/json";
        //    MemoryStream ms = new MemoryStream();
        //    DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(MedicalInformation));
        //    serializerToUplaod.WriteObject(ms, m);
        //    // string s = Convert.ToString(userId);
        //    string ServiceURl = DomainServerPath.Service+"UserInfo/UpdateMedicalInfoForDelete/" + UserID + "/" + domainID;
        //    byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
        //    Stream stream = new MemoryStream(data);

        //    DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
        //    errorCode = Convert.ToInt32(serializerToResult.ReadObject(stream) as string);

        //    return errorCode;
        //}

        public List<PersonalInformation> GetAllUsersbyDomainID(string DomaiID)
        {
            List<PersonalInformation> lstusersPInfo = null;
            //string UserID = Convert.ToString(DomaiID);
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"UserInfo/GetAllUsersPinfo/" + DomaiID + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<PersonalInformation>));
            lstusersPInfo = OutPut.ReadObject(stream) as List<PersonalInformation>;
            return lstusersPInfo;
        }

        public PersonalInformation GetPersonalInfoUserId(string UserId)
        {
            PersonalInformation lstusersPInfo = null;
            //string UserID = Convert.ToString(DomaiID);
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"UserInfo/GetPersonalInfo/" + UserId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(PersonalInformation));
            lstusersPInfo = OutPut.ReadObject(stream) as PersonalInformation;
            return lstusersPInfo;
        }

        public List<PersonalInformation> GetAllActivePinfoUsers()
        {
            List<PersonalInformation> usersInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            //WebClient Proxy1 = new WebClient();
            PerInfoServiceProxy.Headers["Content-type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"UserInfo/GetAllActivepInfoUsers";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<PersonalInformation>));
            usersInfo = OutPut.ReadObject(stream) as List<PersonalInformation>;
            return usersInfo;
        }


        public PersonalInformation GetDoctorinfo(string UserId)
        {

            PersonalInformation hInfo = new PersonalInformation();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"UserInfo/GetDoctorinfo/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(PersonalInformation));

            hInfo = OutPut.ReadObject(stream) as PersonalInformation;
            return hInfo;
        }

        // sandeep
        public PersonalInformation GetPersonalInfoByUserIdAndGroupType(string userId, string GroupType)
        {
            PersonalInformation userInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"UserInfo/GetPersonalInfoByOwnerIdAndGroupType/" + userId + "/" + GroupType;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(PersonalInformation));

            userInfo = OutPut.ReadObject(stream) as PersonalInformation;
            userInfo.dateOfBirth1 = userInfo.DateOfBirth.ToString();
            return userInfo;
        }
        public int UpdateEddDate(PersonalInformation p, string userId)
        {
            int errorCode = 0;
            //RegisteredUser ruser = null;
            //ruser = new RegisteredUser();
            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(PersonalInformation));
            serializerToUplaod.WriteObject(ms, p);
            string ServiceURl = DomainServerPath.Service+"UserInfo/UpdateEddDate/" + userId + "";
            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);

            DataContractJsonSerializer serializerToResult = new DataContractJsonSerializer(typeof(string));
            errorCode = Convert.ToInt32(serializerToResult.ReadObject(stream) as string);

            return errorCode;
        }

    }

}