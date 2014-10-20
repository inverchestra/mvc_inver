using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class DoulaUser
    {
        [Required(ErrorMessage = "Enter the first name")]
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [Required(ErrorMessage = "Enter the last name")]
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        [RegularExpression(@"(\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)|(\d{10})", ErrorMessage = "Enter valid email or phone number ")]
        [Required(ErrorMessage = "Enter valid email or phone number")]
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        //[RegularExpression(@"(\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)|(\d{10})", ErrorMessage = "Enter valid email or phone number ")]
        //[Required(ErrorMessage = "Enter valid email or phone number")]
        private long? _phoneNo;
        public long? PhoneNo
        {
            get { return _phoneNo; }
            set { _phoneNo = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private bool _isVerified = default(Boolean);
        public bool IsVerified
        {
            get { return _isVerified; }
            set { _isVerified = value; }
        }

        private bool _isPhoneVerified = default(Boolean);
        public bool IsPhoneVerified
        {
            get { return _isPhoneVerified; }
            set { _isPhoneVerified = value; }
        }

        private DateTime? _createdOn = null;
        public DateTime? CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        private string _doulaType;
        public string DoulaType
        {
            get { return _doulaType; }
            set { _doulaType = value; }
        }

        private bool _isLoggedin;
        public bool IsLoggedin
        {
            get { return _isLoggedin; }
            set { _isLoggedin = value; }
        }

        private string _userSessionId;
        public string UserSessionId
        {
            get { return _userSessionId; }
            set { _userSessionId = value; }
        }

        private string _errorCode;
        public string ErrorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }
        private int _resultCode;
        public int ResultCode
        {
            get { return _resultCode; }
            set { _resultCode = value; }
        }

        private string _iHealthUserId;
        public string iHealthUserId
        {
            get { return _iHealthUserId; }
            set { _iHealthUserId = value; }
        }

        private string _doulaUserId;
        public string DoulaUserId
        {
            get { return _doulaUserId; }
            set { _doulaUserId = value; }
        }

        public List<SelectListItem> DoulaTypeList { get; set; }

        public List<SelectListItem> DoulaTypes()
        {
            DoulaTypeList = new List<SelectListItem>();
            //DoulaTypeList.Add(new SelectListItem() { Text = "PleaseSelect", Value = "" });
            DoulaTypeList.Add(new SelectListItem() { Text = "Family", Value = "Family", Selected = false });
            DoulaTypeList.Add(new SelectListItem() { Text = "Doctor", Value = "Doctor", Selected = false });
            return DoulaTypeList;
        }

        //public string CreatedOn1
        //{
        //    get
        //    {
        //        return CreatedOn.Value.ToShortDateString();
        //    }
        //    set
        //    {
        //    }
        //}

        private List<DoulaiHealthUser> _doulaihealthUser;
        public List<DoulaiHealthUser> DoulaiHealthUser
        {
            get { return _doulaihealthUser; }
            set { _doulaihealthUser = value; }
        }
    }

    public class DoulaiHealthUser
    {

        private DateTime? _creratedOn;
        public DateTime? CreatedOn
        {
            get { return _creratedOn; }
            set { _creratedOn = value; }
        }
        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        private string _doulaType;
        public string DoulaType
        {
            get
            {
                return _doulaType;
            }
            set
            {
                _doulaType = value;
            }
        }

        private string _iHealthUserId;
        public string iHealthUserId
        {
            get
            {
                return _iHealthUserId;
            }
            set
            {
                _iHealthUserId = value;
            }
        }

        private string _doulauserId;
        public string DoulaUserId
        {
            get
            {
                return _doulauserId;
            }
            set
            {
                _doulauserId = value;
            }
        }

    }
}