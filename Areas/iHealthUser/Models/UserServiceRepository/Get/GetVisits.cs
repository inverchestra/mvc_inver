using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class GetVisits
    {
        public static IList<Visits> GetAllVisitsByCaseId(string CaseId)
        {
            IList<Visits> lstMts = new List<Visits>();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllVisits/" + CaseId.ToString() + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Visits>));
            lstMts = OutPut.ReadObject(stream) as List<Visits>;

            foreach (var item in lstMts)
            {
                item.NurseReadingsInfo = XmlStringListSerializer.Deserialize<NurseReadings>(item.NurseReadings);
                //item.DoctorInfos = XmlStringListSerializer.Deserialize<Doctor>(item.DoctorsInfo);
            }

            return lstMts;
        }

        public static IList<Visits> GetAllVisitsByUserId(string UserId)
        {
            IList<Visits> lstMts = new List<Visits>();
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetAllVisitsByUserId/" + UserId.ToString() + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(List<Visits>));
            lstMts = OutPut.ReadObject(stream) as List<Visits>;

            foreach (var item in lstMts)
            {
                item.NurseReadingsInfo = XmlStringListSerializer.Deserialize<NurseReadings>(item.NurseReadings);
                //item.DoctorInfos = XmlStringListSerializer.Deserialize<Doctor>(item.DoctorsInfo);
            }

            return lstMts;
        }

        public string DeleteVisitsById(string id)
        {
            string Successcode;
            WebClient ServProxy = new WebClient();
            string ServiceURL = DomainServerPath.Service+"CaseManagement/DeleteVisit/" + id.Trim();
            byte[] data = ServProxy.DownloadData(ServiceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(string));
            Successcode = OutPut.ReadObject(stream) as string;
            return Successcode;
        }

        public static Visits GetVisitsById(string visitsId)
        {
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"CaseManagement/GetVisit/" + visitsId.ToString().Trim();
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(Visits));
            Visits visits = OutPut.ReadObject(stream) as Visits;

            visits.NurseReadingsInfo = XmlStringListSerializer.Deserialize<NurseReadings>(visits.NurseReadings);
            //visits.DoctorInfos = XmlStringListSerializer.Deserialize<Doctor>(visits.DoctorsInfo);

            return visits;
        }
    }
}