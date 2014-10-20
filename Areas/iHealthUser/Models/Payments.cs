using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository
{
    public class Payments
    {

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Valid Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Valid Email Address")]
        [RegularExpression(@"\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Enter Valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Cheque No")]
        public string ChequeNo { get; set; }
        [Required(ErrorMessage = "Enter Bank Name")]
        public string BankName { get; set; }
        [Required(ErrorMessage = "Enter Branch Name")]
        public string BranchName { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Select Date")]
        public DateTime ChequeDate { get; set; }
        [Required(ErrorMessage = "Enter Account No")]
        public long AccountNo { get; set; }
        [Required(ErrorMessage = "Enter Amount")]
        public long Amount { get; set; }
        [Required(ErrorMessage = "Select Expire Date")]
        public DateTime Validity { get; set; }

        public string UserId { get; set; }



        //#region Declaration

        //private string _name;
        //private string _email;
        //private string _chequeNo;
        //private string _bankName;
        //private DateTime _chequeDate;
        //private string _branchName;
        //private string _Address;
        //private long _actNo;
        //private long _amount;
        //#endregion

        //#region Properties

        //public virtual string Name
        //{
        //    get { return _name; }
        //    set { _name = value; }
        //}

        //public virtual string Email
        //{
        //    get { return _email; }
        //    set { _email = value; }
        //}

        //public virtual string ChequeNo
        //{
        //    get { return _chequeNo; }
        //    set { _chequeNo = value; }
        //}

        //public virtual string BankName
        //{
        //    get { return _bankName; }
        //    set { _bankName = value; }
        //}

        //public virtual DateTime ChequeDate
        //{
        //    get { return _chequeDate; }
        //    set { _chequeDate = value; }
        //}



        //public virtual string BranchName
        //{
        //    get { return _branchName; }
        //    set { _branchName = value; }
        //}
        //public virtual string Address
        //{
        //    get { return _Address; }
        //    set { _Address = value; }
        //}
        //public virtual long AccountNo
        //{
        //    get { return _actNo; }
        //    set { _actNo = value; }
        //}
        //public virtual long Amount
        //{
        //    get { return _amount; }
        //    set { _amount = value; }
        //}
        //#endregion
    }
}