using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class Doctors
    {
        private string _doctorName;
        public string DoctorName
        {
            get { return _doctorName; }
            set { _doctorName = value; }
        }
        private string _doctorId;
        public string DoctorId
        {
            get { return _doctorId; }
            set { _doctorId = value; }
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
        private int _deptId;
        public int  DeptId
        {
            get { return _deptId; }
            set { _deptId = value; }
        }
        private string _depname;
        public string DeptName
        {
            get { return _depname; }
            set { _depname = value; }
        }
        private IList<HospPersonalInfo> _hosppersonalinfo;
        public IList<HospPersonalInfo> HospPersonalInfo
        {
            get { return _hosppersonalinfo; }
            set { _hosppersonalinfo = value; }
        }
        public List<SelectListItem> Departments { get; set; }

       

    }
}