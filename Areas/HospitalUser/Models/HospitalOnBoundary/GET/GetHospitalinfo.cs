using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.HospitalUser.Models.HospitalOnBoundary.GET
{
    public class GetHospitalinfo
    {
        public static HospitalInformation GetHospitalInfoDomainId(string domainId)
        {
            HospitalInformation hInfo = new HospitalInformation();         
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetHospitalInfoByDomainId/" + domainId ;
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(HospitalInformation));

            hInfo = OutPut.ReadObject(stream) as HospitalInformation;
            return hInfo;
        }


        public static HospitalInformation GetHospitalInfoById(string UserId)
        {
            HospitalInformation hInfo = new HospitalInformation();         
            WebClient PerInfoServiceProxy = new WebClient();
            string ServiceUrl = DomainServerPath.Service+"HospitalInfo/GetHospitalInfoById/" + UserId + "";
            byte[] data = PerInfoServiceProxy.DownloadData(ServiceUrl);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer OutPut = new DataContractJsonSerializer(typeof(HospitalInformation));

            hInfo = OutPut.ReadObject(stream) as HospitalInformation;
            return hInfo;
        }
    }
}