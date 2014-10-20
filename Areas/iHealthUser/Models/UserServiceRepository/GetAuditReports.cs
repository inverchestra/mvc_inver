using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using System;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get
{
    public class GetAuditReports
    {
        public static List<AuditHistory> GetAllAuditReports()
        {
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"AuditManagement/GetAllAudits";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<AuditHistory>));
            List<AuditHistory> lstAudits = OutPut.ReadObject(stream) as List<AuditHistory>;
            lstAudits = lstAudits.OrderByDescending(x => x.AuditDate).ToList();

            return lstAudits;
        }
        public static List<AuditHistory> GetAllAuditReportsByUserId(string userId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            PerInfoServiceProxy.Headers["Content-Type"] = "application/json";
            PerInfoServiceProxy.Headers["Accept"] = "application/json";
            string ServiceUrl = DomainServerPath.Service+"AuditManagement/GetAllAuditsByUserId/" + userId.ToString();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<AuditHistory>));
            List<AuditHistory> lstAudits = OutPut.ReadObject(stream) as List<AuditHistory>;
            lstAudits = lstAudits.OrderByDescending(x => x.AuditDate).ToList();

            return lstAudits;
        }

        

        public static List<AuditHistory> GetAllAuditReportsByDate(AuditHistory ah)
        {

            WebClient Proxy1 = new WebClient();
            Proxy1.Headers["Content-type"] = "application/json";
            Proxy1.Headers["Accept"] = "application/json";
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer serializerToUplaod = new DataContractJsonSerializer(typeof(AuditHistory));

            serializerToUplaod.WriteObject(ms, ah);
            string ServiceURl = DomainServerPath.Service+"AuditManagement/GetAllAuditsByDate";

            byte[] data = Proxy1.UploadData(ServiceURl, "POST", ms.ToArray());
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<AuditHistory>));
            List<AuditHistory> lstAudits = OutPut.ReadObject(stream) as List<AuditHistory>;
            lstAudits = lstAudits.OrderByDescending(x => x.AuditDate).ToList();

            return lstAudits;
        }



    }
}