using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class Branch
    {
        private string _branchName;
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _branchaddress;
        public string BranchAddress
        {
            get { return _branchaddress; }
            set { _branchaddress = value; }
        }
        //private string _bID;
        //public string BranchID
        //{
        // get { return _bID; }
        // set { _bID = value; }
        //}
        private string _branchid;
        public string BranchID
        {
            get { return _branchid; }
            set { _branchid = value; }
        }
        private string _branchdetails;
        public string BranchDetails
        {
            get { return _branchdetails; }
            set { _branchdetails = value; }
        }
        private DateTime? _createdOn;
        public DateTime? CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        private string _domainId;
        public string DomainId
        {
            get { return _domainId; }
            set { _domainId = value; }
        }

        private string _hospitalInfoID;
        public string HospitalInfoId
        {
            get { return _hospitalInfoID; }
            set { _hospitalInfoID = value; }
        }

        private IList<Departments> _departments;
        public IList<Departments> Departments
        {
            get { return _departments; }
            set { _departments = value; }
        }


        private IList<Branch> _lstbranchs;
        public IList<Branch> lstbranchs
        {
            get { return _lstbranchs; }
            set { _lstbranchs = value; }
        }

        private IList<Branch> _lstbranch;
        public IList<Branch> BranchList
        {
            get { return _lstbranch; }
            set { _lstbranch = value; }
        }
    }
}