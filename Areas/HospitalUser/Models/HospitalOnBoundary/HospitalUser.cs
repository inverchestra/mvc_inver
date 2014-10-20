using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class HospitalUserInfo
    {

        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

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

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string email;
        public string Email
        {
            get { return email;}
            set { email = value; }
        }


        private bool _isIndexed;
        public bool IsIndexed
        {
            get { return _isIndexed; }
            set { _isIndexed = value; }
        }

        private DateTime _createdOn;
        public  DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        private DateTime _modifedOn;
        public  DateTime ModifiedOn
        {
            get { return _modifedOn; }
            set { _modifedOn = value; }
        }

        public List<SelectListItem> doctors { get; set; }
       
        //public virtual RegisteredUser DomainUser
        //{
        //    get { return _domainUser; }
        //    set { _domainUser = value; }
        //}
        //public virtual Doctors Doctors
        //{
        //    get { return _doctor; }
        //    set { _doctor = value; }
        //}
        private string domainId;
        public string DomainID
        {
            get { return domainId; }
            set { domainId = value; }
        }
        private bool _isloggedin;
        public bool IsLoggedIn
        {
            get { return _isloggedin; }
            set { _isloggedin = value; }
        }

        private string _loginname;
        public string LoginName
        {
            get { return _loginname; }
            set { _loginname = value; }
        }

        private string _relatedDoctorID; 
        public string RelatedDoctorID
        {
            get { return _relatedDoctorID; }
            set { _relatedDoctorID = value; }
        }

        private string _usersessionid;
        public  string UserSessionID
        {
            get { return _usersessionid; }
            set { _usersessionid = value; }
        }
        private IList<HospPersonalInfo> _hpersonalinfo1;
        public IList<HospPersonalInfo> HPersonalInfo1
        {
            get { return _hpersonalinfo1; }
            set { _hpersonalinfo1 = value; }
        }

        private IList<HospPersonalInfo> _hpersonalinfo;
        public IList<HospPersonalInfo> HPersonalInfo
        {
            get { return _hpersonalinfo; }
            set { _hpersonalinfo = value; }
        }

        private IList<HospMedicalInformation> _hMedicalInfo1;
        public IList<HospMedicalInformation> HMedicalInfo1
        {
            get { return _hMedicalInfo1; }
            set { _hMedicalInfo1 = value; }
        }
        private IList<HospMedicalInformation> _hMedicalInfo;
        public  IList<HospMedicalInformation> HMedicalInfo
        {
            get { return _hMedicalInfo; }
            set { _hMedicalInfo = value; }
        }

        private bool _ismoderator;
        public bool IsModerator
        {
            get { return _ismoderator; }
            set { _ismoderator = value; }
        }
        private bool _isusingmoderatorcredentials;
        public bool IsUsingModeratorCredentials
        {
            get { return _isusingmoderatorcredentials; }
            set { _isusingmoderatorcredentials = value; }
        }
        private string _relationship;
        public string Relationship
        {
            get { return _relationship; }
            set { _relationship = value; }
        }

        private string _inndocsMailId;
        public string InndocsMailId
        {
            get { return _inndocsMailId; }
            set { _inndocsMailId = value; }
        }


        private bool _inndocsMailEnabled;
        public bool InndocsMailEnabled
        {
            get { return _inndocsMailEnabled; }
            set { _inndocsMailEnabled = value; }
        }
        // End by Usha /////

        //Venkateswari

        private bool _isVerified;
        public bool IsVerified
        {
            get { return _isVerified; }
            set { _isVerified = value; }
        }
        //Venkateswari

        //Added by AD
        private string _modifiedBy;
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private bool _isDelete;
        public bool IsDelete
        {
            get { return _isDelete; }
            set { _isDelete = value; }
        }

        private bool _isSuspend;
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

        private string _confirpassword;
        public string ConfirmPassword
        {
            get { return _confirpassword; }
            set { _confirpassword = value; }
        }
        private string userid;

        public string UserId
        {
            get { return userid; }
            set { userid = value; }
        }


    }
}