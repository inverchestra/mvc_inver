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
    public class GetDoctorsInfo
    {
        public static Doctors GetDoctorsById(string doctorsId)
        {
            Doctors doctorsInfo = null;
            doctorsInfo = new Doctors();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetDoctorsById/" + doctorsId ;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Doctors));

            doctorsInfo = OutPut.ReadObject(stream) as Doctors;
            return doctorsInfo;
        }

        public Doctors GetDoctorsByUserId(int UserId)
        {
            Doctors doctorInfo = null;
            doctorInfo = new Doctors();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetMedicalTestsbyUserId/" + UserId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Doctors));

            doctorInfo = OutPut.ReadObject(stream) as Doctors;
            return doctorInfo;
        }
        public static IList<Doctors> GetDoctorsByDeptId(string deptId)
        {
           IList<Doctors> doctorInfo = null;
            doctorInfo = new List<Doctors>();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetMedicalTestsbyUserId/" + deptId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<Doctors>));

            doctorInfo = OutPut.ReadObject(stream) as IList<Doctors>;
            return doctorInfo;
        }
        public static IList<Doctors> GetDoctorsByDomainId(string domainId)
        {
            IList<Doctors> doctorInfo = null;
            doctorInfo = new List<Doctors>();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetDoctorsByDomainId/" + domainId.Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(IList<Doctors>));

            doctorInfo = OutPut.ReadObject(stream) as IList<Doctors>;
            return doctorInfo;
        }
        public string DeleteDoctorsById(int id)
        {

            string Successcode;
            WebClient ServProxy = new WebClient();
            string ServiceURL = DomainServerPath.Service+"HospitalInfo/DeleteMedicalTest/" + id.ToString().Trim();
            byte[] data = ServProxy.DownloadData(ServiceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            Successcode = OutPut.ReadObject(stream) as string;
            return Successcode;
        }

    }
}