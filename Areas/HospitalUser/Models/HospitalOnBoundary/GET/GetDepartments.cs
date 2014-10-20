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

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET
{
    public  class GetDepartments
    {
        public static Departments GetDepartmentById(string deptId)
        {
            Departments deptInfo = null;
            deptInfo = new Departments();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetDepartmentById/" + deptId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Departments));
            deptInfo = OutPut.ReadObject(stream) as Departments;
            return deptInfo;
        }

        public static Departments GetDepartmentsByUserId(string UserId)
        {
            Departments deptInfo = null;
            deptInfo = new Departments();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetMedicalTestsbyUserId/" + UserId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Departments));

            deptInfo = OutPut.ReadObject(stream) as Departments;
            return deptInfo;
        }
        public static IList<Departments> GetDepartmentsByHospitalId(string hospId)
        {
            IList<Departments> deptInfo = new List<Departments>();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetDepartmentsByHospitalId/" + hospId;
            
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Departments>));

            deptInfo = OutPut.ReadObject(stream) as List<Departments>;
            return deptInfo;
        }
        public static IList<Departments> GetDepartmentsByDomainId(string domainId)
        {
            IList<Departments> deptInfo = new List<Departments>();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetDepartmentsByDomainId/" + domainId;

            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Departments>));

            deptInfo = OutPut.ReadObject(stream) as  List<Departments>;
            return deptInfo;
        }
        public string DeleteDepartmentId(int id)
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

    }
}