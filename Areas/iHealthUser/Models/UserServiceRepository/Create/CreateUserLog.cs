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
    public class CreateUserLog
    {
        public static string InsertNewLog(HealthLog logInfo,string grouptype)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Headers["Content-type"] = "application/json";
                wc.Headers["Accept"] = "application/json";
                MemoryStream ms = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HealthLog));
                serializer.WriteObject(ms, logInfo);
                string url = DomainServerPath.Service+"HealthLogs/InsertLog/"+grouptype;
                byte[] data = wc.UploadData(url, "POST", ms.ToArray());
                Stream stream = new MemoryStream(data);
                serializer = new DataContractJsonSerializer(typeof(string));
                string code = serializer.ReadObject(stream) as string;
                return code;
            }
            catch(Exception e)
            {
                return string.Empty;
            }
        }
    }
}