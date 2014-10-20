using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web.Optimization;
using System.ComponentModel.DataAnnotations;
using InnDocs.iHealth.Web.UI.Controllers;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET
{
    public class GetBranch
    {
        public static Branch GetBranchById(string branchId)
        {
            Branch branchInfo = null;
            branchInfo = new Branch();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetBranchById/" + branchId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Branch));

            branchInfo = OutPut.ReadObject(stream) as Branch;
            return branchInfo;
        }

        public static Branch GetBranchByUserId(int UserId)
        {
            Branch branchInfo = null;
            branchInfo = new Branch();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetMedicalTestsbyUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Branch));

            branchInfo = OutPut.ReadObject(stream) as Branch;
            return branchInfo;
        }

        public static IList<Branch> GetAllBranchInfo(string domainId)
        {
            IList<Branch> lstbranchs = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetAllBranchInfoListByDomainId/" + domainId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<Branch>));

            lstbranchs = OutPut.ReadObject(stream) as IList<Branch>;
            return lstbranchs;
        }
        public static IList<Branch> GetAllBranchInfobyHospital(string hospId)
        {
            IList<Branch> lstbranchs = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetAllBranchInfoListByHospital/" + hospId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<Branch>));

            lstbranchs = OutPut.ReadObject(stream) as IList<Branch>;
            return lstbranchs;
        }
        public static IList<Branch> GetAllRelatedBranchInfobyHospital(string hospId, string DepId)
        {
            IList<Branch> lstbranchs = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetAllBranchInfoListByHospital/" + hospId + "/" + DepId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<Branch>));

            lstbranchs = OutPut.ReadObject(stream) as IList<Branch>;
            return lstbranchs;
        }
        public static string DeleteBranchId(int id)
        {

            string Successcode;
            WebClient ServProxy = new WebClient();
            string ServiceURL = DomainServerPath.Service+"CaseManagement/DeleteMedicalTest/" + id.ToString().Trim();
            byte[] data = ServProxy.DownloadData(ServiceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            Successcode = OutPut.ReadObject(stream) as string;
            return Successcode;
        }



        public List<Branch> GetAllbranchesByDomainId(string UserId)
        {
            List<Branch> casesInfo = null;

            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetAllBranchInfoListByDomainId/" + UserId.Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Branch>));
            casesInfo = OutPut.ReadObject(stream) as List<Branch>;
            return casesInfo;
        }

        public List<SelectListItem> UserBranchlist(string UserID, string branchID)
        {
            List<SelectListItem> _branchlist = null;
            _branchlist = new List<SelectListItem>();
            List<Branch> _branches = null;
            _branches = new List<Branch>();
            _branches = GetAllbranchesByDomainId(UserID);
            for (int i = 0; i < _branches.Count; i++)
            {
                _branchlist.Add(new SelectListItem
                {
                    Text = _branches[i].BranchName,
                    Value = _branches[i].Id,
                    //Selected = (_cases[i].CaseId == _cases[i].CaseId)
                    Selected = ((branchID == "" ? _branches[0].Id : branchID) == _branches[i].Id)
                });
            }
            return _branchlist;
        }


      
    }
}