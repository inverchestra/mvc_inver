using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserUtility;
using System.Web.Mvc;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Get
{
    public class GetUltraScan
    {
        public static EarlyScan GetEarlyScanByUserId(string UserId)
        {
            EarlyScan lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service + "Vitals/GetEarlyScanByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(EarlyScan));
            lstposts = OutPut.ReadObject(stream) as EarlyScan;

            return lstposts;


        }

        public static EarlyScan GetEarlyScanByUserIdandId(string UserId, string Id)
        {
            EarlyScan lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service + "Vitals/GetEarlyScanByUserIdandId/" + UserId + "/" + Id + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(EarlyScan));
            lstposts = OutPut.ReadObject(stream) as EarlyScan;

            return lstposts;


        }

        public static NTScan GetNTScanByUserId(string UserId)
        {
            NTScan lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service + "Vitals/GetNTScanByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(NTScan));
            lstposts = OutPut.ReadObject(stream) as NTScan;

            return lstposts;


        }

        public static NTScan GetNTScanByUserIdandId(string UserId, string Id)
        {
            NTScan lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service + "Vitals/GetNTScanByUserIdandId/" + UserId + "/" + Id + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(NTScan));
            lstposts = OutPut.ReadObject(stream) as NTScan;

            return lstposts;


        }
        public static AnomalyScan GetAnomalyScanByUserId(string UserId)
        {
            AnomalyScan lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service + "Vitals/GetAnomalyScanByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(AnomalyScan));
            lstposts = OutPut.ReadObject(stream) as AnomalyScan;

            return lstposts;


        }
        public static AnomalyScan GetAnomalyScanByUserIdandId(string UserId, string Id)
        {
            AnomalyScan lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service + "Vitals/GetAnomalyScanByUserIdandId/" + UserId + "/" + Id + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(AnomalyScan));
            lstposts = OutPut.ReadObject(stream) as AnomalyScan;

            return lstposts;


        }

        public static GrowthScan GetGrowthScanByUserId(string UserId)
        {
            GrowthScan lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service + "Vitals/GetGrowthScanByUserId/" + UserId;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(GrowthScan));
            lstposts = OutPut.ReadObject(stream) as GrowthScan;

            return lstposts;


        }

        public static GrowthScan GetGrowthScanByUserIdandId(string UserId, string Id)
        {
            GrowthScan lstposts = null;
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service + "Vitals/GetGrowthScanByUserIdandId/" + UserId + "/" + Id + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(GrowthScan));
            lstposts = OutPut.ReadObject(stream) as GrowthScan;

            return lstposts;


        }
    }
}