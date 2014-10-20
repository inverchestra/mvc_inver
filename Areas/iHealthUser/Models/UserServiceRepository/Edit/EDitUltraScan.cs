using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository.Edit
{
    public class EDitUltraScan
    {
        public string UpdateEarlyscan(EarlyScan es1)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(es1.GetType());
                serializer.WriteObject(ms, es1);
                string url = DomainServerPath.Service+"Vitals/UpdateEarlyScanDetails/" + es1.UserId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public string UpdateNTScan(NTScan es1)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(es1.GetType());
                serializer.WriteObject(ms, es1);
                string url = DomainServerPath.Service+"Vitals/UpdateNTScanDetails/" + es1.UserId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public string UpdateAnomalyScan(AnomalyScan es1)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(es1.GetType());
                serializer.WriteObject(ms, es1);
                string url = DomainServerPath.Service+"Vitals/UpdateAnomalyScanDetails/" + es1.UserId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
        public string UpdateGrowthScan(GrowthScan es1)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(es1.GetType());
                serializer.WriteObject(ms, es1);
                string url = DomainServerPath.Service+"Vitals/UpdateGrowthScanDetails/" + es1.UserId + "";
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}