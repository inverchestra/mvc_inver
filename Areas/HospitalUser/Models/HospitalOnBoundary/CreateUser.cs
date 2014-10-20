using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class CreateUser
    {
        private string userid;
        public string UserId
        {
            get { return userid; }
            set { userid = value; }
        }

        [Required(ErrorMessage = "Enter the First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter the Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter Valid Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Email Address")]
        [RegularExpression(@"\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Enter Valid Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password, ErrorMessage = "Enter Password")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Re-enter password is required")]
        [Compare("Password", ErrorMessage = "Should match 'PASSWORD'")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

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

       
        private string userSessionId = string.Empty;
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

        private bool _isDeleted;
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        private int _modifiedBy;
        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
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
        
        private string roleName;
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }
        
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

       private string _patientId;
        public string PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }

        //ends here 

    }



}