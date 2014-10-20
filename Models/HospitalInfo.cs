using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using InnDocs.iHealth.Web.UI.Models;
using System.Web.Mvc;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;
using System.Net;


namespace InnDocs.iHealth.Web.UI.Models
{
    public class HospitalInformation1
    {
        [Required(ErrorMessage = "Hospital name is required")]
        public string HospitalName { get; set; }
        [Required(ErrorMessage = "Mainbranch name is required")]
        public string MainBranch { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public string GroupType { get; set; }
        public string HospitalID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string DomainId { get; set; }
        public IList<Branch1> BranchInfo { get; set; }
        




        public static HospitalInformation1 GetHospitalInfoById(string UserId)
        {
            HospitalInformation1 hInfo = new HospitalInformation1();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetHospitalInfoById/" + UserId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(HospitalInformation1));

            hInfo = OutPut.ReadObject(stream) as HospitalInformation1;
            return hInfo;
        }
        public static HospitalInformation1 GetHospitalInfoDomainId(string domainId)
        {
            HospitalInformation1 hInfo = new HospitalInformation1();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetHospitalInfoByDomainId/" + domainId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(HospitalInformation1));

            hInfo = OutPut.ReadObject(stream) as HospitalInformation1;
            return hInfo;
        }
    }

    public class Branch1
    {
        private string _branchName;
        public string BranchName
        {
            get { return _branchName; }
            set { _branchName = value; }
        }

        private int _branchId;
        public int BranchId
        {
            get { return _branchId; }
            set { _branchId = value; }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }


        private DateTime? _createdOn;
        public DateTime? CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        private int _domainId;
        public int DomainId
        {
            get { return _domainId; }
            set { _domainId = value; }
        }

        private int _hospitalInfoID;
        public int HospitalInfoId
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
    }
}