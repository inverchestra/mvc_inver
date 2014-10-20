using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary
{
    public class HospitalInformation 
    {
        public string HospitalInfoId { get; set; }
        public string HospitalName { get; set; }
      
        public string MainBranch { get; set; }
       
        public string Address { get; set; }
       
        public string HospitalID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string DomainId { get; set; }
        
        //private Branch _Branch;

        //public Branch Branchs
        //{
        //    get { return _Branch; }
        //    set { _Branch = value; }
        //}

        private IList<Branch> _lstbranch;
        public IList<Branch> BranchList
        {
            get { return _lstbranch; }
            set { _lstbranch = value; }
        }
      
    }
}
