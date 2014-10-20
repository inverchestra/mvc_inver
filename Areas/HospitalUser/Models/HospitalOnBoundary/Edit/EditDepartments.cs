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

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.Edit
{
    public class EditDepartments
    {
        public static  int UpdateDepartments(DepartToBranch dp)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(dp.GetType());
                serializer.WriteObject(ms, dp);
                string url = DomainServerPath.Service+"HospitalInfo/UpdateDepartmentById";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return Convert.ToInt32(code);


            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteDepatmentFromBranch(string deptid)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                // DataContractJsonSerializer serializer = new DataContractJsonSerializer(bran.GetType());
                // serializer.WriteObject(ms, bran);
                string url = DomainServerPath.Service+"HospitalInfo/DeleteDepartmentFromBranch/" + deptid + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return Convert.ToInt32(code);


            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public int DeleteDepatment(string deptid)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                // DataContractJsonSerializer serializer = new DataContractJsonSerializer(bran.GetType());
                // serializer.WriteObject(ms, bran);
                string url = DomainServerPath.Service+"HospitalInfo/DeleteDepartment/" + deptid + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return Convert.ToInt32(code);


            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static int UpdateDepartments(Departments dp)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(dp.GetType());
                serializer.WriteObject(ms, dp);
                string url = DomainServerPath.Service+"HospitalInfo/UpdateDepartments";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return Convert.ToInt32(code);


            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public static List<DepartToBranch> GetAllBranchInfo(string branchid)
        {
            List<DepartToBranch> lstbranches = null;
            try
            {
                IList<DepartToBranch> lstbranchs = new List<DepartToBranch>();
                WebClient PerInfoServiceProxy = new WebClient();
                string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetAllDepartmentsToBranchInfoList/" + branchid;
                byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<DepartToBranch>));

                lstbranchs = OutPut.ReadObject(stream) as List<DepartToBranch>;

                lstbranches = lstbranchs as List<DepartToBranch>;


            }
            catch (Exception ex)
            {

            }
            return lstbranches;
        }


        public List<SelectListItem> UserDepartmentList(string branchid)
        {
            List<SelectListItem> _deptList = null;
            _deptList = new List<SelectListItem>();

            IList<DepartToBranch> _lstdepts = GetAllBranchInfo(branchid);
            if (_lstdepts != null)
            {
                if (_lstdepts.Count > 0)
                {
                    for (int i = 0; i < _lstdepts.Count; i++)
                    {
                        _deptList.Add(new SelectListItem
                        {
                            Value = _lstdepts[i].DepartmentID,
                        });
                    }
                }
            }
            return _deptList;
        }

        public static int DeleteDepartToBranch(string BranchId)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                // DataContractJsonSerializer serializer = new DataContractJsonSerializer(bran.GetType());
                // serializer.WriteObject(ms, bran);
                string url = DomainServerPath.Service+"HospitalInfo/DeleteDepartToBranch/" + BranchId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return Convert.ToInt32(code);


            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static int InsertDepartToBranch(DepartToBranch dp)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(dp.GetType());
                serializer.WriteObject(ms, dp);
                string url = DomainServerPath.Service+"HospitalInfo/InsertDepartToBranch";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return Convert.ToInt32(code);
            }
            catch (Exception e)
            {
                return 0;
            }
        }



    }
}