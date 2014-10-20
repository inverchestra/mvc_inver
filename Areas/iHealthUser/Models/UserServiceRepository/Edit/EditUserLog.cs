using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserRepository;
using System.Runtime.Serialization.Json;

namespace InnDocs.iHealth.Web.UI.Areas.iHealthUser.Models.UserServiceRepository
{
    public class EditUserLog
    {
        public static int UpdateUserLog(HealthLog log, string GroupType,string logId)
        {
            try
            {
                WebClient wc = new WebClient();
                MemoryStream ms = new MemoryStream();
                wc.Headers["Content-Type"] = "application/json";
                wc.Headers["Accept"] = "application/json";
                //DataContractJsonSerializer serializer = new DataContractJsonSerializer(log.GetType());
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HealthLog));
                serializer.WriteObject(ms, log);
                string url = DomainServerPath.Service+"HealthLogs/UpdateLog/" + GroupType + "/" + logId + "";
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