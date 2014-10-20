using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class Departments
    {
        private string _departmentName;
        public string DepartmentName
        {
            get { return _departmentName; }
            set { _departmentName = value; }
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _deptId;
        public string DeptId
        {
            get { return _deptId; }
            set { _deptId = value; }
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
        private string _branchId;
        public string BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }
        private string _hospInfoId;
        public string HospitalInfoId
        {
            get { return _hospInfoId; }
            set { _hospInfoId = value; }
        }
        private IList<Doctors> _lstdoctor;
        public IList<Doctors> DoctorsList
        {
            get { return _lstdoctor; }
            set { _lstdoctor = value; }
        }

        private string _branchName;
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }

        public List<string> departtobranchs { get; set; }
        public List<SelectListItem> Branches { get; set; }

        public List<SelectListItem> FillBranches(string HospInfoId)
        {
            Branches = new List<SelectListItem>();

            IList<Branch> lstBranches = GetBranch.GetAllBranchInfobyHospital(HospInfoId);


            foreach (Branch a in lstBranches)
            {
                Branches.Add(new SelectListItem() { Text = a.BranchName, Value = a.Id.ToString(), Selected = false });

            }

            return Branches;
        }

        private string _deptspecialization;
        public virtual string DeptSpecialization
        {
            get { return _deptspecialization; }
            set { _deptspecialization = value; }
        }
        private string _deptdetails;
        public virtual string DeptDetails
        {
            get { return _deptdetails; }
            set { _deptdetails = value; }
        }

    }
        
}