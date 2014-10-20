using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET;
using System.Text;
using System.Web.Mvc;


namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models
{
    public   class DepartToBranch
    {

        public string BranchID { get; set; }
        public string DepartmentID { get; set; }
        public string CurrentID { get; set; }
        private IList<Branch> _lstbranh;
        public IList<Branch> lstbranch
        {
            get { return _lstbranh; }
            set { value = _lstbranh; }

        }
        public IList<Departments> lstDept { get; set; }
        public List<string> dep { get; set; }

        public IList<DepartToBranch> lstdb { get; set; }
        //public IList<ResultListdocs> lstresultDoclist { get; set; }
        public IList<Departments> lstlogtolog { get; set; }


        //public IList<Departments> lstdept { get; set; }
        public IList<DepartToBranch> lstdepttobrnach { get; set; }
        public IList<ResultDeptList> rdlist { get; set; }
        public IList<SelectListItem> lstselectdept { get; set; }

    }
}